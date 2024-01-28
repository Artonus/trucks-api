namespace TrucksApi.Domain.TruckStatuses;

public class AtJobStatus : TruckStatus
{
    public override string[] AllowedChangelist { get; } = { OutOfService, Returning };
    public override string StatusName { get; } = AtJob;
}
