CREATE TABLE [dbo].[Zmst_SeatGender] (
    [SeatGenderId]   VARCHAR (1)   NOT NULL,
    [description]    VARCHAR (100) NOT NULL,
    [alternateNames] VARCHAR (500) NULL,
    CONSTRAINT [PK_Zmst_SeatGender] PRIMARY KEY CLUSTERED ([SeatGenderId] ASC)
);

