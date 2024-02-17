namespace Domain;

public class TruckResult
{
    public bool IsSuccess => string.IsNullOrWhiteSpace(ErrorMessage);
    public TruckModel? Truck { get; }
    public string? ErrorMessage { get; }

    public bool NotFound { get; }
    public TruckResult(TruckModel truck)
    {
        Truck = truck;
    }
    public TruckResult(string errMessage, bool notFound = false)
    {
        ErrorMessage = errMessage;
        NotFound = notFound;
    }
}
