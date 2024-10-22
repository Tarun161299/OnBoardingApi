CREATE TABLE [dbo].[Zmst_AgencyExamCouns] (
    [agencyId]    INT           NOT NULL,
    [examCounsId] VARCHAR (3)   NOT NULL,
    [description] VARCHAR (200) NULL,
    CONSTRAINT [PK_Zmst_AgencyExamCouns] PRIMARY KEY CLUSTERED ([agencyId] ASC, [examCounsId] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Zmst_AgencyExamCouns', @level2type = N'COLUMN', @level2name = N'examCounsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Zmst_AgencyExamCouns', @level2type = N'COLUMN', @level2name = N'description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Zmst_AgencyExamCouns', @level2type = N'COLUMN', @level2name = N'agencyId';

