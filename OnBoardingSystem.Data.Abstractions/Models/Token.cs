using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class Token
    {
        public string CreatedToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public DateTime TokenCreated { get; set; } = DateTime.Now;

        public DateTime TokenExpires { get; set; }

        public DateTime RefreshTokenCreated { get; set; } = DateTime.Now;

        public DateTime RefreshTokenExpires { get; set; }

    }
}
