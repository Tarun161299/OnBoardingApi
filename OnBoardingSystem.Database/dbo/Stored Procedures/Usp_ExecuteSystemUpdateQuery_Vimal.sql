CREATE PROCEDURE [dbo].[Usp_ExecuteSystemUpdateQuery_Vimal]  --Usp_ExecuteSystemUpdateQuery_Vimal 'Insert into App_Settings values('1','Name','Vimal','M')'                                    
 @queryStatetment nvarchar(max) 
AS                                      
BEGIN 
/*
Declare @query nvarchar(max) ='Update Board_CandidateProfile set countryid=02 where boardId=129042221 and applicationNo in (220100000005,220100000008)'
Exec Usp_ExecuteSystemUpdateQuery @query
*/
Set NOCOUNT off
 Declare @flag varchar(2)
 DECLARE @AFFECTED_ROWS INT;
 DECLARE @ParamDefinition NVARCHAR(100) = '@ROW_SQL INT OUTPUT'
 SET @queryStatetment += 'SELECT @ROW_SQL = @@ROWCOUNT;';
    set @flag='N'  
    if @queryStatetment <>''
 Begin
   Exec sp_executesql @queryStatetment,  @ParamDefinition, @ROW_SQL=@AFFECTED_ROWS OUTPUT;
   set @flag='Y'
   SELECT CAST(@AFFECTED_ROWS AS VARCHAR(20)) As RowsAffected, @flag As Flag
 End
 Else
 Begin
   set @flag='N'
   Select 0 As RowsAffected, @flag As Flag
 End                               
END