CREATE TABLE [dbo].[App_ProjectCost] (
    [ProjectId]            INT             NOT NULL,
    [FinancialComponentId] INT             NOT NULL,
    [Amount]               DECIMAL (16, 2) NOT NULL,
    [CreatedBy]            VARCHAR (50)    NULL,
    [CreatedOn]            DATETIME        NULL,
    [ModifiedBy]           VARCHAR (50)    NULL,
    [ModifiedOn]           DATETIME        NULL,
    [IsActive]             BIT             CONSTRAINT [DF_App_ProjectCost_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_App_ProjectCost_1] PRIMARY KEY CLUSTERED ([ProjectId] ASC, [FinancialComponentId] ASC)
);



