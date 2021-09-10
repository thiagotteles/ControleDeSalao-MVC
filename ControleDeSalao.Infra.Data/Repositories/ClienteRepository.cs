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
    public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
    {
        public async Task<int> Save(Cliente obj)
        {
            if (obj.Id == 0)
            {
                obj.EmpresaId = Cookies.IdEmpresaLogada.Value;
                obj.IdUsuarioCadastro = Cookies.IdUsuarioLogado.Value;
                obj.DataCadastro = DatetimeBrasil.HorarioDeBrasilia();
                obj.Status = true;
                Db.Clientes.Add(obj);
            }
            else
            {
                var objDb = Db.Clientes.Find(obj.Id);
                if (objDb != null)
                {
                    objDb.Nome = obj.Nome;
                    objDb.CPF = obj.CPF;
                    objDb.CNPJ = obj.CNPJ;
                    objDb.IE = obj.IE;
                    objDb.Telefone = obj.Telefone;
                    objDb.Celular = obj.Celular;
                    objDb.Email = obj.Email;
                    objDb.DataNascimento = obj.DataNascimento;
                    objDb.Endereco = obj.Endereco;
                    objDb.Latitude = obj.Latitude;
                    objDb.Longitude = obj.Longitude;
                    objDb.ValorCredito = obj.ValorCredito;
                    objDb.ValorDebito = obj.ValorDebito;
                    objDb.DataNascimento = obj.DataNascimento;
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

        public async Task<IEnumerable<Cliente>> GetAll()
        {
            return await Db.Clientes.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status).ToListAsync();
        }

        public async Task<IEnumerable<Cliente>> AutoComplete(string nome)
        {
            return await Db.Clientes.Where(m => m.Nome.ToLower().Contains(nome) && m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status).Take(10).ToListAsync();
        }

        public async Task<Cliente> GetById(int id)
        {
            return await Db.Clientes.FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == Cookies.IdEmpresaLogada.Value);
        }

        public async Task<Cliente> GetByNome(string nome)
        {
            return await Db.Clientes.FirstOrDefaultAsync(m => m.Nome == nome && m.EmpresaId == Cookies.IdEmpresaLogada.Value);
        }
    }
}
