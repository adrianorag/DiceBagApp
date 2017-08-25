using System;
using System.Collections.Generic;
using WebApi.Application.Interface;
using WebApi.Domain.Services.Interfaces;

namespace WebApi.Application
{
    public class AppServiceBase<TEntity> : IDisposable, IAppServiceBase<TEntity> where TEntity : class
    {
        private readonly IBaseService<TEntity> _serviceBase;

        public AppServiceBase(IBaseService<TEntity> serviceBase)
        {
            _serviceBase = serviceBase;
        }

        public void Add(TEntity obj)
        {
            _serviceBase.Add(obj);
        }

        public void Dispose()
        {
            _serviceBase.Dispose();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _serviceBase.GetAll();
        }

        public TEntity GetById(int id)
        {
            return _serviceBase.GetById(id);
        }

        public void Remove(TEntity obj)
        {
            _serviceBase.Remove(obj);
        }

        public void Update(TEntity obj)
        {
            _serviceBase.Update(obj);
        }
    }
}
