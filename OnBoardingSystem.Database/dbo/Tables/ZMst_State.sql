CREATE TABLE [dbo].[ZMst_State] (
    [StateId]        CHAR (2)      NOT NULL,
    [StateName]      VARCHAR (200) NULL,
    [alternateNames] VARCHAR (500) NULL,
    CONSTRAINT [PK_ZMst_State] PRIMARY KEY CLUSTERED ([StateId] ASC)
);

