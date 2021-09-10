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
    public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
    {
        public async Task<int> Save(Produto obj)
        {
            if (obj.Id == 0)
            {
                obj.EmpresaId = Cookies.IdEmpresaLogada.Value;
                obj.IdUsuarioCadastro = Cookies.IdUsuarioLogado.Value;
                obj.DataCadastro = DatetimeBrasil.HorarioDeBrasilia();
                obj.Status = true;
                Db.Produtos.Add(obj);
            }
            else
            {
                var objDb = Db.Produtos.Find(obj.Id);
                if (objDb != null)
                {
                    objDb.Descricao = obj.Descricao;
                    objDb.Codigo = obj.Codigo;
                    objDb.EAN = obj.EAN;
                    objDb.PrecoCusto = obj.PrecoCusto;
                    objDb.PrecoVenda = obj.PrecoVenda;
                    objDb.Unidade = obj.Unidade;
                    objDb.QtdEstoque = obj.QtdEstoque;
                    objDb.QtdMinEstoque = obj.QtdMinEstoque;
                    objDb.ValorParaProfissional = obj.ValorParaProfissional;
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

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await Db.Produtos.Where(m => m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status).ToListAsync();
        }

        public async Task<IEnumerable<Produto>> AutoComplete(string nome)
        {
            return await Db.Produtos.Where(m => m.Descricao.ToLower().Contains(nome) && m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status).Take(10).ToListAsync();
        }

        public async Task<Produto> GetById(int id)
        {
            return await Db.Produtos.FirstOrDefaultAsync(m => m.Id == id && m.EmpresaId == Cookies.IdEmpresaLogada.Value);
        }

        public async Task<Produto> GetByNome(string nome)
        {
            return await Db.Produtos.FirstOrDefaultAsync(m => m.Descricao == nome && m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status);
        }

        public async Task<IEnumerable<Produto>> GetComPoucoEstoque()
        {
            return await Db.Produtos.Where(m => m.QtdMinEstoque.HasValue && m.QtdEstoque.HasValue && m.QtdMinEstoque.Value >= m.QtdEstoque.Value && m.EmpresaId == Cookies.IdEmpresaLogada.Value && m.Status).ToListAsync();
        }
    }
}
