CREATE PROCEDURE [dbo].[Mem_PhotoDeleteTagProcessing]
    @AlbumInstanceKey int
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.

	Update [dbo].[UserAlbumInstanceDetails] 
		Set Active = 0, 
			[SetActiveRemarks] = 'Photo Deleted', LastUpdated = getdate()
		from [dbo].[UserAlbumInstanceDetails] ua 
		where ua.UserAlbumInstanceKey = @AlbumInstanceKey

END