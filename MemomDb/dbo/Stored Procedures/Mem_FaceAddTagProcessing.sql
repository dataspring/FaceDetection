CREATE PROCEDURE [dbo].[Mem_FaceAddTagProcessing]
    @UserKey int,
    @MemberId int,
    @FaceImage varchar(255)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    MERGE [dbo].[UserAlbumInstanceDetails] AS T
    USING
    (
        SELECT distinct ai.[AlbumKey] as [UserAlbumInstanceKey], @MemberId as MemberKey, @FaceImage as FaceImage from 
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