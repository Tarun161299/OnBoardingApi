CREATE TABLE [dbo].[Zmst_Trade] (
    [id]             VARCHAR (2)   NOT NULL,
    [description]    VARCHAR (200) NOT NULL,
    [alternateNames] VARCHAR (500) NULL,
    CONSTRAINT [PK_Zmst_Trade] PRIMARY KEY CLUSTERED ([id] ASC)
);

