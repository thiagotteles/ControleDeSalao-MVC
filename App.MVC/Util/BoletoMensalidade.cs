using BoletoNet;
using ControleDeSalao.Domain.Entities;
using System.Globalization;

namespace ControleDeSalao.MVC.Util
{
    public class BoletoMensalidade
    {
        public BoletoBancario BoletoBancario { get; set; }

        public BoletoMensalidade()
        {
            BoletoBancario = new BoletoBancario { CodigoBanco = 237 };
        }

        public string Bradesco(Mensalidade mensalidade)
        {
            if (mensalidade != null)
            {
                var vencimento = mensalidade.DataVencimento;
                var item = new Instrucao_Bradesco(10, 5);
                var nrDoc = mensalidade.Id.ToString(CultureInfo.InvariantCulture).PadLeft(11, '0');

                var c = new Cedente("18.958.208/0001-83", "TagWorks Solutions", "96", "5", "277", "1") { Codigo = "1" };

                var b = new Boleto(vencimento, (decimal)mensalidade.Valor, "09", nrDoc, c)
                {
                    NumeroDocumento = mensalidade.Id.ToString(CultureInfo.InvariantCulture),
                    Sacado = new Sacado(string.IsNullOrEmpty(mensalidade.SacadoCPF) ? string.Empty : mensalidade.SacadoCPF, mensalidade.SacadoNome)
                };

                b.Instrucoes.Add(item);
                BoletoBancario.MostrarContraApresentacaoNaDataVencimento = false;

                BoletoBancario.Boleto = b;
            }
            BoletoBancario.Boleto.Valida();

            return BoletoBancario.MontaHtmlEmbedded();
        }
    }
}