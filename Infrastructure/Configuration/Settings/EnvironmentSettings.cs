namespace Infrastructure.Configuration.Settings
{
    public class EnvironmentSettings
    {
        public const string SectionName = "Environment";
        public bool IsProduction { get; set; }
        public string Instance { get; set; } = string.Empty;
    }
}
