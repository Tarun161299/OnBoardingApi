CREATE TABLE [dbo].[MD_Organization] (
    [OrganizationId]   VARCHAR (500) NOT NULL,
    [OrganizationName] VARCHAR (500) NOT NULL,
    [StateId]          VARCHAR (2)   NULL,
    CONSTRAINT [PK_MD_Organization] PRIMARY KEY CLUSTERED ([OrganizationId] ASC)
);







