CREATE TABLE [dbo].[App_Captcha] (
    [Id]                BIGINT         IDENTITY (1, 1) NOT NULL,
    [Captcha_Key]       VARCHAR (50)   NULL,
    [Captch_BaseString] NVARCHAR (MAX) NULL,
    [Md5_Hash]          VARCHAR (MAX)  NULL,
    [Ip]                VARCHAR (50)   NULL,
    [Created_Date]      DATETIME       NULL,
    CONSTRAINT [PK_App_Captcha] PRIMARY KEY CLUSTERED ([Id] ASC)
);

