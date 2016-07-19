using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Configuration;
using MemomMvc52.Areas.UserAccount;
using MemomMvc52.Areas.UserAccount.Models;
using MemomMvc52.Areas.MemomWeb.Models;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;


using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using MemomMvc52.Utilities;
using Memom.Entities.Models;
using Memom.Service;


namespace MemomMvc52.Areas.MemomWeb.Controllers
{
    [RoutePrefix("api")]
    public class UserLoginController : ApiController
    {
        UserService userSvc;

        public UserLoginController(UserService userSvc
            )
        {
            this.userSvc = userSvc;
        }

        public IHttpActionResult BadRequest<T>(T data)
        {
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, data));
        }

        public IHttpActionResult NoContent()
        {
            return StatusCode(HttpStatusCode.NoContent);
        }


        [AllowAnonymous]
        [HttpPost, Route("login", Name = "api-login")]
        public IHttpActionResult Login(string Username, string Password)
        {
            IEnumerable<AuthResult> result;

            Memom.Entities.Models.UserAccount userAccount;

            result = userSvc.WdaAuthentication(Username, Password, "Login-through-MVC-Api", HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);

            if (result.First().AuthOutCome == 1)
            {

                HttpContext.Current.Response.Cookies.Add(WebUtil.PrepCookie(Username, false));
                HttpContext.Current.Response.Cookies.Add(new HttpCookie("RequestVerificationToken", WebUtil.GetAntiForgeryTokenHeaderValue()));

                userAccount = userSvc.UserDetails(Username);
                LoginApiAccount apiAccount;
                apiAccount = PrepareLoginSuccessData(userAccount);

                    
                //if (account.RequiresPasswordReset)
                //{
                //    // this might mean many things -- 
                //    // it might just mean that the user should change the password, 
                //    // like the expired password below, so we'd just redirect to change password page
                //    // or, it might mean the DB was compromised, so we want to force the user
                //    // to reset their password but via a email token, so we'd want to 
                //    // let the user know this and invoke ResetPassword and not log them in
                //    // until the password has been changed
                //    //userAccountService.ResetPassword(account.ID);

                //    // so what you do here depends on your app and how you want to define the semantics
                //    // of the RequiresPasswordReset property

                //    ModelState.AddModelError("", "RequiresPasswordReset - Use appropriate api method");
                //}

                //if (userAccountService.IsPasswordExpired(account))
                //{
                //    ModelState.AddModelError("", "PasswordExpired, need to ChangePassword - Use appropriate api method");
                //}

                

                //apiAccount.FirstName = Username;
                //apiAccount.LastName = Username;
                //apiAccount.DisplayName = Username;

                //apiAccount.FirstName = account.FirstName;
                //apiAccount.LastName = account.LastName;
                //apiAccount.DisplayName = account.DisplayName;

                foreach (string cookiename in HttpContext.Current.Response.Cookies)
                {
                    HttpCookie tmpCookie = HttpContext.Current.Response.Cookies[cookiename];
                    if (tmpCookie != null)
                    {
                        switch (cookiename)
                        {
                            case ".ASPXAUTH":
                                apiAccount.Tokens.Add(new AuthToken(tmpCookie.Name, tmpCookie.Value));
                                break;
                            case "RequestVerificationToken":
                                apiAccount.Tokens.Add(new AuthToken(tmpCookie.Name, tmpCookie.Value));
                                break;
                        }
                    }

                }


                return Ok(apiAccount);
            }
            else
            {
                ModelState.AddModelError("", "Invalid Email or Password");
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, ModelState.ToError()));
        }

        private LoginApiAccount PrepareLoginSuccessData(Memom.Entities.Models.UserAccount userAccount)
        {
            LoginApiAccount apiAccount = new LoginApiAccount();

            apiAccount.FirstName = userAccount.FirstName;
            apiAccount.LastName = userAccount.LastName;
            apiAccount.DisplayName = userAccount.DisplayName;
            apiAccount.LastLogin = userAccount.LastLogin;
            apiAccount.RequiresPasswordReset = userAccount.RequiresPasswordReset;
            apiAccount.ID = userAccount.ID;
            apiAccount.Key = userAccount.Key;
            apiAccount.Email = userAccount.Email;

            // do this further processing only when reset password is not needed, if not ask teh user to chnage password

            if (!userAccount.RequiresPasswordReset)
            {
                foreach (Album userAlbum in userAccount.Albums)
                {
                    ApiUserAlbum sample = new ApiUserAlbum();
                    sample = new ApiUserAlbum
                    {
                        Key = userAlbum.Key,
                        AlbumName = userAlbum.Name,

                    };

                    apiAccount.UserAlbums.Add(new ApiUserAlbum
                    {
                        Key = userAlbum.Key,
                        AlbumName = userAlbum.Name,
                    });
                }
                
            }

            return apiAccount;
        }


        [AllowAnonymous]
        [HttpPost, Route("register", Name = "api-register")]
        public IHttpActionResult Register(string FirstName, string LastName, string DisplayName, string Email, string Password)
        {
            RegisterResult registerResult = userSvc.WdaRegisterAccount(FirstName, LastName, DisplayName, Email, Password, HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            if (registerResult.IsRegistered == 1)
            {
                return Ok(registerResult);
            }
            else
            {
                ModelState.AddModelError("", registerResult.Remarks);
            }
            
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, ModelState.ToError()));
        }


        [Authorize]
        [HttpPost, Route("changepassword", Name = "api-changepassword")]
        public IHttpActionResult ChangePassword(string Email, string OldPassword, string NewPassword)
        {
            ChangePwdResult changeResult = userSvc.WdaChangePassword(Email, OldPassword, NewPassword, HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            if (changeResult.IsPasswordChanged  == 1)
            {
                return Ok(changeResult);
            }
            else
            {
                ModelState.AddModelError("", changeResult.Remarks);
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, ModelState.ToError()));
        }

        [AllowAnonymous]
        [HttpPost, Route("forgotpassword", Name = "api-forgotpassword")]
        public IHttpActionResult ForgotPassword(string Email)
        {
            ResetResult resetResult = userSvc.WdaForgotPassword (Email,  HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            if (resetResult.IsPasswordReset == 1)
            {
                return Ok(resetResult);
            }
            else
            {
                ModelState.AddModelError("", resetResult.Remarks);
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, ModelState.ToError()));
        }

    }

}








