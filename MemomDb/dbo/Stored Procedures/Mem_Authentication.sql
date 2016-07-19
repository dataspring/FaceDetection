
CREATE PROCEDURE [dbo].[Mem_Authentication] 
(@Email NVarchar(255),@Password NVarchar(254),@Remarks NVarchar(50),
@REMOTE_ADDR NVarchar(50))

AS

If((select count(Id) from UserAccounts Where Email = @Email)>0)
BEGIN
	Declare @UId table(UserId int)
	insert  @UId
	select [Key] from UserAccounts 
	Where Email = @Email and PWDCOMPARE(@Password, HashedPasswordBinary) > 0 

	Declare @Key int
	Select @Key = UserId from @UId

	If((select count(UserId) from @UId)>0)
	BEGIN
	print 'inside'
		--update UserLogInAttempts
		Insert Into UserLogInAttempts(UserKey, Email,Password,Result,IPAddress,Remarks)
		Values(@Key, @Email,substring(@Password,1,2)+REPLICATE('*',LEN(@Password)-2),'Successful',@REMOTE_ADDR,@Remarks)

		--update UserAccounts
		UPDATE UserAccounts SET LastLogIn = GETDATE() Where [Key] IN (select UserId from @UId)
		SELECT 1 AS AuthOutCome, CAST([RequiresPasswordReset] AS INT) As PasswordReset  from UserAccounts Where Email = @Email			
	END
	else
	BEGIN
	    Select TOP 1 @Key = [Key] from UserAccounts Where Email = @Email
		--update UserLogInAttempts
		Insert Into UserLogInAttempts(UserKey, Email,Password,Result,IPAddress,Remarks)
		--Values(@Key, @Email,@Password,'Failed',@REMOTE_ADDR,@Remarks)
		Values(@Key, @Email,substring(@Password,1,2)+REPLICATE('*',LEN(@Password)-2),'Failed',@REMOTE_ADDR,@Remarks)
		--update UserAccounts
		UPDATE UserAccounts SET LastFailedLogIn = GETDATE(), FailedLoginCount=FailedLoginCount+1 Where Email = @Email
		SELECT 0 AS AuthOutCome, 0 As PasswordReset

	END
END
ELSE
	SELECT 2 AS AuthOutCome, 0 As PasswordReset
GO



