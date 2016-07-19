
CREATE PROCEDURE [dbo].[Mem_ViewFaceMemberImages]
(@MemberKey int, @AlbumKey int)

AS
BEGIN


Select ua.[PhotoFile]
      ,ua.[OriginalFile]
      ,ua.[FolderPath] as [AiFolderPath]
      ,ua.[AbsolutePath] as [AiAbsolutePath]
      ,ua.[ThumbnailFile]
      ,ua.[SmallPhotoFile]
      ,ua.[MediumPhotoFile]
      ,ua.[LargePhotoFile]
	  ,uad.* from 
[dbo].[UserAlbumInstanceDetails] uad INNER JOIN [dbo].[UserAlbumInstances] ua ON ua.[Key] = uad.UserAlbumInstanceKey
where 
uad.MemberKey = @MemberKey
and uad.Active = 1
and uad.FaceFound = 1
and ua.IsActive = 1
and ua.AlbumKey = COALESCE(@AlbumKey, AlbumKey)


END