CREATE TABLE [dbo].[UserAlbumInstanceDetails] (
    [Key]                  INT            IDENTITY (1, 1) NOT NULL,
    [UserAlbumInstanceKey] INT            NOT NULL,
    [MemberKey]            INT            NOT NULL,
    [FaceImage]            NVARCHAR (255) NOT NULL,
    [Active]               BIT            CONSTRAINT [DF_UserAlbumInstanceDetails_Active] DEFAULT ((1)) NOT NULL,
    [Processed]            BIT            CONSTRAINT [DF_UserAlbumInstanceDetails_Processed] DEFAULT ((0)) NOT NULL,
    [ProcessedOn]          DATETIME       NULL,
    [Width]                INT            NULL,
    [Height]               INT            NULL,
    [Xpos]                 INT            NULL,
    [Ypos]                 INT            NULL,
    [FaceFound]            BIT            CONSTRAINT [DF_UserAlbumInstanceDetails_FaceFound] DEFAULT ((0)) NOT NULL,
    [Inliers]              INT            NULL,
    [OpenCVMethod]         NVARCHAR (255) NULL,
    [FaceMatchFile]        NVARCHAR (255) NULL,
    [FolderPath]           NVARCHAR (255) NULL,
    [AbsolutePath]         NVARCHAR (255) NULL,
    [SetActiveRemarks]     NVARCHAR (255) NULL,
    [SearchTerms]          NVARCHAR (MAX) NULL,
    [Remarks]              NVARCHAR (255) NULL,
    [Created]              DATETIME       CONSTRAINT [DF_UserAlbumAchievements_Created] DEFAULT (getdate()) NOT NULL,
    [LastUpdated]          DATETIME       NULL,
    CONSTRAINT [PK_UserAlbumAchievements] PRIMARY KEY CLUSTERED ([Key] ASC),
    CONSTRAINT [FK_UserAlbumInstanceDetails_UserAlbumInstances] FOREIGN KEY ([UserAlbumInstanceKey]) REFERENCES [dbo].[UserAlbumInstances] ([Key])
);
















GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserAlbumInstanceDetails]
    ON [dbo].[UserAlbumInstanceDetails]([UserAlbumInstanceKey] ASC, [MemberKey] ASC, [FaceImage] ASC);

