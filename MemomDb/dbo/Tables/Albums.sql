CREATE TABLE [dbo].[Albums] (
    [Key]          INT            IDENTITY (1, 1) NOT NULL,
    [UserKey]      INT            NOT NULL,
    [Name]         NVARCHAR (50)  NOT NULL,
    [Description]  NVARCHAR (255) NULL,
    [IsAttached]   BIT            CONSTRAINT [DF_Albums_IsPlayable] DEFAULT ((1)) NOT NULL,
    [DisplayOrder] INT            NULL,
    [SetupEmail]   NVARCHAR (255) NULL,
    [Remarks]      NVARCHAR (MAX) NULL,
    [Created]      DATETIME       CONSTRAINT [DF_Albums_Created] DEFAULT (getdate()) NULL,
    [LastUpdated]  DATETIME       NULL,
    CONSTRAINT [PK_Albums] PRIMARY KEY CLUSTERED ([Key] ASC),
    CONSTRAINT [FK_Albums_UserAccounts] FOREIGN KEY ([UserKey]) REFERENCES [dbo].[UserAccounts] ([Key])
);



GO


GO


GO
GRANT VIEW DEFINITION
    ON OBJECT::[dbo].[Albums] TO [meme]
    AS [dbo];


GO
GRANT VIEW CHANGE TRACKING
    ON OBJECT::[dbo].[Albums] TO [meme]
    AS [dbo];


GO
GRANT UPDATE
    ON OBJECT::[dbo].[Albums] TO [meme]
    AS [dbo];


GO
GRANT TAKE OWNERSHIP
    ON OBJECT::[dbo].[Albums] TO [meme]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[Albums] TO [meme]
    AS [dbo];


GO
GRANT REFERENCES
    ON OBJECT::[dbo].[Albums] TO [meme]
    AS [dbo];


GO
GRANT INSERT
    ON OBJECT::[dbo].[Albums] TO [meme]
    AS [dbo];


GO
GRANT DELETE
    ON OBJECT::[dbo].[Albums] TO [meme]
    AS [dbo];


GO
GRANT CONTROL
    ON OBJECT::[dbo].[Albums] TO [meme]
    AS [dbo];


GO
GRANT ALTER
    ON OBJECT::[dbo].[Albums] TO [meme]
    AS [dbo];

