using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class FilterInstitute
    {
        public string AgencyId { get; set; }
        public string StateId { get; set; }

        public string InstituteTypeId { get; set; }
    }
}
