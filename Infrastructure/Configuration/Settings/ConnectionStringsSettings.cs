namespace Infrastructure.Configuration.Settings
{
    public class ConnectionStringsSettings
    {
        public const string SectionName = "ConnectionStrings";
        public string ConectionSQL { get; set; } = string.Empty;
    }
}
