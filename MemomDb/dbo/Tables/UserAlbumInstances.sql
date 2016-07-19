CREATE TABLE [dbo].[UserAlbumInstances] (
    [Key]              INT              IDENTITY (1, 1) NOT NULL,
    [AlbumKey]         INT              NOT NULL,
    [PhotoId]          UNIQUEIDENTIFIER CONSTRAINT [DF_UserAlbumInstances_PlayLogFileKey] DEFAULT (newid()) NOT NULL,
    [PhotoFile]        NVARCHAR (225)   NOT NULL,
    [OriginalFile]     NVARCHAR (255)   NOT NULL,
    [FolderPath]       NVARCHAR (255)   NOT NULL,
    [AbsolutePath]     NVARCHAR (255)   NOT NULL,
    [IsActive]         BIT              CONSTRAINT [DF_UserAlbumInstances_IsActive] DEFAULT ((1)) NOT NULL,
    [ThumbnailFile]    NVARCHAR (255)   NULL,
    [SmallPhotoFile]   NVARCHAR (255)   NULL,
    [MediumPhotoFile]  NVARCHAR (255)   NULL,
    [LargePhotoFile]   NVARCHAR (255)   NULL,
    [AnyFacesTagged]   BIT              NOT NULL,
    [PhotosSized]      BIT              CONSTRAINT [DF_UserAlbumInstances_PhotosSized] DEFAULT ((0)) NOT NULL,
    [FacesDetected]    INT              CONSTRAINT [DF_UserAlbumInstances_FacesDetected] DEFAULT ((0)) NOT NULL,
    [FileUploadStatus] NVARCHAR (50)    NOT NULL,
    [ShootDate]        DATETIME         NULL,
    [Resolution]       NVARCHAR (225)   NULL,
    [ImageType]        NVARCHAR (225)   NULL,
    [IpAddress]        NVARCHAR (50)    NULL,
    [GpsCoordinates]   NVARCHAR (225)   NULL,
    [Remarks]          NVARCHAR (MAX)   NULL,
    [Created]          DATETIME         CONSTRAINT [DF_UserAlbumScores_Created] DEFAULT (getdate()) NOT NULL,
    [LastUpdated]      DATETIME         NULL,
    CONSTRAINT [PK_UserAlbumScores] PRIMARY KEY CLUSTERED ([Key] ASC)
);





