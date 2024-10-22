using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class GetApplicationSummary
    {
        public string AgencyId { get; set; }
        public string AgencyName { get; set; }
        public string apiLink { get; set; }
        public string appYear { get; set; }
    }
}
