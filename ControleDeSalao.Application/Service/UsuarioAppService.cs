using System;
using System.Security.Cryptography;
using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControleDeSalao.Infra.Cross.Security;

namespace ControleDeSalao.Application.Service
{
    public class UsuarioAppService : AppServiceBase<Usuario>, IUsuarioAppService
    {
        private readonly IUsuarioService _service;

        public UsuarioAppService(IUsuarioService service)
            : base(service)
        {
            _service = service;
        }

        public async Task<int> Save(Usuario obj)
        {
            return await _service.Save(obj);
        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await _service.GetAll();
        }

        public async Task<Usuario> GetById(int id)
        {
            return await _service.GetById(id);
        }

        public async Task<Usuario> GetByLogin(string login)
        {
            return await _service.GetByLogin(login);
        }

        public async Task<Usuario> GetByEmail(string email)
        {
            return await _service.GetByEmail(email);
        }

        public async Task<Usuario> IsValid(string login, string password)
        {
            var usuario = await _service.GetByEmail(login);

            if (usuario != null)
            {
                try
                {
                    if (Password.Decryption(usuario.Password, usuario.Login) == password)
                    {
                        usuario.Password = Password.Cryptography(password);
                        await _service.Save(usuario);
                        return usuario;
                    }
                }
                catch (CryptographicException)
                {
                }

                try
                {
                    if (Password.Decryption(usuario.Password) == password)
                        return usuario;
                }
                catch (CryptographicException)
                {
                }

            }

            return null;
        }


        public string GetAllDireitos()
        {
            return _service.GetAllDireitos();
        }
    }
}
