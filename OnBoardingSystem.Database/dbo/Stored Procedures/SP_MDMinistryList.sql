CREATE PROCEDURE SP_MDMinistryList
@MinistryId int
AS

Begin
SELECT * FROM MD_Ministry Where MinistryId=@MinistryId
End