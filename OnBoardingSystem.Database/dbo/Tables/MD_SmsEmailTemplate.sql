CREATE TABLE [dbo].[MD_SmsEmailTemplate] (
    [TemplateId]           VARCHAR (6)   NOT NULL,
    [description]          VARCHAR (500) NULL,
    [messageTypeId]        VARCHAR (6)   NULL,
    [messageSubject]       VARCHAR (500) NULL,
    [messageTemplate]      VARCHAR (MAX) NULL,
    [RegisteredTemplateId] VARCHAR (50)  NULL,
    CONSTRAINT [PK_MD_SmsEmailTemplate] PRIMARY KEY CLUSTERED ([TemplateId] ASC)
);



