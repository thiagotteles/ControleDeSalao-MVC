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
    public class ProfissionalRepository : RepositoryBase<Profissional>, IProfissionalRepository
    {
        public async Task<int> Save(Profissional obj)
        {
            if (obj.Id == 0)
            {
                obj.EmpresaId = Cookies.IdEmpresaLogada.Value;
                obj.IdUsuarioCadastro = Cookies.IdUsuarioLogado.Value;
                obj.DataCadastro = DatetimeBrasil.HorarioDeBrasilia();
                obj.Status = true;
                Db.Profissionais.Add(obj);
            }
            else
            {
                var objDb = Db.Profissionais.Find(obj.Id);
                if (objDb != null)
                {
                    objDb.Nome = obj.Nome;
                    objDb.EmpresaId = obj.EmpresaId;
                    objDb.UsuarioId = obj.UsuarioId;
                    objDb.DisponibilidadeId = obj.DisponibilidadeId;
                    objDb.CPF = obj.CPF;
                    objDb.RG = obj.RG;
                    objDb.Telefone = obj.Telefone;
                    objDb.Celular = obj.Celular;
                    objDb.Email = obj.Email;
                    objDb.Endereco = obj.Endereco;
                    objDb.Latitude = obj.Latitude;
                    objDb.Longitude = obj.Longitude;
                    objDb.DataContratacao = obj.DataContratacao;
                    objDb.Comissao = obj.Comissao;
                    objDb.Salario = obj.Salario;
                    objDb.GerarAgenda = obj.GerarAgenda;
                    objDb.FotoPerfil = obj.FotoPerfil;
                    objDb.Observacao = obj.Observacao;
                    objDb.Status = obj.Status;
                    objDb.IdUsuarioCadastro = obj.IdUsuarioCadastro;
                    objDb.IdUsuarioAlteracao = Cookies.IdUsuarioLogado.Value;
                    objDb.DataAlteracao = DatetimeBrasil.HorarioDeBrasilia();
                }
            }

            await Db.SaveChangesAsync();
            return obj.Id;
        }

        public async Task<IEnumerable<Profissional>> GetAll()
        {
            return await Db.Profissionais.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status).ToListAsync();
        }

        public async Task<IEnumerable<Profissional>> AutoComplete(string nome)
        {
            return await Db.Profissionais.Where(m => m.Nome.ToLower().Contains(nome) && m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status).Take(10).ToListAsync();
        }

        public async Task<Profissional> GetById(int id)
        {
            return await Db.Profissionais.FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == Cookies.IdEmpresaLogada.Value);
        }

        public async Task<Profissional> GetByNome(string nome)
        {
            return await Db.Profissionais.FirstOrDefaultAsync(m => m.Nome == nome && m.EmpresaId == Cookies.IdEmpresaLogada.Value);
        }

        public async Task<IEnumerable<Profissional>> GetComAgenda()
        {
            return await Db.Profissionais.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status && m.GerarAgenda).ToListAsync();
        }
    }
}
