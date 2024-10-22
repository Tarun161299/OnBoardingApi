CREATE TABLE [dbo].[Zmst_Institute] (
    [InstCd]           VARCHAR (6)   NOT NULL,
    [InstNm]           VARCHAR (500) NOT NULL,
    [AbbrNm]           VARCHAR (500) NULL,
    [InstTypeId]       VARCHAR (3)   NOT NULL,
    [SeatType]         VARCHAR (2)   NULL,
    [InstAdd]          VARCHAR (300) NULL,
    [State]            CHAR (2)      NULL,
    [District]         VARCHAR (3)   NULL,
    [Pincode]          CHAR (6)      NULL,
    [InstPhone]        VARCHAR (60)  NULL,
    [InstFax]          VARCHAR (50)  NULL,
    [InstWebSite]      VARCHAR (100) NULL,
    [EmailId]          VARCHAR (50)  NULL,
    [AltEmailId]       VARCHAR (50)  NULL,
    [ContactPerson]    VARCHAR (100) NULL,
    [Designation]      VARCHAR (100) NULL,
    [MobileNo]         VARCHAR (60)  NULL,
    [AISHE]            VARCHAR (7)   NULL,
    [oldInstituteCode] VARCHAR (100) NULL,
    CONSTRAINT [PK_Zmst_Institute] PRIMARY KEY CLUSTERED ([InstCd] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'number,maxlength', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Zmst_Institute', @level2type = N'COLUMN', @level2name = N'MobileNo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'alphabet', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Zmst_Institute', @level2type = N'COLUMN', @level2name = N'Designation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'alphabet', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Zmst_Institute', @level2type = N'COLUMN', @level2name = N'ContactPerson';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'email', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Zmst_Institute', @level2type = N'COLUMN', @level2name = N'EmailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'url', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Zmst_Institute', @level2type = N'COLUMN', @level2name = N'InstWebSite';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'number', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Zmst_Institute', @level2type = N'COLUMN', @level2name = N'InstPhone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'number,maxlength', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Zmst_Institute', @level2type = N'COLUMN', @level2name = N'Pincode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Zmst_Institute', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Zmst_Institute', @level2type = N'COLUMN', @level2name = N'InstTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Zmst_Institute', @level2type = N'COLUMN', @level2name = N'InstNm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Zmst_Institute', @level2type = N'COLUMN', @level2name = N'InstCd';

