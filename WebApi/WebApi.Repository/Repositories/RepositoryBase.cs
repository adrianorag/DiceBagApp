using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebApi.Domain.Services.Interfaces;
using WebApi.Repository.Entities.Context;

namespace WebApi.Repository.Repositories
{
    public class RepositoryBase<TEntity> : IDisposable, IBaseService<TEntity> where TEntity : class
    {
        protected WebApiContext Db = new WebApiContext();
        public void Add(TEntity obj)
        {
            Db.Set<TEntity>().Add(obj);
            Db.SaveChanges();
        }


        public IEnumerable<TEntity> GetAll()
        {
            return Db.Set<TEntity>().ToList();
        }

        //todo: implementar no futuro
        public IEnumerable<TEntity> GetAllNoTracking()
        {
            return Db.Set<TEntity>().AsNoTracking().ToList();
        }


        public TEntity GetById(int id)
        {
            return Db.Set<TEntity>().Find(id);
        }

        public void Remove(TEntity obj)
        {
            Db.Set<TEntity>().Remove(obj);
            Db.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            Db.Entry(obj).State = EntityState.Modified;
            Db.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    
    }
}
