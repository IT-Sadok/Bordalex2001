using Application.Features.Users.Commands;
using Application.Features.Users.Commands.Validators;

namespace Tests.Application.Validation;

public class LoginUserCommandValidatorTests
{
    [Fact]
    public void LoginUserCommandValidator_ValidData_PassesValidation()
    {
        var command = new LoginUserCommand(
            "validuser@user.com",
            "ValidPass123!"
        );

        var validator = new LoginUserCommandValidator();

        var result = validator.Validate(command);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void LoginUserCommandValidator_InvalidEmail_FailsValidation()
    {
        var command = new LoginUserCommand(
            "invalidemail",
            "ValidPass123!"
        );
        
        var validator = new LoginUserCommandValidator();
        
        var result = validator.Validate(command);
        
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Email");
    }

    [Fact]
    public void LoginUserCommandValidator_MissingEmail_FailsValidation()
    {
        var command = new LoginUserCommand(
            "",
            "ValidPass123!"
        );

        var validator = new LoginUserCommandValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Email");
    }

    [Fact]
    public void LoginUserCommandValidator_MissingPassword_FailsValidation()
    {
        var command = new LoginUserCommand(
            "validuser@user.com",
            ""
        );

        var validator = new LoginUserCommandValidator();
        
        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Password");
    }
}