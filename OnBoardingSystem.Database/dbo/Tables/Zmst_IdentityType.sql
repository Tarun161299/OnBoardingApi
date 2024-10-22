CREATE TABLE [dbo].[Zmst_IdentityType] (
    [identityTypeId] VARCHAR (2)   NOT NULL,
    [description]    VARCHAR (200) NOT NULL,
    [alternateNames] VARCHAR (500) NULL,
    CONSTRAINT [PK_Zmst_IdentityType] PRIMARY KEY CLUSTERED ([identityTypeId] ASC)
);

