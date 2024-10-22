using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ManageProgramModel
    {public string? SerialNo { get; set;}

        public string? AgencyName { get; set; }
        public string? Name  {get;set; }
        public string?  Shift{get;set; }
        public string? TFW{ get; set; }
        public string? agencyId{ get; set; }
        public string? parent{ get; set; }
                     
        public string? RowNumber { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }

        public string? Message { get; set; }
    }
}
