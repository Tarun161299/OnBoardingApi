CREATE TABLE [dbo].[Zmst_Qualification] (
    [qualificationId] VARCHAR (2)   NOT NULL,
    [description]     VARCHAR (300) NULL,
    [name]            VARCHAR (200) NOT NULL,
    [alternateNames]  VARCHAR (500) NULL,
    CONSTRAINT [PK_Zmst_Qualification] PRIMARY KEY CLUSTERED ([qualificationId] ASC)
);

