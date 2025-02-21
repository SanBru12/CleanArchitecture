using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ApplicationSettings.Settings
{
    public class EnvironmentSettings
    {
        public const string SectionName = "Environment";
        public bool IsProduction { get; set; }
        public string Instance { get; set; } = string.Empty;
    }
}
