//-----------------------------------------------------------------------
// <copyright file="MasterDataDirectories.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{
    [Serializable]
    public class MasterDataDirectory : SMSTemplate
    {
        public string? code { get; set; }

        public string? value { get; set; }

        public string? alternateNames { get; set; }

        public string? AgencyAbbr { get; set; }

        public string? AgencyType { get; set; }

        public string? address { get; set; }

        public string? isActive { get; set; }

        public string? ServiceTypeId { get; set; }

        public new string? agencyId { get; set; }
        public string? StateId { get; set; }

        public string? InstCd { get; set; }

        public string? AbbrNm { get; set; }

        public string? PrimaryStream { get; set; }

        public string? InstTypeId { get; set; }

        public string? SeatType { get; set; }

        //public new string? AgencyId { get; set; }

        public string? InstAdd { get; set; }

        public string? State { get; set; }

        public string? District { get; set; }

        public string? Pincode { get; set; }

        public string? InstPhone { get; set; }

        public string? InstFax { get; set; }

        public string? InstWebSite { get; set; }

        public string? EmailId { get; set; }

        public string? AltEmailId { get; set; }

        public string? ContactPerson { get; set; }

        public string? Designation { get; set; }

        public string? MobileNo { get; set; }

        public string? AISHACode { get; set; }

        public string? subCategoryId { get; set; }

        public string? specialexamId { get; set; }

        public string? qualificationId { get; set; }

        public string? streamId { get; set; }

        public string? SAARCCode { get; set; }

        public string? SAARCName { get; set; }

        public string? isdCode { get; set; }

        public string? priority { get; set; }

        public string? stream { get; set; }

        public string? id { get; set; }

        public string? shift { get; set; }

        public string? tfw { get; set; }

        public string? description { get; set; }

        //public string? messagetemplate { get; set; }

        //public string? messagetemplatetrai { get; set; }

        //public string? templatestatus { get; set; }

        //public string? templateid { get; set; }
    }

    [Serializable]
    public class SMSTemplate
    {
        public string templateid { get; set; }
        public string description { get; set; }
        public string templatestatus { get; set; }
        public string messagetemplate { get; set; }
        public string messagetemplatetrai { get; set; }

    }
}
