CREATE TABLE [dbo].[qualificationDetails] (
    [qualificationDetailsId] INT           NOT NULL,
    [empCode]                VARCHAR (15)  NULL,
    [examPassed]             VARCHAR (2)   NULL,
    [boardUniv]              VARCHAR (150) NULL,
    [passYear]               VARCHAR (4)   NULL,
    [division]               VARCHAR (10)  NULL,
    CONSTRAINT [PK_qualificationDetails] PRIMARY KEY CLUSTERED ([qualificationDetailsId] ASC)
);

