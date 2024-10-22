CREATE TABLE [dbo].[MD_EmailRecipient] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Email]       VARCHAR (500) NULL,
    [RoleId]      INT           NULL,
    [CreatedBy]   VARCHAR (500) NULL,
    [CreatedDate] DATE          NULL,
    CONSTRAINT [PK_MD_EmailRecipient] PRIMARY KEY CLUSTERED ([Id] ASC)
);



