CREATE TABLE [dbo].[Zmst_SubCategoryPriority] (
    [subCategoryPriorityId] VARCHAR (3)   NOT NULL,
    [description]           VARCHAR (500) NOT NULL,
    [subCategoryId]         VARCHAR (5)   NOT NULL,
    [alternateNames]        VARCHAR (500) NULL,
    CONSTRAINT [PK_Zmst_SubCategoryPriority] PRIMARY KEY CLUSTERED ([subCategoryPriorityId] ASC, [subCategoryId] ASC)
);

