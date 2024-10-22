CREATE TABLE [dbo].[Zmst_Quota] (
    [quotaId]        VARCHAR (2)   NOT NULL,
    [name]           VARCHAR (100) NOT NULL,
    [alternateNames] VARCHAR (100) NULL,
    CONSTRAINT [PK_Zmst_Quota] PRIMARY KEY CLUSTERED ([quotaId] ASC)
);

