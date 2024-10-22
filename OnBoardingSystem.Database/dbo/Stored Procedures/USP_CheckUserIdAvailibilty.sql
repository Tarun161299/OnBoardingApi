Create procedure [dbo].[USP_CheckUserIdAvailibilty]   
@UserID varchar(50),          
@IsError bit output        
  
As            
BEGIN      
Set @IsError=0 
 
if exists (Select * from App_LoginDetails where userId=@UserID)  
Begin  
 Set @IsError=1  
End  
else  
Begin  
 Set @IsError=0  
End  
END