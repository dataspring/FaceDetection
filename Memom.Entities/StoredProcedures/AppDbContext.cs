using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;



namespace Memom.Entities.Models
{
    public partial class MemomContext : IAppDbStoredProcedures
    {

        #region Authentication & Authorization
        public IEnumerable<AuthResult> WdaAuthentication(string emailAddress, string pwdPhrase, string remarks, string remoteAddress)
        {
            var emailParameter = emailAddress != null ?
                new SqlParameter("@Email", emailAddress) :
                new SqlParameter("@Email", typeof(string));
    
            var passwordParameter = pwdPhrase != null ?
                new SqlParameter("@Password", pwdPhrase) :
                new SqlParameter("@Password", typeof(string));
    
            var remarksParameter = remarks != null ?
                new SqlParameter("@Remarks", remarks) :
                new SqlParameter("@Remarks", typeof(string));
    
            var rEMOTE_ADDRParameter = remoteAddress != null ?
                new SqlParameter("@REMOTE_ADDR", remoteAddress) :
                new SqlParameter("@REMOTE_ADDR", typeof(string));
    
            return Database.SqlQuery<AuthResult>("Mem_Authentication @Email, @Password, @Remarks, @REMOTE_ADDR", emailParameter, passwordParameter, remarksParameter, rEMOTE_ADDRParameter).ToList();
        }

        public IEnumerable<RegisterResult> WdaRegisterAccount(string FirstName, string LastName, string DisplayName, string Email, string Password, string remoteAddress)
        {
            var emailParameter = Email != null ?
                new SqlParameter("@Email", Email) :
                new SqlParameter("@Email", typeof(string));

            var firstNameParameter = FirstName != null ?
                new SqlParameter("@FirstName", FirstName) :
                new SqlParameter("@FirstName", typeof(string));

            var lastNameParameter = LastName != null ?
                new SqlParameter("@LastName", LastName) :
                new SqlParameter("@LastName", typeof(string));

            var displayNameParameter = DisplayName != null ?
                new SqlParameter("@DisplayName", DisplayName) :
                new SqlParameter("@DisplayName", typeof(string));

            var passwordParameter = Password != null ?
                new SqlParameter("@Password", Password) :
                new SqlParameter("@Password", typeof(string));

            var rEMOTE_ADDRParameter = remoteAddress != null ?
                new SqlParameter("@REMOTE_ADDR", remoteAddress) :
                new SqlParameter("@REMOTE_ADDR", typeof(string));

            return Database.SqlQuery<RegisterResult>("Mem_RegisterAccount @FirstName, @LastName, @DisplayName, @Email, @Password, @REMOTE_ADDR", firstNameParameter, lastNameParameter, displayNameParameter, emailParameter, passwordParameter, rEMOTE_ADDRParameter).ToList();
        }

        public IEnumerable<ChangePwdResult> WdaChangePassword(string Email, string OldPassword, string NewPassword, string remoteAddress)
        {
            var emailParameter = Email != null ?
               new SqlParameter("@Email", Email) :
               new SqlParameter("@Email", typeof(string));

            var oldPasswordParameter = OldPassword != null ?
                new SqlParameter("@OldPassword", OldPassword) :
                new SqlParameter("@OldPassword", typeof(string));

            var newPasswordParameter = NewPassword != null ?
                new SqlParameter("@NewPassword", NewPassword) :
                new SqlParameter("@PNewassword", typeof(string));

            var rEMOTE_ADDRParameter = remoteAddress != null ?
                new SqlParameter("@REMOTE_ADDR", remoteAddress) :
                new SqlParameter("@REMOTE_ADDR", typeof(string));

            return Database.SqlQuery<ChangePwdResult>("Mem_ChangePassword @Email, @OldPassword, @NewPassword, @REMOTE_ADDR", emailParameter, oldPasswordParameter, newPasswordParameter, rEMOTE_ADDRParameter).ToList();
  
        }

        public IEnumerable<ResetResult> WdaForgotPassword(string Email, string NewPassword, int MaxFailedPasswordResetCount, string remoteAddress)
        {
            var emailParameter = Email != null ?
               new SqlParameter("@Email", Email) :
               new SqlParameter("@Email", typeof(string));

            var newPasswordParameter = NewPassword != null ?
                new SqlParameter("@NewPassword", NewPassword) :
                new SqlParameter("@NewPassword", typeof(string));

            var maxFailedPasswordResetCount =
                new SqlParameter("@MaxFailedPasswordResetCount", MaxFailedPasswordResetCount);

            var rEMOTE_ADDRParameter = remoteAddress != null ?
                new SqlParameter("@REMOTE_ADDR", remoteAddress) :
                new SqlParameter("@REMOTE_ADDR", typeof(string));

            return Database.SqlQuery<ResetResult>("Mem_ResetPassword @Email, @NewPassword, @MaxFailedPasswordResetCount, @REMOTE_ADDR", emailParameter, newPasswordParameter, maxFailedPasswordResetCount, rEMOTE_ADDRParameter).ToList();
  
        }

        public IEnumerable<AuthorizeCheckResult> AuthorizeCheck(string Email, string Roles, string remoteAddress)
        {
            var emailParameter = Email != null ?
               new SqlParameter("@Email", Email) :
               new SqlParameter("@Email", typeof(string));

            var newRoleParameter = Roles != null ?
                new SqlParameter("@Roles", Roles) :
                new SqlParameter("@Roles", typeof(string));

            var rEMOTE_ADDRParameter = remoteAddress != null ?
                new SqlParameter("@REMOTE_ADDR", remoteAddress) :
                new SqlParameter("@REMOTE_ADDR", typeof(string));

            return Database.SqlQuery<AuthorizeCheckResult>("Mem_AuthorizeCheck @Email, @Roles, @REMOTE_ADDR", emailParameter, newRoleParameter, rEMOTE_ADDRParameter).ToList();

        }

        public IEnumerable<RoleBoolResult>  IsUserInRole(string username, string roleName)
        {
            var usernameParameter = username != null ?
               new SqlParameter("@Email", username) :
               new SqlParameter("@Email", typeof(string));

            var roleNameParameter = roleName != null ?
                new SqlParameter("@Roles", roleName) :
                new SqlParameter("@Roles", typeof(string));

            return Database.SqlQuery<RoleBoolResult>("Mem_IsUserInRole @Email, @Roles", usernameParameter, roleNameParameter).ToList();

        }

        public IEnumerable<RoleBoolResult>  RoleExists(string roleName)
        {
            var roleNameParameter = roleName != null ?
                new SqlParameter("@Roles", roleName) :
                new SqlParameter("@Roles", typeof(string));

            return Database.SqlQuery<RoleBoolResult>("Mem_RoleExists  @Roles", roleNameParameter).ToList();

        }
        
        #endregion

        #region Scores
        public IEnumerable<UpdateAlbumDownloadResult> UpdateDownload(string Email, string AlbumName, string DownloadDateTime, string remoteAddress)
        {
            var emailParameter = Email != null ?
                  new SqlParameter("@Email", Email) :
                  new SqlParameter("@Email", typeof(string));

            var gameNameParameter = AlbumName != null ?
                new SqlParameter("@AlbumName", AlbumName) :
                new SqlParameter("@AlbumName", typeof(string));


            var downloadDateTimeNameParameter = DownloadDateTime != null ?
                new SqlParameter("@DownloadDateTime", DownloadDateTime) :
                new SqlParameter("@DownloadDateTime", typeof(string));

            var rEMOTE_ADDRParameter = remoteAddress != null ?
                new SqlParameter("@REMOTE_ADDR", remoteAddress) :
                new SqlParameter("@REMOTE_ADDR", typeof(string));

            return Database.SqlQuery<UpdateAlbumDownloadResult>("Mem_UpdateDownload @Email, @AlbumName, @DownloadDateTime, @REMOTE_ADDR", emailParameter, gameNameParameter, downloadDateTimeNameParameter, rEMOTE_ADDRParameter).ToList();
  
        }

        public IEnumerable<Scores> DashboardScores(string Email)
        {
            var emailParameter = Email != null ?
               new SqlParameter("@Email", Email) :
               new SqlParameter("@Email", typeof(string));

            return Database.SqlQuery<Scores>("Mem_DashboardScores @Email", emailParameter).ToList();
        }

        public IEnumerable<AlbumScores> AlbumDashboardScores(string Email)
        {
            var emailParameter = Email != null ?
               new SqlParameter("@Email", Email) :
               new SqlParameter("@Email", typeof(string));

            return Database.SqlQuery<AlbumScores>("Mem_AlbumDashboardScores @Email", emailParameter).ToList();
        }

        public MemberDetails MemberViewDetails(string Email, int MemberKey)
        {
            var emailParameter = Email != null ?
               new SqlParameter("@Email", Email) :
               new SqlParameter("@Email", typeof(string));

            var memberKeyParameter =
                new SqlParameter("@MemberKey", MemberKey);

            return Database.SqlQuery<MemberDetails>("Mem_MemberDetails @Email, @MemberKey", emailParameter, memberKeyParameter).FirstOrDefault();
        }

        #endregion

        public void FaceAddTagProcessing(int UserKey, int MemberKey, string Face)
        {

            var userKeyParameter =
                new SqlParameter("@UserKey", UserKey);

            var memberKeyParameter =
                new SqlParameter("@MemberKey", MemberKey);

            var faceParameter = Face != null ?
               new SqlParameter("@Face", Face) :
               new SqlParameter("@Face", typeof(string));

            Database.ExecuteSqlCommand("Mem_FaceAddTagProcessing @UserKey, @MemberKey, @Face", userKeyParameter, memberKeyParameter, faceParameter);
        }

        public void FaceReplaceTagProcessing(int UserKey, int MemberKey, string Face, string OldFace)
        {
            var userKeyParameter =
                new SqlParameter("@UserKey", UserKey);

            var memberKeyParameter =
                new SqlParameter("@MemberKey", MemberKey);

            var faceParameter = Face != null ?
               new SqlParameter("@Face", Face) :
               new SqlParameter("@Face", typeof(string));

            var oldFaceParameter = OldFace != null ?
               new SqlParameter("@OldFace", OldFace) :
               new SqlParameter("@OldFace", typeof(string));


            Database.ExecuteSqlCommand("Mem_FaceReplaceTagProcessing @UserKey, @MemberKey, @Face, @OldFace", userKeyParameter,memberKeyParameter, faceParameter, oldFaceParameter);
        }

        public void PhotoAddTagProcessing(int UserKey, int AlbumInstanceKey)
        {

            var userKeyParameter =
                new SqlParameter("@UserKey", UserKey);

            var albumInstanceKeyParameter =
                new SqlParameter("@AlbumInstanceKey", AlbumInstanceKey);

            Database.ExecuteSqlCommand("Mem_PhotoAddTagProcessing @UserKey, @AlbumInstanceKey", userKeyParameter, albumInstanceKeyParameter);
        }

        public void PhotoDeleteTagProcessing(int UserKey, int AlbumInstanceKey)
        {
            var userKeyParameter =
                new SqlParameter("@UserKey", UserKey);


            var albumInstanceKeyParameter =
                new SqlParameter("@AlbumInstanceKey", AlbumInstanceKey);

            Database.ExecuteSqlCommand("Mem_PhotoDeleteTagProcessing @UserKey, @AlbumInstanceKey", userKeyParameter, albumInstanceKeyParameter);
        }


        public IEnumerable<UserAlbumInstanceDetail> GetPhotosForBatchProcssing()
        {
            return Database.SqlQuery<UserAlbumInstanceDetail>("Mem_GetPhotosForBatchProcssing ").ToList();
        }


        public IEnumerable<FaceViewAlbumInstance> ViewFaceMemberPhotos(int MemberKey, int? AlbumKey)
        {
            var memberKeyParameter =
                new SqlParameter("@MemberKey", MemberKey);

            var albumKeyParameter =
                new SqlParameter("@AlbumKey", System.Data.SqlDbType.Int);
            albumKeyParameter.IsNullable = true;
            if (AlbumKey == null)
                albumKeyParameter.Value = DBNull.Value;
            else
                albumKeyParameter.Value = (int)AlbumKey;

            return Database.SqlQuery<FaceViewAlbumInstance>("Mem_ViewFaceMemberImages @MemberKey, @AlbumKey", memberKeyParameter, albumKeyParameter).ToList();

        }
    }
}
