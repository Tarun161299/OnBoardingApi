CREATE Proc Check_Captcha        
@key varchar(50),      
@hash varchar(max),  
@check bit output      
as        
Begin        
    
if Exists(Select top 1 *   from App_Captcha Where Captcha_Key=@key and Md5_Hash=@hash)        
Begin      
Delete from  App_Captcha Where Captcha_Key=@key and Md5_Hash=@hash        
Set @check=1      
End      
Else      
Begin       
Set @check=0      
End      
End