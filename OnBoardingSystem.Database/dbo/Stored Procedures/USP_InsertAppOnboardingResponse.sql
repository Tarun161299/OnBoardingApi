CREATE procedure [dbo].[USP_InsertAppOnboardingResponse] --exec USP_InsertAppOnboardingResponse 'CA20221042','A','All Good Ok','Ajay','::1','PsgGeMcVGBdo5PZlKozUFa5Vq35rMscRywBb*38Hi5o='                   
@RequestNo varchar(50),          
@Status varchar(500),          
@Remarks varchar(500),          
@UserId varchar(500),         
@IpAddress varchar(50),  
@EncryptedRequestNumber varchar(5000),  
@IsError bit output      
--@Message varchar(500) output  
As          
BEGIN          
          
/*          
DECLARE @RequestNo varchar(50),@Status varchar(500),@Remarks varchar(500),@UserId varchar(500),@IpAddress varchar(50),@IsError bit output          
SET @RequestNo='CA20221042',          
SET @Status='P',          
SET @Remarks='All Good Ok',           
SET @UserId='OM',          
SET @IpAddress='::1',          
EXEC USP_SaveOnboardingDetail @RequestNo, @Status , @Remarks , @UserId ,@IpAddress  , @IsError output          
*/    
DECLARE @LinkVersion as int,@Link as varchar(5000),@ResponseVersion int,@ResponseId int,@CurrentStage varchar(50) 
      
	Set @IsError=0    
	if(@Status='RA')
		Begin
			SET @CurrentStage='Request Approved & Details Form Send'
		End
	if(@Status='RP')
		Begin
			SET @CurrentStage='Request Pending'
		End
	if(@Status='RH')
		Begin
			SET @CurrentStage='Request Hold'
		End
	if(@Status='RR')
		Begin
			SET @CurrentStage='Request Reject'
		End
Set @Link ='https://demo.ecounselling.nic.in/pmucounse/OnBoardingSystem/#/onboardingdetails/'+@EncryptedRequestNumber 
		Select @ResponseId=max(Id) from App_OnboardingResponse where RequestNo = @RequestNo  
		Begin
		IF Exists (Select * from App_OnboardingResponseLink where RequestNo = @RequestNo)  
        Begin  
		Select @LinkVersion=max(Version) from App_OnboardingResponseLink where RequestNo = @RequestNo  
		Insert into App_OnboardingResponseLink values (@RequestNo,@LinkVersion+1,@ResponseId,@Link,@UserId,GetDate(),@IpAddress,@Status,GetDate()+7) 
		End
		else
		Begin
		SET @LinkVersion=0
		Select @ResponseVersion=max(Version) from App_OnboardingResponse where RequestNo = @RequestNo  
		Update App_OnboardingRequest set CurrentStage=@CurrentStage, Status=@Status,Remarks=@Remarks  Where  RequestNo=@RequestNo 
		Insert into App_OnboardingResponse values(@ResponseId+1,@RequestNo,@Status,@Remarks,@ResponseVersion+1,@UserId,GETDATE(),@IpAddress)  
        if(@Status='RA')
		Insert into App_OnboardingResponseLink values (@RequestNo,@LinkVersion+1,@ResponseId+1,@Link,@UserId,GetDate(),@IpAddress,@Status,GetDate()+7)  
		End
		End
		Set @IsError=1  
END