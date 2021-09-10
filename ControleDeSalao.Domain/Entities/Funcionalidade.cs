using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeSalao.Domain.Entities
{
    public class Funcionalidade
    {
        /// <summary>
        ///     Configurações
        /// </summary>
        public bool MCON { get; set; }

        /// <summary>
        ///     Cadastros
        /// </summary>
        public bool MCAD { get; set; }

        /// <summary>
        ///     Meu sistema
        /// </summary>
        public bool MMSIS { get; set; }

        /// <summary>
        ///     Menu Usuários
        /// </summary>
        public bool SUSU { get; set; }

        /// <summary>
        ///     Incluir Usuário
        /// </summary>
        public bool SUSUI { get; set; }

        /// <summary>
        ///     Alterar Usuário
        /// </summary>
        public bool SUSUA { get; set; }

        /// <summary>
        ///     Excluir Usuário
        /// </summary>
        public bool SUSUE { get; set; }

        /// <summary>
        ///     Menu Cliente
        /// </summary>
        public bool SCLI { get; set; }

        /// <summary>
        ///     Incluir Cliente
        /// </summary>
        public bool SCLII { get; set; }

        /// <summary>
        ///     Alterar Cliente
        /// </summary>
        public bool SCLIA { get; set; }

        /// <summary>
        ///     Excluir Cliente
        /// </summary>
        public bool SCLIE { get; set; }

        /// <summary>
        ///     Menu fornecedor
        /// </summary>
        public bool SFOR { get; set; }

        /// <summary>
        ///     Incluir fornecedor
        /// </summary>
        public bool SFORI { get; set; }

        /// <summary>
        ///     Alterar fornecedor
        /// </summary>
        public bool SFORA { get; set; }

        /// <summary>
        ///     Excluir fornecedor
        /// </summary>
        public bool SFORE { get; set; }

        /// <summary>
        ///     Menu produtos
        /// </summary>
        public bool SPRO { get; set; }

        /// <summary>
        ///     Incluir produtos
        /// </summary>
        public bool SPROI { get; set; }

        /// <summary>
        ///     Alterar produtos
        /// </summary>
        public bool SPROA { get; set; }

        /// <summary>
        ///     Excluir produtos
        /// </summary>
        public bool SPROE { get; set; }

        /// <summary>
        ///     Menu serviços
        /// </summary>
        public bool SSER { get; set; }

        /// <summary>
        ///     Incluir serviços
        /// </summary>
        public bool SSERI { get; set; }

        /// <summary>
        ///     Alterar serviços
        /// </summary>
        public bool SSERA { get; set; }

        /// <summary>
        ///     Excluir serviços
        /// </summary>
        public bool SSERE { get; set; }

        /// <summary>
        ///     Menu profissionais
        /// </summary>
        public bool SPRF { get; set; }

        /// <summary>
        ///     Incluir profissionais
        /// </summary>
        public bool SPRFI { get; set; }

        /// <summary>
        ///     Alterar profissionais
        /// </summary>
        public bool SPRFA { get; set; }

        /// <summary>
        ///     Pagar Comissões
        /// </summary>
        public bool SPRFC { get; set; }

        /// <summary>
        ///     Excluir profissionais
        /// </summary>
        public bool SPRFE { get; set; }

        /// <summary>
        ///     Menu Agenda
        /// </summary>
        public bool SAGE { get; set; }

        /// <summary>
        ///     Incluir Agenda
        /// </summary>
        public bool SAGEI { get; set; }

        /// <summary>
        ///     Alterar Agenda
        /// </summary>
        public bool SAGEA { get; set; }

        /// <summary>
        ///     Excluir Agenda
        /// </summary>
        public bool SAGEE { get; set; }

        /// <summary>
        ///     Menu Comanda
        /// </summary>
        public bool SCOM { get; set; }

        /// <summary>
        ///     Incluir Comanda
        /// </summary>
        public bool SCOMI { get; set; }

        /// <summary>
        ///     Fechar Comanda
        /// </summary>
        public bool SCOMF { get; set; }

        /// <summary>
        ///     Alterar Comanda
        /// </summary>
        public bool SCOMA { get; set; }

        /// <summary>
        ///     Excluir Comanda
        /// </summary>
        public bool SCOME { get; set; }

        /// <summary>
        ///     Menu Plano De Conta
        /// </summary>
        public bool SPDC { get; set; }

        /// <summary>
        ///     Incluir Plano De Conta
        /// </summary>
        public bool SPDCI { get; set; }

        /// <summary>
        ///     Incluir Plano De Conta
        /// </summary>
        public bool SPDCA { get; set; }

        /// <summary>
        ///     Excluir Plano De Conta
        /// </summary>
        public bool SPDCE { get; set; }

        /// <summary>
        ///     Menu Compras
        /// </summary>
        public bool SCMP { get; set; }

        /// <summary>
        ///     Incluir Compras
        /// </summary>
        public bool SCMPI { get; set; }

        /// <summary>
        ///     Alterar Compras
        /// </summary>
        public bool SCMPA { get; set; }

        /// <summary>
        ///     Excluir Compras
        /// </summary>
        public bool SCMPE { get; set; }

        /// <summary>
        ///     Faturar Compras
        /// </summary>
        public bool SCMPF { get; set; }

        /// <summary>
        ///     Menu Contas Fixas
        /// </summary>
        public bool SCFX { get; set; }

        /// <summary>
        ///     Incluir Contas Fixas
        /// </summary>
        public bool SCFXI { get; set; }

        /// <summary>
        ///     Alterar Contas Fixas
        /// </summary>
        public bool SCFXA { get; set; }

        /// <summary>
        ///     Excluir Contas Fixas
        /// </summary>
        public bool SCFXE { get; set; }

        /// <summary>
        ///     Menu Financeiro
        /// </summary>
        public bool SFIN { get; set; }

        /// <summary>
        ///     Menu Extrato
        /// </summary>
        public bool SEXT { get; set; }

        /// <summary>
        ///     Incluir Recebimento
        /// </summary>
        public bool SEXTIR { get; set; }

        /// <summary>
        ///     Receber Recebimento
        /// </summary>
        public bool SEXTRR { get; set; }

        /// <summary>
        ///     Excluir Recebimento
        /// </summary>
        public bool SEXTER { get; set; }

        /// <summary>
        ///     Incluir Despesa
        /// </summary>
        public bool SEXTDI { get; set; }

        /// <summary>
        ///     pAGAR Despesa
        /// </summary>
        public bool SEXTDP { get; set; }

        /// <summary>
        ///     Excluir Despesa
        /// </summary>
        public bool SEXTDE { get; set; }

        /// <summary>
        ///    Menu de Suporte
        /// </summary>
        public bool SSUPO { get; set; }

        /// <summary>
        ///     Menu Dashboard
        /// </summary>
        public bool SDASH { get; set; }

        /// <summary>
        ///     Menu Dashboard
        /// </summary>
        public bool SOPEDASH { get; set; }

        /// <summary>
        ///     Relatórios
        /// </summary>
        public bool MREL { get; set; }

        /// <summary>
        ///     Relatório Finanças
        /// </summary>
        public bool SRFIN { get; set; }

        /// <summary>
        ///     Relatório Fechamento
        /// </summary>
        public bool RFFEC { get; set; }

        /// <summary>
        ///     Relatório movimentação
        /// </summary>
        public bool RFMOV { get; set; }

        /// <summary>
        ///     Relatório Despesas
        /// </summary>
        public bool RFDES { get; set; }

        /// <summary>
        ///     Relatório Recebimentos
        /// </summary>
        public bool RFREC { get; set; }

        /// <summary>
        ///     Relatório Descontos
        /// </summary>
        public bool RFDESC { get; set; }

        /// <summary>
        ///     Relatório Compras por fornecedor
        /// </summary>
        public bool RFCPF { get; set; }

        /// <summary>
        ///     Relatório Clientes
        /// </summary>
        public bool SRCLI { get; set; }

        /// <summary>
        ///     Agendamentos
        /// </summary>
        public bool RCAGE { get; set; }

        /// <summary>
        ///     Ranking
        /// </summary>
        public bool RCRAN { get; set; }

        /// <summary>
        ///     Comandas
        /// </summary>
        public bool RCCOM { get; set; }

        /// <summary>
        ///     Relatório Profissionais
        /// </summary>
        public bool SRPRO { get; set; }

        /// <summary>
        ///     Comissões
        /// </summary>
        public bool RPCOM { get; set; }

        /// <summary>
        ///     Agendamentos
        /// </summary>
        public bool RPAGE { get; set; }

        /// <summary>
        ///     Ranking
        /// </summary>
        public bool RPRAN { get; set; }

        /// <summary>
        ///     Relatório Produtos e Serviços
        /// </summary>
        public bool SRPRS { get; set; }

        /// <summary>
        ///     Relatório Ranking de Produtos
        /// </summary>
        public bool RPRDRAN { get; set; }

        /// <summary>
        ///     Relatório Estoque de Produtos
        /// </summary>
        public bool RPRDEST { get; set; }

        /// <summary>
        ///     Relatório Serviços mais utilizados
        /// </summary>
        public bool RSERRAN { get; set; }


        /// <summary>
        ///     Configuração de SMS
        /// </summary>
        public bool SCSMS { get; set; }

        /// <summary>
        ///     Mensalidades
        /// </summary>
        public bool SMENSAL { get; set; }
    }
}
