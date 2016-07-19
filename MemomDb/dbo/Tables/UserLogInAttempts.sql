CREATE TABLE [dbo].[UserLogInAttempts] (
    [Key]         INT            IDENTITY (1, 1) NOT NULL,
    [UserKey]     INT            NOT NULL,
    [Email]       NVARCHAR (255) NOT NULL,
    [Password]    NVARCHAR (255) NOT NULL,
    [AttemptDate] DATETIME       CONSTRAINT [DF_UserLogInAttempts_AttemptDate] DEFAULT (getdate()) NOT NULL,
    [Result]      NVARCHAR (50)  CONSTRAINT [DF_UserLogInAttempts_Result] DEFAULT ('') NOT NULL,
    [IPAddress]   NVARCHAR (50)  CONSTRAINT [DF_UserLogInAttempts_IPAddress] DEFAULT ('') NOT NULL,
    [Remarks]     NVARCHAR (255) CONSTRAINT [DF_UserLogInAttempts_Remarks] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_UserLogInAttempts] PRIMARY KEY CLUSTERED ([Key] ASC)
);



GO


GO


GO


GO


GO