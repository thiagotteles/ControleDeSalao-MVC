namespace ControleDeSalao.Domain.Entities
{
    public class ServicoComanda
    {
        public int Id { get; set; }
        public int ComandaId { get; set; }
        public int? ServicoId { get; set; }
        public double Valor { get; set; }
        public virtual Servico Servico { get; set; }
    }
}
