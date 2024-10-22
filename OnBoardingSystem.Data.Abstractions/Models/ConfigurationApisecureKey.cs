using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ConfigurationApisecureKey
    {
        public string? KeyName { get; set; }

        public string? SecretKey { get; set; }

        public string? Salt { get; set; }
    }
}
