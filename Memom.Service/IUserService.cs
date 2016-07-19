using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using Memom.Entities.Models;
using Repository.Pattern.Repositories;
using Service.Pattern;
using PagedList;

namespace Memom.Service
{
    public interface IUserService : IService<UserAccount>
    {

        #region Authentication & Authorization

        
        IEnumerable<AuthResult> WdaAuthentication(string emailAddress, string pwdPhrase, string remarks, string remoteAddress);

        
        UserAccount UserDetails(string emailAddress);

        
        RegisterResult WdaRegisterAccount(string FirstName, string LastName, string DisplayName, string Email, string Password, string remoteAddress);


        
        ChangePwdResult WdaChangePassword(string Email, string OldPassword, string NewPassword, string remoteAddress);


        
        ResetResult WdaForgotPassword(string Email, string remoteAddress);

        
        AuthorizeCheckResult AuthorizeCheck(string Email, string Roles, string remoteAddress);

        
        bool IsUserInRole(string username, string roleName);

        
        bool RoleExists(string roleName);
 
        #endregion


        #region UserAccount

        
        decimal UsersTotal();
        
        IPagedList<UserAccount> UsersByName(string userName, int pageNumber = 1, int pageSize = 15);

        IPagedList<UserAccount> UsersByEmail(string userEmail, int pageNumber = 1, int pageSize = 15);


        IPagedList<UserAccount> UsersAll(int pageNumber = 1, int pageSize = 15);

        
        void Insert(UserAccount entity);
        
        void Delete(object id);
        
        void Update(UserAccount entity);
        
        UserAccount FindUser(int id);
        #endregion
    }
}
