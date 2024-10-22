CREATE FUNCTION [dbo].[fn_EmpCode] (
)
RETURNS Varchar(1000)
/*
Select [dbo].[fn_capitalize]('vimal1',3,'U')
Select [dbo].[fn_capitalize]('VIMAL2',2,'L')
*/
AS
BEGIN
	RETURN (select cast(Year(getdate()) as varchar)+(select Cast(IsNull(max(cast(SUBSTRING(empCode,5,len(empCode)) as int)+1),100001) as varchar(30))  from EmployeeDetails))
END