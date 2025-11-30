using Application.Features.Users.Commands;
using Infrastructure.Identity;
using Infrastructure.Identity.Interfaces;
using Infrastructure.Users.Handlers;
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
}
