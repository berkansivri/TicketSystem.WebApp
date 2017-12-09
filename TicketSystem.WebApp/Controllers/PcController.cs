using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TicketSystem.BusinessLayer;
using TicketSystem.Entities;
using TicketSystem.WebApp.Filters;

namespace TicketSystem.WebApp.Controllers
{
    public class PcController : Controller
    {
        private TicketManager ticketManager = new TicketManager();
        private PcDetailManager pcDetailManager = new PcDetailManager();
        private UserManager userManager = new UserManager();


        public ActionResult Index()
        {
            return View();
        }

        [Auth]
        public ActionResult GetPcDetail(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PcDetail pc = pcDetailManager.Find(x => x.Owner.Id == id);
            if(pc==null)
            {
                return HttpNotFound();
            }

            return PartialView("_PartialPcDetail",pc);
        }
    }
}