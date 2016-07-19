

CREATE PROCEDURE [dbo].[Mem_ResetPassword]
(@Email NVarchar(255), @NewPassword NVarchar(254), @MaxFailedPasswordResetCount int, @REMOTE_ADDR NVarchar(50))

AS

   --trim the strngs to required length---------------------------------------------------
   SET @NewPassword = LTRIM(RTRIM(@NewPassword))
   SET @Email = LTRIM(RTRIM(@Email))

   -------------------------------------------------------------------------------------------

   ----check for existing email id in username/emai to be unique
   IF (select Count(*) from [dbo].[UserAccounts] where Username = @Email) <= 0 
	   BEGIN
			---insert into log of the changes made-----------------------------------------
			Insert into [dbo].[UserPasswordResets] ([Username], [AttemptDate], [Remarks], [IPAddress])
			Select @Email, GETDATE(), 'This Email account is not present', @REMOTE_ADDR
			-----------------------------------------------------------------------------------------------
			SELECT 0 AS IsPasswordReset, 'This Email account is not present' As Remarks
	   END
   ELSE
	  ----check email is valid
	   BEGiN
			IF (Select ISNULL([FailedPasswordResetCount],0) from [dbo].[UserAccounts] where Username = @Email) > @MaxFailedPasswordResetCount
				BEGIN
					---insert into log of the changes made-----------------------------------------
					Insert into [dbo].[UserPasswordResets] ([Username], [AttemptDate], [Remarks], [IPAddress])
					Select @Email, GETDATE(), 'Password resets exceeded maximum number of resets', @REMOTE_ADDR
					-----------------------------------------------------------------------------------------------
					SELECT 0 AS IsPasswordReset, 'Password resets exceeded maximum number of resets' As Remarks
				END
			ELSE
			    ----after all validations are ok, proceed
				BEGIN
					--- main inserton logic begins and other stuff goes here ---------------------------------
					DECLARE @UserKey int, @UserId uniqueidentifier, @HashedPasswordBinary varbinary(200), @PasswordResetCount int
					Select @UserKey = [Key], @HashedPasswordBinary = [HashedPasswordBinary] from [dbo].[UserAccounts] where [Username] = @Email


					Update [dbo].[UserAccounts] 
					SET 
					  [LastUpdated] = GETDATE()
					  ,[PasswordChanged] = GETDATE()
					  ,[LastFailedPasswordReset] = GETDATE()
					  ,[FailedPasswordResetCount] = ISNULL([FailedPasswordResetCount],0) + 1
					  ,[HashedPasswordBinary] = PWDENCRYPT(@NewPassword)
					WHERE
					 Username = @Email

					 Select @PasswordResetCount = ISNULL([FailedPasswordResetCount],0) from [dbo].[UserAccounts] 
					 WHERE
					 Username = @Email


					---insert into log of the changes made-----------------------------------------
					Insert into [dbo].[UserPasswordResets] ([UserKey], [Username], [OldHashedPasswordBinary], [AttemptDate], [Remarks], [IPAddress])
					Select @UserKey, @Email, @HashedPasswordBinary, GETDATE(), 'Password reset successfully, Reset Count : ' + CAST(@PasswordResetCount AS nvarchar(3)), @REMOTE_ADDR
					-----------------------------------------------------------------------------------------------

					SELECT 1 AS IsPasswordReset, 'Password reset successfully, Reset Count : ' + CAST(@PasswordResetCount AS nvarchar(3)) As Remarks

					------------------------------------------------------------------------------------------
				END
	   END


