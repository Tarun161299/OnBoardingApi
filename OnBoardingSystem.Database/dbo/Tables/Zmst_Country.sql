CREATE TABLE [dbo].[Zmst_Country] (
    [code]      VARCHAR (2)   NOT NULL,
    [name]      VARCHAR (100) NOT NULL,
    [sAARCCode] VARCHAR (2)   NOT NULL,
    [sAARCName] VARCHAR (100) NOT NULL,
    [isdcode]   VARCHAR (50)  NOT NULL,
    [priority]  INT           NOT NULL,
    CONSTRAINT [PK_Zmst_Country] PRIMARY KEY CLUSTERED ([code] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'number', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Zmst_Country', @level2type = N'COLUMN', @level2name = N'priority';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'number;maxlength', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Zmst_Country', @level2type = N'COLUMN', @level2name = N'isdcode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'alphabet;maxlength', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Zmst_Country', @level2type = N'COLUMN', @level2name = N'sAARCName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'alphabet;maxlength', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Zmst_Country', @level2type = N'COLUMN', @level2name = N'sAARCCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'alphabet;maxlength', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Zmst_Country', @level2type = N'COLUMN', @level2name = N'name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'alphabet;maxlength', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Zmst_Country', @level2type = N'COLUMN', @level2name = N'code';

