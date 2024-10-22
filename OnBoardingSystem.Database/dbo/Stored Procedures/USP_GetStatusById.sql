CREATE Procedure USP_GetStatusById--USP_GetStatusById 'CA20221004'  
@RequestId varchar(50)
 
AS  
Begin   
  select R.RequestNo,R.Remarks,R.Status,R.Email,P.Name ,P.EmailId cordmail, 
  Case When D.RequestNo IS NULL then 'NA' else D.RequestNo End RequestNOD,  
  Case When D.Remarks IS NULL then 'NA' else D.Remarks End RemarksD,  
  Case When D.Status IS NULL then 'NA' else D.Status End StatusD  
  from App_OnboardingRequest as R
   inner join App_ContactPersonDetails as P on R.requestNo=P.RequestNo   and p.RoleId='11' 
  left join App_OnboardingDetails as D  on R.requestNo=D.RequestNo   
  where R.requestNO=@RequestId   
End