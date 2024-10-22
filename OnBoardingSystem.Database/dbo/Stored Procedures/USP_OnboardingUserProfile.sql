CREATE procedure USP_OnboardingUserProfile   -- Exec USP_OnboardingUserProfile 'Tarun1234'
(    
 @UserId varchar(50)  
)    
As    
Declare @RequestNo varchar(32);  
BEGIN    
 if Exists(Select top 1 1 from [dbo].[App_LoginDetails] with(nolock) where userId=@UserId )    
 Begin    
  Select @RequestNo=RequestNo from App_LoginDetails   where userId=@UserId   
Select userName as name,'' Designation from [App_LoginDetails] where userId=@UserId
 End    
 else     
 Begin    
  Select * from [dbo].[App_LoginDetails] where userId=''    
 End    
END