using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ConfigurationEnvironment
    {
        public int ApplicationId { get; set; }

        public string? EnvironmentMode { get; set; }

        public string? EnvironmentModeDesc { get; set; }

        public string? CaptchaMode { get; set; }

        public string? CaptchaModeDesc { get; set; }

        public string? CaptchaValue { get; set; }

        public string? IsOfflineEnabled { get; set; }

        public string? OfflineModeMessage { get; set; }

        public string? IsOfflineEnabledAdmin { get; set; }

        public string? OfflineModeMessageAdmin { get; set; }

        public string? IsDataCached { get; set; }

        public string? AdminKey { get; set; }

        public string? AgencyKey { get; set; }

        public string? AuthMode { get; set; }

        public string? OtpMedium { get; set; }
    }
}
