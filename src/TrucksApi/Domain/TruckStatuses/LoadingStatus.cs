namespace TrucksApi.Domain.TruckStatuses;

public class LoadingStatus : TruckStatus
{
    public override string[] AllowedChangelist { get; } = { OutOfService, ToJob };
    public override string StatusName { get; } = Loading;
}
