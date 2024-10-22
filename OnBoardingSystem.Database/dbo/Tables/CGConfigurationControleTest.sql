CREATE TABLE [dbo].[CGConfigurationControleTest] (
    [Id]            INT             NOT NULL,
    [Name]          VARCHAR (50)    NULL,
    [Gender]        VARCHAR (2)     NULL,
    [DOB]           VARCHAR (50)    NULL,
    [Email]         VARCHAR (50)    NULL,
    [Mobile]        INT             NULL,
    [Qualification] VARCHAR (50)    NULL,
    [Image]         VARCHAR (MAX)   NULL,
    [Addresss]      VARCHAR (MAX)   NULL,
    [State]         VARCHAR (10)    NULL,
    [District]      VARCHAR (10)    NULL,
    [Fee]           DECIMAL (18, 2) NULL,
    [Hobbies]       VARCHAR (50)    NULL,
    CONSTRAINT [PK___ConfigurationTest] PRIMARY KEY CLUSTERED ([Id] ASC)
);

