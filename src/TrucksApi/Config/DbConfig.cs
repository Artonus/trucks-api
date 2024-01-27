namespace TrucksApi.Config;

public class DbConfig
{
    public const string ConfigName = "Database";
    public string ConnectionString { get; set; } = default!;
}
