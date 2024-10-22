CREATE TABLE [dbo].[Zmst_InstituteType] (
    [InstituteTypeId] VARCHAR (3)   NOT NULL,
    [InstituteType]   VARCHAR (100) NULL,
    [Priority]        INT           NOT NULL,
    CONSTRAINT [PK_Zmst_InstituteType] PRIMARY KEY CLUSTERED ([InstituteTypeId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'number,required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Zmst_InstituteType', @level2type = N'COLUMN', @level2name = N'Priority';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Zmst_InstituteType', @level2type = N'COLUMN', @level2name = N'InstituteType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Zmst_InstituteType', @level2type = N'COLUMN', @level2name = N'InstituteTypeId';

