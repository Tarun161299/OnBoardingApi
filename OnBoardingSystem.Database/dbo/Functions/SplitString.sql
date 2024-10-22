
Create FUNCTION [dbo].[SplitString]  
(      
      @Input NVARCHAR(MAX),  
      @Character CHAR(1)  
)  
RETURNS @Output TABLE (  
      Item NVARCHAR(1000)  
)  
AS  
BEGIN  
      DECLARE @StartIndex INT, @EndIndex INT  
   
      SET @StartIndex = 1  
      IF SUBSTRING(rtrim(ltrim(@Input)), LEN(rtrim(ltrim(@Input))) - 1, LEN(rtrim(ltrim(@Input)))) <> @Character  
      BEGIN  
            SET @Input = rtrim(ltrim(@Input)) + @Character  
      END  
   
      WHILE CHARINDEX(@Character, rtrim(ltrim(@Input))) > 0  
      BEGIN  
            SET @EndIndex = CHARINDEX(@Character, rtrim(ltrim(@Input)))  
             
            INSERT INTO @Output(Item)  
            SELECT SUBSTRING(rtrim(ltrim(@Input)), @StartIndex, @EndIndex - 1)
             
            SET @Input = SUBSTRING(rtrim(ltrim(@Input)), @EndIndex + 1, LEN(rtrim(ltrim(@Input))))  
      END  
   
      RETURN  
END