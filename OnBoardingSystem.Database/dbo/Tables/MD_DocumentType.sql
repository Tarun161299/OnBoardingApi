CREATE TABLE [dbo].[MD_DocumentType] (
    [id]                     VARCHAR (2)   NOT NULL,
    [title]                  VARCHAR (300) NOT NULL,
    [format]                 VARCHAR (50)  NULL,
    [minSize]                VARCHAR (5)   NULL,
    [maxSize]                VARCHAR (5)   NULL,
    [displayPriority]        INT           NULL,
    [documentNatureType]     CHAR (1)      NULL,
    [documentNatureTypeDesc] VARCHAR (50)  NULL,
    [isPasswordProtected]    CHAR (1)      NULL,
    [created_date]           VARCHAR (50)  NULL,
    [created_by]             VARCHAR (15)  NULL,
    [modified_date]          VARCHAR (50)  NULL,
    [modified_by]            VARCHAR (15)  NULL,
    CONSTRAINT [PK_MD_DocumentType] PRIMARY KEY CLUSTERED ([id] ASC)
);



