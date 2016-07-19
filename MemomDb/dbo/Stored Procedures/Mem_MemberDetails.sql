CREATE PROCEDURE [dbo].[Mem_MemberDetails]
(@Email nvarchar(255), @MemberKey int)

AS

DECLARE @Tagged int, @UnTagged int 

SET @Tagged = 0
SET @UnTagged = 0

Select 
m.*
,(Select count(*) from [dbo].[UserAlbumInstanceDetails] ai where ai.MemberKey = m.[Key]) AS TotalFaceTags
,(Select count(distinct a.AlbumKey) from [dbo].[UserAlbumInstanceDetails] ai INNER JOIN [dbo].[UserAlbumInstances] a ON a.[Key] = ai.UserAlbumInstanceKey where ai.MemberKey = m.[Key]) AS AlbumTags
,DATEDIFF(Year, m.[DateOfBirth], GETDATE()) AS Age
FROM [dbo].[Members] m INNER JOIN [dbo].[UserAccounts] u ON u.[Key] = m.[UserKey]
where u.Email = @Email and m.[Key] = @MemberKey


