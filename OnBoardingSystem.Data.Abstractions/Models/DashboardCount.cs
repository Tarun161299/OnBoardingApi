//-----------------------------------------------------------------------
// <copyright file="DashboardCount.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------
namespace OnBoardingSystem.Data.Abstractions.Models;
public  class DashboardCount
{
    public int  TotalRequest { get; set; }
          
    public int  ApprovedRequest { get; set; }
           
    public int  PendingRequest { get; set; }
           
    public int  HoldRequest { get; set; }
         
    public int  RejectRequest { get; set; }
           
    public int  ApprovedDetails { get; set; }
          
    public int  PendingDetails { get; set; }
           
    public int  ReturnDetails { get; set; }
          
    public int  RejectDetails { get; set; }
           
    public int  EligileDetails { get; set; }
    public List<DashboardCount> StatusDetail { get; set; }
}