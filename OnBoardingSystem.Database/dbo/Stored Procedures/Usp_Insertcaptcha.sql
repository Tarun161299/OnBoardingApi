CREATE Proc Usp_Insertcaptcha  
@key varchar(50),  
@base64String nvarchar(max),  
@hashvalue varchar(max),  
@ip varchar(50)  
as  
Begin  
if Exists(Select top 1 *   from App_Captcha Where Captcha_Key=@key and Md5_Hash=@hashvalue )  
Begin  
Delete from App_Captcha Where Captcha_Key=@key and Md5_Hash=@hashvalue  
Insert into App_Captcha values(@key,@base64String,@hashvalue,@ip,GETDATE())  
End  
else  
Begin  
Insert into App_Captcha values(@key,@base64String,@hashvalue,@ip,GETDATE())  
End  
End