CREATE TABLE [dbo].[Zmst_CourseAppliedLevel] (
    [courseLevelId]   CHAR (2)      NOT NULL,
    [courseLevelName] VARCHAR (500) NULL,
    CONSTRAINT [PK_Zmst_CourseAppliedLevel] PRIMARY KEY CLUSTERED ([courseLevelId] ASC)
);

