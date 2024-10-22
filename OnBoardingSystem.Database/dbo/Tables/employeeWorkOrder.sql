CREATE TABLE [dbo].[employeeWorkOrder] (
    [empCode]     VARCHAR (15) NOT NULL,
    [workorderNo] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_employeeWorkOrder] PRIMARY KEY CLUSTERED ([empCode] ASC, [workorderNo] ASC)
);

