

CREATE TABLE [dbo].[Members] (
    [Key]                   INT            IDENTITY (1, 1) NOT NULL,
    [UserKey]               INT            NOT NULL,
    [Name]                  NVARCHAR (255) NULL,
    [DisplayName]           NVARCHAR (25)  NULL,
    [DateOfBirth]           DATETIME       NOT NULL,
    [Sex]                   NVARCHAR (15)  NULL,
    [Relation]              NVARCHAR (50)  NULL,
    [FaceImage]             NVARCHAR (255) NULL,
    [IsFaceDetected]        BIT            CONSTRAINT [DF_Members_IsFaceDetected] DEFAULT ((0)) NULL,
    [IsFaceTagged]          BIT            CONSTRAINT [DF_Members_IsFaceTagged] DEFAULT ((0)) NOT NULL,
    [IsActive]              BIT            CONSTRAINT [DF_Members_IsActive] DEFAULT ((1)) NULL,
    [DetectedFaceCount]     INT            CONSTRAINT [DF_Members_DetectedFaceCount] DEFAULT ((0)) NOT NULL,
    [DetectedFaces]         NVARCHAR (255) NULL,
    [DetectedFaceImage]     NVARCHAR (255) NULL,
    [UnDetectedFaceImage]   NVARCHAR (255) NULL,
    [FolderPath]            NVARCHAR (255) NULL,
    [AbsoultePath]          NVARCHAR (255) NULL,
    [AllDetectedFaceImages] NVARCHAR (MAX) NULL,
    [OriginalFaceFileName]  NVARCHAR (255) NULL,
    [FaceDetectedDate]      DATETIME       NULL,
    [FaceDetectionRemarks]  NVARCHAR (255) NULL,
    [Created]               DATETIME       NOT NULL,
    [LastUpdated]           DATETIME       NULL,
    CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED ([Key] ASC),
    CONSTRAINT [FK_Members_UserAccounts] FOREIGN KEY ([UserKey]) REFERENCES [dbo].[UserAccounts] ([Key])
);





GO


GO


GO


