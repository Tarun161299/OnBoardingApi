CREATE TABLE [dbo].[Zmst_Subject] (
    [qualificationId] VARCHAR (2)   NOT NULL,
    [subjectId]       VARCHAR (4)   NOT NULL,
    [subjectName]     VARCHAR (200) NOT NULL,
    [alternateNames]  VARCHAR (500) NULL,
    CONSTRAINT [PK_Zmst_Subject] PRIMARY KEY CLUSTERED ([qualificationId] ASC, [subjectId] ASC)
);

