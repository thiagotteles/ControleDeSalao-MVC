using ControleDeSalao.Domain.Entities;
using System.Data.Entity.ModelConfiguration;


namespace ControleDeSalao.Infra.Data.EntityConfig
{
    public class ComissaoConfiguration : EntityTypeConfiguration<Comissao>
    {
        public ComissaoConfiguration()
        {
            HasRequired(p => p.Profissional)
                .WithMany()
                .HasForeignKey(p => p.ProfissionalId);
        }
    }
}
