using Application.Features.Users.Commands;
using Application.Features.Users.Interfaces;
using Infrastructure.Features.Users.Handlers;
using Infrastructure.Identity;
using Infrastructure.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Tests.Infrastructure.Identity;

public class LoginUserHandlerTests
{
    private readonly Mock<ISignInManagerWrapper<AppUser>> _signInManagerWrapperMock = new();
    private readonly Mock<IUserManagerWrapper<AppUser>> _userManagerWrapperMock = new();
    private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock = new();

    [Fact]
    public async Task HandleAsync_ValidCredentials_ReturnsJwtToken()
    {
        var command = new LoginUserCommand("test@example.com", "Password123!");
        var user = new AppUser { Id = Guid.NewGuid().ToString(), Email = command.Email };
        var roles = new List<string> { "Client" };
        var expectedToken = "jwt-token";

        _userManagerWrapperMock
            .Setup(um => um.FindByEmailAsync(command.Email))
            .ReturnsAsync(user);
        _signInManagerWrapperMock
            .Setup(sm => sm.CheckPasswordSignInAsync(user, command.Password, false))
            .ReturnsAsync(SignInResult.Success);
        _userManagerWrapperMock
            .Setup(um => um.GetRolesAsync(user)).ReturnsAsync(roles);
        _jwtTokenGeneratorMock
            .Setup(jt => jt.GenerateTokenAsync(Guid.Parse(user.Id), user.Email, roles))
            .ReturnsAsync(expectedToken);

        var handler = new LoginUserHandler(_signInManagerWrapperMock.Object, _userManagerWrapperMock.Object, _jwtTokenGeneratorMock.Object);

        var result = await handler.HandleAsync(command);

        Assert.Equal(expectedToken, result);

        _userManagerWrapperMock.Verify(um => um.FindByEmailAsync(command.Email), Times.Once);
        _signInManagerWrapperMock.Verify(sm => sm.CheckPasswordSignInAsync(user, command.Password, false), Times.Once);
        _userManagerWrapperMock.Verify(um => um.GetRolesAsync(user), Times.Once);
        _jwtTokenGeneratorMock.Verify(jt => jt.GenerateTokenAsync(Guid.Parse(user.Id), user.Email, roles), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_IfUserNotFound()
    {
        var command = new LoginUserCommand("notfound@example.com", "Password123!");

        _userManagerWrapperMock
            .Setup(um => um.FindByEmailAsync(command.Email))
            .ReturnsAsync((AppUser?)null);

        var handler = new LoginUserHandler(_signInManagerWrapperMock.Object, _userManagerWrapperMock.Object, _jwtTokenGeneratorMock.Object);

        await Assert.ThrowsAsync<InvalidOperationException>(async () => await handler.HandleAsync(command));

        _signInManagerWrapperMock.Verify(sm => sm.CheckPasswordSignInAsync(It.IsAny<AppUser>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Never);
        _jwtTokenGeneratorMock.Verify(jt => jt.GenerateTokenAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<IEnumerable<string>>()), Times.Never);
    }

    [Fact]
    public async Task HandleAsync_IfPasswordIncorrect()
    {
        var command = new LoginUserCommand("wrongpassword@example.com", "WrongPassword!");
        var user = new AppUser { Id = Guid.NewGuid().ToString(), Email = command.Email };

        _userManagerWrapperMock
            .Setup(um => um.FindByEmailAsync(command.Email))
            .ReturnsAsync(user);
        _signInManagerWrapperMock
            .Setup(sm => sm.CheckPasswordSignInAsync(user, command.Password, false))
            .ReturnsAsync(SignInResult.Failed);

        var handler = new LoginUserHandler(_signInManagerWrapperMock.Object, _userManagerWrapperMock.Object, _jwtTokenGeneratorMock.Object);

        await Assert.ThrowsAsync<InvalidOperationException>(async () => await handler.HandleAsync(command));

        _userManagerWrapperMock.Verify(um => um.GetRolesAsync(It.IsAny<AppUser>()), Times.Never);
        _jwtTokenGeneratorMock.Verify(jt => jt.GenerateTokenAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<IEnumerable<string>>()), Times.Never);
    }
}
