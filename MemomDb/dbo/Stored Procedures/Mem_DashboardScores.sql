CREATE PROCEDURE [dbo].[Mem_DashboardScores]
(@Email nvarchar(255))

AS

DECLARE @Tagged int, @UnTagged int, @UserKey int, @RndBoy char(1), @RndGirl char(1), @RndMan char(1), @RndWoman char(1)

SET @Tagged = 0
SET @UnTagged = 0

Select @UserKey = [Key] from [dbo].[UserAccounts] u where u.Email = @Email

SELECT @Tagged = Count(*) FROM [dbo].[Members] m INNER JOIN [dbo].[UserAccounts] u ON u.[Key] = m.[UserKey]
where u.Email = @Email and m.[IsFaceDetected] = 1

SELECT @UnTagged = Count(*) FROM [dbo].[Members] m INNER JOIN [dbo].[UserAccounts] u ON u.[Key] = m.[UserKey]
where u.Email = @Email and m.[IsFaceDetected] = 0

SET @Tagged = ISNULL(@Tagged,0)
SET @UnTagged = ISNULL(@UnTagged,0)

SELECT @RndBoy = CONVERT(CHAR(1), FLOOR(RAND()*(4-1)+1)), @RndGirl = CONVERT(CHAR(1), FLOOR(RAND()*(4-1)+1)), @RndMan = CONVERT(CHAR(1), FLOOR(RAND()*(4-1)+1)), @RndWoman = CONVERT(CHAR(1), FLOOR(RAND()*(4-1)+1))


Select * from 
(
Select 
ROW_NUMBER() OVER (ORDER BY m.[Key]) AS FaceKey
,m.[Key] as MemberKey
,m.UserKey
,m.[Name]
,m.[DisplayName]
,m.[DateOfBirth]
,m.[Sex]
,m.[Relation]
,Isnull(m.FolderPath,'') + Isnull(m.[FaceImage], 'Unknown.png') AS FaceImage
,m.[IsFaceDetected]
,m.[IsFaceTagged]
,m.[DetectedFaceCount]
,Isnull(m.FolderPath,'') + m.[DetectedFaceImage] AS DetectedFaceImage
,(Select count(*) from [dbo].[UserAlbumInstanceDetails] ai where ai.MemberKey = m.[Key] and ai.FaceImage = m.FaceImage and ai.FaceFound = 1) AS TotalFaceTags
FROM [dbo].[Members] m INNER JOIN [dbo].[UserAccounts] u ON u.[Key] = m.[UserKey]
where u.Email = @Email and m.[IsFaceDetected] = 1 and m.IsActive = 1

UNION 

Select 
@Tagged + ROW_NUMBER() OVER (ORDER BY m.[Key]) AS FaceKey
,m.[Key] as MemberKey
,m.UserKey
,m.[Name]
,m.[DisplayName]
,m.[DateOfBirth]
,m.[Sex]
,m.[Relation]
,Isnull(m.FolderPath,'') + Isnull(m.[FaceImage], 'Unknown.png') AS FaceImage
,m.[IsFaceDetected]
,m.[IsFaceTagged]
,m.[DetectedFaceCount]
,Isnull(m.FolderPath,'') + m.[DetectedFaceImage] AS DetectedFaceImage
,(Select count(*) from [dbo].[UserAlbumInstanceDetails] ai where ai.MemberKey = m.[Key]  and ai.FaceImage = m.FaceImage and ai.FaceFound = 1) AS TotalFaceTags
FROM [dbo].[Members] m INNER JOIN [dbo].[UserAccounts] u ON u.[Key] = m.[UserKey]
where u.Email = @Email and m.[IsFaceDetected] = 0 and m.IsActive = 1

Union

Select @Tagged +  @UnTagged +1 AS FaceKey, 0, @UserKey, '', 'Add Member', null, '', '', '/UserContent/Member/GirlFace' + @RndGirl + '.png', 0, 0, 0, null, -1
Union
Select @Tagged +  @UnTagged +2 AS FaceKey, 0, @UserKey,'', 'Add Member', null, '', '', '/UserContent/Member/BoyFace' + @RndBoy + '.png', 0, 0, 0, null, -1
Union
Select @Tagged +  @UnTagged +3 AS FaceKey, 0, @UserKey,'', 'Add Member', null, '', '', '/UserContent/Member/WomanFace' + @RndWoman + '.png', 0, 0, 0, null, -1
Union
Select @Tagged +  @UnTagged +4 AS FaceKey, 0, @UserKey,'', 'Add Member', null, '', '', '/UserContent/Member/ManFace' + @RndMan + '.png', 0, 0, 0, null, -1
) CombiTable
ORDER BY FaceKey 

