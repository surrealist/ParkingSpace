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
      Context = this.NewDbContext(); 
    }

    public virtual void Dispose() {
      //foreach (var item in ServiceContainer) {
      //  if (item.Value != null && item.Value is IDisposable) {
      //    ((IDisposable)(item.Value)).Dispose();
      //  }
      //}
      if (Context != null) Context.Dispose();
    }


    public int SaveChanges() {
      if (Context == null) return -1;
      return Context.SaveChanges();
    }

    //protected void AddService<TModel, TService, TRepository>(ContainerBuilder builder)
      //where TModel : class
      //where TService : class, IService<TModel>, new()
      //where TRepository : class, IRepository<TModel>, new() {

     
      //Type key = typeof(TService);

      //if (!ServiceContainer.ContainsKey(key)) {

      //  Lazy<IService<TModel>> obj;
      //  obj = new Lazy<IService<TModel>>(valueFactory: () => {
      //    IService<TModel> x = new TService();
      //    x.Root = this;

      //    if (x.RequiresOwnDbContext) {
      //      x.Context = this.NewDbContext();
      //    }
      //    else {
      //      x.Context = this.Context; // shared
      //    }

      //    x.Repository = new TRepository();
      //    x.Repository.Context = x.Context;
      //    return x;
      //  });

      //  ServiceContainer.Add(key: key, value: obj);
      //}
    //}

    //public TService Services<TModel, TService>()
    //  where TModel : class
    //  where TService : class, IService<TModel> {

    //  //Type key = typeof(TService);

    //  //if (ServiceContainer.ContainsKey(key)) {
    //  //  var lazy = (Lazy<IService<TModel>>)ServiceContainer[key];
    //  //  return lazy.Value as TService;
    //  //}
    //  //else {
    //  //  var s = string.Format("Service '{0}' has not been registered to the App.",
    //  //    typeof(TService).Name);
    //  //  throw new Exception(s);
    //  //}
    //}

  } 
}