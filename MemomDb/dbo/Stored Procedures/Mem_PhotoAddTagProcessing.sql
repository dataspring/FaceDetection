CREATE PROCEDURE [dbo].[Mem_PhotoAddTagProcessing]
    @UserKey int,
    @AlbumInstanceKey int
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.

        INSERT [dbo].[UserAlbumInstanceDetails]
            ([UserAlbumInstanceKey],[MemberKey],[FaceImage],[Active],[Processed],[Remarks],[Created])
        Select
            @AlbumInstanceKey, m.[Key] , m.[FaceImage], 1, 0, 'Tagged for Processing', GETDATE() from 
			[dbo].[Members] m where m.UserKey = @UserKey and m.FaceImage IS NOT NULL
END