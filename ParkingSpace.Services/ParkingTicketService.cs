// ParkingTicketService
using GFX.Core;
using ParkingSpace.Models;
using ParkingSpace.Services.Core;
using System.Data.Entity;
using System.Linq;
using System;
using System.Collections.Generic;

namespace ParkingSpace.Services {
  public class ParkingTicketService : ServiceBase<App, ParkingTicket> {

    public int GateId { get; set; }
    //  public int NextId { get; set; }

    public override IRepository<ParkingTicket> Repository {
      get; set;
    }

    public IEnumerable<ParkingTicket> ActiveTickets {
      get {
        var q = from t in All()
                where t.DateOut == null
                select t;

        return q.AsEnumerable();
      }
    }

    public ParkingTicketService() {
      GateId = 0;
      // NextId = 1;
    }

    public ParkingTicket CreateParkingTicket(string plateNo) {
      var ticket = new ParkingTicket();

      ticket.PlateNumber = plateNo;
      ticket.DateIn = SystemTime.Now();
      ticket.Id = generateId();
      ticket.GateId = GateId;

      App.ParkingTickets.Add(ticket);
      App.ParkingTickets.SaveChanges();

      return ticket;
    }

    private string generateId() {
      var NextId = 1;
      var maxId = App.ParkingTickets.All().Max(t => t.Id);

      if (maxId != null) {
        NextId = int.Parse(maxId.Substring(maxId.Length - 5)) + 1;
      }

      string s = $"{GateId:00}-{NextId:00000}";

      return s;
    }

    public override ParkingTicket Find(params object[] keys) {
      string key = (string)keys[0];
      return Query(t => t.Id == key).SingleOrDefault();
    }

    public void Checkout(ParkingTicket ticket) {
      ticket.DateOut = SystemTime.Now();
    }
  }
}
