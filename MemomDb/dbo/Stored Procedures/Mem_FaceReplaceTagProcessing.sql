CREATE PROCEDURE [dbo].[Mem_FaceReplaceTagProcessing]
    @UserKey int,
    @MemberId int,
    @FaceImage varchar(255),
	@OldFaceImage varchar(255)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;


	Update [dbo].[UserAlbumInstanceDetails] Set [Active] = 0 
	from [dbo].[UserAlbumInstanceDetails] ua 
	where ua.FaceImage = @OldFaceImage

    MERGE [dbo].[UserAlbumInstanceDetails] AS T
    USING
    (
        SELECT distinct ai.[Key] as [UserAlbumInstanceKey], @MemberId as MemberKey, @FaceImage as FaceImage from 
		[dbo].[UserAccounts] ua 
		INNER JOIN [dbo].[Albums] al ON al.[UserKey] = ua.[Key]
		INNER JOIN [dbo].[UserAlbumInstances] ai ON ai.[AlbumKey] = al.[Key]
		--INNER JOIN [dbo].[UserAlbumInstanceDetails] aid ON aid.[UserAlbumInstanceKey] = ai.[Key]
		where ua.[Key] = @UserKey

    ) AS S
    ON (T.[UserAlbumInstanceKey] = S.[UserAlbumInstanceKey] and T.MemberKey = S.MemberKey and T.[FaceImage] = S.[FaceImage])

    WHEN MATCHED THEN
        UPDATE SET [Active] = 1, [Remarks] = 'Re-identified'
    WHEN NOT MATCHED THEN
        INSERT
            ([UserAlbumInstanceKey],[MemberKey],[FaceImage],[Active],[Processed],[Remarks])
        VALUES
            (S.[UserAlbumInstanceKey], S.MemberKey, S.[FaceImage], 1, 0, 'Tagged for Processing');
    
END


/*
EXEC [dbo].[Mem_FaceReplaceTagProcessing] 1, 20, '3de39d6c-a83d-4a04-b3c4-a8765fed1bb4.jpg', 'f865d574-520d-4d51-9ab6-edb16656efc3.jpg'

*/