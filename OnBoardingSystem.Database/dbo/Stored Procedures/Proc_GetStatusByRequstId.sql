CREATE PROCEDURE [dbo].[Proc_GetStatusByRequstId]
(
@RequestNo Varchar(30)
)
AS
-- Select Status,* from App_OnboardingRequest
-- Proc_GetStatusByRequstId 'CA20221022'
BEGIN
	DECLARE @StatusId char(2)
	DECLARE @StatusTable Table(StatusId char(2),Status varchar(30))

	Select @StatusId=Status from App_OnboardingRequest with(nolock) where RequestNo=@RequestNo

	INSERT INTO @StatusTable(StatusId,Status)
	Values ('RP','Pending'),('RA','Accepted'),('RR','Rejected'),('RH','Hold')
	select * from @StatusTable where StatusId!=@StatusId
END