using System;
using System.Linq;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Enum = ControleDeSalao.Domain.Enums.Enum;
using ControleDeSalao.Infra.Cross.Security;

namespace ControleDeSalao.Infra.Data.Repositories
{
    public class ComandaRepository : RepositoryBase<Comanda>, IComandaRepository
    {

        public async Task<int> Save(Comanda obj)
        {
            if (obj.Id == 0)
            {
                obj.EmpresaId = Cookies.IdEmpresaLogada.Value;
                obj.IdUsuarioCadastro = Cookies.IdUsuarioLogado.Value;
                obj.DataCadastro = DatetimeBrasil.HorarioDeBrasilia();
                obj.Status = Enum.StatusComanda.Aberta;
                Db.Comandas.Add(obj);
            }
            else
            {
                var objDb = Db.Comandas.Find(obj.Id);
                if (objDb != null)
                {
                    objDb.ProfissionalId = obj.ProfissionalId;
                    objDb.ClienteId = obj.ClienteId;
                    objDb.Data = obj.Data;
                    objDb.ValorTotal = obj.ValorTotal;
                    objDb.ValorDesconto = obj.ValorDesconto;
                    objDb.DataCadastro = obj.DataCadastro;
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


        public async Task<IEnumerable<Comanda>> GetAll()
        {
            return await Db.Comandas.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status != Enum.StatusComanda.Excluida).ToListAsync();
        }

        public async Task<Comanda> GetById(int id)
        {
            return await Db.Comandas.FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == Cookies.IdEmpresaLogada.Value);
        }

        public async Task<IEnumerable<Comanda>> GetByProfissionalID(int profissionalID)
        {
            return await Db.Comandas.Where(m => m.ProfissionalId == profissionalID && m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status != Enum.StatusComanda.Excluida).ToListAsync();
        }

        public async Task<IEnumerable<Comanda>> GetByStatusAndData(Enum.StatusComanda status, DateTime? data)
        {
            return await Db.Comandas.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status == status && (m.Data == data || data == null)).ToListAsync();
        }

        public async Task<IEnumerable<Comanda>> GetByStatusAndPeriodo(Enum.StatusComanda status, DateTime dataInicial, DateTime dataFinal)
        {
            return await Db.Comandas.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status == status && m.Data >= dataInicial && m.Data <= dataFinal).ToListAsync();
        }

        public async Task<IEnumerable<Comanda>> GetByClienteIdStatusAndPeriodo(int clienteId, Enum.StatusComanda status, DateTime dataInicial, DateTime dataFinal)
        {
            return await Db.Comandas.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status == status && m.Data >= dataInicial && m.Data <= dataFinal && m.ClienteId == clienteId).ToListAsync();
        }

        public async Task<IEnumerable<Comanda>> GetByProfissionalIdStatusAndPeriodo(int profissionalId, Enum.StatusComanda status, DateTime dataInicial, DateTime dataFinal)
        {
            return await Db.Comandas.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status == status && m.Data >= dataInicial && m.Data <= dataFinal && m.ProfissionalId == profissionalId).ToListAsync();
        }

        public async Task<IEnumerable<Comanda>> GetDescontoByStatusAndPeriodo(Enum.StatusComanda status, DateTime dataInicial, DateTime dataFinal)
        {
            return await Db.Comandas.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.ValorDesconto.HasValue && m.ValorDesconto.Value > 0 && m.Status == status && m.Data >= dataInicial && m.Data <= dataFinal).ToListAsync();
        }
    }
}
