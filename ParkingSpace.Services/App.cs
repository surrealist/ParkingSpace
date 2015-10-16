// App.cs

using GFX.Core;
using System.Data.Entity;
using ParkingSpace.DataAccess.Context;
using ParkingSpace.DataAccess;
using ParkingSpace.Models;
using Autofac;

namespace ParkingSpace.Services {
  public class App : RootClass {

    public bool TestingMode { get; private set; }


    public App(bool testing = false)
      : base(testing) {

      this.TestingMode = testing;
    }

    protected override DbContext NewDbContext() {
      return new ParkingSpaceDb();
    }

    protected override void RegisterServices(ContainerBuilder builder) {

      builder.RegisterType<App>().As<RootClass>();
      builder.RegisterType<ParkingSpaceDb>().As<DbContext>();

      builder.RegisterType<ParkingTicketRepository>()
             .As<RepositoryBase<ParkingTicket>>()
             .As<IRepository<ParkingTicket>>();

      builder.RegisterType<SettingRepository>()
             .As<RepositoryBase<Setting>>()
             .As<IRepository<Setting>>();

      // services
      builder.RegisterType<ParkingTicketService>()
             .As<ServiceBase<ParkingTicket>>()
             .As<IService<ParkingTicket>>()
             .AsSelf();

      builder.RegisterType<SettingService>()
             .As<ServiceBase<Setting>>()
             .As<IService<Setting>>()
             .AsSelf();
    }

    protected override void RegisterServicesForUnitTests(ContainerBuilder builder ) {
      //this.AddService<ParkingTicket, ParkingTicketService, FakeRepository<ParkingTicket>>(builder);
      //this.AddService<Setting, SettingService, FakeRepository<Setting>>(builder);
    }

    public ParkingTicketService ParkingTickets {
      get {
        return container.Resolve<ParkingTicketService>(); 
      }
    }

    public SettingService Settings {
      get {
        return container.Resolve<SettingService>();
      }
    }

  }
}