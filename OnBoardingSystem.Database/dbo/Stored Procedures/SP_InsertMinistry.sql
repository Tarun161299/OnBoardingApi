CREATE PROCEDURE [dbo].[SP_InsertMinistry]  
@MinistryId int,  
@MinisrtyName varchar(50),
@IsError bit output
AS  
  
Begin  
	Insert into  MD_Ministry values(@MinistryId,@MinisrtyName)   
	Set @IsError=1
End