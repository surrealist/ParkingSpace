using System.Data.Entity;
using ParkingSpace.Models;

namespace ParkingSpace.DataAccess.Context {
  public class ParkingSpaceDb : DbContext {

    public DbSet<ParkingTicket> ParkingTickets { get; set; }

  }
}
