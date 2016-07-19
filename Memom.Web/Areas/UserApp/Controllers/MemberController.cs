using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memom.Service;
using MemomMvc52.Utilities;
using Memom.Entities.Models;

namespace MemomMvc52.Areas.UserApp.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        MemberService _membSvc;
        int pageSize;

        public MemberController(MemberService membSvc)
        {
            this._membSvc = membSvc;
            this.pageSize = WebUtil.PageSize();
        }
        
        // GET: UserApp/Member
        public ActionResult Index(int? page)
        {
            //return View(_membSvc.MembersByEmail(User.Identity.Name, page ?? 1, Request.Browser.IsMobileDevice ? 5 : pageSize));
            return View(_membSvc.MembersByEmail(User.Identity.Name, page ?? 1, pageSize));
        }


        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Remove(int? MemberId)
        {
            Member memb;
            if (MemberId.HasValue)
            {
                memb = _membSvc.FindMember(User.Identity.Name, (int)MemberId );
                if (memb == null)
                    return View(memb);
                else
                {
                    memb.IsActive = false;
                    _membSvc.Update(memb);
                    return View(memb);
                }
                
            }
            else
                return RedirectToAction("Error404", "Error");

        }

        public ActionResult ViewStatus(int? MemberId)
        {
            if (MemberId.HasValue)
            {
                return View(_membSvc.FindMemberDetails (User.Identity.Name, (int)MemberId));
            }
            else
                return RedirectToAction("Error404", "Error");
        }

    }
}