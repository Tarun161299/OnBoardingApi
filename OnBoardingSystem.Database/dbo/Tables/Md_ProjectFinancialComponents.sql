CREATE TABLE [dbo].[Md_ProjectFinancialComponents] (
    [FinancialComponentId] INT           NOT NULL,
    [FinancialComponent]   VARCHAR (300) NOT NULL,
    [ParentId]             INT           NOT NULL,
    [CreatedBy]            INT           NULL,
    [CreatedOn]            DATETIME      NULL,
    [ModifiedBy]           INT           NULL,
    [ModifiedOn]           DATETIME      NULL,
    [IsActive]             BIT           CONSTRAINT [DF_Md_ProjectFinancialComponents_IsActive] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_Md_ProjectFinancialComponents] PRIMARY KEY CLUSTERED ([FinancialComponentId] ASC)
);

