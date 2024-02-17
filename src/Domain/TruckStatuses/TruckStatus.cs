namespace Domain.TruckStatuses;

public abstract class TruckStatus
{
    public const string OutOfService = "Out Of Service";
    public const string Loading = "Loading";
    public const string ToJob = "To Job";
    public const string AtJob = "At Job";
    public const string Returning = "Returning";
    public abstract string[] AllowedChangelist { get; }
    public abstract string StatusName { get; }    

    public static TruckStatus FromString(string status)
    {
        return status switch
        {
            OutOfService => new OutOfServiceStatus(),
            Loading => new LoadingStatus(),
            ToJob => new ToJobStatus(),
            AtJob => new AtJobStatus(),
            Returning => new ReturningStatus(),
            _ => throw new ArgumentException($"Unknown truck status: {status}", status),
        };
    }
}
