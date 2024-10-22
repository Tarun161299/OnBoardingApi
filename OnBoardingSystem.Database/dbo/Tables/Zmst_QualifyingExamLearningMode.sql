CREATE TABLE [dbo].[Zmst_QualifyingExamLearningMode] (
    [Id]             VARCHAR (2)   NOT NULL,
    [description]    VARCHAR (200) NOT NULL,
    [alternateNames] VARCHAR (500) NULL,
    CONSTRAINT [PK_Zmst_QualifyingExamLearningMode] PRIMARY KEY CLUSTERED ([Id] ASC)
);

