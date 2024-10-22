CREATE TABLE [dbo].[Zmst_SeatGroup] (
    [Id]             CHAR (2)      NOT NULL,
    [description]    VARCHAR (200) NOT NULL,
    [alternateNames] VARCHAR (500) NULL,
    CONSTRAINT [PK_Zmst_SeatGroup] PRIMARY KEY CLUSTERED ([Id] ASC)
);

