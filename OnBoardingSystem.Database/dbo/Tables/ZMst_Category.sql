CREATE TABLE [dbo].[ZMst_Category] (
    [categoryId]     VARCHAR (5)   NOT NULL,
    [categoryName]   VARCHAR (100) NULL,
    [alternateNames] VARCHAR (500) NULL,
    CONSTRAINT [PK_ZMst_Category] PRIMARY KEY CLUSTERED ([categoryId] ASC)
);

