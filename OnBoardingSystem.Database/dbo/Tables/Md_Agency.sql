CREATE TABLE [dbo].[Md_Agency] (
    [AgencyId]      INT           NOT NULL,
    [AgencyName]    VARCHAR (200) NOT NULL,
    [Abbreviation]  VARCHAR (500) NULL,
    [AgencyType]    VARCHAR (20)  NULL,
    [StateId]       VARCHAR (2)   NULL,
    [ServiceTypeId] VARCHAR (10)  NULL,
    [address]       VARCHAR (200) NULL,
    [isActive]      CHAR (1)      NULL,
    [priority]      INT           NULL,
    CONSTRAINT [PK_Md_Agency] PRIMARY KEY CLUSTERED ([AgencyId] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required;pattern;maxlength;minlength', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Md_Agency', @level2type = N'COLUMN', @level2name = N'AgencyName';



