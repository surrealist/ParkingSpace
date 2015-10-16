﻿using System;
using System.Linq;

using System.Data.Entity;

namespace GFX.Core {
  public abstract class ServiceBase<T> : IService<T> 
    where T : class {
    

    public ServiceBase(RootClass root, DbContext context, IRepository<T> repo) {
      Root = root;
      Context = context;
      Repository = repo;
    }

    public RootClass Root { get; set; }
    public DbContext Context { get; set; }

    public RootClass App { get { return Root; } }

    public abstract IRepository<T> Repository { get; set; }

    public virtual IQueryable<T> All() {
      return Query(_ => true);
    }

    public abstract T Find(params object[] keys);

    public virtual IQueryable<T> Query(Func<T, bool> predicate) {
      if (Repository == null) return null;
      return Repository.Query(predicate);
    }


    public virtual T Add(T item) {
      if (Repository == null) return null;
      return Repository.Add(item);
    }

    public virtual void Remove(T item) {
      if (Repository == null) return;
      Repository.Remove(item);
    }

    public virtual int SaveChanges() {
      if (Repository == null) return -1;
      return Repository.SaveChanges();
    }

    public virtual bool RequiresOwnDbContext {
      get { return false; }
    }

    public virtual void SetModified(T item) {
      this.Context.Entry(item).State = EntityState.Modified;
    }

    public virtual void SetAdded(T item) {
      this.Context.Entry(item).State = EntityState.Added;
    }

  }
}