CREATE TABLE [dbo].[Zmst_DocumentType] (
    [documentTypeId] VARCHAR (2)   NOT NULL,
    [title]          VARCHAR (200) NOT NULL,
    [alternateNames] VARCHAR (500) NULL,
    PRIMARY KEY CLUSTERED ([documentTypeId] ASC)
);



