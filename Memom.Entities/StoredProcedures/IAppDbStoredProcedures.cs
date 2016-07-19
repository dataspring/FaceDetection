using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memom.Entities.Models
{
    public interface IAppDbStoredProcedures
    {
        //IEnumerable<CustomerOrderHistory> CustomerOrderHistory(string customerID);
        //int CustOrdersDetail(int? orderID);
        //IEnumerable<CustomerOrderDetail> CustomerOrderDetail(string customerID);
        //int EmployeeSalesByCountry(DateTime? beginningDate, DateTime? endingDate);
        //int SalesByCategory(string categoryName, string ordYear);
        //int SalesByYear(DateTime? beginningDate, DateTime? endingDate);

        #region Authorization & Authentication
        IEnumerable<AuthResult> WdaAuthentication(string emailAddress, string pwdPhrase, string remarks, string remoteAddress);

        //LoginUserDetails GetUserDetails(string emailAddress);

        IEnumerable<RegisterResult> WdaRegisterAccount(string FirstName, string LastName, string DisplayName, string Email, string Password, string remoteAddress);

        IEnumerable<ChangePwdResult> WdaChangePassword(string Email, string OldPassword, string NewPassword, string remoteAddress);

        IEnumerable<ResetResult> WdaForgotPassword(string Email, string NewPassword, int MaxFailedPasswordResetCount, string remoteAddress);


        IEnumerable<AuthorizeCheckResult> AuthorizeCheck(string Email, string Roles, string remoteAddress);

        IEnumerable<RoleBoolResult> IsUserInRole(string username, string roleName);

        IEnumerable<RoleBoolResult> RoleExists(string roleName);
        #endregion

        #region Scores
        IEnumerable<Scores> DashboardScores(string Email);

        IEnumerable<AlbumScores> AlbumDashboardScores(string Email);

        IEnumerable<UpdateAlbumDownloadResult> UpdateDownload(string Email, string AlbumName, string DownloadDateTime, string remoteAddress);

        MemberDetails MemberViewDetails(string Email, int MemberKey);

        IEnumerable<FaceViewAlbumInstance> ViewFaceMemberPhotos(int MemberKey, int? AlbumKey);


        #endregion

        #region Album Instance Details
        void FaceAddTagProcessing(int UserKey, int MemberKey, string Face);

        void FaceReplaceTagProcessing(int UserKey, int MemberKey, string Face, string OldFace);

        void PhotoAddTagProcessing(int UserKey, int AlbumInstanceKey);

        void PhotoDeleteTagProcessing(int UserKey, int AlbumInstanceKey);

        #endregion

        #region Batch

        IEnumerable<UserAlbumInstanceDetail> GetPhotosForBatchProcssing();


        #endregion

    }
}
