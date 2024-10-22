CREATE TABLE [dbo].[App_LoginDetails] (
    [RequestNo]                  VARCHAR (32)   NOT NULL,
    [userId]                     VARCHAR (15)   NOT NULL,
    [userName]                   VARCHAR (50)   NULL,
    [isActive]                   CHAR (1)       NULL,
    [isPasswordChanged]          CHAR (1)       NULL,
    [lastLoginTime]              DATETIME       NULL,
    [lastLoginIP]                VARCHAR (20)   NULL,
    [password]                   VARCHAR (64)   NULL,
    [passwordHistory1]           VARCHAR (64)   NULL,
    [passwordHistory2]           VARCHAR (64)   NULL,
    [passwordHistory3]           VARCHAR (64)   NULL,
    [authenticationType]         CHAR (2)       NULL,
    [securityQuestionId]         CHAR (2)       NULL,
    [securityAnswer]             VARCHAR (64)   NULL,
    [lastFailedLoginAttemptTime] DATETIME       NULL,
    [lastFailedLoginAttemptIP]   VARCHAR (50)   NULL,
    [failedLoginAttemptCount]    INT            NULL,
    [lastSuccessfulLoginTime]    DATETIME       NULL,
    [lastSuccessfulLoginIP]      VARCHAR (50)   NULL,
    [lastPasswordChangeTime]     DATETIME       NULL,
    [lastPasswordChangeIP]       VARCHAR (50)   NULL,
    [lastPasswordResetTime]      DATETIME       NULL,
    [lastPasswordResetIP]        VARCHAR (50)   NULL,
    [userToken]                  VARCHAR (500)  NULL,
    [accessToken]                VARCHAR (1000) NULL,
    CONSTRAINT [PK_App_LoginDetails] PRIMARY KEY CLUSTERED ([userId] ASC)
);





