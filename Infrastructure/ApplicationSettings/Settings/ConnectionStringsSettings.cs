using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ApplicationSettings.Settings
{
    public class ConnectionStringsSettings
    {
        public const string SectionName = "ConnectionStrings";
        public string ConectionSQL { get; set; } = string.Empty;
    }
}
