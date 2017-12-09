using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TicketSystem.BusinessLayer;
using TicketSystem.BusinessLayer.Result;
using TicketSystem.Entities;
using TicketSystem.WebApp.Filters;
using TicketSystem.WebApp.Models;

namespace TicketSystem.WebApp.Controllers
{
    public class TicketController : Controller
    {
        TicketManager ticketManager = new TicketManager();
        // GET: Ticket
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(Ticket ticket, HttpPostedFileBase ticketImg)
        {
            ModelState.Remove("Owner");
            if (ModelState.IsValid)
            {
                if (ticketImg != null && (ticketImg.ContentType == "image/jpeg" || ticketImg.ContentType == "image/jpg" || ticketImg.ContentType == "image/png"))
                {
                    string filename = $"ticket_{ticket.Id}.{ticketImg.ContentType.Split('/')[1]}";
                    ticketImg.SaveAs(Server.MapPath($"~/Images/{filename}"));
                    ticket.Img = filename;
                }
                ticket.Owner = CurrentSession.user;
                ticket.IsSolved = true;
                ticketManager.Insert(ticket);

                return RedirectToAction("Index","Home");
            }
            return View(ticket);
        }

        [Auth]
        public ActionResult GetTicketsById(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Ticket> list = ticketManager.ListQueryable().Where(x => x.Owner.Id == id).ToList();
            
            if(list==null)
            {
                return HttpNotFound();
            }

            return PartialView("_PartialTickets", list);
        }

        public ActionResult GetDetailsById(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ticket ticket = ticketManager.Find(x => x.Id == id);
            if(ticket==null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialTicketDetail",ticket);
        }

        public ActionResult IsSolved(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }
            Ticket ticket = ticketManager.Find(x => x.Id == id);
            if(ticket==null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialSolvedButton",ticket);
        }

        [Auth]
        [HttpPost]
        public ActionResult Solving(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }
            Ticket ticket = ticketManager.Find(x => x.Id == id);
            if (ticket == null)
            {
                return HttpNotFound();
            }

            ticket.IsSolved = !ticket.IsSolved;
            int result = ticketManager.Update(ticket);

            if (result > 0)
                return PartialView("_PartialSolvedButton", ticket);

            return View();
        }
    }
}