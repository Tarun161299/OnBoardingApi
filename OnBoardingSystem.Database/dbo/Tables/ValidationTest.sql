CREATE TABLE [dbo].[ValidationTest] (
    [Id]     INT          NOT NULL,
    [Name]   VARCHAR (50) NULL,
    [Email]  VARCHAR (50) NULL,
    [Mobile] VARCHAR (50) NULL,
    [Status] BIT          NULL,
    [URL]    VARCHAR (50) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'url', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ValidationTest', @level2type = N'COLUMN', @level2name = N'URL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'number;maxlength;minlength', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ValidationTest', @level2type = N'COLUMN', @level2name = N'Mobile';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'email;maxlength;minlength', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ValidationTest', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'maxlength,minlength', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ValidationTest', @level2type = N'COLUMN', @level2name = N'Name';

