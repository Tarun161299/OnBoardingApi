CREATE TABLE [dbo].[Registration] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [FirstName]   VARCHAR (50) NULL,
    [LastName]    VARCHAR (50) NULL,
    [Email]       VARCHAR (50) NULL,
    [Password]    VARCHAR (50) NULL,
    [CountryId]   INT          NULL,
    [CountryName] VARCHAR (50) NULL,
    [Status]      INT          NULL,
    [SubmitTime]  DATETIME     NULL,
    CONSTRAINT [PK_Registration] PRIMARY KEY CLUSTERED ([Id] ASC)
);

