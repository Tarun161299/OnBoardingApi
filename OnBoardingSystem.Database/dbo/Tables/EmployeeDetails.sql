CREATE TABLE [dbo].[EmployeeDetails] (
    [empId]            INT           IDENTITY (100001, 1) NOT NULL,
    [empCode]          AS            (CONVERT([varchar],datepart(year,getdate()))+right('100000'+CONVERT([varchar](7),[empId]),(7))),
    [empName]          VARCHAR (50)  NULL,
    [designation]      VARCHAR (50)  NULL,
    [fName]            VARCHAR (50)  NULL,
    [mName]            VARCHAR (50)  NULL,
    [dob]              VARCHAR (10)  NULL,
    [address]          VARCHAR (150) NULL,
    [phoneNumber]      VARCHAR (15)  NULL,
    [mobileNumber]     VARCHAR (10)  NULL,
    [emailId]          VARCHAR (50)  NULL,
    [id]               VARCHAR (2)   NULL,
    [idNumber]         VARCHAR (50)  NULL,
    [joinDate]         VARCHAR (10)  NULL,
    [reportingOfficer] VARCHAR (50)  NULL,
    [remarks]          VARCHAR (150) NULL,
    [empStatus]        VARCHAR (2)   NULL,
    [resignDate]       VARCHAR (10)  NULL,
    [division]         VARCHAR (1)   NULL,
    [submitTime]       DATETIME      NULL,
    [ipaddress]        VARCHAR (32)  NULL,
    CONSTRAINT [PK_EmpDetails] PRIMARY KEY CLUSTERED ([empId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EmployeeDetails', @level2type = N'COLUMN', @level2name = N'division';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EmployeeDetails', @level2type = N'COLUMN', @level2name = N'resignDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EmployeeDetails', @level2type = N'COLUMN', @level2name = N'empStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required,alphabet', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EmployeeDetails', @level2type = N'COLUMN', @level2name = N'reportingOfficer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EmployeeDetails', @level2type = N'COLUMN', @level2name = N'joinDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EmployeeDetails', @level2type = N'COLUMN', @level2name = N'idNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EmployeeDetails', @level2type = N'COLUMN', @level2name = N'id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required,email', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EmployeeDetails', @level2type = N'COLUMN', @level2name = N'emailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required,number,maxlength,minlength', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EmployeeDetails', @level2type = N'COLUMN', @level2name = N'mobileNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'number', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EmployeeDetails', @level2type = N'COLUMN', @level2name = N'phoneNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required,alphanumeric', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EmployeeDetails', @level2type = N'COLUMN', @level2name = N'address';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EmployeeDetails', @level2type = N'COLUMN', @level2name = N'dob';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required,alphanumeric', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EmployeeDetails', @level2type = N'COLUMN', @level2name = N'mName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required,alphanumeric', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EmployeeDetails', @level2type = N'COLUMN', @level2name = N'fName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EmployeeDetails', @level2type = N'COLUMN', @level2name = N'designation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'required,alphanumeric', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EmployeeDetails', @level2type = N'COLUMN', @level2name = N'empName';

