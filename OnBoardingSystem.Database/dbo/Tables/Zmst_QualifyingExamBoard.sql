CREATE TABLE [dbo].[Zmst_QualifyingExamBoard] (
    [Id]              VARCHAR (4)   NOT NULL,
    [description]     VARCHAR (500) NULL,
    [qualificationId] VARCHAR (2)   NOT NULL,
    [alternateNames]  VARCHAR (500) NULL,
    CONSTRAINT [PK_Zmst_QualifyingExamBoard] PRIMARY KEY CLUSTERED ([Id] ASC, [qualificationId] ASC)
);

