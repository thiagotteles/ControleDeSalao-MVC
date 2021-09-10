﻿using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Interface
{
    public interface IProdutoComandaAppService : IAppServiceBase<ProdutoComanda>
    {
        Task<int> Save(ProdutoComanda obj);
        Task<ProdutoComanda> GetById(int id);
        Task<IEnumerable<ProdutoComanda>> GetByComandaId(int comandaId);
        Task<int> Remove(ProdutoComanda obj);
        Task<IEnumerable<ProdutoComanda>> GetByYear(int year);
        Task<IEnumerable<ProdutoComanda>> GetByMonth(int year, int month);
    }
}
