using Infrastructure.Configuration.Settings;
using Microsoft.Extensions.Options;

namespace Infrastructure.Configuration.Extensions
{
    public class ProgramExtension
    {
        private readonly IOptions<EnvironmentSettings> _environmentSettings;
        private readonly IOptions<ConnectionStringsSettings> _connectionStringsSettings;

        public ProgramExtension(IOptions<EnvironmentSettings> environmentSettings,
                                IOptions<ConnectionStringsSettings> connectionStringsSettings)
        {
            _environmentSettings = environmentSettings;
            _connectionStringsSettings = connectionStringsSettings;
        }

        public bool IsProduction => _environmentSettings.Value.IsProduction;
        public string Instance => _environmentSettings.Value.Instance;
        public string SqlConnection => _connectionStringsSettings.Value.ConectionSQL;
    }
}
