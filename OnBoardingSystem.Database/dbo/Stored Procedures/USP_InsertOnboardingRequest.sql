CREATE procedure [dbo].[USP_InsertOnboardingRequest] --exec USP_InsertOnboardingRequest 2,1,'string',11,'string',2022,'test','201009','str ing','string','string','string','string',' ','','','string','string','string','string',''                     
@AgencyID int,                    
@MinistryId int,                    
@MinistryOther varchar(256),                    
@NameOfOrganisation int,                    
@OtherOganisation varchar(256),                    
@SessionYear int,                    
@Address nvarchar(1024),                    
@PinCode varchar(6),                    
@HeadOfOrganisation varchar(32),                    
@Designation varchar(32),                    
@Services varchar(500),                    
@Email varchar(300),                    
@Mobileno varchar(500),                    
@IpAddress varchar(32),                  
@filename varchar(32),                  
@fileextension varchar(32),                  
@modifieddate varchar(32),                  
@fileContent varchar(MAX),                
@format varchar(32),                
@id varchar(4),            
@coordinatorName varchar(50),            
@coordinatorEmail varchar(300),            
@coordinatorMobile varchar(50),            
@coordinatorDesignation varchar(50),            
@stateID varchar(50),            
@districtID varchar(50),          
@AgencyStateId int,          
@CurrentStage varchar(50),    
--@createdBy varchar(100),     
@requestId varchar(50) output            
        
As                    
BEGIN                    
Declare @AgencyCode as varchar(5000),@AgencyType as varchar(500),@RequestNo varchar(50),@RequestNoTemp varchar(50),@ResponseId int              
 BEGIN                    
  if(@AgencyID=1)                    
  begin                     
   set @AgencyCode='CA'                        
  end                    
  else if(@AgencyID=2)                   
  begin                    
   set @AgencyCode='SA'                       
  end              
             
  SELECT @RequestNoTemp=IsNull(max(right(t.RequestNo,4))+1,1000) from App_OnboardingRequest T WITH(NOLOCK)            
  SET @RequestNo = @AgencyCode+Cast(@SessionYear as varchar(100))+Cast(@RequestNoTemp as varchar(100))            
                  
  Insert Into App_OnboardingRequest(RequestNo,AgencyTypeId,Services,SessionYear,MinistryId,MinistryOther, OrganizationId,OrganizationOther,AgencyStateId,Address,PinCode,ContactPerson,Designation,Email,MobileNo,CurrentStage,SubmitTime,IPAddress,Status,Remarks,ModifyOn,IsActive,        
StateId,DistrictId)            
  values(@RequestNo,@AgencyID,@Services,@SessionYear,@MinistryId,@MinistryOther, @NameOfOrganisation,@OtherOganisation,@AgencyStateId,@Address,@PinCode,@HeadOfOrganisation,@Designation,@Email,@Mobileno,@CurrentStage,GETDATE(),@IpAddress,'RP','',Null,'1',cast(@stateID as int)        
,@districtID)                
              
              
  Insert Into App_DocumentUploadedDetail             
  ([activityid], [requestNo], [docType], [docId], [docContent], [objectId], [objectUrl], [docNatureId], [ipAddress], [subTime],[createdBy])              
  values ('101',   @RequestNo,   '01',       '',     @fileContent,   '',        '',          '',          @IpAddress,GETDATE(),'By User')                
  --select 'message'='Data Stored Successfully...'            
  Insert Into App_ContactPersonDetails([RequestNo],[DepartmentId],[RoleId],[Name],[Designation],[MobileNo],[EmailId]) values(@RequestNo,'1','11',@coordinatorName,@coordinatorDesignation,@coordinatorMobile,@coordinatorEmail)            
   set @requestId=@RequestNo            
   Insert Into App_OnboardingResponse (Id,RequestNo,Status,Remarks,Version,UserId,SubmitTime,IPAddress)          
   values(1,@RequestNo,'P','',0,'',GETDATE(),@IpAddress)          
  --   select 'requestno'= @RequestId            
  --select @requestId            
 END            
END