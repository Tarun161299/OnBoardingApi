CREATE procedure GetRefreshToken    
@userid varchar(100),  
@refreshToken varchar(500),  
@Token varchar(1000)  
AS    
begin    

select * from User_Authorization where userId= @userid and refreshToken= @refreshToken and Token=@Token   
    
end