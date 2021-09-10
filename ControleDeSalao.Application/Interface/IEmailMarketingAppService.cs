using ControleDeSalao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Interface
{
    public interface IEmailMarketingAppService : IAppServiceBase<EmailMarketing>
    {
        int Save(EmailMarketing obj);
        IEnumerable<EmailMarketing> GetAll();
        EmailMarketing GetById(int id);
        EmailMarketing GetByEndereco(string endereco);
    }
}
