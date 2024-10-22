CREATE TABLE [dbo].[Zmst_ServiceType] (
    [serviceTypeId]   CHAR (1)     NOT NULL,
    [serviceTypeName] VARCHAR (50) NULL,
    CONSTRAINT [PK_Zmst_ServiceType] PRIMARY KEY CLUSTERED ([serviceTypeId] ASC)
);

