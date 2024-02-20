using Client.Organizations;
using FluentValidation;

namespace Api.Features.Organizations.Validators;

internal class CreateOrganizationRequestValidator : AbstractValidator<CreateOrganizationRequest>
{
    public CreateOrganizationRequestValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("Organization name is required.")
            .Length(2, 100).WithMessage("Organization name must be between 2 and 100 characters.");
    }
}