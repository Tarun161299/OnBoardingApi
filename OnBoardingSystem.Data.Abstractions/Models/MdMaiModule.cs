using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class MdMainModule
    {
        public string MainModuleId { get; set; } = null!;

        public string? Heading { get; set; }

        public string? SubHeading { get; set; }

        public string? Path { get; set; }

        public string? IsActive { get; set; }

        public int?    DisplayPriority { get; set; }

        public string? Remarks { get; set; }
        public string? Icon { get; set; }

        public string? CssClass { get; set; }
    }
}
