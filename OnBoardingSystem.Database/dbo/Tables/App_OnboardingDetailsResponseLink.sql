CREATE TABLE [dbo].[App_OnboardingDetailsResponseLink] (
    [RequestNo]  VARCHAR (32)   NULL,
    [Version]    INT            NULL,
    [ResponseId] NVARCHAR (128) NULL,
    [Link]       VARCHAR (512)  NULL,
    [UserId]     VARCHAR (16)   NULL,
    [SubmitTime] DATETIME       NULL,
    [IPAddress]  VARCHAR (32)   NULL,
    [Status]     VARCHAR (10)   NULL,
    [ExpiryDate] VARCHAR (50)   NULL
);



