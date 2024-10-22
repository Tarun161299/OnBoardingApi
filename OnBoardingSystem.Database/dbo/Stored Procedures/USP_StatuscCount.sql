CREATE Procedure USP_StatuscCount    
 As    
  Begin    
   Declare @TotalRequest int,@ApprovedRequest int,@PendingRequest int,@HoldRequest int,@RejectRequest int,    
   @DetailsApproved int,@DetailsPending int,@DetailsReturn int,@DetailsReject int,@DetailsEligible int    
   Set @TotalRequest= (Select COUNT(*) from App_OnboardingRequest )    
   Set @ApprovedRequest= (Select COUNT(*) from App_OnboardingRequest Where Status='RA')    
   Set @PendingRequest= (Select COUNT(*) from App_OnboardingRequest Where Status='RP')    
   Set @HoldRequest= (Select COUNT(*) from App_OnboardingRequest Where Status='RH')    
   Set @RejectRequest= (Select COUNT(*) from App_OnboardingRequest Where Status='RR')    
    
   Set @DetailsApproved= (Select COUNT(*) from App_OnboardingDetails Where Status='DA')    
   Set @DetailsPending= (Select COUNT(*) from App_OnboardingDetails Where Status='DP')    
   Set @DetailsReturn= (Select COUNT(*) from App_OnboardingDetails Where Status='DT')    
   Set @DetailsReject= (Select COUNT(*) from App_OnboardingDetails Where Status='DR')    
   --Set @DetailsEligible= @ApprovedRequest-(@DetailsApproved+@DetailsPending+@DetailsReturn+@DetailsReject)   
   Set @DetailsEligible= @ApprovedRequest   
   Select @TotalRequest as TotalRequest,@ApprovedRequest as ApprovedRequest,@PendingRequest as PendingRequest,@HoldRequest as HoldRequest,@RejectRequest as RejectRequest    
   ,@DetailsApproved as ApprovedDetails,@DetailsPending as PendingDetails,@DetailsReturn as ReturnDetails,@DetailsReject as RejectDetails,@DetailsEligible as EligileDetails     
 End