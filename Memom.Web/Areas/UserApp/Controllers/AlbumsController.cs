using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System;


//using MemomMvc52.Areas.UserAccount;
//using MemomMvc52.Areas.UserAccount.Models;
using MemomMvc52.Areas.UserApp.Models;
using Memom.Entities.Models;
using Memom.Service;
using MemomMvc52.Utilities;
using System.Net;
using PagedList;

namespace MemomMvc52.Areas.UserApp
{
        [Authorize]
        public class AlbumsController : Controller
        {
            AlbumService _albumSvc;
            UserService _userSvc;
            AlbumInstanceService _albumInstSvc;
            MemberService _memberSvc;

            int pageSize;

            public AlbumsController(AlbumService albumSvc, UserService userSvc, AlbumInstanceService albumInstSvc, MemberService memberSvc)
            {
                this._albumSvc = albumSvc;
                this._userSvc = userSvc;
                this._albumInstSvc = albumInstSvc;
                this._memberSvc = memberSvc;
                this.pageSize = WebUtil.PageSize();
            }

            // GET: Albums
            public ActionResult Index(int? page)
            {
                return View(_albumSvc.AlbumDashboardScores(User.Identity.Name, page ?? 1, pageSize));
            }

            public ActionResult Add()
            {
                AlbumAddInput albumAdd = new AlbumAddInput();
                albumAdd.Email = User.Identity.Name;
                albumAdd.UserKey = _userSvc.UserDetails(User.Identity.Name).Key.ToString();

                return View(albumAdd);
            }

            public ActionResult ViewAlbum(int? albumKey, int? page)
            {
                Album album;
                if (albumKey.HasValue)
                {
                    album = _albumSvc.FindAlbumsInEmail(User.Identity.Name, (int)albumKey);
                    if (album == null)
                        return RedirectToAction("UnAuthorized", "Error");
                    else
                    {
                        return View(_albumInstSvc.AlbumInstancesByAlbumKey(album.Key, page ?? 1, pageSize));
                    }

                }
                else
                    return RedirectToAction("Error404", "Error");
            }

            public ActionResult ViewFaceMember(int? MemberKey, int?AlbumKey, int? page)
            {
                Member _member; Album album;
                if (MemberKey.HasValue)
                {
                    _member = _memberSvc.FindMember(User.Identity.Name, (int)MemberKey);
                    if (_member == null)
                        return RedirectToAction("UnAuthorized", "Error");

                    if (AlbumKey.HasValue)
                    {
                        album = _albumSvc.FindAlbumsInEmail(User.Identity.Name, (int)AlbumKey);
                        if (album == null)
                        return RedirectToAction("UnAuthorized", "Error");
                    }
                    else AlbumKey = null;

                    return View(_albumSvc.ViewFaceMemberPhotos(_member.Key, AlbumKey, page ?? 1, pageSize));
                }
                else
                    return RedirectToAction("Error404", "MemberKey is not present");
            }


        }
}
