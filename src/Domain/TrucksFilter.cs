namespace Domain;

public class TrucksFilter
{
    public string? NameFilter { get; set; }
    public string? StatusFilter { get; set; }
    public string? IdFilter { get; set; }
    public string? DescriptionFilter { get; set; }

    public bool IsSet()
    {
        return string.IsNullOrEmpty(NameFilter) == false
            || string.IsNullOrEmpty(StatusFilter) == false
            || string.IsNullOrEmpty(IdFilter) == false
            || string.IsNullOrEmpty(DescriptionFilter) == false;
    }
}
