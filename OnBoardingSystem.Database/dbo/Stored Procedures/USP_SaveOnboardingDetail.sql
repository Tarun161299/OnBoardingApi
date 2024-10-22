CREATE PROCEDURE [dbo].[USP_SaveOnboardingDetail]    
(    
	@InputJson Varchar(MAX),  
	@IsError BIT output 
	--@Message Varchar(100) output    
	-- @Mode varchar(20)=null
)    
AS    
/*    
DECLARE @InputJson Varchar(MAX), @IsError bit 
 --@Message varchar(100)
--SET @Mode='FinalSubmit'
Set @InputJson ='{"RequestNo":"CA20231008","Website":"www.gnail.com","YearOfFirstTimeAffilitionSession":"2024-2025","ExamLastSessionConductedIn":null,"ExamLastSessionTechSupportBy"
:null,"ExamLastSessionDescription":null,"CounsLastSessionConductedIn":null,"CounsLastSessionTechSupportBy":"sas","CounsLastSessionDescription":"asas","ExamExpectedApplicant":null,
"ExamCourseList":null,"ExamTotalCourse":null,"ExamTentativeScheduleStart":null,"ExamTentativeScheduleEnd":null,"ExamDissimilarityOfSchedule":false,"CounsExpectedApplicant":12,
"CounsExpectedSeat":13,"CounsCourseList":"as","CounsTotalCourse":123,"CounsExpectedRound":13,"CounsExpectedSpotRound":56,"CounsExpectedParticipatingInstitute":2345,
"CounsTentativeScheduleStart":"2023-02-23T00:00:00","CounsTentativeScheduleEnd":"2023-02-25T00:00:00","CounsDissimilarityOfSchedule":true,"SubmitTime":"2023-01-30T00:00:00",
"Ipaddress":"","Status":"","Remarks":"","IsActive":"","Mode":"SaveDraft","contactdetails":[{"id":0,"requestNo":"CA20231008","departmentId":"1","roleId":"11","name":"tarun",
"designation":"engineer","mobileNo":"oQ5xmLaR9g55gZ5eq6BPGA==","emailId":"G4NQ7BhpxV200y3q5x0LrA=="},{"id":0,"requestNo":"CA20231008","departmentId":"1","roleId":"12","name":"",
"designation":"","mobileNo":"MpohMOQ8JQQdcTLmyG7hgQ==","emailId":"MpohMOQ8JQQdcTLmyG7hgQ=="},{"id":0,"requestNo":"CA20231008","departmentId":"1","roleId":"13","name":"",
"designation":"","mobileNo":"MpohMOQ8JQQdcTLmyG7hgQ==","emailId":"MpohMOQ8JQQdcTLmyG7hgQ=="},{"id":0,"requestNo":"CA20231008","departmentId":"2","roleId":"21","name":"",
"designation":"","mobileNo":"MpohMOQ8JQQdcTLmyG7hgQ==","emailId":"MpohMOQ8JQQdcTLmyG7hgQ=="},{"id":0,"requestNo":"CA20231008","departmentId":"2","roleId":"22","name":"",
"designation":"","mobileNo":"MpohMOQ8JQQdcTLmyG7hgQ==","emailId":"MpohMOQ8JQQdcTLmyG7hgQ=="},{"id":0,"requestNo":"CA20231008","departmentId":"3","roleId":"31","name":"",
"designation":"","mobileNo":"MpohMOQ8JQQdcTLmyG7hgQ==","emailId":"MpohMOQ8JQQdcTLmyG7hgQ=="},{"id":0,"requestNo":"CA20231008","departmentId":"3","roleId":"32","name":"",
"designation":"","mobileNo":"MpohMOQ8JQQdcTLmyG7hgQ==","emailId":"MpohMOQ8JQQdcTLmyG7hgQ=="},
{"id":0,"requestNo":"CA20231008","departmentId":"3","roleId":"33","name":"","designation":"","mobileNo":"MpohMOQ8JQQdcTLmyG7hgQ==","emailId":"MpohMOQ8JQQdcTLmyG7hgQ=="}]}  
'    
EXEC USP_SaveOnboardingDetail @InputJson  
, @IsError output-- ,  @Message output    
select @IsError ,  @Message    
*/    
BEGIN 
	SET @IsError=0
	--SET @Message ='Saved'    
		
	SELECT *  INTO #TT  FROM OpenJson(@InputJson)  
	WITH (RequestNo VARCHAR(50) '$.RequestNo',  
	Website varchar(64) '$.Website',   
	YearOfFirstTimeAffilitionSession varchar(16) '$.YearOfFirstTimeAffilitionSession',  
	ExamLastSessionConductedIn varchar(16) '$.ExamLastSessionConductedIn',  
	ExamLastSessionTechSupportBy varchar(200) '$.ExamLastSessionTechSupportBy',  
	ExamLastSessionDescription varchar(max) '$.ExamLastSessionDescription',  
	CounsLastSessionConductedIn varchar(16) '$.CounsLastSessionConductedIn',  
	CounsLastSessionTechSupportBy varchar(200) '$.CounsLastSessionTechSupportBy',  
	CounsLastSessionDescription varchar(max) '$.CounsLastSessionDescription',
	ExamExpectedApplicant int '$.ExamExpectedApplicant',  
	ExamCourseList varchar(max) '$.ExamCourseList',  
	ExamTotalCourse INT '$.ExamTotalCourse',  
	ExamTentativeScheduleStart datetime '$.ExamTentativeScheduleStart',  
	ExamTentativeScheduleEnd datetime '$.ExamTentativeScheduleEnd',  
	ExamDissimilarityOfSchedule bit '$.ExamDissimilarityOfSchedule',  
	CounsExpectedApplicant INT '$.CounsExpectedApplicant',  
	CounsExpectedSeat INT '$.CounsExpectedSeat',  
	CounsCourseList varchar(max) '$.CounsCourseList',  
	CounsTotalCourse INT '$.CounsTotalCourse', 
	CounsExpectedRound INT '$.CounsExpectedRound',  
	CounsExpectedSpotRound INT '$.CounsExpectedSpotRound', 
	CounsExpectedParticipatingInstitute INT '$.CounsExpectedParticipatingInstitute',  
	CounsTentativeScheduleStart datetime '$.CounsTentativeScheduleStart',  
	CounsTentativeScheduleEnd datetime '$.CounsTentativeScheduleEnd',  
	CounsDissimilarityOfSchedule bit '$.CounsDissimilarityOfSchedule',  
	SubmitTime datetime '$.SubmitTime',  
	IPAddress varchar(20) '$.IPAddress',  
	Status varchar(20) '$.Status',  
	Remarks varchar(max) '$.Remarks',  
	IsActive varchar(2) '$.IsActive',  
	Mode varchar(20) '$.Mode',
	Contactdetails NVARCHAR(MAX) '$.contactdetails' AS JSON  
	)   
  
	----------------------------Contactdetails Json----------------------  
  
	Declare @ContactInputJson Varchar(MAX) , @RequestNo varchar(50),@Mode varchar(20),@Status varchar(2)
	Set @ContactInputJson=(Select #TT.Contactdetails from #TT)  
	Set @RequestNo=(Select #TT.RequestNo from #TT)  
	SET @Mode=(Select #TT.Mode from #TT)

	SELECT *  INTO #TTCdetails  FROM OpenJson(@ContactInputJson)  
	--CROSS APPLY OPENJSON(B.Contactdetails)  
	WITH (RequestNo VARCHAR(50) '$.RequestNo',  
	DepartmentId varchar(50) '$.departmentId',  
	RoleId varchar(50) '$.roleId',  
	Name varchar(50) '$.name',  
	Designation varchar(50) '$.designation',  
	MobileNo varchar(300) '$.mobileNo',  
	EmailId varchar(300) '$.emailId'  
	)  

		if(@Mode='FinalSubmit')
		Begin
		
		set @Status='DP'
		Update App_OnboardingRequest Set CurrentStage='Details form submitted' Where RequestNo=@RequestNo
		End
	
		if(@Mode='SaveDraft')
		Begin
	
		set @Status='DD'
		End

		if exists(Select A.RequestNo  from App_OnboardingDetails A Where A.RequestNo=@RequestNo)
	Begin	
		UPDATE D SET 
		D.RequestNo								=T.RequestNo							,	
		D.Website								=T.Website								,
		D.YearOfFirstTimeAffilitionSession		=T.YearOfFirstTimeAffilitionSession		,
		D.ExamLastSessionConductedIn			=T.ExamLastSessionConductedIn			,
		D.ExamLastSessionTechSupportBy			=T.ExamLastSessionTechSupportBy			,
		D.ExamLastSessionDescription			=T.ExamLastSessionDescription			,
		D.CounsLastSessionConductedIn			=T.CounsLastSessionConductedIn			,
		D.CounsLastSessionTechSupportBy			=T.CounsLastSessionTechSupportBy		,	
		D.CounsLastSessionDescription			=T.CounsLastSessionDescription			,
		D.ExamExpectedApplicant					=T.ExamExpectedApplicant				,	
		D.ExamCourseList						=T.ExamCourseList						,
		D.ExamTotalCourse						=T.ExamTotalCourse						,
		D.ExamTentativeScheduleStart			=T.ExamTentativeScheduleStart			,
		D.ExamTentativeScheduleEnd				=T.ExamTentativeScheduleEnd				,
		D.ExamDissimilarityOfSchedule			=T.ExamDissimilarityOfSchedule			,
		D.CounsExpectedApplicant				=T.CounsExpectedApplicant				,
		D.CounsExpectedSeat						=T.CounsExpectedSeat					,	
		D.CounsCourseList						=T.CounsCourseList						,
		D.CounsTotalCourse						=T.CounsTotalCourse						,
		D.CounsExpectedRound					=T.CounsExpectedRound					,
		D.CounsExpectedSpotRound				=T.CounsExpectedSpotRound				,
		D.CounsExpectedParticipatingInstitute	=T.CounsExpectedParticipatingInstitute	,
		D.CounsTentativeScheduleStart			=T.CounsTentativeScheduleStart			,
		D.CounsTentativeScheduleEnd				=T.CounsTentativeScheduleEnd			,	
		D.CounsDissimilarityOfSchedule			=T.CounsDissimilarityOfSchedule			,		
		D.SubmitTime=T.SubmitTime,D.IPAddress=T.IPAddress, 
		D.Status=@Status,D.Remarks=T.Remarks,D.IsActive=T.IsActive 
		FROM App_OnboardingDetails D 
		INNER JOIN #TT T on D.RequestNo=D.RequestNo 
		where D.RequestNo=@RequestNo

		SET @IsError = 1
	End

	if not exists(Select A.RequestNo  from App_OnboardingDetails A Where A.RequestNo=@RequestNo)
	Begin
	
		---------------------------Insert App_OnboardingDetails------------------ 
		Insert into App_OnboardingDetails([RequestNo], [Website], [YearOfFirstTimeAffilitionSession], 
		[ExamLastSessionConductedIn], [ExamLastSessionTechSupportBy], [ExamLastSessionDescription], 
		[CounsLastSessionConductedIn], [CounsLastSessionTechSupportBy], [CounsLastSessionDescription], 
		[ExamExpectedApplicant], [ExamCourseList], [ExamTotalCourse], [ExamTentativeScheduleStart], [ExamTentativeScheduleEnd], [ExamDissimilarityOfSchedule], 
		[CounsExpectedApplicant], [CounsExpectedSeat], [CounsCourseList], [CounsTotalCourse], [CounsExpectedRound], [CounsExpectedSpotRound], 
		[CounsExpectedParticipatingInstitute], [CounsTentativeScheduleStart], [CounsTentativeScheduleEnd], [CounsDissimilarityOfSchedule], 
		[SubmitTime], [IPAddress], [Status], [Remarks], [IsActive])  
		Select T.[RequestNo], T.[Website], T.[YearOfFirstTimeAffilitionSession], 
		T.[ExamLastSessionConductedIn], T.[ExamLastSessionTechSupportBy], T.[ExamLastSessionDescription], 
		T.[CounsLastSessionConductedIn], T.[CounsLastSessionTechSupportBy], T.[CounsLastSessionDescription], 
		T.[ExamExpectedApplicant], T.[ExamCourseList], T.[ExamTotalCourse], T.[ExamTentativeScheduleStart], T.[ExamTentativeScheduleEnd], T.[ExamDissimilarityOfSchedule], 
		T.[CounsExpectedApplicant], T.[CounsExpectedSeat], T.[CounsCourseList], T.[CounsTotalCourse], T.[CounsExpectedRound], T.[CounsExpectedSpotRound], 
		T.[CounsExpectedParticipatingInstitute], T.[CounsTentativeScheduleStart], T.[CounsTentativeScheduleEnd], T.[CounsDissimilarityOfSchedule],
		T.SubmitTime,T.IPAddress,@Status,T.Remarks,T.IsActive 
		from #TT T  
  
		----------------Insert App_ContactPersonDetails-------------------  
		Insert into  App_ContactPersonDetails (RequestNo,DepartmentId,RoleId,Name,Designation,MobileNo,EmailId)  
		Select @RequestNo,Tc.DepartmentId,Tc.RoleId,Tc.Name,Tc.Designation,Tc.MobileNo,Tc.EmailId from #TTCdetails Tc  
		SET @IsError = 1
	End

	if exists(Select A.RequestNo  from App_ContactPersonDetails A Where A.RequestNo=@RequestNo)
	Begin
	
	Print @RequestNo
		UPDATE CPD 
		SET CPD.MobileNo=T.MobileNo,CPD.Name=T.Name,CPD.Designation=T.Designation,CPD.EmailId=T.EmailId 
		from 
		App_ContactPersonDetails CPD 
		INNER JOIN #TTCdetails T on CPD.RequestNo=@RequestNo and CPD.DepartmentId=T.DepartmentId and CPD.RoleId=T.RoleId 
			SET @IsError = 1
	End

	
END