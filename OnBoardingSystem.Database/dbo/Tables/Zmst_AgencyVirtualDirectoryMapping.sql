CREATE TABLE [dbo].[Zmst_AgencyVirtualDirectoryMapping] (
    [AgencyId]             VARCHAR (10) NOT NULL,
    [BaseDirectory]        VARCHAR (50) NULL,
    [VirtualDirectory]     VARCHAR (50) NOT NULL,
    [VirtualDirectoryType] VARCHAR (50) NOT NULL,
    [IsActive]             BIT          NULL,
    CONSTRAINT [PK_Zmst_AgencyVirtualDirectoryMapping] PRIMARY KEY CLUSTERED ([AgencyId] ASC, [VirtualDirectoryType] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Zmst_AgencyVirtualDirectoryMapping', @level2type = N'COLUMN', @level2name = N'VirtualDirectoryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'alphanumeric,required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Zmst_AgencyVirtualDirectoryMapping', @level2type = N'COLUMN', @level2name = N'VirtualDirectory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'alphanumeric,required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Zmst_AgencyVirtualDirectoryMapping', @level2type = N'COLUMN', @level2name = N'BaseDirectory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Zmst_AgencyVirtualDirectoryMapping', @level2type = N'COLUMN', @level2name = N'AgencyId';

