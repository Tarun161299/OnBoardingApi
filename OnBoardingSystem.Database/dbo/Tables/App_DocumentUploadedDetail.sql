CREATE TABLE [dbo].[App_DocumentUploadedDetail] (
    [documentId]  INT            IDENTITY (1, 1) NOT NULL,
    [activityid]  VARCHAR (5)    NULL,
    [requestNo]   VARCHAR (15)   NOT NULL,
    [cycleId]     VARCHAR (20)   NULL,
    [docType]     VARCHAR (50)   NOT NULL,
    [docId]       VARCHAR (50)   NULL,
    [docSubject]  VARCHAR (500)  NULL,
    [docContent]  VARCHAR (MAX)  NULL,
    [objectId]    VARCHAR (50)   NULL,
    [objectUrl]   VARCHAR (1500) NULL,
    [docNatureId] CHAR (1)       NULL,
    [ipAddress]   VARCHAR (20)   NULL,
    [subTime]     DATETIME       NULL,
    [createdBy]   VARCHAR (100)  NULL,
    CONSTRAINT [PK_App_DocumentUploadedDetail] PRIMARY KEY CLUSTERED ([documentId] ASC)
);







