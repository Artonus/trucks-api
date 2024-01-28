namespace TrucksApi.Domain.TruckStatuses;

public class OutOfServiceStatus : TruckStatus
{
    public override string Status { get; } = OutOfService;
    public override string[] AllowedChangelist { get; } = { Loading, ToJob, AtJob, Returning };
}
