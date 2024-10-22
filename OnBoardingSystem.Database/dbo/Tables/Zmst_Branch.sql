CREATE TABLE [dbo].[Zmst_Branch] (
    [BrCd]   VARCHAR (6)   NOT NULL,
    [BrNm]   VARCHAR (300) NOT NULL,
    [Stream] VARCHAR (4)   NULL,
    CONSTRAINT [PK_Zmst_Branch_1] PRIMARY KEY CLUSTERED ([BrCd] ASC)
);

