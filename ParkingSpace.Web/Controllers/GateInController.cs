using ParkingSpace.Models;
using ParkingSpace.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParkingSpace.Web.Controllers {

  [RoutePrefix("gate-in")]
  public class GateInController : Controller {

    private static ParkingTicketService service;

    static GateInController() {
      service = new ParkingTicketService();
    }

    [Route]
    public ActionResult Index() {
      return View();
    }

    [HttpPost]
    [Route("CreateTicket")]
    public ActionResult CreateTicket(string plateNo) {
      var ticket = service.CreateParkingTicket(plateNo);
      printParkingTicket(ticket);

      TempData["newTicket"] = ticket;
      return RedirectToAction("Index");
    }

    public ActionResult OpenBarrier() {
      throw new NotImplementedException();
    }

    private void printParkingTicket(ParkingTicket t) {
      //
    }
  }
}