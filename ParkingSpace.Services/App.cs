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
      return container.Resolve<DbContext>();
    }

    protected override void RegisterServices(ContainerBuilder builder) {

      builder.RegisterType<App>().As<RootClass>().SingleInstance();
      builder.RegisterType<ParkingSpaceDb>().As<DbContext>().SingleInstance();

      builder.RegisterType<ParkingTicketRepository>()
             .As<IRepository<ParkingTicket>>().SingleInstance();

      builder.RegisterType<SettingRepository>()
             .As<IRepository<Setting>>().SingleInstance();

      // services
      builder.RegisterType<ParkingTicketService>()
             .As<IService<ParkingTicket>>()
             .AsSelf().SingleInstance();

      builder.RegisterType<SettingService>()
             .As<IService<Setting>>()
             .AsSelf().SingleInstance();
    }

    protected override void RegisterServicesForUnitTests(ContainerBuilder builder) {

      builder.RegisterType<App>().As<RootClass>().SingleInstance();
      builder.RegisterType<ParkingSpaceDb>().As<DbContext>().SingleInstance();

      builder.RegisterType<FakeRepository<ParkingTicket>>()
             .As<IRepository<ParkingTicket>>()
             .SingleInstance();

      builder.RegisterType<FakeRepository<Setting>>()
             .As<IRepository<Setting>>()
             .SingleInstance();

      // services
      builder.RegisterType<ParkingTicketService>()
             .As<IService<ParkingTicket>>()
             .AsSelf()
             .SingleInstance();

      builder.RegisterType<SettingService>()
             .As<IService<Setting>>()
             .AsSelf()
             .SingleInstance();
    }

    public ParkingTicketService ParkingTickets {
      get {
        return service<ParkingTicketService>();
      }
    }

    public SettingService Settings {
      get {
        return service<SettingService>();
      }
    }
    
    private T service<T>() where T : IService {
      var s = container.Resolve<T>();
      s.Root = this;
      return s;
    }
  }
}