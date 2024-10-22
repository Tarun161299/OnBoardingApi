//-----------------------------------------------------------------------
// <copyright file="OnBoardingRequestDetailUpsert.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class OnBoardingRequestDetailUpsert : OnBoardingRequestDetail
    {
        public string MinistryName { get; set; }

        public string AgencyType { get; set; }

        public string docContent { get; set; }

        public string Services { get; set; }

        public string OranizationName { get; set; }

        public string Designation { get; set; }

        public string ShowStatus { get; set;}

        public string CurrentStage { get; set; }

        public DateTime? ModifyOn { get; set; }
    }
}