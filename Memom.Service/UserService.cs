using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


using Memom.Entities.Models;
using Memom.Repo.Repositories;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using PagedList;
using Service.Pattern;
using System.Web.Configuration;

namespace Memom.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Userservice" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Userservice.svc or Userservice.svc.cs at the Solution Explorer and start debugging.
    public class UserService : Service<UserAccount>, IUserService
    {
        /// <summary>
        ///     All methods that are exposed from Repository in Service are overridable to add business logic,
        ///     business logic should be in the Service layer and not in repository for separation of concerns.
        /// </summary>

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryAsync<UserAccount> _repositoryUser;
        private readonly IAppDbStoredProcedures _storedProcedures;

        public UserService(IRepositoryAsync<UserAccount> repositoryUser, IAppDbStoredProcedures storedProcedures, IUnitOfWorkAsync unitOfWork)
            : base(repositoryUser)
        {
            _repositoryUser = repositoryUser;
            _storedProcedures = storedProcedures;
            _unitOfWork = unitOfWork;
        }

        #region Authentication & Authorization

        public IEnumerable<AuthResult> WdaAuthentication(string emailAddress, string pwdPhrase, string remarks, string remoteAddress)
        {
            return _storedProcedures.WdaAuthentication(emailAddress, pwdPhrase, remarks, remoteAddress);
        }

        public UserAccount UserDetails(string Email)
        {
            // add business logic here
            return _repositoryUser.UserDetails(Email);

        }

        public RegisterResult WdaRegisterAccount(string FirstName, string LastName, string DisplayName, string Email, string Password, string remoteAddress)
        {
            RegisterResult registerResult = new RegisterResult();
            registerResult = _storedProcedures.WdaRegisterAccount(FirstName, LastName, DisplayName, Email, Password, remoteAddress).First();

            if (registerResult.IsRegistered == 1)
                Utility.SendMailForRegisterConfirmation(Email);

            return registerResult;

        }

        public ChangePwdResult WdaChangePassword(string Email, string OldPassword, string NewPassword, string remoteAddress)
        {
            ChangePwdResult changePwdResult = new ChangePwdResult();
            changePwdResult = _storedProcedures.WdaChangePassword(Email, OldPassword, NewPassword, remoteAddress).First();

            if (changePwdResult.IsPasswordChanged == 1)
                Utility.SendMailForPasswordChange(Email);

            return changePwdResult;

        }

        public ResetResult WdaForgotPassword(string Email, string remoteAddress)
        {
            ResetResult resetResult = new ResetResult();
            string NewPassword = Utility.GenPassword(10);
            int MaxFailedPasswordResetCount;

            string strMaxFailedPasswordResetCount = WebConfigurationManager.AppSettings["MaxFailedPasswordResetCount"];
            if (!String.IsNullOrEmpty(strMaxFailedPasswordResetCount))
                MaxFailedPasswordResetCount = Int32.Parse(strMaxFailedPasswordResetCount);
            else
                MaxFailedPasswordResetCount = 15;


            resetResult = _storedProcedures.WdaForgotPassword(Email, NewPassword, MaxFailedPasswordResetCount, remoteAddress).First();

            if (resetResult.IsPasswordReset == 1)
                Utility.SendMailForPasswordReset(Email, NewPassword);

            return resetResult;

        }


        public AuthorizeCheckResult AuthorizeCheck(string Email, string Roles, string remoteAddress)
        {
            AuthorizeCheckResult authResult = new AuthorizeCheckResult();

            authResult = _storedProcedures.AuthorizeCheck(Email, Roles, remoteAddress).First();

            return authResult;

        }



        public bool IsUserInRole(string username, string roleName)
        {
            RoleBoolResult roleBool = new RoleBoolResult();
            roleBool = _storedProcedures.IsUserInRole(username, roleName).First();
            return Convert.ToBoolean(roleBool.IsPresent);
        }

        public bool RoleExists(string roleName)
        {
            RoleBoolResult roleBool = new RoleBoolResult();
            roleBool = _storedProcedures.RoleExists(roleName).First();
            return Convert.ToBoolean(roleBool.IsPresent);
        }

        #endregion


        #region UserAccount

        public decimal UsersTotal()
        {
            // add business logic here
            return _repositoryUser.UsersTotal();
        }

        public IPagedList<UserAccount> UsersByEmail(string userEmail, int pageNumber, int pageSize)
        {
            // add business logic here
            return _repositoryUser.UsersByEmail(userEmail).ToPagedList<UserAccount>(pageNumber, pageSize);
        }

        public IPagedList<UserAccount> UsersByName(string userName, int pageNumber, int pageSize)
        {
            // add business logic here
            return _repositoryUser.UsersByName(userName).ToPagedList<UserAccount>(pageNumber, pageSize);
        }


        public IPagedList<UserAccount> UsersAll(int pageNumber, int pageSize)
        {
            // add business logic here
            return _repositoryUser.UsersAll().ToPagedList<UserAccount>(pageNumber, pageSize);
        }


        public override void Insert(UserAccount entity)
        {
            // e.g. add business logic here before inserting
            _repositoryUser.Insert(entity);
            _unitOfWork.SaveChanges();
        }

        public override void Delete(object id)
        {
            // e.g. add business logic here before deleting
            base.Delete(id);
            _unitOfWork.SaveChanges();
        }


        public override void Update(UserAccount entity)
        {
            // e.g. add business logic here before inserting
            base.Update(entity);
            _unitOfWork.SaveChanges();

        }

        public UserAccount FindUser(int id)
        {
            return base.Find(id);
        }

        #endregion

    }
}
