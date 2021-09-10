using System.Linq;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ControleDeSalao.Infra.Data.Repositories
{
    public class EmailMarketingRepository : RepositoryBase<EmailMarketing>, IEmailMarketingRepository
    {
        public int Save(EmailMarketing obj)
        {
            if (obj.Id == 0)
            {
                Db.EmailsMarketing.Add(obj);
            }
            else
            {
                var objDb = Db.EmailsMarketing.FirstOrDefault(m => m.Id == obj.Id);
                if (objDb != null)
                {
                    objDb.Endereco = obj.Endereco;
                    objDb.Enviado = obj.Enviado;
                    objDb.Visualizou = obj.Visualizou;
                    objDb.UrlRedirect = obj.UrlRedirect;
                    objDb.DataDeVisualizacao = obj.DataDeVisualizacao;
                }
            }

            Db.SaveChangesAsync();
            return obj.Id;
        }
               
        public IEnumerable<EmailMarketing> GetAll()
        {
            return Db.EmailsMarketing.ToList();
        }
               
        public EmailMarketing GetById(int id)
        {
            return Db.EmailsMarketing.FirstOrDefault(m => m.Id == id);
        }
               
        public EmailMarketing GetByEndereco(string endereco)
        {
            return Db.EmailsMarketing.FirstOrDefault(m => m.Endereco == endereco);
        }
    }
}
