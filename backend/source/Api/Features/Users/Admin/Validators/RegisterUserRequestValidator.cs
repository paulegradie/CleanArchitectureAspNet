using Client.User;
using FluentValidation;

namespace Api.Features.Users.Admin.Validators;

public class RegisterUserRequestValidator : AbstractValidator<RegisterRequest>
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