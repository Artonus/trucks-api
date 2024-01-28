using FluentValidation;
using TrucksApi.Domain.TruckStatuses;

namespace TrucksApi.Validation;

public class TruckStatusValidator : AbstractValidator<string>
{
    private readonly string[] _validStatusNames = new[] { TruckStatus.ToJob, TruckStatus.AtJob, TruckStatus.Loading, TruckStatus.Returning, TruckStatus.OutOfService };
    public TruckStatusValidator()
    {
        RuleFor(x => x)
        .NotNull()
            .NotEmpty()
            .MaximumLength(30)
            .Must(BeAvailableTruckStatusName)
            .WithMessage(x => $"{x} is not a valid option. " +
                              $"Valid options are: {string.Join(", ", _validStatusNames)}");
    }
    private bool BeAvailableTruckStatusName(string value)
    {
        return _validStatusNames.Contains(value);
    }
}
