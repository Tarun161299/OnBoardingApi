CREATE TABLE [dbo].[ApiSubscriptionKey] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [ApplicationName] VARCHAR (100) NULL,
    [ApplicationKey]  VARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

