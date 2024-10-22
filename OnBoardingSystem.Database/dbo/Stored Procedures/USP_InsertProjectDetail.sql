CREATE Procedure [dbo].[USP_InsertProjectDetail]  --exec [USP_InsertProjectDetail] 'CA20221011','L0000004','','TP100001','2024','1','asas'          
	@RequestNo varchar(50),          
	@RequestLetterNo varchar(50),          
	@DateOfrqstletter varchar(50),          
	@ProjectName varchar(50),          
	@ProjectYear varchar(50),         
	@Agency int,      
	@AgencyName varchar(500),    
	@IPAddress varchar(32),    
	@SubmitBy varchar(50),
	@IsWorkOrderReq bit
    
As          
 Begin      
	 if NOT EXISTS( Select RequestNo from App_ProjectDetails Where RequestNo=@RequestNo)      
	 Begin       
		Declare @ProJectCode varchar(20)=''
		select @ProJectCode= 'TEMP'+cast(Year(getdate()) as varchar)+(select RIGHT('0000'+ Cast(IsNull(max(id+1),100001) as varchar(30)),4) from App_ProjectDetails)
		Insert into App_ProjectDetails([RequestNo],[RequestLetterNo],[RequestLetterDate],[ProjectCode],[ProjectName],[ProjectYear],[IsWorkOrderRequired],[AgencyId],[AgencyName],[EFileNo],[PrizmId],[Status],[Remarks],[NICSIPINo],[PIDate],  [PIAmount],  [SubmitTime],[IPAddress],      [SubmitBy],[ModifyBy],[ModifyOn],[IsActive])          
		values(@RequestNo, @RequestLetterNo, @DateOfrqstletter ,@ProJectCode    ,@ProjectName,  @ProjectYear ,@IsWorkOrderReq,@Agency , @AgencyName   ,  ''      ,''       ,''          ,''             ,''        ,
		GETDATE()  ,0       ,GETDATE()    ,@IPAddress   ,@SubmitBy       ,Null      ,NULL   ,'')          
	 End      
 End