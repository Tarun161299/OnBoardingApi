CREATE TABLE [dbo].[MD_WorkOrderAgency] (
    [id]         INT           IDENTITY (1, 1) NOT NULL,
    [agencyName] VARCHAR (100) NULL,
    CONSTRAINT [PK_MD_WorkOrderAgency] PRIMARY KEY CLUSTERED ([id] ASC)
);

