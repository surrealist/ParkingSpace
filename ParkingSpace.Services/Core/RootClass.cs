using System;
using System.Collections.Generic;

using System.Data.Entity;
using Autofac;

namespace GFX.Core {
  public abstract class RootClass : IDisposable {

    public DbContext Context { get; set; } 
    protected bool IsTesting { get; set; }
    protected abstract DbContext NewDbContext();
    protected abstract void RegisterServices(ContainerBuilder builder);
    protected abstract void RegisterServicesForUnitTests(ContainerBuilder builder);

    protected readonly IContainer container;

    public RootClass(bool testing = false) {
      IsTesting = testing;

      var builder = new ContainerBuilder();

      if (testing) {
        RegisterServicesForUnitTests(builder);
      }
      else {
        RegisterServices(builder);
      }

      container = builder.Build();
    }

    public virtual void Dispose() {
      //
    }


    public int SaveChanges() {
      if (Context == null) return -1;
      return Context.SaveChanges();
    }

  } 
}