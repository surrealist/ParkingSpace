using GFX.Core;
using ParkingSpace.Models;
using System.Data.Entity;

namespace ParkingSpace.DataAccess {
  public class SettingRepository : RepositoryBase<Setting> {
    public SettingRepository(DbContext context) : base(context) {
      //
    }
  }
}
