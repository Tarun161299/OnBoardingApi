CREATE TABLE [dbo].[Zmst_FeeType] (
    [activityId]  INT           NOT NULL,
    [description] VARCHAR (100) NULL,
    CONSTRAINT [PK_Zmst_FeeType] PRIMARY KEY CLUSTERED ([activityId] ASC)
);

