CREATE TABLE [dbo].[Zmst_Willingness] (
    [willingnessId]  VARCHAR (2)   NOT NULL,
    [description]    VARCHAR (200) NOT NULL,
    [alternateNames] VARCHAR (500) NULL,
    CONSTRAINT [PK_Zmst_Willingness] PRIMARY KEY CLUSTERED ([willingnessId] ASC)
);

