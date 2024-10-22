
CREATE procedure USP_OnboardingAdminLogin --exec USP_OnboardingAdminLogin  'Admin123','TANQQJXNX4CLHLWC12URSG=='    
(    
 @UserId varchar(50),    
 @password varchar(250)    
)    
As    
BEGIN    
 if Exists(Select top 1 1 from [dbo].[App_LoginDetails] with(nolock) where userId=@UserId and password=@password)    
 Begin    
  Select * from [dbo].[App_LoginDetails] with(nolock)  where userId=@UserId and password=@password    
  Update App_LoginDetails Set lastSuccessfulLoginTime=GETDATE() where userId=@UserId and password=@password    
 End    
 else     
 Begin    
  Select * from [dbo].[App_LoginDetails] where userId=''  
 End    
END