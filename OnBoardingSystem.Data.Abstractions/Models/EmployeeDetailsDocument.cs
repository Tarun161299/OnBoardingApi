using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class AppDocumentFilter
    {
        public string ModuleRefId { get; set; }

        public string DocType { get; set; }

        public string ActivityId { get; set; }
    }
}
