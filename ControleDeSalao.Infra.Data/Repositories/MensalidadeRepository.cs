using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Infra.Cross.Security;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeSalao.Infra.Data.Repositories
{
    public class MensalidadeRepository : RepositoryBase<Mensalidade>, IMensalidadeRepository
    {
        public async Task<int> Save(Mensalidade obj)
        {
            if (obj.Id == 0)
            {
                Db.Mensalidades.Add(obj);
            }
            else
            {
                var objDb = Db.Mensalidades.Find(obj.Id);
                if (objDb != null)
                {
                    objDb.DataVencimento = obj.DataVencimento;
                    objDb.Valor = obj.Valor;
                    objDb.DataPagamento = obj.DataPagamento;
                    objDb.ValorPago = obj.ValorPago;
                    objDb.SacadoCPF = obj.SacadoCPF;
                    objDb.SacadoNome = obj.SacadoNome;
                    objDb.ArquivoRetorno = obj.ArquivoRetorno;
                }
            }

            await Db.SaveChangesAsync();
            return obj.Id;
        }

        public async Task<IEnumerable<Mensalidade>> GetAll()
        {
            return await Db.Mensalidades.ToListAsync();
        }

        public async Task<IEnumerable<Mensalidade>> GetByEmpresaId(int empresaId)
        {
            return await Db.Mensalidades.Where(m => m.EmpresaId == empresaId).ToListAsync();
        }

        public async Task<Mensalidade> GetById(int id)
        {
            return await Db.Mensalidades.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
