CREATE procedure Usp_getrequestdetailsStatus  
@requestno varchar(500),  
@Email varchar(500)
AS  
Begin  
select *
from (select RequestNo,Services,AgencyType,OranizationName,MinistryId,Address,Pincode,ContactPerson,Designation,Email,d.Status,MobileNo from App_OnboardingRequest  
Inner Join RequestListInfo as d on  App_OnboardingRequest.RequestNo =d.RequestId )   
as t   where
 t.RequestNo='CA20231030'  and t.Email='Ajaykumar@gmail.com'
end