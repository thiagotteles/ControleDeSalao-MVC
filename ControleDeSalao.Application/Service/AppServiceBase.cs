using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Interfaces.Services;
using System;

namespace ControleDeSalao.Application.Service
{
    public class AppServiceBase<TEntity> : IDisposable, IAppServiceBase<TEntity> where TEntity : class
    {
        private readonly IServiceBase<TEntity> _serviceBase;

        public AppServiceBase(IServiceBase<TEntity> serviceBase)
        {
            _serviceBase = serviceBase;
        }

        public void Dispose()
        {
        }
    }
}
