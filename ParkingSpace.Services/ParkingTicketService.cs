using System;
using GFX.Core;
using ParkingSpace.Models;
using ParkingSpace.Services.Core;
using System.Data.Entity;
using System.Linq;

namespace ParkingSpace.Services {
  public class ParkingTicketService : ServiceBase<RootClass, ParkingTicket>{

    public int GateId { get; set; }
    public int NextId { get; set; }

    public override IRepository<ParkingTicket> Repository {
      get; set;
    }

    public ParkingTicketService() {
      GateId = 0;
      NextId = 1;
    }

    public ParkingTicket CreateParkingTicket(string plateNo) {
      var ticket = new ParkingTicket();

      ticket.PlateNumber = plateNo;
      ticket.DateIn = SystemTime.Now(); 
      ticket.Id = generateId();
      ticket.GateId = GateId;

      return ticket;
    }

    private string generateId() {
      string s = $"{GateId:00}-{NextId:00000}";
      NextId++;
      return s;
    }

    public override ParkingTicket Find(params object[] keys) {
      string key = (string)keys[0];
      return Query(t => t.Id == key).SingleOrDefault();
    }
  }
}
