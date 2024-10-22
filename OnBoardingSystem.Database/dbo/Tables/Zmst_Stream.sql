CREATE TABLE [dbo].[Zmst_Stream] (
    [streamId]       VARCHAR (2)   NOT NULL,
    [streamName]     VARCHAR (200) NOT NULL,
    [alternateNames] VARCHAR (500) NULL,
    CONSTRAINT [PK_Zmst_Stream] PRIMARY KEY CLUSTERED ([streamId] ASC)
);

