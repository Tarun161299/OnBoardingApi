CREATE TABLE [dbo].[App_ProjectDetails] (
    [Id]                  INT             IDENTITY (1, 1) NOT NULL,
    [RequestNo]           VARCHAR (30)    NOT NULL,
    [RequestLetterNo]     VARCHAR (30)    NOT NULL,
    [RequestLetterDate]   DATETIME        NOT NULL,
    [ProjectCode]         VARCHAR (50)    NULL,
    [ProjectName]         VARCHAR (500)   NOT NULL,
    [ProjectYear]         VARCHAR (10)    NOT NULL,
    [IsWorkOrderRequired] BIT             NULL,
    [AgencyId]            INT             NOT NULL,
    [AgencyName]          VARCHAR (500)   NULL,
    [EFileNo]             VARCHAR (30)    NOT NULL,
    [PrizmId]             VARCHAR (30)    NOT NULL,
    [Status]              CHAR (2)        NULL,
    [Remarks]             VARCHAR (MAX)   NOT NULL,
    [NICSIPINo]           VARCHAR (30)    NOT NULL,
    [PIDate]              DATETIME        NOT NULL,
    [PIAmount]            DECIMAL (18, 2) NULL,
    [SubmitTime]          DATETIME        NULL,
    [IPAddress]           VARCHAR (32)    NULL,
    [SubmitBy]            VARCHAR (50)    NULL,
    [ModifyBy]            VARCHAR (50)    NULL,
    [ModifyOn]            DATETIME        NULL,
    [IsActive]            VARCHAR (2)     NULL,
    CONSTRAINT [PK_App_ProjectDetails] PRIMARY KEY CLUSTERED ([Id] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'System Generated;pattern;maxlength', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'App_ProjectDetails', @level2type = N'COLUMN', @level2name = N'ProjectCode';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required;pattern;maxlength;minlength', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'App_ProjectDetails', @level2type = N'COLUMN', @level2name = N'ProjectName';

