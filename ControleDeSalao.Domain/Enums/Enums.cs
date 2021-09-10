using System.ComponentModel;

namespace ControleDeSalao.Domain.Enums
{
    public class Enum
    {
        public enum TelaPadrao
        {
            [Description("")]
            Nenhuma = 0,
            Compras = 1,
            Comandas = 2,
            Salario = 3,
            Comissão = 4

        }

        public enum TipoCreditoDebito
        {
            Crédito = 0,
            Débito = 1
        }

        public enum StatusCompra
        {
            Aberta = 0,
            Faturada = 1,
            Excluida = 2
        }

        public enum FormaDePagamento
        {
            Dinheiro = 0,
            [Description("Cartão de Crédito")]
            Crédito = 1,
            [Description("Cartão de Débito")]
            Débito = 2,
            [Description("Cheque Pré")]
            Cheque = 3,
            [Description("Depósito")]
            Depósito = 5,
            [Description("Boleto")]
            Boleto = 6,
            [Description("Outros")]
            Outros = 7,
            [Description("Pacote")]
            Pacote = 8,
            [Description("Promissória")]
            Promissória = 9,
        }

        public enum StatusFinanceiro
        {
            Aberto = 0,
            Pago = 1,
            Excluida = 2
        }

        public enum StatusComanda
        {
            Aberta = 0,
            Faturada = 1,
            Excluida = 2
        }

        public enum StatusComissao
        {
            Aberto = 0,
            Pago = 1,
            Excluido = 2
        }

        public enum StatusChamado
        {
            Aberto = 0,
            Concluído = 1,
            Excluído = 2
        }

        public enum ChamadoComQuem
        {
            TagWorks = 0,
            Cliente = 1
        }
    }
}
