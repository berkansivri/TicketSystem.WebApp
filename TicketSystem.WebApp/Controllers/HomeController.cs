using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketSystem.BusinessLayer;
using TicketSystem.BusinessLayer.Result;
using TicketSystem.Entities;
using TicketSystem.Entities.ValueObjects;
using TicketSystem.WebApp.Filters;
using TicketSystem.WebApp.Models;

namespace TicketSystem.WebApp.Controllers
{
    [Exc]
    public class HomeController : Controller
    {
        TicketManager ticketManager = new TicketManager();
        UserManager userManager = new UserManager();

        public ActionResult Index()
        {
            return View(ticketManager.ListQueryable().OrderByDescending(x => x.ModifiedOn).ToList());
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Users> res = userManager.Login(model);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
                CurrentSession.Set<Users>("login", res.result);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Users> res = userManager.Register(model);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
                CurrentSession.Set<Users>("login", res.result);
                return RedirectToAction("Index");
            }
            return View();
        }

        [Auth]
        public ActionResult ShowProfile()
        {
            if(TempData["showuser"]!=null)
            {
                return View(TempData["showuser"] as Users);
            }
            BusinessLayerResult<Users> res = userManager.GetUserById(CurrentSession.user.Id);

            if (res.Errors.Count > 0)
            {
                res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                return RedirectToAction("Login");
            }

            return View(res.result);
        }

        [Auth]
        [HttpGet]
        public ActionResult EditProfile()
        {
            BusinessLayerResult<Users> res = userManager.GetUserById(CurrentSession.user.Id);
            if (res.Errors.Count > 0)
            {
                res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                return View();
            }
            return View(res.result);
        }

        [Auth]
        [HttpPost]
        public ActionResult EditProfile(Users model)
        {
            ModelState.Remove("ModifiedUser");
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Users> res = userManager.UpdateProfile(model);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }

                CurrentSession.Set<Users>("login", res.result);
                return RedirectToAction("ShowProfile");
            }

            return View(model);
        }

        [Auth]
        public ActionResult RemoveProfile()
        {
            BusinessLayerResult<Users> res = userManager.RemoveUserById(CurrentSession.user.Id);

            if(res.Errors.Count > 0)
            {
                res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                return HttpNotFound();
            }

            CurrentSession.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult Filter(int? id)
        {
            if(id==null)
            {
                return View("Index");
            }
            List<Ticket> list = new List<Ticket>();
            switch (id)
            {
                case 2: list = ticketManager.ListQueryable().Where(x => x.IsSolved == true).OrderByDescending(x => x.ModifiedOn).ToList();
                        break;
                case 3: list = ticketManager.ListQueryable().Where(x => x.IsSolved == false).OrderByDescending(x => x.ModifiedOn).ToList();
                    break;
                default: list = ticketManager.List();
                    break;
            }

            return View("Index",list);
        }
    }
}