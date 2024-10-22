using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class AppProjectDetaillView
    {

        public int Id { get; set; }

        public string RequestNo { get; set; }

        public string RequestLetterNo { get; set; }

        public DateTime? RequestLetterDate { get; set; }

        public bool? IsWorkOrderRequired { get; set; }

        public string ProjectCode { get; set; }

        public string ProjectName { get; set; }

        public int? ProjectYear { get; set; }

        public int AgencyId { get; set; }

        public string AgencyName { get; set; }

        public string EfileNo { get; set; }

        public string PrizmId { get; set; }

        public string Status { get; set; }

        public string Remarks { get; set; }

        public string NicsiprojectCode { get; set; }

        public string Nicsipino { get; set; }

        public DateTime? Pidate { get; set; }

        public decimal? Piamount { get; set; }

        public DateTime? SubmitTime { get; set; }

        public string Ipaddress { get; set; }

        public string SubmitBy { get; set; }

        public string ModifyBy { get; set; }

        public DateTime? ModifyOn { get; set; }

        public string IsActive { get; set; }

        public string? MinistryName { get; set; }

        public string? OrganizationName { get; set; }

        public string? Doccontent { get; set; }
        public string? AgencyTypeName { get; set; }

        public string? MinistryId { get; set; }
        public string? OrgaizationId { get; set; }

        public string? MinistryOther { get; set; }
        public string? OrganizationOther { get; set; }

        public string? serviceType { get; set; }

    }
}
