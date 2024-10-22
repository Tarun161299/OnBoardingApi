CREATE Procedure [USP_SaveDocumentUploadDetails]      
(            
 @InputJson Varchar(MAX),          
 @IsError BIT output         
 --@Message Varchar(100) output            
 -- @Mode varchar(20)=null        
)            
AS        
/*      
Set @InputJson ={"DocumentId":0,      
"Activityid":"",      
"RequestNo":"CA20221055",      
"CycleId":"16,15"      
"DocType":"2G",      
"DocId":"",      
"DocSubject":"",      
"DocContent":"",      
"ObjectId":"",      
"ObjectUrl":"",      
"DocNatureId":"",      
"IpAddress":"",      
"SubTime":"2023-02-27T09:53:56.11Z"      
}      
EXEC [USP_SaveDocumentUploadDetails] @InputJson          
, @IsError output-- ,  @Message output            
select @IsError ,  @Message        
*/      
DECLARE @IpAddress as  varchar(50)    
 SET @IsError=0        
Begin       
      
 SELECT *  INTO #TT  FROM OpenJson(@InputJson)          
 WITH (RequestNo VARCHAR(50) '$.RequestNo',       
 CycleId  INT '$.CycleId',      
 DocType  varchar(5) '$.DocType',           
 DocSubject  varchar(20) '$.DocSubject',      
 DocContent  varchar(max) '$.DocContent',      
 ObjectId  varchar(50) '$.ObjectId',      
 ObjectUrl  varchar(500) '$.ObjectUrl',      
 DocNatureId  varchar(50) '$.DocNatureId',      
 IpAddress  varchar(50) '$.IpAddress',      
 SubTime  datetime '$.SubTime',  
 CreatedBy varchar(100) '$.CreatedBy',
 Activityid varchar(5) '$.Activityid')      
    
Set @IpAddress=(select top(1) IpAddress from  #TT)    
   Insert into App_DocumentUploadedDetail([activityid],[requestNo],[cycleId],[docType],[docId],[docSubject],[docContent],[objectId],[objectUrl],[docNatureId],[ipAddress],[subTime],[createdBy])         
   Select T.Activityid,T.RequestNo,T.CycleId,T.DocType,'',T.DocSubject,T.DocContent,T.ObjectId,T.ObjectUrl,T.DocNatureId,@IpAddress,T.SubTime,T.CreatedBy      
  from #TT T        
  SET @IsError=1      
End