CREATE TABLE [dbo].[ZMST_ApplicationSummaryBKP] (
    [AgencyId]      VARCHAR (15)    NULL,
    [appYear]       VARCHAR (4)     NULL,
    [appType]       VARCHAR (5)     NULL,
    [appId]         VARCHAR (10)    NULL,
    [appTitle]      VARCHAR (500)   NULL,
    [appURL]        VARCHAR (1000)  NULL,
    [summary]       VARCHAR (MAX)   NULL,
    [status]        CHAR (1)        NULL,
    [apiLink]       VARCHAR (1000)  NULL,
    [updatedTime]   DATETIME        NULL,
    [updatedBy]     VARCHAR (15)    NULL,
    [ipAddress]     VARCHAR (20)    NULL,
    [priority]      INT             NULL,
    [ScheduleDoc]   VARBINARY (MAX) NULL,
    [id]            INT             IDENTITY (1, 1) NOT NULL,
    [totalRound]    INT             NULL,
    [AdminURL]      VARCHAR (500)   NULL,
    [ContactDetail] VARCHAR (500)   NULL
);

