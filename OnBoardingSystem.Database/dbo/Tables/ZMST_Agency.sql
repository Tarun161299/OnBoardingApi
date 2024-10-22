CREATE TABLE [dbo].[ZMST_Agency] (
    [AgencyId]           VARCHAR (15)    NOT NULL,
    [AgencyName]         VARCHAR (200)   NOT NULL,
    [AgencyAbbr]         VARCHAR (500)   NULL,
    [AgencyType]         VARCHAR (20)    NULL,
    [StateId]            VARCHAR (2)     NULL,
    [ServiceTypeId]      VARCHAR (10)    NULL,
    [address]            VARCHAR (200)   NULL,
    [isActive]           CHAR (1)        NULL,
    [priority]           INT             NULL,
    [boardRequestLetter] VARBINARY (MAX) NULL,
    CONSTRAINT [PK_ZMST_Board] PRIMARY KEY CLUSTERED ([AgencyId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ZMST_Agency', @level2type = N'COLUMN', @level2name = N'boardRequestLetter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Only Number,Required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ZMST_Agency', @level2type = N'COLUMN', @level2name = N'priority';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ZMST_Agency', @level2type = N'COLUMN', @level2name = N'isActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ZMST_Agency', @level2type = N'COLUMN', @level2name = N'StateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ZMST_Agency', @level2type = N'COLUMN', @level2name = N'AgencyType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Alphanumeric', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ZMST_Agency', @level2type = N'COLUMN', @level2name = N'AgencyName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ZMST_Agency', @level2type = N'COLUMN', @level2name = N'AgencyId';

