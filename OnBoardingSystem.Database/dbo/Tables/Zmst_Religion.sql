CREATE TABLE [dbo].[Zmst_Religion] (
    [religionId]     VARCHAR (2)   NOT NULL,
    [description]    VARCHAR (200) NOT NULL,
    [alternateNames] VARCHAR (500) NULL,
    CONSTRAINT [PK_Zmst_Religion] PRIMARY KEY CLUSTERED ([religionId] ASC)
);

