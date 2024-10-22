CREATE TABLE [dbo].[RequestListInfo] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [RequestId]       VARCHAR (50)  NULL,
    [AgencyType]      VARCHAR (50)  NULL,
    [OranizationName] VARCHAR (500) NULL,
    [RequestDate]     DATETIME      NULL,
    [Status]          VARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

