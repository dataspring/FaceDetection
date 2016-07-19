CREATE TABLE [dbo].[UserGroups]
(
	[Key] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[GroupKey] [int] NOT NULL,
	[UserAccountKey] [int] NOT NULL,
	[Created] [datetime] NOT NULL,
	[LastUpdated] [datetime] NOT NULL, 
    CONSTRAINT [FK_UserGroups_UserAccounts] FOREIGN KEY ([UserAccountKey]) REFERENCES [UserAccounts]([Key])
)
