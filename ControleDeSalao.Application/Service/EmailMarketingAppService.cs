using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Application.Service
{
    public class EmailMarketingAppService : AppServiceBase<EmailMarketing>, IEmailMarketingAppService
    {
        private readonly IEmailMarketingService _service;

        public EmailMarketingAppService(IEmailMarketingService service)
            : base(service)
        {
            _service = service;
        }

        public int Save(EmailMarketing obj)
        {
            return _service.Save(obj);
        }

        public IEnumerable<EmailMarketing> GetAll()
        {
            return _service.GetAll();
        }

        public EmailMarketing GetById(int id)
        {
            return _service.GetById(id);
        }

        public EmailMarketing GetByEndereco(string endereco)
        {
            return _service.GetByEndereco(endereco);
        }
    }
}
