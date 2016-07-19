CREATE TABLE [dbo].[UserAlbumStatusChanges](
	[Key] [int] IDENTITY(1,1) NOT NULL,
	[UserKey] [int] NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[AlbumKey] [int] NOT NULL,
	[AlbumName] [nvarchar](255) NOT NULL,
	[AttemptDate] [datetime] NOT NULL CONSTRAINT [DF_UserAlbumStatusChanges_AttemptDate]  DEFAULT (getdate()),
	[Result] [nvarchar](50) NOT NULL CONSTRAINT [DF_UserAlbumStatusChanges_Result]  DEFAULT (''),
	[IPAddress] [nvarchar](50) NOT NULL CONSTRAINT [DF_UserAlbumStatusChanges_IPAddress]  DEFAULT (''),
	[Remarks] [nvarchar](255) NOT NULL CONSTRAINT [DF_UserAlbumStatusChanges_Remarks]  DEFAULT (''),
 CONSTRAINT [PK_UserAlbumStatusChanges] PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
