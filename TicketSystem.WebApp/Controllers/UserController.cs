using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TicketSystem.BusinessLayer;
using TicketSystem.BusinessLayer.Result;
using TicketSystem.Entities;

namespace TicketSystem.WebApp.Controllers
{
    public class UserController : Controller
    {
        UserManager userManager = new UserManager();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetUserById(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessLayerResult<Users> res = userManager.GetUserById(id.Value);

            if(res.Errors.Count>0)
            {
                res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                return View();
            }
            TempData["showuser"] = res.result;
            return RedirectToAction("ShowProfile","Home");
        }
    }
}