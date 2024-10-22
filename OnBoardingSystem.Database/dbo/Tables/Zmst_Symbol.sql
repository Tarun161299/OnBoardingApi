CREATE TABLE [dbo].[Zmst_Symbol] (
    [symbolId]       CHAR (1)      NOT NULL,
    [description]    VARCHAR (200) NOT NULL,
    [alternateNames] VARCHAR (500) NULL,
    CONSTRAINT [PK_Zmst_Symbol] PRIMARY KEY CLUSTERED ([symbolId] ASC)
);

