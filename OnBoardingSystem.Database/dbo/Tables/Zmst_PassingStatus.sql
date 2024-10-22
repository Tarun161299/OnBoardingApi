CREATE TABLE [dbo].[Zmst_PassingStatus] (
    [passingStatusId] VARCHAR (2)   NOT NULL,
    [description]     VARCHAR (200) NOT NULL,
    [alternateNames]  VARCHAR (500) NULL,
    CONSTRAINT [PK_Zmst_PassingStatus] PRIMARY KEY CLUSTERED ([passingStatusId] ASC)
);

