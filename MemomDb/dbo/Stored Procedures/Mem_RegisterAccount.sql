
CREATE PROCEDURE [dbo].[Mem_RegisterAccount]
(@FirstName NVarchar(254), @LastName NVarchar(254), @DisplayName NVarchar(50), @Email NVarchar(255), @Password NVarchar(254), @REMOTE_ADDR NVarchar(50))

AS

   --trim the strngs to required length---------------------------------------------------
   SET @FirstName = LTRIM(RTRIM(@FirstName))
   SET @LastName = LTRIM(RTRIM(@LastName))
   SET @DisplayName = LTRIM(RTRIM(@DisplayName))
   SET @Email = LTRIM(RTRIM(@Email))

   IF LEN(@FirstName) > 254		BEGIN SET @FirstName = SUBSTRING(@FirstName,1,254) END
   IF LEN(@LastName) > 254		BEGIN SET @LastName = SUBSTRING(@LastName,1,254) END
   IF LEN(@DisplayName) > 50	BEGIN SET @DisplayName = SUBSTRING(@DisplayName,1,50) END
   IF LEN(@Email) > 254			BEGIN SET @Email = SUBSTRING(@Email,1,254) END
   -------------------------------------------------------------------------------------------

   ----check for existing email id in username/emai to be unique
   IF (select Count(*) from [dbo].[UserAccounts] where Username = @Email) > 0 
	   BEGIN
			SELECT 0 AS IsRegistered, 'This Email is already present, Please use a different Email address' As Remarks, '' As Email, '' As UserId
	   END
   ELSE
	  ----check email is valid
	   BEGiN
			DECLARE @Res int = 0;
			select @Res = 1
					where @Email not like '%[^a-z,0-9,@,.,+]%'
					and @Email like '%_@_%_.__%'
		    --PRINT @Res
			IF @Res < 1 
				BEGIN
					SELECT 0 AS IsRegistered, 'This Email is invalid, Please use a valid Email address' As Remarks, '' As Email, '' As UserId
				END
			ELSE
			    ----after all validations are ok, proceed
				BEGIN
					--- main inserton logic begins and other stuff goes here ---------------------------------
					DECLARE @UserKey int, @UserId uniqueidentifier

					Insert into [dbo].[UserAccounts] 
					([ID]
					  ,[Tenant]
					  ,[Username]
					  ,[Created]
					  ,[LastUpdated]
					  ,[FirstName]
					  ,[LastName]
					  ,[DisplayName]
					  ,[IsAccountClosed]
					  ,[IsLoginAllowed]
					  ,[RequiresPasswordReset]
					  ,[FailedPasswordResetCount]
					  ,[Email]
					  ,[IsAccountVerified]
					  ,[HashedPasswordBinary]
					  ,[FailedLoginCount]
					  ,[AccountTwoFactorAuthMode]
					  ,[CurrentTwoFactorAuthStatus]
					)
					Select
					NEWID()
					,'Default-' + @REMOTE_ADDR
					,@Email
					,GETDATE()
					,GETDATE()
					,@FirstName
					,@LastName
					,@DisplayName
					,0
					,1
					,0
					,0
					,@Email
					,1
					,PWDENCRYPT(@Password)
					,0
					,0
					,0

					Select @UserKey = [Key], @UserId = [ID] from [dbo].[UserAccounts] u where u.Username = @Email


					SELECT 1 AS IsRegistered, 'Registration Successful' As Remarks, @Email As Email, Convert(nvarchar(36), @UserId) As UserId

					------------------------------------------------------------------------------------------
				END
	   END

