CREATE TABLE [dbo].[ApplicationSchedule] (
    [id]          INT           NULL,
    [appId]       VARCHAR (10)  NOT NULL,
    [summary]     VARCHAR (MAX) NULL,
    [updatedTime] DATETIME      NULL,
    [updatedBy]   VARCHAR (15)  NULL,
    [ipAddress]   VARCHAR (20)  NULL,
    [priority]    INT           NULL,
    [status]      CHAR (1)      NULL,
    PRIMARY KEY CLUSTERED ([appId] ASC)
);

