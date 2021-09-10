using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ControleDeSalao.Infra.Data.Repositories
{
    public class EmpresaRepository : RepositoryBase<Empresa>, IEmpresaRepository
    {
        public async Task<int> Save(Empresa obj)
        {
            if (obj.Id == 0)
            {
                obj.QtdSMSBonus = 0;
                obj.QtdSMSPago = 0;
                Db.Empresas.Add(obj);
            }
            else
            {
                var objDb = Db.Empresas.Find(obj.Id);
                if (objDb != null)
                {
                    objDb.PlanoId = obj.PlanoId;
                    objDb.Nome = obj.Nome;
                    objDb.Email = obj.Email;
                    objDb.Telefone = obj.Telefone;
                    objDb.Celular = obj.Celular;
                    objDb.Endereco = obj.Endereco;
                    objDb.Latitude = obj.Latitude;
                    objDb.Longitude = obj.Longitude;
                    objDb.PassoWizard = obj.PassoWizard;
                    objDb.NomeResponsavel = obj.NomeResponsavel;
                    objDb.CPFResponsavel = obj.CPFResponsavel;
                    objDb.DataBloqueio = obj.DataBloqueio;
                    objDb.DataAlerta = obj.DataAlerta;
                    objDb.DiaParaVencimento = obj.DiaParaVencimento;
                    objDb.CodigoPromocional = obj.CodigoPromocional;
                    objDb.QtdSMSBonus = obj.QtdSMSBonus;
                    objDb.QtdSMSPago = obj.QtdSMSPago;
                    objDb.SmsAgenda = string.IsNullOrEmpty(obj.SmsAgenda) ? objDb.SmsAgenda : obj.SmsAgenda;
                    objDb.SmsAniversario = string.IsNullOrEmpty(obj.SmsAniversario) ? objDb.SmsAniversario : obj.SmsAniversario;
                }
            }

            await Db.SaveChangesAsync();
            return obj.Id;
        }

        public async Task<Empresa> GetById(int id)
        {
            return await Db.Empresas.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
