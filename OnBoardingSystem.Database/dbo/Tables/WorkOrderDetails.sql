CREATE TABLE [dbo].[WorkOrderDetails] (
    [workorderId]      INT           IDENTITY (1, 1) NOT NULL,
    [workorderNo]      VARCHAR (50)  NOT NULL,
    [projectCode]      VARCHAR (50)  NOT NULL,
    [issueDate]        DATETIME      NULL,
    [agencyName]       VARCHAR (150) NULL,
    [resourceCategory] VARCHAR (100) NULL,
    [resourceNo]       VARCHAR (2)   NULL,
    [noofMonths]       VARCHAR (3)   NULL,
    [workorderFrom]    DATETIME      NULL,
    [workorderTo]      DATETIME      NULL,
    [docName]          VARCHAR (100) NULL,
    CONSTRAINT [PK_WorkOrderDetails] PRIMARY KEY CLUSTERED ([workorderId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorkOrderDetails', @level2type = N'COLUMN', @level2name = N'docName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorkOrderDetails', @level2type = N'COLUMN', @level2name = N'workorderTo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorkOrderDetails', @level2type = N'COLUMN', @level2name = N'workorderFrom';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorkOrderDetails', @level2type = N'COLUMN', @level2name = N'noofMonths';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorkOrderDetails', @level2type = N'COLUMN', @level2name = N'resourceNo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'alphanumeric,required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorkOrderDetails', @level2type = N'COLUMN', @level2name = N'resourceCategory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorkOrderDetails', @level2type = N'COLUMN', @level2name = N'agencyName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorkOrderDetails', @level2type = N'COLUMN', @level2name = N'issueDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorkOrderDetails', @level2type = N'COLUMN', @level2name = N'projectCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'alphanumeric,required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorkOrderDetails', @level2type = N'COLUMN', @level2name = N'workorderNo';

