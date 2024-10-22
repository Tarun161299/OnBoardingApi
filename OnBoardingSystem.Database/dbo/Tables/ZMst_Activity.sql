CREATE TABLE [dbo].[ZMst_Activity] (
    [activityId]      VARCHAR (2)   NOT NULL,
    [activityName]    VARCHAR (200) NOT NULL,
    [DisplayPriority] INT           NULL,
    CONSTRAINT [PK_ZMst_Activity] PRIMARY KEY CLUSTERED ([activityId] ASC)
);

