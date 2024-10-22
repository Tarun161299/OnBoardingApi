CREATE TABLE [dbo].[MD_State] (
    [id]            VARCHAR (2)   NOT NULL,
    [description]   VARCHAR (100) NOT NULL,
    [created_date]  VARCHAR (50)  NULL,
    [created_by]    VARCHAR (15)  NULL,
    [modified_date] VARCHAR (50)  NULL,
    [modified_by]   VARCHAR (15)  NULL,
    CONSTRAINT [PK_MD_State] PRIMARY KEY CLUSTERED ([id] ASC)
);

