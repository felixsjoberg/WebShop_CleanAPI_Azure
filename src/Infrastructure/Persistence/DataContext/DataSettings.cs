namespace Infrastructure.Persistence.DataContext;

public class DataSettings
{
    public const string SectionName = "ConnectionStrings";

    public string SqlServer { get; set; } = null!;
}

