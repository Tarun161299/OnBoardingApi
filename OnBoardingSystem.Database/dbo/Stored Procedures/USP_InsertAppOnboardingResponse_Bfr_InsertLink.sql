CREATE procedure [dbo].[USP_InsertAppOnboardingResponse_Bfr_InsertLink]         
--exec USP_InsertAppOnboardingResponse 'CA20221042','P','All Good Ok','Ajay','All'        
@RequestNo varchar(50),        
@Status varchar(500),        
@Remarks varchar(500),        
@UserId varchar(500),        
@IpAddress varchar(50),        
@IsError bit output    
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
        
Set @IsError=0        
 If exists(Select * from App_OnboardingDetailsResponse where RequestNo=@RequestNo)        
  Begin        
  update App_OnboardingResponse set Status=@Status,Remarks=@Remarks  Where  RequestNo=@RequestNo  
 Update App_OnboardingRequest set Status=@Status,Remarks=@Remarks  Where  RequestNo=@RequestNo      
 --Update App_OnboardingResponseLink set Status=@Status,Remarks=@Remarks  Where  RequestNo=@RequestNo   
     --Update App_OnboardingDetailsResponse set Status=@Status,Remarks=@Remarks  Where  RequestNo=@RequestNo      
  Set @IsError=1        
  End        
else        
 Begin        
 Update App_OnboardingRequest set Status=@Status,Remarks=@Remarks  Where  RequestNo=@RequestNo      
 Insert into App_OnboardingResponse values(@RequestNo,@Status,@Remarks,0,@UserId,GETDATE(),@IpAddress)  
   --Insert into App_OnboardingDetailsResponse values(@RequestNo,@Status,@Remarks,0,@UserId,GETDATE(),@IpAddress)        
     --Insert into App_OnboardingResponseLink values (@RequestNo,0,'',0,@UserId,GETDATE(),@IpAddress)        
  Set @IsError=1        
  end        
        
--select 'message'='Data Stored Successfully'        
        
END