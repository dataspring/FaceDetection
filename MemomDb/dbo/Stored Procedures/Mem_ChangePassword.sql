

CREATE PROCEDURE [dbo].[Mem_ChangePassword]
(@Email NVarchar(255), @OldPassword NVarchar(254), @NewPassword NVarchar(254), @REMOTE_ADDR NVarchar(50))

AS

   --trim the strngs to required length---------------------------------------------------
   SET @OldPassword = LTRIM(RTRIM(@OldPassword))
   SET @NewPassword = LTRIM(RTRIM(@NewPassword))
   SET @Email = LTRIM(RTRIM(@Email))

   -------------------------------------------------------------------------------------------

   ----check for existing email id in username/emai to be unique
   IF (select Count(*) from [dbo].[UserAccounts] where Username = @Email) <= 0 
	   BEGIN
	   		---insert into log of the changes made--------------------
				Insert into [dbo].[UserPasswordChanges] ([Username], [AttemptDate], [Remarks], [IPAddress])
				Select @Email,  GETDATE(), 'This Email account is not present', @REMOTE_ADDR
			-------------------------------------------------------------
			SELECT 0 AS IsPasswordChanged, 'This Email account is not present' As Remarks
	   END
   ELSE
	  ----check email is valid
	   BEGiN
			IF (Select Count(*) from [dbo].[UserAccounts] where Username = @Email and PWDCOMPARE(@OldPassword, HashedPasswordBinary) > 0) <= 0
				BEGIN
					---insert into log of the changes made--------------------
						Insert into [dbo].[UserPasswordChanges] ([Username], [AttemptDate], [Remarks], [IPAddress])
						Select @Email,  GETDATE(), 'Old password does not match', @REMOTE_ADDR
					-------------------------------------------------------------
					SELECT 0 AS IsPasswordChanged, 'Old password does not match' As Remarks
				END
			ELSE
			    ----after all validations are ok, proceed
				BEGIN
					--- main inserton logic begins and other stuff goes here ---------------------------------
					DECLARE @UserKey int, @UserId uniqueidentifier, @HashedPasswordBinary varbinary(200)
					Select @UserKey = [Key], @HashedPasswordBinary = [HashedPasswordBinary] from [dbo].[UserAccounts] where [Username] = @Email


					Update [dbo].[UserAccounts] 
					SET 
					  [LastUpdated] = GETDATE()
					  ,[PasswordChanged] = GETDATE()
					  ,[HashedPasswordBinary] = PWDENCRYPT(@NewPassword)
					WHERE
					 Username = @Email


					---insert into log of the changes made
					Insert into [dbo].[UserPasswordChanges] ([UserKey], [Username], [OldHashedPasswordBinary], [AttemptDate], [Remarks], [IPAddress])
					Select @UserKey, @Email, @HashedPasswordBinary, GETDATE(), 'Password changed successfully', @REMOTE_ADDR


					SELECT 1 AS IsPasswordChanged, 'Password changed successfully' As Remarks

					------------------------------------------------------------------------------------------
				END
	   END

