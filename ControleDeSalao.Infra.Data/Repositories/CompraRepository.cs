using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Infra.Cross.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Enum = ControleDeSalao.Domain.Enums.Enum;

namespace ControleDeSalao.Infra.Data.Repositories
{
    public class CompraRepository : RepositoryBase<Compra>, ICompraRepository
    {
        public async Task<int> Save(Compra obj)
        {
            if (obj.Id == 0)
            {
                obj.EmpresaId = Cookies.IdEmpresaLogada.Value;
                obj.IdUsuarioCadastro = Cookies.IdUsuarioLogado.Value;
                obj.DataCadastro = DatetimeBrasil.HorarioDeBrasilia();
                obj.Status = Enum.StatusCompra.Aberta;
                Db.Compras.Add(obj);
            }
            else
            {
                var objDb = Db.Compras.Find(obj.Id);
                if (objDb != null)
                {
                    objDb.EmpresaId = obj.EmpresaId;
                    objDb.FornecedorId = obj.FornecedorId;
                    objDb.NumeroNota = obj.NumeroNota;
                    objDb.Data = obj.Data;
                    objDb.ValorTotal = obj.ValorTotal;
                    objDb.Observacao = obj.Observacao;
                    objDb.Status = obj.Status;
                    objDb.IdUsuarioCadastro = obj.IdUsuarioCadastro;
                    objDb.DataAlteracao = DatetimeBrasil.HorarioDeBrasilia();
                    objDb.IdUsuarioAlteracao = Cookies.IdUsuarioLogado.Value;
                }
            }

            await Db.SaveChangesAsync();
            return obj.Id;
        }

        public async Task<IEnumerable<Compra>> GetAll()
        {
            return await Db.Compras.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status != Enum.StatusCompra.Excluida).ToListAsync();
        }

        public async Task<Compra> GetById(int id)
        {
            return await Db.Compras.FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == Cookies.IdEmpresaLogada.Value);
        }

        public async Task<IEnumerable<Compra>> GetByStatus(Enum.StatusCompra status)
        {
            return await Db.Compras.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status == status).ToListAsync();
        }

        public async Task<IEnumerable<Compra>> GetByFornecedorIdAndPeriodo(int fornecedorId, DateTime dataInicial, DateTime dataFinal)
        {
            return await Db.Compras.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status == Enum.StatusCompra.Faturada && m.FornecedorId == fornecedorId && m.Data >= dataInicial && m.Data <= dataFinal).ToListAsync();
        }
    }
}
