using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ZmstProgram
    {
        public int Id { get; set; }

        public string? Brcd { get; set; }

        public string? Brnm { get; set; }

        public string? Agencyid { get; set; }
        public string? AgencyName { get; set; }

        public string? BrcdOrg { get; set; }

        public string? Bshift { get; set; }

        public string? Btfw { get; set; }
    }
}
