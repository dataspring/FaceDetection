CREATE TABLE [dbo].[UserPasswordChanges] (
    [Key]                     INT             IDENTITY (1, 1) NOT NULL,
    [UserKey]                 INT             NULL,
    [Username]                NVARCHAR (255)  NOT NULL,
    [OldHashedPasswordBinary] VARBINARY (200) NULL,
    [AttemptDate]             DATETIME        CONSTRAINT [DF_UserPasswordChanges_AttemptDate] DEFAULT (getdate()) NOT NULL,
    [Remarks]                 NVARCHAR (255)  NULL,
    [IPAddress]               NVARCHAR (50)   CONSTRAINT [DF_UserPasswordChanges_IPAddress] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_UserPasswordChanges] PRIMARY KEY CLUSTERED ([Key] ASC)
);

