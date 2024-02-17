namespace Domain;

public class PaginationModel
{
    public int Page { get; set; }
    public int PageSize { get; set; }

    public bool IsSet()
    {
        return Page != default && PageSize != default;
    }
}