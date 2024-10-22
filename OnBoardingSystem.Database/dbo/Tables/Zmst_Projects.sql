CREATE TABLE [dbo].[Zmst_Projects] (
    [agencyId]      INT             NULL,
    [examCounsid]   VARCHAR (3)     NULL,
    [academicYear]  INT             NULL,
    [serviceType]   INT             NULL,
    [attempt]       INT             NULL,
    [projectId]     BIGINT          NOT NULL,
    [projectName]   VARCHAR (200)   NULL,
    [description]   VARCHAR (200)   NULL,
    [requestLetter] VARBINARY (MAX) NULL,
    [created_date]  VARCHAR (50)    NULL,
    [created_by]    VARCHAR (15)    NULL,
    [modified_date] VARCHAR (50)    NULL,
    [modified_by]   VARCHAR (15)    NULL,
    [IsLive]        CHAR (1)        NULL,
    [PInitiated]    CHAR (1)        NULL,
    CONSTRAINT [PK_Zmst_Projects] PRIMARY KEY CLUSTERED ([projectId] ASC)
);

