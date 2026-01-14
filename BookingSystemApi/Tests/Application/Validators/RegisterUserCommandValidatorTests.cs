using Application.Features.Users.Commands;
using Application.Features.Users.Commands.Validators;

namespace Tests.Application.Validation;

public class RegisterUserCommandValidatorTests
{
    [Fact]
    public void RegisterUserCommandValidator_ValidData_PassesValidation()
    {
        var command = new RegisterUserCommand(
            "validemail@user.com",
            "ValidPass123!",
            "Valid User",
            new DateOnly(1995, 5, 15)
        );

        var validator = new RegisterUserCommandValidator();

        var result = validator.Validate(command);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void RegisterUserCommandValidator_InvalidEmail_FailsValidation()
    {
        var command = new RegisterUserCommand(
            "invalidemail",
            "ValidPass123!",
            "Valid User",
            new DateOnly(1995, 5, 15)
        );
        var validator = new RegisterUserCommandValidator();
        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Email");
    }

    [Fact]
    public void RegisterUserCommandValidator_MissingEmail_FailsValidation()
    {
        var command = new RegisterUserCommand(
            "",
            "ValidPass123!",
            "Valid User",
            new DateOnly(1995, 5, 15)
        );

        var validator = new RegisterUserCommandValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Email");
    }

    [Fact]
    public void RegisterUserCommandValidator_WeakPassword_FailsValidation()
    {
        var command = new RegisterUserCommand(
            "email@email.com",
            "weak",
            "Valid User",
            new DateOnly(1995, 5, 15)
        );

        var validator = new RegisterUserCommandValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Password");
    }

    [Fact]
    public void RegisterUserCommandValidator_PasswordTooShort_FailsValidation()
    {
        var command = new RegisterUserCommand(
            "email@email.com",
            "Short1!",
            "Valid User",
            new DateOnly(1995, 5, 15)
        );

        var validator = new RegisterUserCommandValidator();
        
        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Password");
    }

    [Fact]
    public void RegisterUserCommandValidator_PasswordMissingSpecialCharacter_FailsValidation()
    {
        var command = new RegisterUserCommand(
            "email@email.com",
            "NoSpecialChar1",
            "Valid User",
            new DateOnly(1995, 5, 15)
        );
        
        var validator = new RegisterUserCommandValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Password");
    }

    [Fact]
    public void RegisterUserCommandValidator_PasswordMissingDigit_FailsValidation()
    {
        var command = new RegisterUserCommand(
            "email@email.com",
            "NoDigitPass!",
            "Valid User",
            new DateOnly(1995, 5, 15)
        );

        var validator = new RegisterUserCommandValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Password");
    }

    [Fact]
    public void RegisterUserCommandValidator_PasswordMissingUppercase_FailsValidation()
    {
        var command = new RegisterUserCommand(
            "email@email.com",
            "nouppercase1!",
            "Valid User",
            new DateOnly(1995, 5, 15)
            );

        var validator = new RegisterUserCommandValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Password");
    }

    [Fact]
    public void RegisterUserCommandValidator_PasswordMissingLowercase_FailsValidation()
    {
        var command = new RegisterUserCommand(
            "email@email.com",
            "NOLOWERCASE1!",
            "Valid User",
            new DateOnly(1995, 5, 15)
            );

        var validator = new RegisterUserCommandValidator();
        
        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Password");
    }

    [Fact]
    public void RegisterUserCommandValidator_PasswordWithWhitespace_FailsValidation()
    {
        var command = new RegisterUserCommand(
            "email@email.com",
            "Pass word1!",
            "Valid User",
            new DateOnly(1995, 5, 15)
        );

        var validator = new RegisterUserCommandValidator();
        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Password");
    }

    [Fact]
    public void RegisterUserCommandValidator_MissingPassword_FailsValidation()
    {
        var command = new RegisterUserCommand(
            "email@email.com",
            "",
            "Valid User",
            new DateOnly(1995, 5, 15)
        );

        var validator = new RegisterUserCommandValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Password");
    }

    [Fact]
    public void RegisterUserCommandValidator_YearOfBirthInFuture_FailsValidation()
    {
        var command = new RegisterUserCommand(
            "user@email.com",
            "ValidPass123!",
            "Valid User",
            new DateOnly(DateTime.Now.Year + 1, 1, 1)
        );

        var validator = new RegisterUserCommandValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "DateOfBirth");
    }

    [Fact]
    public void RegisterUserCommandValidator_DisplayNameTooLong_FailsValidation()
    {
        var longDisplayName = new string('A', 101);
        
        var command = new RegisterUserCommand(
            "email@email.com",
            "ValidPass123!",
            longDisplayName,
            new DateOnly(1995, 5, 15)
        );

        var validator = new RegisterUserCommandValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "DisplayName");
    }

    [Fact]
    public void RegisterUserCommandValidator_MissinDateOfBirth_PassesValidation()
    {
        var command = new RegisterUserCommand(
            "user@email.com",
            "ValidPass123!",
            "Valid User",
            null
        );

        var validator = new RegisterUserCommandValidator();

        var result = validator.Validate(command);

        Assert.True(result.IsValid);
    }
}
