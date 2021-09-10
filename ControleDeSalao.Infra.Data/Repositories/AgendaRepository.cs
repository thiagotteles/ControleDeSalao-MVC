using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Infra.Cross.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeSalao.Infra.Data.Repositories
{
    public class AgendaRepository : RepositoryBase<Agenda>, IAgendaRepository
    {
        public async Task<int> Save(Agenda obj)
        {
            if (obj.Id == 0)
            {
                obj.EmpresaId = Cookies.IdEmpresaLogada.Value;
                obj.IdUsuarioCadastro = Cookies.IdUsuarioLogado.Value;
                obj.DataCadastro = DatetimeBrasil.HorarioDeBrasilia();
                obj.Status = true;
                Db.Agendas.Add(obj);
            }
            else
            {
                var objDb = Db.Agendas.Find(obj.Id);
                if (objDb != null)
                {
                    objDb.EmpresaId = obj.EmpresaId;
                    objDb.ProfissionalId = obj.ProfissionalId;
                    objDb.ServicoId = obj.ServicoId;
                    objDb.ClienteId = obj.ClienteId;
                    objDb.ComandaId = obj.ComandaId;
                    objDb.Data = obj.Data;
                    objDb.Valor = obj.Valor;
                    objDb.HoraInicial = obj.HoraInicial;
                    objDb.HoraFinal = obj.HoraFinal;
                    objDb.Observacao = obj.Observacao;
                    objDb.Status = obj.Status;
                    objDb.EnviarSMS = obj.EnviarSMS;
                    objDb.EnviouSMS = obj.EnviouSMS;
                    objDb.IdUsuarioCadastro = obj.IdUsuarioCadastro;
                    objDb.IdUsuarioAlteracao = Cookies.IdUsuarioLogado.Value;
                    objDb.DataAlteracao = DatetimeBrasil.HorarioDeBrasilia();
                }
            }
            await Db.SaveChangesAsync();
            return obj.Id;
        }

        public async Task<IEnumerable<Agenda>> GetAll()
        {
            return await Db.Agendas.Where(m => m.Status && m.EmpresaId == Cookies.IdEmpresaLogada.Value).ToListAsync();
        }

        public async Task<Agenda> GetById(int id)
        {
            return await Db.Agendas.FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == Cookies.IdEmpresaLogada.Value);
        }

        public async Task<IEnumerable<Agenda>> GetByData(DateTime data)
        {
            return await Db.Agendas.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status == true && m.Data == data).ToListAsync();
        }

        public async Task<IEnumerable<Agenda>> GetByPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            return await Db.Agendas.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status && m.Data >= dataInicial && m.Data <= dataFinal).ToListAsync();
        }

        public async Task<IEnumerable<Agenda>> GetByProfissionalIdAndData(int profissionalId, DateTime data)
        {
            return await Db.Agendas.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status && m.ProfissionalId == profissionalId && m.Data == data).ToListAsync();
        }

        public IEnumerable<Agenda> GetByProfissionalIdAndDataNotAsync(int profissionalId, DateTime data)
        {
            return Db.Agendas.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status && m.ProfissionalId == profissionalId && m.Data == data).ToList();
        }

        public async Task<IEnumerable<Agenda>> GetByProfissionalIdAndPeriodo(int profissionalId, System.DateTime dataInicial, System.DateTime dataFinal)
        {
            return await Db.Agendas.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status && m.ProfissionalId == profissionalId && m.Data >= dataInicial && m.Data <= dataFinal).ToListAsync();
        }

        public async Task<IEnumerable<Agenda>> GetByComandaId(int comandaId)
        {
            return await Db.Agendas.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status && m.ComandaId == comandaId).ToListAsync();
        }
    }
}
