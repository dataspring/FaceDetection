using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System;


using MemomMvc52.Areas.UserApp.Models;
using Memom.Service;
using MemomMvc52.Utilities;
using System.Net;
using PagedList;
using Memom.Entities.Models;


namespace MemomMvc52.Areas.UserApp
{
        [Authorize(Roles="Admin")]
        public class UsersController : Controller
        {
            UserService userSvc;
            int pageSize;

            public UsersController(UserService userSvc)
            {
                this.userSvc = userSvc;
                this.pageSize = WebUtil.PageSize();
            }

            // GET: UserAccounts
            public ActionResult Index(string searchby, string searchon, int? page)
            {
                var userCount = this.userSvc.UsersTotal();
                ViewBag.UserCount = Convert.ToInt32(userCount);
                ViewBag.SearchBy = "";
                ViewBag.SearchOn = searchon;

                if (string.IsNullOrEmpty(searchby))
                {
                    ViewBag.SearchBy = "Name";
                    return View(this.userSvc.UsersAll(page ?? 1, pageSize));
                }
                else if (searchby == "Email")
                {
                    ViewBag.SearchBy = "Email";
                    return View(this.userSvc.UsersByEmail(searchon ?? "", page ?? 1, pageSize));
                    
                }
                else if (searchby == "Name")
                {
                    ViewBag.SearchBy = "Name";
                    return View(this.userSvc.UsersByName(searchon ?? "", page ?? 1, pageSize));
                    
                }
                else 
                {
                    ViewBag.SearchBy = "Name";
                    return View(this.userSvc.UsersAll(page ?? 1, pageSize));
                    
                }
             
            }


            // POST: 
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Index([Bind(Include = "SearchByEmail,SearchByName,SearchBy,SearchOn")] UserSearch UserSearch)
            {
                if (ModelState.IsValid)
                {

                    try
                    {
                        return Index(string.IsNullOrEmpty(UserSearch.SearchBy) ? "Email" : UserSearch.SearchBy, UserSearch.SearchOn, 1);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                        return View();
                    }

                }
                return View();
            }


            // GET: UserAccounts/Details/5
            public ActionResult Details(int? id)
            {
                int idSought = id ?? 1;

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Memom.Entities.Models.UserAccount UserAccount = this.userSvc.FindUser(idSought);
                if (UserAccount == null)
                {
                    return HttpNotFound();
                }
                return View(UserAccount);
            }

            // GET: UserAccounts/Create
            public ActionResult Create()
            {
               return RedirectToAction("Index", "Register", new { area = "UserAccount" });
            }

            // POST: UserAccounts/Create
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            //[HttpPost]
            //[ValidateAntiForgeryToken]
            //public ActionResult Create([Bind(Include = "UserKey,Username,FirstName,LastName,DisplayName,IsAccountClosed,IsLoginAllowed,RequiresPasswordReset,Email,IsAccountVerified")] UserAccount UserAccount)
            //{
            //    if (ModelState.IsValid)
            //    {
            //        UserAccount.Created = DateTime.Now;
            //        UserAccount.LastUpdated = UserAccount.Created;

            //        this.userSvc.Insert(UserAccount);

            //        return RedirectToAction("Index");
            //    }

            //    return View(UserAccount);
            //}

            // GET: UserAccounts/Edit/5
            public ActionResult Edit(int? id)
            {
                int idSought = id ?? 1;

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Memom.Entities.Models.UserAccount UserAccount = this.userSvc.FindUser(idSought);
                if (UserAccount == null)
                {
                    return HttpNotFound();
                }
                return View(UserAccountConvert(UserAccount));
            }

            // POST: UserAccounts/Edit/5
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit([Bind(Include = "UserKey,Username,FirstName,LastName,DisplayName,IsAccountClosed,IsLoginAllowed,RequiresPasswordReset,Age,Email,IsAccountVerified")] UserAccountInput UserAccountInput)
            {
                if (ModelState.IsValid)
                {
                    Memom.Entities.Models.UserAccount UserAccount = UserAccountConvert(UserAccountInput);
                    UserAccount.LastUpdated = DateTime.Now;
                    try 
                    { 
                        this.userSvc.Update(UserAccount);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                        return View(UserAccountInput);
                    }

                    return RedirectToAction("Index");
                }
                return View(UserAccountInput);
            }

            // GET: UserAccounts/Delete/5
            public ActionResult Delete(int? id)
            {
                int idSought = id ?? 1;

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Memom.Entities.Models.UserAccount UserAccount = this.userSvc.FindUser(idSought);
                if (UserAccount == null)
                {
                    return HttpNotFound();
                }
                return View(UserAccount);
            }

            // POST: UserAccounts/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(int id)
            {
                ViewBag.ErrorMessage = "";
                Memom.Entities.Models.UserAccount UserAccount = this.userSvc.FindUser(id);
                if (UserAccount == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    try
                    {
                        this.userSvc.Delete(id);
                    }
                    catch(Exception ex)
                    {
                        ViewBag.ErrorMessage = ex.Message;
                        return View("CustomError");
                    }
                }
                return RedirectToAction("Index");
            }

            private UserAccountInput UserAccountConvert(Memom.Entities.Models.UserAccount UserAccount)
            {
                UserAccountInput UserAccountInput = new UserAccountInput();
                if (UserAccount!= null)
                {
                    UserAccountInput.UserKey = UserAccount.Key; //.ToString();
                    UserAccountInput.Username = UserAccount.Username;
                    UserAccountInput.FirstName = UserAccount.FirstName;
                    UserAccountInput.LastName = UserAccount.LastName;
                    UserAccountInput.DisplayName = UserAccount.DisplayName;
                    UserAccountInput.Age = UserAccount.Age;
                    UserAccountInput.IsAccountClosed = UserAccount.IsAccountClosed;
                    UserAccountInput.IsLoginAllowed = UserAccount.IsLoginAllowed;
                    UserAccountInput.RequiresPasswordReset = UserAccount.RequiresPasswordReset;
                    UserAccountInput.Email = UserAccount.Email;
                    UserAccountInput.IsAccountVerified = UserAccount.IsAccountVerified;

                }

                return UserAccountInput;
            }

            private Memom.Entities.Models.UserAccount UserAccountConvert(UserAccountInput UserAccountInput)
            {
                Memom.Entities.Models.UserAccount UserAccount = null;

                if (UserAccountInput != null)
                {
                    UserAccount = this.userSvc.FindUser(UserAccountInput.UserKey);

                    //UserAccount.Username = UserAccountInput.Username;
                    UserAccount.FirstName = UserAccountInput.FirstName;
                    UserAccount.LastName = UserAccountInput.LastName;
                    UserAccount.DisplayName = UserAccountInput.DisplayName;
                    UserAccount.Age = UserAccountInput.Age;
                    UserAccount.IsAccountClosed = UserAccountInput.IsAccountClosed;
                    UserAccount.IsLoginAllowed = UserAccountInput.IsLoginAllowed;
                    UserAccount.RequiresPasswordReset = UserAccountInput.RequiresPasswordReset;
                    //UserAccount.Email = UserAccountInput.Email;
                    UserAccount.IsAccountVerified = UserAccountInput.IsAccountVerified;
                    //UserAccount.LastUpdated = System.DateTime.Now;

                }

                return UserAccount;
            }


        }
}
