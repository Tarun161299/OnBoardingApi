CREATE TABLE [dbo].[Zmst_MinimumQualification] (
    [minimumQualId]  VARCHAR (2)   NOT NULL,
    [description]    VARCHAR (200) NOT NULL,
    [alternateNames] VARCHAR (500) NULL,
    CONSTRAINT [PK_Zmst_MinimumQualification] PRIMARY KEY CLUSTERED ([minimumQualId] ASC)
);

