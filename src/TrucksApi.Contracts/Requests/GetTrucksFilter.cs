namespace TrucksApi.Contracts.Requests
{
    public class GetTrucksFilter
    {
        public string? NameFilter { get; set; }
        public string? StatusFilter { get; set; }
        public string? IdFilter { get; set; }
        public string? DescriptionFilter { get; set; }
    }
}
