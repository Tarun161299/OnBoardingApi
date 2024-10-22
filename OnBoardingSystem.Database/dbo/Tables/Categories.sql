CREATE TABLE [dbo].[Categories] (
    [Id]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (MAX) NOT NULL,
    [Description]  NVARCHAR (MAX) NOT NULL,
    [DisplayOrder] INT            NOT NULL,
    [isDeleted]    BIT            NOT NULL,
    [Created]      DATETIME2 (7)  NOT NULL,
    [Author]       BIGINT         NOT NULL,
    [Modified]     DATETIME2 (7)  NULL,
    [Editor]       BIGINT         NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED ([Id] ASC)
);

