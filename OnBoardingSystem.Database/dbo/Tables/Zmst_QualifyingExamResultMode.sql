CREATE TABLE [dbo].[Zmst_QualifyingExamResultMode] (
    [id]             VARCHAR (2)   NOT NULL,
    [description]    VARCHAR (200) NOT NULL,
    [alternatenames] VARCHAR (500) NULL,
    CONSTRAINT [PK_Zmst_QualifyingExamResultMode] PRIMARY KEY CLUSTERED ([id] ASC)
);

