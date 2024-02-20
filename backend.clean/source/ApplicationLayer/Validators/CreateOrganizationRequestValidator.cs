using ApplicationLayer.Contracts.Requests;
using FluentValidation;

namespace ApplicationLayer.Validators;

internal class CreateOrganizationRequestValidator : AbstractValidator<CreateAppOrganizationRequest>
{
    public CreateOrganizationRequestValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("Organization name is required.")
            .Length(2, 100).WithMessage("Organization name must be between 2 and 100 characters.");
    }
}