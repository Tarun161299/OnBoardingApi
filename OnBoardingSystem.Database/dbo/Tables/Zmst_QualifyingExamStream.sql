CREATE TABLE [dbo].[Zmst_QualifyingExamStream] (
    [qualStreamId]   VARCHAR (2)    NOT NULL,
    [qualStreamName] VARCHAR (1000) NULL,
    CONSTRAINT [PK_Zmst_QualifyingExamStream] PRIMARY KEY CLUSTERED ([qualStreamId] ASC)
);

