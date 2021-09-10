using System;

namespace ControleDeSalao.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Direitos { get; set; }
        public DateTime DtCadastro { get; set; }
        public DateTime? DtUltLogin { get; set; }
        public string IpUltLogin { get; set; }
        public int? ProfissionalId { get; set; }
        public string Tutorial { get; set; }
        public string Cargo { get; set; }
        public string UrlAvatar { get; set; }
    }
}
