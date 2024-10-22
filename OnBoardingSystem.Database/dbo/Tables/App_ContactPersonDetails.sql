CREATE TABLE [dbo].[App_ContactPersonDetails] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [RequestNo]    VARCHAR (50)  NULL,
    [DepartmentId] VARCHAR (50)  NULL,
    [RoleId]       VARCHAR (50)  NULL,
    [Name]         VARCHAR (50)  NULL,
    [Designation]  VARCHAR (50)  NULL,
    [MobileNo]     VARCHAR (300) NULL,
    [EmailId]      VARCHAR (300) NULL,
    CONSTRAINT [PK_App_ContactPersonDetails] PRIMARY KEY CLUSTERED ([Id] ASC)
);



