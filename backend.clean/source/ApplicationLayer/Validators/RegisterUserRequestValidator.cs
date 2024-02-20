using ApplicationLayer.Contracts.Requests;
using FluentValidation;

namespace ApplicationLayer.Validators;

public class RegisterUserRequestValidator : AbstractValidator<AppNewUserRegistrationRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(command => command.UserName)
            .NotEmpty().WithMessage("Username is required.")
            .Length(3, 50).WithMessage("Username must be between 3 and 50 characters.");

        RuleFor(command => command.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
    }
}