namespace TrucksApi.Domain.TruckStatuses;

public class ReturningStatus : TruckStatus
{
    public override string[] AllowedChangelist { get; } = { OutOfService, Loading };
    public override string Status { get; } = Returning;
}
