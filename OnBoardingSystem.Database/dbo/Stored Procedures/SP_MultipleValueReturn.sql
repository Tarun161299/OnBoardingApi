CREATE PROCEDURE [dbo].[SP_MultipleValueReturn]
@MinistryId int,
@RequestId  varchar(20)
AS

Begin
Select * from MD_Ministry Where MinistryId=@MinistryId
Select * from RequestListInfo Where RequestId=@RequestId
End