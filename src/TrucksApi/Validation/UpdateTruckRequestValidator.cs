using FluentValidation;
using TrucksApi.Contracts.Requests;

namespace TrucksApi.Validation;

public class UpdateTruckRequestValidator : AbstractValidator<UpdateTruckRequest>
{
    public UpdateTruckRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.Status)
            .SetValidator(new TruckStatusValidator());
    }
}
