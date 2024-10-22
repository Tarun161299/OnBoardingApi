CREATE TABLE [dbo].[Zmst_SeatType] (
    [Id]             VARCHAR (10)  NOT NULL,
    [description]    VARCHAR (250) NOT NULL,
    [alternateNames] VARCHAR (500) NULL,
    CONSTRAINT [PK_Zmst_SeatType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

