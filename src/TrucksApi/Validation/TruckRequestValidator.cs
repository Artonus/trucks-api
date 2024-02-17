using FluentValidation;
using TrucksApi.Contracts.Requests;

namespace TrucksApi.Validation;

public class TruckRequestValidator : AbstractValidator<CreateTruckRequest>
{
    
    public TruckRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .Matches("[a-zA-Z0-9]")
            .MaximumLength(30);
        RuleFor(x=>x.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.Status).SetValidator(new TruckStatusValidator());
    }    
}
