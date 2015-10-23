using ParkingSpace.Models;
using ParkingSpace.Services;
using ParkingSpace.Services.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Should;

namespace ParkingSpace.Facts.Services {
  public class ParkingTicketServiceFacts {

    public class GeneralUsage {
 
    }

    public class CreateParkingTicketMethod {

      private readonly ITestOutputHelper output;

      public CreateParkingTicketMethod(ITestOutputHelper output) {
        this.output = output;
      }

      [Fact]
      public void ReturnParkingTicket() {
        using (var app = new App(testing: true)) {

          var t = app.ParkingTickets.CreateParkingTicket("1122");

          Assert.NotNull(t);
          Assert.Equal("1122", t.PlateNumber);
        }
      }

      [Fact]
      public void NewTicket_HasNoDateOut() {
        using (var app = new App(testing: true)) {
          var s = app.ParkingTickets;

          var dt = DateTime.Now;
          SystemTime.SetNow(dt);

          var t = s.CreateParkingTicket("1122");

          Assert.NotEqual(default(DateTime), t.DateIn);
          Assert.Equal(dt, t.DateIn);
          Assert.Null(t.DateOut);
        }
      }

      [Fact]
      public void NewTicket_HasAutoRunningId() {
        using (var app = new App(testing: true)) {
          var s = app.ParkingTickets;

          var ticket1 = s.CreateParkingTicket("23"); 
          var ticket2 = s.CreateParkingTicket("555"); 
          displayTicket(ticket1); 
          displayTicket(ticket2);

          Assert.NotEqual(ticket2.Id, ticket1.Id);
        }
      }

      [Fact]
      public void NewTicket_UsesGateIdFromService() {
        using (var app = new App(testing: true)) {
          var gateId = app.Settings.Current.GateId; 

          var ticket = app.ParkingTickets.CreateParkingTicket("23");

          Assert.Equal(gateId, ticket.GateId);
        }
      }

      private void displayTicket(ParkingTicket t) {
        output.WriteLine("TICKET:");
        output.WriteLine($"  Id:      {t.Id}");
        output.WriteLine($"  Gate:    {t.GateId}");
        output.WriteLine($"  Plate:   {t.PlateNumber}");
        output.WriteLine($"  Date In: {t.DateIn:s}");
      }


      [Fact] 
      public void NewTicket_HasInsertedToDatabase() { 
        using (var app = new App(testing: true)) { 
          var s = app.ParkingTickets; 
          var t = s.CreateParkingTicket("112233"); 

          var count = s.All().Count(); 

          Assert.Equal(1, count);

          var firstTicket = s.All().FirstOrDefault();
          Assert.Equal("112233", firstTicket.PlateNumber);
        }
      }
    }

    public class CheckoutMethod {

      [Fact]
      public void ShouldStampDateOutToTicket() {
        using (var app = new App(testing: true)) {
          var t1 = app.ParkingTickets.CreateParkingTicket("113");
          var dt = DateTime.Now;
          SystemTime.SetNow(dt);

          app.ParkingTickets.Checkout(t1);

          Assert.NotNull(t1.DateOut);
          t1.DateOut.ShouldEqual(dt);
        }
      }
    }

    public class GetActiveTicketsProperty {

      [Fact]
      public void ShouldReturnOnlyActiveTickets() {
        using (var app = new App(testing: true)) {
          var t1 = app.ParkingTickets.CreateParkingTicket("111");
          var t2 = app.ParkingTickets.CreateParkingTicket("112");
          var t3 = app.ParkingTickets.CreateParkingTicket("113");
          app.ParkingTickets.Checkout(t2);

          IEnumerable<ParkingTicket> tickets = app.ParkingTickets.ActiveTickets;

          tickets.Count().ShouldEqual(2);
          tickets.ShouldNotContain(t2);
          tickets.ShouldContain(t1);
          tickets.ShouldContain(t3);
        }
      }
    }
  }
}
