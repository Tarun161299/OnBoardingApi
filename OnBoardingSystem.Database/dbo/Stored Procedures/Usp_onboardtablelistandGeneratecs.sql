CREATE procedure Usp_onboardtablelistandGeneratecs 
@mode nvarchar(100)=''
as
begin
if(@mode='BindTables')
begin
 select name as [key],name as [value] from sys.tables
end
end