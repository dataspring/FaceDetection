using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using System.Web.Helpers;
using System.Web.Http.Controllers;

using System.Web.Security;
using MemomMvc52.Areas.UserAccount.Models;

using Memom.Service;
using Memom.Entities.Models;
using Memom.Repo.Repositories;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.Ef6;
using System.Configuration;
using Repository.Pattern.Ef6.Factories;





namespace MemomMvc52.Utilities
{


    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string allowedroles;
        private UserService userSvc;

        public AdminAuthorizeAttribute(UserService userSvc)
        {
            this.allowedroles = WebUtil.GetAppSetting("AdminRoleDbString", "Admin");
            this.userSvc = userSvc;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            //Get the current claims principal
            var prinicpal = (System.Security.Claims.ClaimsPrincipal)Thread.CurrentPrincipal;
            //Make sure they are authenticated
            if (!prinicpal.Identity.IsAuthenticated)
                return false;

            var authResult = this.userSvc.AuthorizeCheck(HttpContext.Current.User.Identity.Name, allowedroles, HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }

    public class ProviderInitializationHttpModule : IHttpModule
    {
        public ProviderInitializationHttpModule(RoleProvider roleProvider)
        {
        }

        public void Init(HttpApplication context)
        {
        }

        public void Dispose()
        {
        }
    }


    public class RoleService : IDisposable
    {
        private UserService _userSvc;
        private DataContext dataContext;
        private Repository<UserAccount> userRep;
        private UnitOfWork unitOfWork;
        private MemomContext appDbContext;
        private bool disposed = false; // to detect redundant calls

        public RoleService()
        {
                dataContext = null;
                userRep = null;
                unitOfWork = null;
                appDbContext = null;
                dataContext = new DataContext(ConfigurationManager.ConnectionStrings["MemomContext"].ConnectionString);
                appDbContext = new MemomContext();
                unitOfWork = new UnitOfWork(dataContext);
                userRep = new Repository<UserAccount>(dataContext, unitOfWork);
                _userSvc = new UserService(userRep, appDbContext, unitOfWork);
        }

        public UserService userSvc
        {
            get
            {
                return _userSvc;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (_userSvc != null)
                    {
                        dataContext = null;
                        userRep = null;
                        unitOfWork = null;
                        appDbContext = null;
                        _userSvc = null;
                    }
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }


    public class WdaRoleProvider : RoleProvider
    {
        //private RoleService userSvc;

        public WdaRoleProvider() 
        {
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            string[] ret = {"Admin", "User"};
            string[] norm = { "User" };
            //throw new NotImplementedException();
            using (RoleService roleSvc = new RoleService())
            {
                if (roleSvc.userSvc.IsUserInRole(username, "Admin"))
                {
                    return ret;
                }
                return norm;
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            using (RoleService roleSvc = new RoleService())
            {
                return roleSvc.userSvc.IsUserInRole(username, roleName);
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            using (RoleService roleSvc = new RoleService())
            {
                return roleSvc.userSvc.RoleExists(roleName);
            }
        }
    }


}