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
    public class FornecedorRepository : RepositoryBase<Fornecedor>, IFornecedorRepository
    {
        public async Task<int> Save(Fornecedor obj)
        {
            if (obj.Id == 0)
            {
                obj.EmpresaId = Cookies.IdEmpresaLogada.Value;
                obj.IdUsuarioCadastro = Cookies.IdUsuarioLogado.Value;
                obj.DataCadastro = DatetimeBrasil.HorarioDeBrasilia();
                obj.Status = true;
                Db.Fornecedores.Add(obj);
            }
            else
            {
                var objDb = Db.Fornecedores.Find(obj.Id);
                if (objDb != null)
                {
                    objDb.Nome = obj.Nome;
                    objDb.CPF = obj.CPF;
                    objDb.CNPJ = obj.CNPJ;
                    objDb.IE = obj.IE;
                    objDb.Telefone = obj.Telefone;
                    objDb.Celular = obj.Celular;
                    objDb.Email = obj.Email;
                    objDb.Endereco = obj.Endereco;
                    objDb.Latitude = obj.Latitude;
                    objDb.Longitude = obj.Longitude;
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

        public async Task<IEnumerable<Fornecedor>> GetAll()
        {
            return await Db.Fornecedores.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status).ToListAsync();
        }

        public async Task<IEnumerable<Fornecedor>> AutoComplete(string nome)
        {
            return await Db.Fornecedores.Where(m => m.Nome.ToLower().Contains(nome) && m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status).Take(10).ToListAsync();
        }

        public async Task<Fornecedor> GetById(int id)
        {
            return await Db.Fornecedores.FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == Cookies.IdEmpresaLogada.Value);
        }

        public async Task<Fornecedor> GetByNome(string nome)
        {
            return await Db.Fornecedores.FirstOrDefaultAsync(m => m.Nome == nome && m.EmpresaId == Cookies.IdEmpresaLogada.Value);
        }

    }
}
