using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpace.Models {
  public class ParkingTicket {

    public string Id { get; set; }
    public string PlateNumber { get; set; }

    public int GateId { get; set; }

    public DateTime DateIn { get; set; }
    public DateTime? DateOut { get; set; }

  }
}
