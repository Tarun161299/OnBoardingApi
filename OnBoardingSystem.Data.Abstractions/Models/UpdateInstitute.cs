//-----------------------------------------------------------------------
// <copyright file="UpdateInstitute.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class UpdateInstitute
    {
        public string boardid { get; set; }

        public string instituteid { get; set; }

        public string state { get; set; }

        public string district { get; set; }

        public string pincode { get; set; }

        public string address { get; set; }

        public string fax { get; set; }

        public string website { get; set; }

        public string ipaddress { get; set; }

        public string user { get; set; }
    }
}
