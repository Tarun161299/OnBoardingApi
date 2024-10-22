CREATE TABLE [dbo].[Zmst_SpecialExamPaper] (
    [id]             VARCHAR (2)   NOT NULL,
    [description]    VARCHAR (200) NULL,
    [alternateNames] VARCHAR (500) NULL,
    [specialExamId]  VARCHAR (2)   NOT NULL,
    CONSTRAINT [PK_Zmst_SpecialExamPaper] PRIMARY KEY CLUSTERED ([id] ASC, [specialExamId] ASC)
);

