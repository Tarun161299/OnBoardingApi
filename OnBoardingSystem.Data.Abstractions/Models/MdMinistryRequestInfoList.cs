//-----------------------------------------------------------------------
// <copyright file="MdMinistryRequestInfoList.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------
namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class MdMinistryRequestInfoList
    { 
     
        public List<RequestList> RequestLists { get; set; }
        public List<MdMinistry> MdMinistries { get; set; }
        //public int MinistryId { get; set; }

        //public string MinistryName { get; set; }

        //public int Id { get; set; }

        //public string RequestId { get; set; }

        //public string AgencyType { get; set; }

        //public string OranizationName { get; set; }

        //public DateTime? RequestDate { get; set; }

        //public string Status { get; set; }
    }
}
