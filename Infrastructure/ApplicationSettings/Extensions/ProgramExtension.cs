using Infrastructure.ApplicationSettings.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ApplicationSettings.Extensions
{
    public class ProgramExtension
    {
        private readonly IOptions<EnvironmentSettings> _environmentSettings;

        public ProgramExtension(IOptions<EnvironmentSettings> environmentSettings)
        {
            _environmentSettings = environmentSettings;
        }

        public bool IsProduction => _environmentSettings.Value.IsProduction;
    }
}
