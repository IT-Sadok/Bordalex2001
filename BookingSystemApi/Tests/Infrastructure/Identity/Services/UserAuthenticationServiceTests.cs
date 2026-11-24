using Application.Features.Users.Interfaces;
using Infrastructure.Consts;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Tests.Infrastructure.Identity.Services;

public class UserAuthenticationServiceTests
{
    private readonly Mock<UserManager<AppUser>> _userManagerMock;
    private readonly Mock<SignInManager<AppUser>> _signInManagerMock;
    private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock = new();
    private readonly UserAuthenticationService _service;

    public UserAuthenticationServiceTests()
    {
        var userStoreMock = new Mock<IUserStore<AppUser>>();

        _userManagerMock = new Mock<UserManager<AppUser>>(userStoreMock.Object, null!, null!, null!, null!, null!, null!, null!, null!);

        _signInManagerMock = new Mock<SignInManager<AppUser>>(
            _userManagerMock.Object,
            Mock.Of<IHttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<AppUser>>(),
            Mock.Of<IOptions<IdentityOptions>>(),
            Mock.Of<ILogger<SignInManager<AppUser>>>(),
            null!,
            null!);

        _service = new UserAuthenticationService(_userManagerMock.Object, _signInManagerMock.Object, _jwtTokenGeneratorMock.Object);
    }

    [Fact]
    public async Task LoginAsync_WhenUserNotFound_ThrowsException()
    {
        var email = "notfound@example.com";
        var password = "Password123!";

        _userManagerMock
            .Setup(um => um.FindByEmailAsync(email))
            .ReturnsAsync((AppUser?)null);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _service.LoginAsync(
                email,
                password,
                TestContext.Current.CancellationToken));

        Assert.Equal("Invalid email or password.", exception.Message);

        _signInManagerMock.Verify(
            sm => sm.CheckPasswordSignInAsync(
                It.IsAny<AppUser>(),
                It.IsAny<string>(),
                It.IsAny<bool>()),
            Times.Never);
        _jwtTokenGeneratorMock.Verify(
            jwt => jwt.GenerateTokenAsync(
                It.IsAny<Guid>(),
                It.IsAny<string>(),
                It.IsAny<IEnumerable<string>>()),
            Times.Never);
    }

    [Fact]
    public async Task LoginAsync_WhenPasswordIncorrect_ThrowsException()
    {
        var email = "wrongpassword@example.com";
        var password = "WrongPassword!";

        var user = new AppUser
        {
            Id = Guid.NewGuid().ToString(),
            Email = email
        };

        _userManagerMock
            .Setup(um => um.FindByEmailAsync(email))
            .ReturnsAsync(user);
        _signInManagerMock
            .Setup(sm => sm.CheckPasswordSignInAsync(user, password, false))
            .ReturnsAsync(SignInResult.Failed);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _service.LoginAsync(
                email,
                password,
                TestContext.Current.CancellationToken));

        Assert.Equal("Invalid email or password.", exception.Message);

        _userManagerMock.Verify(
            um => um.GetRolesAsync(It.IsAny<AppUser>()),
            Times.Never);
        _jwtTokenGeneratorMock.Verify(
            jwt => jwt.GenerateTokenAsync(
                It.IsAny<Guid>(),
                It.IsAny<string>(),
                It.IsAny<IEnumerable<string>>()),
            Times.Never);
    }

    [Fact]
    public async Task LoginAsync_WhenCredentialsValid_ReturnsJwtToken()
    {
        var email = "test@example.com";
        var password = "Password123!";
        var userId = Guid.NewGuid();
        var expectedToken = "jwt-token";

        var user = new AppUser
        {
            Id = userId.ToString(),
            Email = email
        };

        List<string> roles = [Roles.Client];

        _userManagerMock
            .Setup(um => um.FindByEmailAsync(email))
            .ReturnsAsync(user);
        _userManagerMock
            .Setup(um => um.GetRolesAsync(user))
            .ReturnsAsync(roles);
        _signInManagerMock
            .Setup(sm => sm.CheckPasswordSignInAsync(user, password, false))
            .ReturnsAsync(SignInResult.Success);
        _jwtTokenGeneratorMock
            .Setup(jwt => jwt.GenerateTokenAsync(userId, email, It.IsAny<IEnumerable<string>>()))
            .ReturnsAsync(expectedToken);

        var result = await _service.LoginAsync(email, password, TestContext.Current.CancellationToken);

        Assert.Equal(expectedToken, result);

        _userManagerMock.Verify(
            um => um.FindByEmailAsync(email),
            Times.Once);
        _signInManagerMock.Verify(
            sm => sm.CheckPasswordSignInAsync(user, password, false),
            Times.Once);
        _userManagerMock.Verify(
            um => um.GetRolesAsync(user),
            Times.Once);
        _jwtTokenGeneratorMock.Verify(
            jwt => jwt.GenerateTokenAsync(
                userId,
                email,
                It.Is<IEnumerable<string>>(r =>
                    r.SequenceEqual(roles))),
            Times.Once);
    }

    [Fact]
    public async Task LoginAsync_WhenCancellationRequested_ThrowsException()
    {
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            () => _service.LoginAsync(
                "test@example.com",
                "Password123!",
                cts.Token));

        _userManagerMock.Verify(
            um => um.FindByEmailAsync(It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public async Task LoginAsync_WhenTokenGenerationFails_PropagatesException()
    {
        var email = "test@example.com";
        var password = "Password123!";
        var userId = Guid.NewGuid();

        var user = new AppUser
        {
            Id = userId.ToString(),
            Email = email
        };

        _userManagerMock
            .Setup(um => um.FindByEmailAsync(email))
            .ReturnsAsync(user);
        _signInManagerMock
            .Setup(sm => sm.CheckPasswordSignInAsync(user, password, false))
            .ReturnsAsync(SignInResult.Success);
        _userManagerMock
            .Setup(um => um.GetRolesAsync(user))
            .ReturnsAsync([Roles.Client]);
        _jwtTokenGeneratorMock
            .Setup(jwt => jwt.GenerateTokenAsync(userId, email, It.IsAny<IEnumerable<string>>()))
            .ThrowsAsync(new InvalidOperationException("JWT generation failed."));

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _service.LoginAsync(
                email,
                password,
                TestContext.Current.CancellationToken));

        Assert.Equal("JWT generation failed.", exception.Message);

        _jwtTokenGeneratorMock.Verify(
            jwt => jwt.GenerateTokenAsync(
                userId,
                email,
                It.IsAny<IEnumerable<string>>()),
            Times.Once);
    }
}