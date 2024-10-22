CREATE TABLE [dbo].[MD_District] (
    [stateId]       VARCHAR (2)   NOT NULL,
    [id]            VARCHAR (2)   NOT NULL,
    [description]   VARCHAR (100) NOT NULL,
    [created_date]  VARCHAR (50)  NULL,
    [created_by]    VARCHAR (15)  NULL,
    [modified_date] VARCHAR (50)  NULL,
    [modified_by]   VARCHAR (15)  NULL,
    CONSTRAINT [PK_MD_District] PRIMARY KEY CLUSTERED ([stateId] ASC, [id] ASC),
    CONSTRAINT [FK_MD_District_MD_State] FOREIGN KEY ([stateId]) REFERENCES [dbo].[MD_State] ([id])
);



