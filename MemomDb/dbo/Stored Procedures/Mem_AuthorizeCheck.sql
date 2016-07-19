CREATE PROCEDURE [dbo].[Mem_AuthorizeCheck]
(@Email NVarchar(255), @Roles NVarchar(254), @REMOTE_ADDR NVarchar(50))

AS

   --trim the strngs to required length---------------------------------------------------
   SET @Roles = LTRIM(RTRIM(@Roles))
   SET @Email = LTRIM(RTRIM(@Email))

   -------------------------------------------------------------------------------------------

   ----check for existing email id in [dbo].[Groups], [dbo].[UserGroups], [dbo].[UserAccounts]
   IF (select Count(*) from 
			[dbo].[UserAccounts] ua INNER JOIN [dbo].[UserGroups] ug
				ON ua.[Key] = ug.[UserAccountKey]
			INNER JOIN [dbo].[Groups] gp
				ON gp.[Key] = ug.GroupKey
			where ua.Username = @Email
			and gp.Name = @Roles
			) >= 1 
	   BEGIN
			SELECT 1 AS IsAuthorized, 'This User under Admin Role is Authorized' As Remarks
	   END
   ELSE
	  ----check email is valid
	   BEGiN
			SELECT 0 AS IsAuthorized, 'User UnAuthorized' As Remarks
	   END





GO

