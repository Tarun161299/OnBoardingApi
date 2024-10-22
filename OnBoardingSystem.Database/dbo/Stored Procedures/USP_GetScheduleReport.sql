CREATE Proc [dbo].[USP_GetScheduleReport]     
(    
 @BoardIds varchar(Max)    
)    
AS    
-- USP_GetScheduleReport '103012121,'    
-- USP_GetScheduleReport ''    
BEGIN    
 IF(IsNull(@BoardIds,'')='')    
 BEGIN    
  SELECT @BoardIds+=Cast(projectId as varchar(100))+','     
  FROM Zmst_Projects     
 END    
    
 SELECT PR.projectName,boardid,roundno,activityid,S.description,sdate+' - '+cDate as DateRange,ScheduleStatus    
 INTO #TT    
 FROM ApplicationSchedule SH with(nolock)    
 INNER JOIN Zmst_Projects PR with(nolock) ON SH.appId=PR.projectId    
 CROSS APPLY OPENJSON(summary)     
 WITH (    
   boardid int,    
   roundno int,    
   activityid varchar(300),    
   --ActivityNm varchar(300),    
   description varchar(300),    
   sdate varchar(30),    
   cDate varchar(30),    
   ScheduleStatus varchar(30)    
 ) s    
 WHERE Activityid in ('7','8','9','20','26')    
 AND BoardId  in(select Value from [dbo].[fnSplit] (@BoardIds,','))    
     
 DECLARE @Columns varchar(MAX)=''    
 select @Columns=@Columns+',['+description+']' from     
   (    
   SELECT distinct description from #TT WITH(NOLOCK)       
   ) AA ORDER BY description    
 SELECT @Columns=SUBSTRING(@Columns,2, Len(@Columns))    
 SELECT @Columns=Replace(@Columns,'[],','')    
 --drop table #TT    
 --Select @Columns    
 --return    
  
  DECLARE @HeadCol varchar(max)=''  
  SELECT @HeadCol+=','+'IsNull('+Value+',''-'')'+Value  
  FROM [dbo].[fnSplit] (@Columns,',')     
  
 DECLARE @SqlStatement NVARCHAR(MAX)    
 SET @SqlStatement = N'    
 SELECT ( 
-- boardid,
   SELECT projectName as [Counselling Name],roundno as [Round No]'+@HeadCol+'     
   FROM (    
    SELECT boardid,projectName,roundno,description,DateRange     
    FROM #TT    
   )     
   Rs    
   PIVOT (    
    Max(DateRange)    
    FOR description    
    IN ('+@Columns+')    
  ) AS PivotTable    
  FOR JSON AUTO, WITHOUT_ARRAY_WRAPPER) JsonString'    
      
 EXEC(@SqlStatement)     
 drop table #TT    
END