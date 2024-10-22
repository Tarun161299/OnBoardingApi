using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ZmstWillingness
    {
        public string WillingnessId { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? AlternateNames { get; set; }
    }
}
