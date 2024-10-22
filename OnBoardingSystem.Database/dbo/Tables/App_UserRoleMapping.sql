CREATE TABLE [dbo].[App_UserRoleMapping] (
    [UserID]     VARCHAR (50) NULL,
    [RoleID]     VARCHAR (50) NULL,
    [IsReadOnly] CHAR (2)     CONSTRAINT [DF_App_UserRoleMapping_IsReadOnly] DEFAULT ('Y') NULL,
    [IsActive]   CHAR (2)     CONSTRAINT [DF_App_UserRoleMapping_IsActive] DEFAULT ('Y') NULL
);

