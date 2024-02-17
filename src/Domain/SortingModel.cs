namespace Domain;

public class SortingModel
{
    public string? SortFileld { get; set; }
    public bool Ascending { get; set; } = true;
    public bool IsSet() => string.IsNullOrEmpty(SortFileld) == false;
}
