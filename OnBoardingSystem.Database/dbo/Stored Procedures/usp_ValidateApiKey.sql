CREATE proc usp_ValidateApiKey
(	
@Apikey varchar(100)
)
As
Begin
	Declare @message bit
	If exists(Select * from ApiSubscriptionKey where ApplicationKey = @Apikey)
		Begin
			Set @message = 1
			Select @message as message
		End
	Else
		Begin
			set @message=0
			select @message as message
		End		
End