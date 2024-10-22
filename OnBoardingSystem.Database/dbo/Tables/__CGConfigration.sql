CREATE TABLE [dbo].[__CGConfigration] (
    [TableName]               VARCHAR (100) NULL,
    [CoulmnName]              VARCHAR (100) NULL,
    [DisplayCaption]          VARCHAR (100) NULL,
    [Validation]              VARCHAR (500) NULL,
    [ControlType]             VARCHAR (100) NULL,
    [IsVisibleInGrid]         BIT           NULL,
    [IsVisibleInForm]         BIT           NULL,
    [IsReadOnlyForm]          BIT           NULL,
    [MasterDataProviderTable] VARCHAR (100) NULL
);

