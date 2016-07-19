
CREATE PROCEDURE dbo.Mem_GetPhotosForBatchProcssing

AS
BEGIN

UPDATE br SET Processed = 1
OUTPUT deleted.*
FROM [dbo].[UserAlbumInstanceDetails]  br WITH (ROWLOCK, READPAST, UPDLOCK)
WHERE br.[Key] in (select top 100 [Key] from [dbo].[UserAlbumInstanceDetails] with(readpast, updlock) WHERE Processed = 0 and Active = 1 order by Created)


--update [dbo].[UserAlbumInstanceDetails] Set Processed = 0

END