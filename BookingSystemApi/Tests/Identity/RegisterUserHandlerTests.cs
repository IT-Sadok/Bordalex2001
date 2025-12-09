using Application.Features.Users.Commands;
using Infrastructure.Features.Users.Handlers;
using Infrastructure.Identity;
using Infrastructure.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Tests.Identity;

public class RegisterUserHandlerTests
{
    private readonly Mock<IUserManagerWrapper<AppUser>> _userManagerWrapperMock = new();
    private readonly Mock<IRoleManagerWrapper<IdentityRole>> _roleManagerWrapperMock = new();

    [Fact]
    public async Task HandleAsync_IfUserAlreadyExists()
    {
        var command = new RegisterUserCommand(
            "existingemail@example.com",
            "Password123!",
            "Existing User",
            new DateOnly(1990, 1, 1)
        );

        _userManagerWrapperMock
            .Setup(um => um.FindByEmailAsync(command.Email))
            .ReturnsAsync(new AppUser { Email = command.Email });

        var handler = new RegisterUserHandler(_userManagerWrapperMock.Object, _roleManagerWrapperMock.Object);

        await Assert.ThrowsAsync<InvalidOperationException>(async () => await handler.HandleAsync(command));

        _userManagerWrapperMock.Verify(um => um.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()), Times.Never);

    }

    [Fact]
    public async Task HandleAsync_CreatesUserSuccessfully()
    {
        var command = new RegisterUserCommand(
            "test@example.com",
            "Password123!",
            "Test User",
            new DateOnly(2000, 1, 1)
        );

        _userManagerWrapperMock
            .Setup(um => um.FindByEmailAsync(command.Email))
            .ReturnsAsync((AppUser?)null);
        _userManagerWrapperMock
            .Setup(um => um.CreateAsync(It.IsAny<AppUser>(), command.Password))
            .ReturnsAsync(IdentityResult.Success);
        _roleManagerWrapperMock
            .Setup(rm => rm.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
        _userManagerWrapperMock
            .Setup(um => um.AddToRoleAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        var handler = new RegisterUserHandler(_userManagerWrapperMock.Object, _roleManagerWrapperMock.Object);

        var result = await handler.HandleAsync(command);

        Assert.NotEqual(Guid.Empty, result);

        _userManagerWrapperMock.Verify(um => um.CreateAsync(It.IsAny<AppUser>(), command.Password), Times.Once);
        _userManagerWrapperMock.Verify(um => um.AddToRoleAsync(It.IsAny<AppUser>(), It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_FailsToCreateUser()
    {
        var command = new RegisterUserCommand(
            "fail@example.com",
            "Password123!",
            "Fail User",
            new DateOnly(1995, 5, 5)
        );

        _userManagerWrapperMock
            .Setup(um => um.FindByEmailAsync(command.Email))
            .ReturnsAsync((AppUser?)null);
        _userManagerWrapperMock
            .Setup(um => um.CreateAsync(It.IsAny<AppUser>(), command.Password))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Password too weak." }));

        var handler = new RegisterUserHandler(_userManagerWrapperMock.Object, _roleManagerWrapperMock.Object);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () => await handler.HandleAsync(command));

        Assert.Contains("Password too weak.", exception.Message);

        _userManagerWrapperMock.Verify(um => um.AddToRoleAsync(It.IsAny<AppUser>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task HandleAsync_FailsToAddUserToRole()
    {
        var command = new RegisterUserCommand(
            "norole@example.com",
            "Password123!",
            "No Role User",
            new DateOnly(1985, 12, 12)
        );

        _userManagerWrapperMock
            .Setup(um => um.FindByEmailAsync(command.Email))
            .ReturnsAsync((AppUser?)null);
        _userManagerWrapperMock
            .Setup(um => um.CreateAsync(It.IsAny<AppUser>(), command.Password))
            .ReturnsAsync(IdentityResult.Success);
        _roleManagerWrapperMock.Setup(rm => rm.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(false);

        var handler = new RegisterUserHandler(_userManagerWrapperMock.Object, _roleManagerWrapperMock.Object);

        var result = await handler.HandleAsync(command);

        Assert.NotEqual(Guid.Empty, result);

        _userManagerWrapperMock.Verify(um => um.AddToRoleAsync(It.IsAny<AppUser>(), It.IsAny<string>()), Times.Never);
    }
}
