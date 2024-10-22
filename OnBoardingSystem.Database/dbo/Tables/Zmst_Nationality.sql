CREATE TABLE [dbo].[Zmst_Nationality] (
    [nationalityId] VARCHAR (2)   NOT NULL,
    [description]   VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_Zmst_Nationality] PRIMARY KEY CLUSTERED ([nationalityId] ASC)
);

