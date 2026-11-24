using Application.Features.Users.Commands;
using Application.Features.Users.Commands.Handlers;
using Application.Features.Users.Interfaces;
using Moq;

namespace Tests.Application.Features.Users.Commands.Handlers;

public class RegisterUserHandlerTests
{
    private readonly Mock<IUserRegistrationService> _registrationServiceMock = new();
    private readonly RegisterUserCommandHandler _handler;

    public RegisterUserHandlerTests()
    {
        _handler = new RegisterUserCommandHandler(_registrationServiceMock.Object);
    }

    [Fact]
    public async Task HandleAsync_RegistrationSucceds_ReturnsUserId()
    {
        var command = new RegisterUserCommand(
            "test@example.com",
            "Password123!",
            "Test User",
            new DateOnly(2000, 1, 1));

        var expectedUserId = Guid.NewGuid();

        _registrationServiceMock
            .Setup(s => s.RegisterAsync(
                command.Email,
                command.Password,
                command.DisplayName,
                command.DateOfBirth,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedUserId);

        var result = await _handler.HandleAsync(command, TestContext.Current.CancellationToken);

        Assert.Equal(expectedUserId, result);

        _registrationServiceMock.Verify(s => s.RegisterAsync(
            command.Email,
            command.Password,
            command.DisplayName,
            command.DateOfBirth,
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task HandleAsync_RegistrationFails_ThrowsException()
    {
        var command = new RegisterUserCommand(
            "existing@example.com",
            "Password123!",
            "Existing User",
            new DateOnly(1990, 1, 1));

        _registrationServiceMock
            .Setup(s => s.RegisterAsync(
                command.Email,
                command.Password,
                command.DisplayName,
                command.DateOfBirth,
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException(
                "User with this email already exists."));

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _handler.HandleAsync(command, TestContext.Current.CancellationToken));

        Assert.Contains("already exists", exception.Message);
    }
}
