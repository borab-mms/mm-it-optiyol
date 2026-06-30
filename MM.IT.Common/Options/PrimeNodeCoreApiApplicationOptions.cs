using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Options
{

    public class PrimeNodeCoreApiApplicationOptions
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

        public double Version { get; set; } = 1.0;

        public bool ForceSsl { get; set; } = true;

        public bool UseGlobalExceptionHandler { get; set; } = true;

        public bool ActionGeneratorEnabled { get; set; } = false;

        public string CorsPolicy { get; set; } = "MM.Platform.OnlyMM";

        public PrimeNodeCoreApiLocalizationOptions LocalizationOptions { get; set; } = new PrimeNodeCoreApiLocalizationOptions()
        {
            DefaultCulture = "tr-TR",
            SupportedCultures = new List<string>
            {
                "en-US",
                "tr-TR"
            }
        };
        public PrimeNodeCoreApiDeploymentOptions DeploymentOptions { get; set; } = new PrimeNodeCoreApiDeploymentOptions()
        {
            Host = "https://api.mediamarkt.com.tr",
            BasePath = ""
        };
    }
}
