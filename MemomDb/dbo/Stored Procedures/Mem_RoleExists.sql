CREATE PROCEDURE [dbo].[Mem_RoleExists]
(@Roles NVarchar(254))

AS

   --trim the strngs to required length---------------------------------------------------
   SET @Roles = LTRIM(RTRIM(@Roles))
  

   -------------------------------------------------------------------------------------------

   ----check for existing email id in [dbo].[Groups], [dbo].[UserGroups], [dbo].[UserAccounts]
   IF (select Count(*) from 
			[dbo].[Groups] gp
			where gp.Name = @Roles
			) >= 1 
	   BEGIN
			SELECT 1 AS IsPresent, 'This Role is present' As Remarks
	   END
   ELSE
	  ----check email is valid
	   BEGiN
			SELECT 0 AS IsPresent, 'No Such Role' As Remarks
	   END
GO
