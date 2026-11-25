using Infrastructure.Consts;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Tests.Infrastructure.Identity.Services;

public class UserRegistrationServiceTests
{
    private readonly Mock<UserManager<AppUser>> _userManagerMock;
    private readonly Mock<RoleManager<IdentityRole>> _roleManagerMock;
    private readonly UserRegistrationService _service;

    public UserRegistrationServiceTests()
    {
        var userStoreMock = new Mock<IUserStore<AppUser>>();
        var roleStoreMock = new Mock<IRoleStore<IdentityRole>>();

        _userManagerMock = new Mock<UserManager<AppUser>>(userStoreMock.Object, null!, null!, null!, null!, null!, null!, null!, null!);
        _roleManagerMock = new Mock<RoleManager<IdentityRole>>(roleStoreMock.Object, null!, null!, null!, null!);
        _service = new UserRegistrationService(_userManagerMock.Object, _roleManagerMock.Object);
    }

    [Fact]
    public async Task RegisterAsync_WhenUserAlreadyExists_ThrowsException()
    {
        var email = "existing@example.com";
        var password = "Password123!";

        _userManagerMock
            .Setup(um => um.FindByEmailAsync(email))
            .ReturnsAsync(new AppUser { Email = email });

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _service.RegisterAsync(
                email,
                password,
                null,
                null,
                TestContext.Current.CancellationToken));

        Assert.Equal("User with this email already exists.", exception.Message);

        _userManagerMock.Verify(
            um => um.CreateAsync(
                It.IsAny<AppUser>(),
                It.IsAny<string>()),
            Times.Never);
        _roleManagerMock.Verify(
            rm => rm.RoleExistsAsync(It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public async Task RegisterAsync_WhenValidData_CreatesUserSuccessfully()
    {
        var email = "test@example.com";
        var password = "Password123!";
        var expectedUserId = Guid.NewGuid();

        _userManagerMock
            .Setup(um => um.FindByEmailAsync(email))
            .ReturnsAsync((AppUser?)null);
        _userManagerMock
            .Setup(um => um.CreateAsync(It.IsAny<AppUser>(), password))
            .Callback<AppUser, string>((user, _) =>
            {
                user.Id = expectedUserId.ToString();
            })
            .ReturnsAsync(IdentityResult.Success);
        _roleManagerMock
            .Setup(rm => rm.RoleExistsAsync(Roles.Client))
            .ReturnsAsync(true);
        _userManagerMock
            .Setup(um => um.AddToRoleAsync(It.IsAny<AppUser>(), Roles.Client))
            .ReturnsAsync(IdentityResult.Success);

        var result = await _service.RegisterAsync(
            email,
            password,
            "Test User",
            null,
            TestContext.Current.CancellationToken);

        Assert.Equal(expectedUserId, result);

        _userManagerMock.Verify(
            um => um.CreateAsync(
                It.Is<AppUser>(u =>
                    u.Email == email &&
                    u.UserName == email &&
                    u.DisplayName == "Test User" &&
                    u.DateOfBirth == DateOnly.MinValue),
                password),
            Times.Once);
        _userManagerMock.Verify(
            um => um.AddToRoleAsync(
                It.IsAny<AppUser>(),
                Roles.Client),
            Times.Once);
    }

    [Fact]
    public async Task RegisterAsync_WhenUserCreationFails_ThrowsException()
    {
        var email = "fail@example.com";
        var password = "111";

        _userManagerMock
            .Setup(um => um.FindByEmailAsync(email))
            .ReturnsAsync((AppUser?)null);
        _roleManagerMock
            .Setup(rm => rm.RoleExistsAsync(Roles.Client))
            .ReturnsAsync(true);
        _userManagerMock
            .Setup(um => um.CreateAsync(It.IsAny<AppUser>(), password))
            .ReturnsAsync(IdentityResult.Failed(
                new IdentityError
                {
                    Description = "Password too weak."
                }));

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _service.RegisterAsync(
                email,
                password,
                null,
                null,
                TestContext.Current.CancellationToken));

        Assert.Contains("Password too weak.", exception.Message);

        _userManagerMock.Verify(
            um => um.AddToRoleAsync(
                It.IsAny<AppUser>(),
                It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public async Task RegisterAsync_WhenClientRoleDoesNotExist_ThrowsException()
    {
        var email = "norole@example.com";
        var password = "Password123!";

        _userManagerMock
            .Setup(um => um.FindByEmailAsync(email))
            .ReturnsAsync((AppUser?)null);
        _roleManagerMock
            .Setup(rm => rm.RoleExistsAsync(Roles.Client))
            .ReturnsAsync(false);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _service.RegisterAsync(
                email,
                password,
                "No Role User",
                null,
                TestContext.Current.CancellationToken));

        Assert.Equal($"Required role '{Roles.Client}' does not exist.", exception.Message);

        _userManagerMock.Verify(
            um => um.AddToRoleAsync(
                It.IsAny<AppUser>(),
                Roles.Client),
            Times.Never);
        _userManagerMock.Verify(
            um => um.CreateAsync(
                It.IsAny<AppUser>(),
                It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public async Task RegisterAsync_WhenRoleAssignmentFails_ThrowsException()
    {
        var email = "rolefail@example.com";
        var password = "Password123!";

        _userManagerMock
            .Setup(um => um.FindByEmailAsync(email))
            .ReturnsAsync((AppUser?)null);
        _userManagerMock
            .Setup(um => um.CreateAsync(It.IsAny<AppUser>(), password))
            .ReturnsAsync(IdentityResult.Success);
        _roleManagerMock
            .Setup(rm => rm.RoleExistsAsync(Roles.Client))
            .ReturnsAsync(true);
        _userManagerMock
            .Setup(um => um.AddToRoleAsync(It.IsAny<AppUser>(), Roles.Client))
            .ReturnsAsync(IdentityResult.Failed(
                new IdentityError
                {
                    Description = "Role assignment failed."
                }));

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _service.RegisterAsync(
                email,
                password,
                null,
                null,
                TestContext.Current.CancellationToken));

        Assert.Contains("Role assignment failed.", exception.Message);
    }
}
