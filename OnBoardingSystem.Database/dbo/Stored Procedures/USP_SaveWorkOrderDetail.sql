CREATE procedure USP_SaveWorkOrderDetail          
(          
@InputJson Varchar(MAX),              
 @IsError BIT output           
 )          
 As          
 BEGIN  
 declare @WorkOrderID int
      SET @IsError=0          
  SELECT *  INTO #TT  FROM OpenJson(@InputJson)              
  WITH (             
    WorkorderNo varchar(50) '$.WorkorderNo',               
    ProjectCode varchar(50) '$.ProjectCode',              
   IssueDate datetime '$.IssueDate',              
   AgencyName varchar(150) '$.AgencyName',              
   ResourceCategory varchar(100) '$.ResourceCategory',              
   ResourceNo varchar(2) '$.ResourceNo',              
   NoofMonths varchar(3) '$.NoofMonths',              
   WorkorderFrom varchar(50) '$.WorkorderFrom',            
   WorkorderTo datetime '$.WorkorderTo',              
   DocName varchar(100) '$.DocName',              
   Document varchar(max) '$.Document'            
             
  )           
 --SET @TicketId=(SELECT Max(TicketId) from App_Ticket)           
  --SET @DocumentId=(SELECT IDENT_CURRENT ('App_DocumentUploadedDetail'))         
 insert into Workorderdetails  
         ([workorderNo],[projectCode],[issueDate],[agencyName],[resourceCategory],[resourceNo],[noofMonths],[workorderFrom],[workorderTo],[docName])          
  select T.WorkorderNo,T.ProjectCode,T.IssueDate,T.AgencyName,T.ResourceCategory  ,T.ResourceNo,T.NoofMonths,T.WorkorderFrom,T.WorkorderTo,T.DocName from #TT T 

set @WorkOrderID=(SELECT IDENT_CURRENT ('WorkOrderdetails'))     
 Insert into App_DocumentUploadedDetail               
       ([requestNo],[docType],  [docContent],[activityid])              
 select Cast(@WorkOrderID as varchar(15)),'',T.Document,'201'          
 from #TT T             
         
          
   set  @IsError=1   
          
End