using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Infra.Data.Contexto;
using System;

namespace ControleDeSalao.Infra.Data.Repositories
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        protected ControleDeSalaoContext Db = new ControleDeSalaoContext();

        public void Dispose()
        {
            GC.Collect();
        }
    }
}
