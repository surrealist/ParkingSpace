﻿using ParkingSpace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParkingSpace.Web.Controllers {

  [RoutePrefix("gate-in")]
  public class GateInController : Controller {
   
    [Route]
    public ActionResult Index() {
      return View();
    }

    public ActionResult CreateTicket(string plateNo) {
      throw new NotImplementedException();
    }

    public ActionResult OpenBarrier() {
      throw new NotImplementedException();
    }

    private void printParkingTicket(ParkingTicket t) {
      //
    }
  }
}