CREATE TABLE [dbo].[ZMst_SubCategory] (
    [subCategoryId]   VARCHAR (5)   NOT NULL,
    [subCategoryName] VARCHAR (500) NOT NULL,
    [alternateNames]  VARCHAR (500) NULL,
    CONSTRAINT [PK_ZMst_SubCategory] PRIMARY KEY CLUSTERED ([subCategoryId] ASC)
);

