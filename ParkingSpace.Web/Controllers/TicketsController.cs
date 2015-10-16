using ParkingSpace.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParkingSpace.Web.Controllers {
  public class TicketsController : Controller {

    private App app = new App();

    protected override void Dispose(bool disposing) {
      if (disposing) {
        app.Dispose();
      }
      base.Dispose(disposing);
    }

    public ActionResult Index() {
      var tickets = app.ParkingTickets.ActiveTickets;
      return View(tickets);
    }
  }
}