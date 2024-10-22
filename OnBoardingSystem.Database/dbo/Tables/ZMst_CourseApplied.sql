CREATE TABLE [dbo].[ZMst_CourseApplied] (
    [courseId]       VARCHAR (2)   NOT NULL,
    [courseName]     VARCHAR (200) NULL,
    [alternameNames] VARCHAR (200) NULL,
    CONSTRAINT [PK_ZMst_CourseApplied] PRIMARY KEY CLUSTERED ([courseId] ASC)
);

