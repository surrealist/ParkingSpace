using GFX.Core;
using ParkingSpace.Models;
using System.Data.Entity;

namespace ParkingSpace.DataAccess {
  public class ParkingTicketRepository : RepositoryBase<ParkingTicket> {

    public ParkingTicketRepository(DbContext context): base(context) {

    }
  }
}
