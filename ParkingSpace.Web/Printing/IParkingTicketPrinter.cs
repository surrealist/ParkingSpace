using ParkingSpace.Models;

namespace ParkingSpace.Web.Printing {
  public interface IParkingTicketPrinter {

    void Print(ParkingTicket ticket, object args = null);

  }
}
