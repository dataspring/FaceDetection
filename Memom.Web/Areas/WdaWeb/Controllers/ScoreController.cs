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
using Memom.Service;
using Memom.Entities.Models;


namespace MemomMvc52.Areas.MemomWeb.Controllers
{
    [RoutePrefix("api")]
    public class MemomController : ApiController
    {
        AlbumService gameSvc;
        MemberService membSvc;

        public MemomController(AlbumService albumSvc, MemberService membSvc
            )
        {
            this.gameSvc = albumSvc;
            this.membSvc = membSvc;
        }

        public IHttpActionResult BadRequest<T>(T data)
        {
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, data));
        }

        public IHttpActionResult NoContent()
        {
            return StatusCode(HttpStatusCode.NoContent);
        }


        [Authorize]
        [HttpPost, Route("updatedownload", Name = "api-updatedownload")]
        public IHttpActionResult UpdateDownload(string Email, string AlbumName, string DownloadTime)
        {
            if (User.Identity.Name != Email)
            {
                ModelState.AddModelError("", "You cannot update other people download status");
            }
            else
            {
                UpdateAlbumDownloadResult downResult = gameSvc.UpdateDownload(Email, AlbumName, DownloadTime, HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
                if (downResult.IsAlbumDownloadUpdated == 1)
                {
                    return Ok(downResult);
                }
                else
                {
                    ModelState.AddModelError("", downResult.Remarks);
                }
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, ModelState.ToError()));
        }


        [Authorize]
        [HttpPost, Route("usermemberscores", Name = "api-usermemberscores")]
        public IHttpActionResult DashboardScores(string Email)
        {
            if (User.Identity.Name != Email)
            {
                ModelState.AddModelError("", "You cannot see other people scores");
            }
            else 
            { 
                IEnumerable<Scores> scoreResult = membSvc.DashboardScores(Email);
                if (scoreResult.Count() > 0)
                {
                    return Ok(scoreResult);
                }
                else
                {
                    ModelState.AddModelError("", "No user scores available");
                }
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, ModelState.ToError()));
        }

    }

}








