// GateInController
using System.Web.Mvc;
using ParkingSpace.Models;
using ParkingSpace.Services;
using System;
using ParkingSpace.Web.Printing;
using Rotativa;

namespace ParkingSpace.Web.Controllers {

  [RoutePrefix("gate-in")]
  public class GateInController : Controller {

    private App app;
    private IParkingTicketPrinter printer;

    public GateInController() {
      printer = new PdfParkingTicketPrinter();
      app = new App();
    }

    public GateInController(IParkingTicketPrinter printer, App app) {
      this.printer = printer;
      this.app = app;
    }

    [Route]
    public ActionResult Index() {
      ViewBag.GateId = app.Settings.Current.GateId.ToString("00");
      return View();
    }

    [HttpPost]
    [Route("CreateTicket")]
    public ActionResult CreateTicket(string plateNo) {
      var ticket = app.ParkingTickets.CreateParkingTicket(plateNo);
       
      printer.Print(ticket, this.ControllerContext); 

      TempData["newTicket"] = ticket;
      return RedirectToAction("Index");
    }

    public ActionResult OpenBarrier() {
      throw new NotImplementedException();
    }

  
  }
}