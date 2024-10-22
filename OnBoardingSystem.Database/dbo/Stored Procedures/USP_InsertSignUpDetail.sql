CREATE procedure [dbo].[USP_InsertSignUpDetail] --exec USP_InsertSignUpDetail 'Kumar.Gautam','Test@123'                 
               
@UserID varchar(256),        
@Password varchar(500),      
@RequestNo varchar(32),  
@LastSuccessfulLoginIP varchar(50),  
@LastLoginIP varchar(20),
@UserName varchar(50)
As                
BEGIN       
      
  Insert Into App_LoginDetails(RequestNo,userId,isActive,isPasswordChanged,lastLoginTime,lastLoginIP,password,passwordHistory1,passwordHistory2,passwordHistory3        
  ,authenticationType,securityQuestionId,securityAnswer,lastFailedLoginAttemptTime,lastFailedLoginAttemptIP,failedLoginAttemptCount        
  ,lastSuccessfulLoginTime,lastSuccessfulLoginIP,lastPasswordChangeTime,lastPasswordChangeIP,lastPasswordResetTime,lastPasswordResetIP,userName)        
  values(@RequestNo,@UserID,'Y','N',Getdate(),@LastLoginIP,@Password,Null,Null,Null,'O','','',Getdate(),'','',Getdate(),@LastSuccessfulLoginIP,Getdate(),'',Getdate(),'',@UserName)          
  Insert into App_UserRoleMapping (UserID,RoleID,IsReadOnly,IsActive)      
  Values(@UserID,'USER','Y','Y')      
  --select 'message'='Data Stored Successfully'        
      
END