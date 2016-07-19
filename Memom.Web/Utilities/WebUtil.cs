using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Security;
using MemomMvc52.Areas.UserAccount.Models;
using System.Configuration;
using System.Xml.Linq;
using System.Xml;



namespace MemomMvc52.Utilities
{
    public static class WebUtil
    {
        public static HttpCookie PrepCookie(string Username, bool RememberMe)
        {
            TimeSpan _expirationTimeSpan;

            //---------------forms authentication stuff --
            var now = DateTime.UtcNow.ToLocalTime();
            _expirationTimeSpan = FormsAuthentication.Timeout;

            var ticket = new FormsAuthenticationTicket(
                1 /*version*/,
                Username,
                now,
                now.Add(_expirationTimeSpan),
                RememberMe,
                Username,
                FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.HttpOnly = true;
            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }
            return cookie;
            //----------------------------------------------------------
        }

        public static string GetAntiForgeryTokenHeaderValue()
        {
            string cookieToken, formToken;
            System.Web.Helpers.AntiForgery.GetTokens(null, out cookieToken, out formToken);
            return cookieToken + ":" + formToken;
        }


        public static T GetAppSetting<T>(string key, T defaultValue)
        {
            if (!string.IsNullOrEmpty(key))
            {
                string value = ConfigurationManager.AppSettings[key];
                try
                {
                    if (value != null)
                    {
                        var theType = typeof(T);
                        if (theType.IsEnum)
                            return (T)Enum.Parse(theType, value.ToString(), true);

                        return (T)Convert.ChangeType(value, theType);
                    }

                    //return default(T);
                    return defaultValue;
                }
                catch { }
            }

            return defaultValue;
        }


        public static int PageSize()
        {
            return Convert.ToInt32(GetAppSetting("PageSize", "25"));
        }

        public static string GetErrorMessage(Exception faultException)
        {
            string errorMessage;
            errorMessage = faultException.Message;
            return errorMessage;
        }

        private static string GetXmlNodeText(XmlNode xElement, string xpath)
        {
            XmlNode node = xElement.SelectSingleNode(xpath);
            return node != null ? node.InnerText : null;
        }

    }


    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class ValidateWebApiAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            try
            {
                string cookieToken = "";
                string formToken = "";

                IEnumerable<string> tokenHeaders;
                if (actionContext.Request.Headers.TryGetValues("RequestVerificationToken", out tokenHeaders))
                {
                    string[] tokens = tokenHeaders.First().Split(':');
                    if (tokens.Length == 2)
                    {
                        cookieToken = tokens[0].Trim();
                        formToken = tokens[1].Trim();
                    }
                }
                AntiForgery.Validate(cookieToken, formToken);
            }
            catch (System.Web.Mvc.HttpAntiForgeryException e)
            {
                actionContext.Response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    RequestMessage = actionContext.ControllerContext.Request
                };
                return FromResult(actionContext.Response);
            }
            return continuation();
        }

        private Task<HttpResponseMessage> FromResult(HttpResponseMessage result)
        {
            var source = new TaskCompletionSource<HttpResponseMessage>();
            source.SetResult(result);
            return source.Task;
        }
    }

}