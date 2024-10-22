CREATE procedure [dbo].[usp_ImportPrograms]                    
(                    
@tableName VARCHAR(128)=null,                    
@Mode Varchar(10)=null,  -- V: Validate , S:Save                   
@isError bit output,                    
@Message  VARCHAR(50) output                    
)                    
AS                    
BEGIN                    
/*                    
Declare @isErrorOut bit                    
Declare @MessageOut VARCHAR(50)                    
exec usp_ImportPrograms 'asd',@isErrorOut output ,@MessageOut output                    
Select @isErrorOut ,@MessageOut                    
Select * from Program                  
Select * from ZMST_Program where id='9041'                  
*/                    
--SELECT @isError=1 ,@Message='Duplicate data founddfcwsf.'                    
                  
--set @tableName='ExcelData_2d93d81f66ee488ca69cf19aae83c01e20220705114412948'                    
Declare  @Program TABLE                    
(                   
Code VARCHAR(1000),                    
[Name] VARCHAR(1000),                    
[Shift]  VARCHAR(1000),                    
agencyId VARCHAR(10),                    
TFW VARCHAR(10),                    
parent VARCHAR(110),                   
parentOrg VARCHAR(110),                    
parentid VARCHAR(110) null,                  
IsAlreadyExists bit null                  
)                    
Declare @maxId int                  
Select  @maxId=max(id) From ZMST_Program                  
                  
INSERT INTO @Program(Code,[Name],[Shift],TFW,agencyId,parent,parentOrg)                    
EXEC('select '''', Name,Shift,TFW ,agencyId,Parent,Parent from '+@tableName+'' )                    
                  
if exists(Select Count(*)  SName,Name from @Program group by Name,Shift,TFW,AgencyId,parent having Count(*)>1)                    
BEGIN                    
             pRint 'tarun'        
 Select  q.AgencyName,t.Code,t.[Name],t.[Shift],t.agencyId,t.TFW,t.parent   from @Program t    inner join MD_Agency q on q.AgencyID=t.agencyId      
 PRINT 'OM123'  
 set @isError=1  
 set @Message='Duplicate data found.'  
-- SELECT @isError=1 ,@Message='Duplicate data found.'  
select @isError,@Message  
return                  
END                    
  print 'om 1'                  
DECLARE @MaxProgramId int                    
SELECT @MaxProgramId=MAX(CAST(LEFT(brcd,5) as int)) FROM [dbo].[ZMST_Program]                    
                  
UPDATE T                    
SET T.Code=P.brcd,parentid=P.brcd  , IsAlreadyExists=1                  
FROM @Program T                    
INNER JOIN                    
(Select brcd, brnm Description from [dbo].[ZMST_Program]) P On LTRIM(RTRIM(T.Name))=LTRIM(RTRIM(P.description))                    
where LTRIM(RTRIM(P.description)) !=''                  
                  
UPDATE T                    
SET parentid=P.brcd                    
From @Program T                    
INNER JOIN                    
(SELECT distinct brcd, brnm Description from [dbo].[ZMST_Program]) P On LTRIM(RTRIM(T.parent))=LTRIM(RTRIM(P.description))                    
where LTRIM(RTRIM(P.description)) !=''                  
                  
Update T                    
SET T.parent=RNo                    
FROM (                    
SELECT row_number() over(order by name asc) RNo, * from @Program T Where IsNull(T.parent,'')=''                    
)T                    
                    
Update T                    
SET T.Code= LEFT(T.parentid,5)+ [shift]+TFW  FROM @Program T WHERE IsNull(T.Code,'')='' and IsNull(T.parentid,'')!=''                    
                    
Update T                    
SET T.Code= CAST(@MaxProgramId+RNo as varchar(30))+[shift]+TFW                    
from (                    
SELECT dense_Rank() over(order by parent asc) RNo, * from @Program T Where IsNull(T.Code,'')='' and IsNull(T.parentid,'')=''                    
)T                    
print 'om 2'                  
--if((Select Count(*) from @Program T Inner Join                    
--[dbo].[ZMST_Program] P on T.Code=P.brcd and T.agencyId=P.agencyid and T.Name=P.brnm)>'0')                  
-- BEGIN                  
--    --PRINT 'OM if'                  
--  Select @isError=1,@Message='Data already exist'                  
                  
--  --Select [Name],[Shift],agencyId,TFW,parent,'' RowNumber, @Message Description from @Program                   
--  Select  [Name],[Shift],T.agencyId,TFW,parent,                  
--  Case when P.brcd is null then null else 1 end RowNumber,                   
--  Case when P.brcd is null then null else @Message end  Description                  
--  from @Program T                   
--  LEFT OUTER Join                    
--  [dbo].[ZMST_Program] P on T.Code=P.brcd and T.agencyId=P.agencyid and T.Name=P.brnm                  
-- END                  
--else                  
-- BEGIN                  
--   --PRINT 'OM else'                  
--  Select @isError=0,@Message='Data uploadsuccessfully'              
--  INSERT INTO ZMST_Program(id,[brcd],[brnm],agencyid,brcdOrg,bshift,btfw)                  
--  select (@maxId+ROW_NUMBER()OVER(ORDER BY Code)),Code, Name,agencyId,Code,Shift,TFW from @Program                     
                  
                    
-- END                  
                  
 IF(@Mode='S')                  
 BEGIN         
  set @isError=0  
 set @Message='Code Generated and Saved.'  
-- SELECT @isError=1 ,@Message='Duplicate data found.'  
select @isError,@Message  
  --Select @isError,@Message                  
  INSERT INTO ZMST_Program(id,[brcd],[brnm],agencyid,brcdOrg,bshift,btfw)                  
  select (@maxId+ROW_NUMBER()OVER(ORDER BY Code)),Code, Name,agencyId,Code,Shift,TFW                
  FROM                  
  (  select q.AgencyName,p.agencyid,p.code,p.Name,p.Shift,p.TFW from                 
  (Select agencyId,Code,Name,Shift,TFW from @Program                     
   EXCEPT                   
   Select T.agencyId,brcd as Code,brnm Name, bshift Shift,btfw                   
   from [dbo].[ZMST_Program] P                  
   Inner Join @Program T on T.Code=P.brcd and T.agencyId=P.agencyid and T.Name=P.brnm) p inner join MD_Agency q on q.AgencyID=p.agencyId                   
  )X                  
  select q.AgencyName,a.agencyid,a.code,a.Name,a.Shift,a.parent,TFW, @isError RowNumber,Description from               
  (Select T.agencyId,Code,Name,Shift,TFW ,parentOrg parent,                  
  @isError RowNumber,case when IsAlreadyExists = 1 then 'Already Exists' else  @Message end Description                  
  from [dbo].[ZMST_Program] P                  
  Inner Join  @Program T on T.Code=P.brcd and T.agencyId=P.agencyid and T.Name=P.brnm  ) a inner join MD_Agency q on q.AgencyID=a.agencyId                   
                 
 END                  
 ELSE                   
 BEGIN                
 
  Select Name, q.AgencyName, T.agencyId,Code,Shift,TFW ,parentOrg parent,                  
  @isError RowNumber,case when IsAlreadyExists = 1 then 'Already Exists' else  @Message end Description                  
  from @Program T    inner join MD_Agency q on q.AgencyID=T.agencyId   
  print 'OM Else'          
 set @isError=0
 set @Message='Code Generated'
   Select @isError,@Message
 END                  
 IF OBJECT_ID(@tableName,'U') IS NOT NULL                   
 BEGIN                  
  EXEC('Drop table '+@tableName+'' )                    
 END                  
END