CREATE FUNCTION [dbo].[fn_getAngGroupVal] (
    @InPutStr VarChar(max),
    @MaxLenth int
)
RETURNS Varchar(MAX)
/*
Select dbo.fn_getAngGroupVal('email,max_length,min_length,url,alphanumeric',';') vv
*/
AS
BEGIN
--DECLARE @InPutStr VARCHAR(MAX)='abcd ;required;pattern;maxlength;minlength'
--DECLARE @Seprator varchar(10)=';'
DECLARE @String varchar(max)=''
DECLARE @Tbl Table(KeyWords varchar(200),Patern varchar(200))
INSERT INTO @Tbl
	  Select 'url'			,'Validators.pattern("^((https?|ftp|smtp):\/\/)?(www.)?[a-z0-9]+\.[a-z]+(\/[a-zA-Z0-9#]+\/?)*$")'
UNION Select 'alphabet'		,'Validators.pattern("^[A-Za-z. ]+$")'
UNION Select 'number'		,'Validators.pattern("^[0-9]*$")'
UNION Select 'alphanumeric'	,'Validators.pattern("^([A-Za-z. ]+.,[a-zA-z. ])+1|[A-Za-z. ]+$")'
UNION Select 'required'		,'Validators.required'
UNION Select 'maxlength'	,'Validators.maxLength(#LNTH)'
UNION Select 'minlength'	,'Validators.minLength(#LNTH)'
UNION SELECT 'email'		,'Validators.email'
UNION Select 'email'		,'Validators.pattern("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,5}$")'
UNION Select 'checkbox'		,'Validators.requiredTrue'
--Select KeyWords ,Patern from @Tbl
select @String=@String+','+CASE WHEN Patern LIKE '%#LNTH%' THEN REPLACE(Patern,'#LNTH',Cast(@MaxLenth as varchar(10))) ELSE Patern END +'' from	
		(
		Select * from @Tbl T 
		INNER JOIN fn_Split(@InPutStr,';') TT
		ON T.KeyWords=TT.strval		 
		) AA ORDER BY strval
--IF(LEN(@String)>2)
--BEGIN
--	SELECT @String=SUBSTRING(@String,2, Len(@String))
--END
--select @String
--Select * from @Tbl
--Select * from  fn_Split(@InPutStr,',') 
RETURN ISNULL(@String,'')
END


--Select dbo.fn_getAngGroupVal('abcd ;required;pattern;maxlength;minlength',';')
--Validators.maxLength(#MAX),Validators.minLength(#MIN),Validators.required