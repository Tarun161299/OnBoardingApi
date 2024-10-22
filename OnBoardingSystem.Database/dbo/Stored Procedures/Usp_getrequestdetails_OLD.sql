Create procedure [dbo].[Usp_getrequestdetails_OLD]
@requestno varchar(500)
AS
Begin
SELECT * FROM 
(
	select MinistryName,o.AgencyType,o.RequestNo,o.docContent,o.Services,o.OranizationName,o.MinistryId,o.Address,o.Pincode,o.ContactPerson,o.Designation,o.Email,o.MobileNo 
	from 
	(
		select docContent,t.AgencyType,t.RequestNo,t.Services,t.OranizationName,t.MinistryId,t.Address,t.Pincode,t.ContactPerson,t.Designation,t.Email,t.MobileNo 
		from 
		(
		select RequestNo,Services,AgencyType,OranizationName,MinistryId,Address,Pincode,ContactPerson,Designation,Email,MobileNo from App_OnboardingRequest
		Inner Join RequestListInfo on  App_OnboardingRequest.RequestNo =RequestListInfo.RequestId 
		) 
		as t inner join App_DocumentUploadedDetail on t.RequestNo =App_DocumentUploadedDetail.requestNo
	) 
	as o inner join MD_ministry on o.MinistryId=MD_ministry.MinistryId
) as V  where V.RequestNo=@requestno
end