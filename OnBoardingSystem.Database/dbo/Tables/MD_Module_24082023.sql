CREATE TABLE [dbo].[MD_Module_24082023] (
    [ModuleId]   VARCHAR (10)   NOT NULL,
    [Heading]    VARCHAR (100)  NULL,
    [SubHeading] VARCHAR (100)  NULL,
    [Url]        VARCHAR (500)  NULL,
    [Path]       VARCHAR (500)  NULL,
    [Parent]     VARCHAR (10)   NULL,
    [IsActive]   CHAR (2)       NULL,
    [Remarks]    NVARCHAR (MAX) NULL
);

