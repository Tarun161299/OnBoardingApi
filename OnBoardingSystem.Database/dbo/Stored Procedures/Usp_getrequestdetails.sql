CREATE procedure [dbo].[Usp_getrequestdetails]
@requestno varchar(500)
AS
Begin
Select Rq.RequestNo,Rq.Services,Rq.MinistryId,Rq.Address,Rq.Pincode,Rq.ContactPerson,Rq.Designation,Rq.Email,Rq.MobileNo,Rql.AgencyType,
Rql.OranizationName,Doc.docContent,Mm.MinistryName from App_OnboardingRequest Rq 
Inner Join RequestListInfo Rql on Rql.RequestId=Rq.RequestNo
Inner Join App_DocumentUploadedDetail Doc on Doc.requestNo=Rq.RequestNo
inner Join MD_Ministry Mm on Mm.MinistryId=Rq.MinistryId 
Where Rq.RequestNo=@requestno
end