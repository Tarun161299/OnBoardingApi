CREATE TABLE [dbo].[Zmst_InstituteAgency] (
    [InstCd]   VARCHAR (6) NOT NULL,
    [AgencyId] VARCHAR (3) NOT NULL,
    CONSTRAINT [PK_Zmst_InstituteAgency] PRIMARY KEY CLUSTERED ([InstCd] ASC, [AgencyId] ASC)
);

