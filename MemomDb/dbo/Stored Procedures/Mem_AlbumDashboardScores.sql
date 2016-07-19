
CREATE PROCEDURE [dbo].[Mem_AlbumDashboardScores]
(@Email nvarchar(255))

AS

/*
        public string RandomDisplayPhoto { get; set; }
        public string MemberTagDetails { get; set; }
        public int TotalPhotos { get; set; }
        public int TotalTagged { get; set; } 
*/

DECLARE @Tagged int, @UnTagged int, @UserKey int

SET @Tagged = 0
SET @UnTagged = 0

Select @UserKey = [Key] from [dbo].[UserAccounts] u where u.Email = @Email



Select a.*, '' as MemberTagDetails,
(Select TOP 1 ua.FolderPath + ua.PhotoFile from [dbo].[UserAlbumInstances] ua WHERE ua.AlbumKey = a.[Key]) as RandomDisplayPhoto,
(Select Count(*) from [dbo].[UserAlbumInstances] ua WHERE ua.AlbumKey = a.[Key]) as TotalPhotos,
(Select Count(DISTINCT aid.[UserAlbumInstanceKey]) from [dbo].[UserAlbumInstances] uai 
					  INNER JOIN [dbo].[UserAlbumInstanceDetails] aid ON aid.UserAlbumInstanceKey = uai.[Key]
					  WHERE uai.AlbumKey = a.[Key] and aid.FaceFound = 1 ) as TotalTagged 
into #AlbumDash
from 
[dbo].[Albums] a 
where a.[UserKey] = @UserKey

----- stuff to compute member and related tag count and update MemberTagDetails field - 2 for loops and concatenation

Select * from #AlbumDash

