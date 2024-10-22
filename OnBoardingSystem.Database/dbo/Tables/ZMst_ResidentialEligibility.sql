CREATE TABLE [dbo].[ZMst_ResidentialEligibility] (
    [residentialEligibilityId]   VARCHAR (2)   NOT NULL,
    [residentialEligibilityName] VARCHAR (200) NULL,
    CONSTRAINT [PK_ZMst_ResidentialEligibility] PRIMARY KEY CLUSTERED ([residentialEligibilityId] ASC)
);

