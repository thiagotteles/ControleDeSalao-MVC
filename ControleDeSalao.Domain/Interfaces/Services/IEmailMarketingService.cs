using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Interfaces.Services
{
    public interface IEmailMarketingService : IServiceBase<EmailMarketing>
    {
        int Save(EmailMarketing obj);
        IEnumerable<EmailMarketing> GetAll();
        EmailMarketing GetById(int id);
        EmailMarketing GetByEndereco(string endereco);
    }
}
