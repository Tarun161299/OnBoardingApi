CREATE Procedure Usp_OnboardingDetailsUpdateStatus    
    
@RequestNo varchar(50),    
@Status varchar (2),    
@Remarks varchar(max)    
As    
Begin    
update  App_OnboardingDetails set Status = @Status, Remarks = @Remarks where RequestNo=@RequestNo  
if(@Status='DT')  
Begin   
Update App_OnboardingRequest Set CurrentStage='Details form returned',ModifyOn=GetDate()  Where RequestNo=@RequestNo  
End  
if(@Status='DR')  
Begin   
Update App_OnboardingRequest Set CurrentStage='Details form Rejected',ModifyOn=GetDate() Where RequestNo=@RequestNo  
End  
if(@Status='DA')  
Begin   
Update App_OnboardingRequest Set CurrentStage='Details form Approved and Registration form sent',ModifyOn=GetDate() Where RequestNo=@RequestNo  
End  
END