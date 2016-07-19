CREATE PROCEDURE [dbo].[Mem_IsUserInRole]
(@Email NVarchar(255), @Roles NVarchar(254))

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
			SELECT 1 AS IsPresent, 'This User under given Role is present' As Remarks
	   END
   ELSE
	  ----check email is valid
	   BEGiN
			SELECT 0 AS IsPresent, 'User Not in Role' As Remarks
	   END
GO
