CREATE TABLE [dbo].[Zmst_Gender] (
    [genderId]       VARCHAR (2)   NOT NULL,
    [genderName]     VARCHAR (30)  NOT NULL,
    [alternateNames] VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_Zmst_Gender] PRIMARY KEY CLUSTERED ([genderId] ASC)
);

