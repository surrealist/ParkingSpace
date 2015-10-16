using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Autofac;
using ParkingSpace.Services;
using ParkingSpace.Web.Printing;
using Autofac.Integration.Mvc;

namespace ParkingSpace.Web {
  public class MvcApplication : System.Web.HttpApplication {
    protected void Application_Start() {

      registerAutofac();

      AreaRegistration.RegisterAllAreas();
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);
    }

    private void registerAutofac() {
      var builder = new ContainerBuilder();

      builder.RegisterControllers(typeof(MvcApplication).Assembly);

      builder.RegisterType<App>();
      builder.RegisterType<PdfParkingTicketPrinter>()
             .As<IParkingTicketPrinter>();

      var container = builder.Build();
      DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
    }
  }
}
