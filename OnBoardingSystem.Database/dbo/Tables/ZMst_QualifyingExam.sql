CREATE TABLE [dbo].[ZMst_QualifyingExam] (
    [qualifyingExamId]   VARCHAR (2)   NOT NULL,
    [qualifyingExamName] VARCHAR (500) NULL,
    CONSTRAINT [PK_ZMst_QualifyingExam] PRIMARY KEY CLUSTERED ([qualifyingExamId] ASC)
);

