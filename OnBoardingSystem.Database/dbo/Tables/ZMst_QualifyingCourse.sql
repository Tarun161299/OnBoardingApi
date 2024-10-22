CREATE TABLE [dbo].[ZMst_QualifyingCourse] (
    [qualificationCourseId]   VARCHAR (2)   NOT NULL,
    [qualificationCourseName] VARCHAR (500) NULL,
    [qualificationId]         VARCHAR (2)   NOT NULL,
    CONSTRAINT [PK_ZMst_QualifyingCourse] PRIMARY KEY CLUSTERED ([qualificationCourseId] ASC, [qualificationId] ASC)
);

