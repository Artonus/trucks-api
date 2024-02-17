namespace Domain.TruckStatuses;

public class OutOfServiceStatus : TruckStatus
{
    public override string StatusName { get; } = OutOfService;
    public override string[] AllowedChangelist { get; } = { Loading, ToJob, AtJob, Returning };
}
