using Application.Features.Users.Commands;
using Application.Features.Users.Commands.Handlers;
using Application.Features.Users.Interfaces;
using Moq;

namespace Tests.Application.Features.Users.Commands.Handlers;

public class LoginUserHandlerTests
{
    private readonly Mock<IUserAuthenticationService> _authenticationServiceMock = new();
    private readonly LoginUserCommandHandler _handler;

    public LoginUserHandlerTests()
    {
        _handler = new LoginUserCommandHandler(_authenticationServiceMock.Object);
    }

    [Fact]
    public async Task HandleAsync_ValidCredentials_ReturnsJwtToken()
    {
        var command = new LoginUserCommand(
            "test@example.com",
            "Password123!");

        var expectedToken = "jwt-token";

        _authenticationServiceMock
            .Setup(s => s.LoginAsync(
                command.Email,
                command.Password,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedToken);

        var result = await _handler.HandleAsync(command, TestContext.Current.CancellationToken);

        Assert.Equal(expectedToken, result);

        _authenticationServiceMock.Verify(
            s => s.LoginAsync(
                command.Email,
                command.Password,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task HandleAsync_InvalidCredentials_ThrowsException()
    {
        var command = new LoginUserCommand(
            "wrong@example.com",
            "WrongPassword!");

        _authenticationServiceMock
            .Setup(s => s.LoginAsync(
                command.Email,
                command.Password,
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException(
                "Invalid email or password."));

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _handler.HandleAsync(command, TestContext.Current.CancellationToken));

        Assert.Equal("Invalid email or password.", exception.Message);

        _authenticationServiceMock.Verify(
            s => s.LoginAsync(
                command.Email,
                command.Password,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
