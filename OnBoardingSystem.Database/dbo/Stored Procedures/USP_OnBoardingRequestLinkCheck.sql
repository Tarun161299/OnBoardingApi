Create procedure [dbo].[USP_OnBoardingRequestLinkCheck] 
@RequestNo varchar(50),        
@IsError bit output      

As          
BEGIN    
Set @IsError=0    
Declare @Version int,@LinkDate datetime
	Select @Version=(Select max(Version) from App_OnboardingResponseLink Where RequestNo=@RequestNo)
	Select @LinkDate=(Select ExpiryDate from App_OnboardingResponseLink Where RequestNo=@RequestNo and Version=@Version)
if(@LinkDate>GETDATE())
Begin
	Set @IsError=1
End
else
Begin
	Set @IsError=0
End
END