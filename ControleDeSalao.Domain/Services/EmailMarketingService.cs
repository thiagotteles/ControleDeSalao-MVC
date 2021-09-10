using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using ControleDeSalao.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Services
{
    public class EmailMarketingService : ServiceBase<EmailMarketing>, IEmailMarketingService
    {
        private readonly IEmailMarketingRepository _repository;

        public EmailMarketingService(IEmailMarketingRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public int Save(EmailMarketing obj)
        {
            return _repository.Save(obj);
        }

        public IEnumerable<EmailMarketing> GetAll()
        {
            return _repository.GetAll();
        }

        public EmailMarketing GetById(int id)
        {
            return _repository.GetById(id);
        }

        public EmailMarketing GetByEndereco(string endereco)
        {
            return _repository.GetByEndereco(endereco);
        }
    }
}
