using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public partial class MdYear
    {
        public string YearId { get; set; } = null!;

        public string? Description { get; set; }

        public string? Abbrivation { get; set; }

        public string? YearGroup { get; set; }

        public string? IsActive { get; set; }
    }
}
