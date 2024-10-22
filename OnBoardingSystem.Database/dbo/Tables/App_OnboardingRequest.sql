CREATE TABLE [dbo].[App_OnboardingRequest] (
    [RequestNo]         VARCHAR (32)    NOT NULL,
    [AgencyTypeId]      INT             NULL,
    [Services]          VARCHAR (500)   NULL,
    [SessionYear]       INT             NULL,
    [MinistryId]        INT             NULL,
    [MinistryOther]     VARCHAR (256)   NULL,
    [OrganizationId]    INT             NULL,
    [OrganizationOther] VARCHAR (256)   NULL,
    [AgencyStateId]     INT             NULL,
    [Address]           NVARCHAR (1024) NULL,
    [StateId]           INT             NULL,
    [DistrictId]        INT             NULL,
    [PinCode]           VARCHAR (6)     NULL,
    [ContactPerson]     VARCHAR (64)    NULL,
    [Designation]       VARCHAR (120)   NULL,
    [Email]             VARCHAR (300)   NULL,
    [MobileNo]          VARCHAR (100)   NULL,
    [CurrentStage]      VARCHAR (150)   NULL,
    [SubmitTime]        DATETIME        NULL,
    [IPAddress]         VARCHAR (32)    NULL,
    [Remarks]           VARCHAR (MAX)   NULL,
    [ModifyOn]          DATETIME        NULL,
    [Status]            VARCHAR (2)     NULL,
    [IsActive]          VARCHAR (2)     NULL,
    CONSTRAINT [PK_App_OnboardingRequest] PRIMARY KEY CLUSTERED ([RequestNo] ASC)
);



