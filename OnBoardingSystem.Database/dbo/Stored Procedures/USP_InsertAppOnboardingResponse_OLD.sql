CREATE procedure [dbo].[USP_InsertAppOnboardingResponse_OLD] --exec USP_InsertAppOnboardingResponse 'CA20221042','A','All Good Ok','Ajay','::1','PsgGeMcVGBdo5PZlKozUFa5Vq35rMscRywBb*38Hi5o='                   
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
DECLARE @Version as int,@Link as varchar(5000);  
      
    Set @IsError=0    
    Set @Link ='https://demo.ecounselling.nic.in/pmucounse/OnBoardingSystem/#/onboardingdetails/'+@EncryptedRequestNumber  
    If exists(Select * from App_OnboardingDetailsResponse where RequestNo=@RequestNo)          
      Begin      
      IF Exists (Select * from App_OnboardingResponseLink where RequestNo = @RequestNo)  
         Begin  
          Select @Version=max(Version) from App_OnboardingResponseLink where RequestNo = @RequestNo  
          Insert into App_OnboardingResponseLink values (@RequestNo,@Version+1,'',@Link,@UserId,GetDate(),@IpAddress,@Status,GetDate()+7)  
         End  
        update App_OnboardingResponse set Status=@Status,Remarks=@Remarks  Where  RequestNo=@RequestNo    
        Update App_OnboardingRequest set Status=@Status,Remarks=@Remarks  Where  RequestNo=@RequestNo  
       --Update App_OnboardingDetailsResponseLink set Status=@Status where RequestNo = @RequestNo  
       --Update App_OnboardingResponseLink set Status=@Status,Remarks=@Remarks  Where  RequestNo=@RequestNo     
       --Update App_OnboardingDetailsResponse set Status=@Status,Remarks=@Remarks  Where  RequestNo=@RequestNo        
    Set @IsError=1          
      End          
  else          
      Begin          
        Update App_OnboardingRequest set Status=@Status,Remarks=@Remarks  Where  RequestNo=@RequestNo        
       Insert into App_OnboardingResponse values(2,@RequestNo,@Status,@Remarks,0,@UserId,GETDATE(),@IpAddress)  
       Insert into App_OnboardingResponseLink values (@RequestNo,1,'',@Link,@UserId,GetDate(),@IpAddress,@Status,GetDate()+7)  
          --IF((Select * from App_OnboardingResponseLink where RequestNo = @RequestNo) > )  
          -- Begin  
          --  Set @Message = 'Link Expired'  
          -- End  
          --Else   
          --  Set @Message = 'Active'  
             
       --Insert into App_OnboardingResponseLink values (@RequestNo,@Version,'',@Link,@UserId,GetDate(),@IpAddress,@Status,GetDate()+7)  
       --Insert into App_OnboardingDetailsResponse values(@RequestNo,@Status,@Remarks,0,@UserId,GETDATE(),@IpAddress)          
       --Insert into App_OnboardingResponseLink values (@RequestNo,0,'',0,@UserId,GETDATE(),@IpAddress)          
     Set @IsError=1          
      end          
          
--select 'message'='Data Stored Successfully'          
 END