CREATE FUNCTION [dbo].[fn_capitalize] (
    @InPutStr VarChar(100),
    @Lenth int,
	@Case char(1)
)
RETURNS Varchar(1000)
/*
Select [dbo].[fn_capitalize]('vimal1',3,'U')
Select [dbo].[fn_capitalize]('VIMAL2',2,'L')
*/
AS
BEGIN
DECLARE @String varchar(1000)=''
SET @String=  CASE	WHEN @Case = 'L' THEN LOWER(SUBSTRING(@InPutStr,1,@Lenth))+SUBSTRING(@InPutStr,@Lenth+1,LEN(@InPutStr))
					WHEN @Case = 'U' THEN Upper(SUBSTRING(@InPutStr,1,@Lenth))+SUBSTRING(@InPutStr,@Lenth+1,LEN(@InPutStr))
					ELSE @InPutStr END
RETURN ISNULL(@String,'')
END