CREATE TABLE [dbo].[Zmst_InstituteStream] (
    [InstCd]   VARCHAR (6) NOT NULL,
    [StreamId] VARCHAR (2) NOT NULL,
    CONSTRAINT [PK_Zmst_InstituteStream] PRIMARY KEY CLUSTERED ([InstCd] ASC, [StreamId] ASC)
);

