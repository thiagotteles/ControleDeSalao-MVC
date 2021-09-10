using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using App.MVC.Util;
using App.MVC.ViewModel.Dashboards;
using App.MVC.ViewModels.Relatorios;
using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using Microsoft.Ajax.Utilities;
using Enum = ControleDeSalao.Domain.Enums.Enum;

namespace App.MVC.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IFinanceiroAppService _financeiroAppService;
        private readonly IServicoAppService _servicoAppService;
        private readonly IComandaAppService _comandaAppService;
        private readonly IServicoComandaAppService _servicoComandaAppService;
        private readonly IProfissionalAppService _profissionalAppService;
        private readonly IAgendaAppService _agendaAppService;
        private readonly IClienteAppService _clienteAppService;
        private readonly IProdutoComandaAppService _produtoComandaAppService;
        private readonly IComissaoAppService _comissaoAppService;
        private readonly IProdutoAppService _produtoAppService;
        
        private readonly IPlanoDeContaAppService _planoDeContaAppService;
        private readonly IUsuarioAppService _usuarioAppService;

        public DashboardController(IFinanceiroAppService financeiroAppService, IServicoAppService servicoAppService, IComandaAppService comandaAppService, IServicoComandaAppService servicoComandaAppService, IProfissionalAppService profissionalAppService, IAgendaAppService agendaAppService, IClienteAppService clienteAppService, IProdutoComandaAppService produtoComandaAppService, IComissaoAppService comissaoAppService, IProdutoAppService produtoAppService,  IPlanoDeContaAppService planoDeContaAppService, IUsuarioAppService usuarioAppService)
        {
            _financeiroAppService = financeiroAppService;
            _servicoAppService = servicoAppService;
            _comandaAppService = comandaAppService;
            _servicoComandaAppService = servicoComandaAppService;
            _profissionalAppService = profissionalAppService;
            _agendaAppService = agendaAppService;
            _clienteAppService = clienteAppService;
            _produtoComandaAppService = produtoComandaAppService;
            _comissaoAppService = comissaoAppService;
            _produtoAppService = produtoAppService;
            
            _planoDeContaAppService = planoDeContaAppService;
            _usuarioAppService = usuarioAppService;
        }

        // GET: Dashboard
        public async Task<ActionResult> Gerencial()
        {
            ViewBag.ReceitasMesAtual = await _financeiroAppService.GetByVencimento(Enum.TipoCreditoDebito.Crédito, DateTime.Today.AddMonths(-1), DateTime.Today);
            ViewBag.DespesasMesAtual = await _financeiroAppService.GetByVencimento(Enum.TipoCreditoDebito.Débito, DateTime.Today.AddMonths(-1), DateTime.Today);

            ViewBag.ReceitasMesAnterior = await _financeiroAppService.GetByVencimento(Enum.TipoCreditoDebito.Crédito, DateTime.Today.AddMonths(-2), DateTime.Today.AddMonths(-1).AddDays(-1));
            ViewBag.DespesasMesAnterior = await _financeiroAppService.GetByVencimento(Enum.TipoCreditoDebito.Débito, DateTime.Today.AddMonths(-2), DateTime.Today.AddMonths(-1).AddDays(-1));

            double saldoMesAtual = ((((IEnumerable<Financeiro>)ViewBag.ReceitasMesAtual).Any() ? ((IEnumerable<Financeiro>)ViewBag.ReceitasMesAtual).Sum(m => m.Valor).Value : 0) - (((IEnumerable<Financeiro>)ViewBag.DespesasMesAtual).Any() ? ((IEnumerable<Financeiro>)ViewBag.DespesasMesAtual).Sum(m => m.Valor).Value : 0));
            double saldoMesAnterior = ((((IEnumerable<Financeiro>)ViewBag.ReceitasMesAnterior).Any() ? ((IEnumerable<Financeiro>)ViewBag.ReceitasMesAnterior).Sum(m => m.Valor).Value : 0) - (((IEnumerable<Financeiro>)ViewBag.DespesasMesAnterior).Any() ? ((IEnumerable<Financeiro>)ViewBag.DespesasMesAnterior).Sum(m => m.Valor).Value : 0));
            double porcentagemMesAtual = saldoMesAnterior > saldoMesAtual ? Math.Abs(((saldoMesAtual - saldoMesAnterior) * 100) / saldoMesAnterior) : Math.Abs(((saldoMesAnterior - saldoMesAtual) * 100) / saldoMesAtual);

            ViewBag.SaldoMesAtual = saldoMesAtual;
            ViewBag.SaldoMesAnterior = saldoMesAnterior;
            ViewBag.PorcentagemMesAtual = porcentagemMesAtual;

            await PreencherReceitaDespesaNoMes();
            await PreencherDespesasPorPlanoDeConta();
            await PreencherLucroAnual();
            return View();
        }

        private async Task PreencherReceitaDespesaNoMes()
        {
            var stats = new List<ReceitaDespesa>();
            var financeiros = await _financeiroAppService.GetPagoByYear(DateTime.Today.Year);

            for (int i = 1; i <= DateTime.Today.Month; i++)
            {
                var receita = financeiros.Where(m => m.Tipo == Enum.TipoCreditoDebito.Crédito &&
                              m.Status != Enum.StatusFinanceiro.Excluida
                              && m.DataPagamento.Value.Month == i).Sum(m => m.Valor != null ? m.Valor.Value : 0);

                var despesa = financeiros.Where(m => m.Tipo == Enum.TipoCreditoDebito.Débito &&
                              m.Status != Enum.StatusFinanceiro.Excluida
                              && m.DataPagamento.Value.Month == i).Sum(m => m.Valor != null ? m.Valor.Value : 0);

                stats.Add(new ReceitaDespesa
                {
                    mes = new DateTime(DateTime.Today.Year, i, 1).ToString("MMM"),
                    receita = receita,
                    despesa = despesa,
                    dashLengthColumn = 1,
                    alpha = 1,
                    additional = ""
                });
            }

            stats[stats.Count() - 1].alpha = 0.2;
            stats[stats.Count() - 1].dashLengthColumn = 5;
            stats[stats.Count() - 1].additional = "Projeção";

            if (stats.Count > 1)
                stats[stats.Count() - 2].dashLengthLine = 5;

            Session["ReceitaDespesa"] = stats;
        }

        public ActionResult ReceitaDespesaNoMes()
        {
            try
            {
                var stats = (List<ReceitaDespesa>)Session["ReceitaDespesa"];
                return Json(stats, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        private async Task PreencherDespesasPorPlanoDeConta()
        {
            var stats = new List<PieChart>();
            var financeiros = await _financeiroAppService.GetByVencimento(Enum.TipoCreditoDebito.Débito, DateTime.Today.AddMonths(-1), DateTime.Today);
            var planosDeConta = await _planoDeContaAppService.GetAll();

            foreach (var plano in planosDeConta)
            {
                var stat = new PieChart();
                stat.label = plano.Nome;
                stat.value = financeiros.Where(m => m.PlanoDeContaId == plano.Id).Sum(m => m.Valor.Value);
                if (stat.value > 0)
                    stats.Add(stat);
            }

            Session["DespesasPorPlanoDeConta"] = stats;
        }

        public ActionResult DespesasPorPlanoDeConta()
        {
            try
            {
                var stats = (List<PieChart>)Session["DespesasPorPlanoDeConta"];
                return Json(stats, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        private async Task PreencherLucroAnual()
        {
            var stats = new List<LucroAnual>();
            var data = new DateTime(DateTime.Today.Year, 1, 1);
            var financeiros = await _financeiroAppService.GetPagoByYear(DateTime.Today.Year);
            double saldo = 0.0;
            while (data < DateTime.Today)
            {
                var stat = new LucroAnual();

                var receita = financeiros.Where(m => m.Tipo == Enum.TipoCreditoDebito.Crédito &&
                              m.Status != Enum.StatusFinanceiro.Excluida
                              && m.DataPagamento.Value == data).Sum(m => m.Valor != null ? m.Valor.Value : 0);

                var despesa = financeiros.Where(m => m.Tipo == Enum.TipoCreditoDebito.Débito &&
                              m.Status != Enum.StatusFinanceiro.Excluida
                              && m.DataPagamento.Value == data).Sum(m => m.Valor != null ? m.Valor.Value : 0);

                saldo += (receita - despesa);
                stat.date = data;
                stat.value = saldo;
                stat.volume = saldo;
                stats.Add(stat);
                data = data.AddDays(1);
            }
            Session["LucroAnual"] = stats;
        }

        public ActionResult LucroAnual()
        {
            try
            {
                var stats = (List<LucroAnual>)Session["LucroAnual"];
                return Json(stats, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> Operacional()
        {
            ViewBag.AgendasHoje = await _agendaAppService.GetByData(DateTime.Today);
            ViewBag.Clientes = await _clienteAppService.GetAll();
            ViewBag.Comissoes = await _comissaoAppService.GetByStatus(Enum.StatusComissao.Aberto);
            ViewBag.Usuarios = await _usuarioAppService.GetAll();
            await PreencherAgendaComanda();
            return View();
        }

        private async Task PreencherAgendaComanda()
        {
            var stats = new List<AgendaComanda>();
            var data = DateTime.Today.AddMonths(-1);

            while (data <= DateTime.Today)
            {
                var agendas = await _agendaAppService.GetByData(data);
                var comandas = await _comandaAppService.GetByStatusAndData(Enum.StatusComanda.Faturada, data);

                var stat = new AgendaComanda();
                stat.agendas = agendas.Count();
                stat.comandas = comandas.Count();
                stat.date = data;
                stats.Add(stat);
                data = data.AddDays(1);
            }

            Session["AgendaComandaReceita"] = stats;
        }

        public ActionResult AgendaComanda()
        {
            try
            {
                var stats = (List<AgendaComanda>)Session["AgendaComandaReceita"];
                return Json(stats, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> MelhoresServicos()
        {
            var servicos = await _servicoAppService.GetAll();

            var ranking = servicos.Select(ser => new RelServicoRanking()
            {
                ServicoId = ser.Id,
                NomeServico = ser.Descricao
            }).ToList();

            var comandas = await _servicoComandaAppService.GetAll();
            foreach (var com in comandas)
            {
                var rnk = ranking.FirstOrDefault(m => m.ServicoId == com.ServicoId);
                if (rnk == null) continue;
                rnk.Quantidade += 1;
            }

            var stats = ranking.Where(m => m.Quantidade > 0).Select(ser => new PieChart()
            {
                label = ser.NomeServico,
                value = ser.Quantidade
            }).ToList();

            return Json(stats, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> MelhoresProfissionais()
        {
            var profissionais = await _profissionalAppService.GetAll();
            var stats = new List<PieChart>();

            foreach (var profissional in profissionais)
            {
                var lancamentos = await _comandaAppService.GetByProfissionalID(profissional.Id);
                stats.Add(new PieChart
                {
                    label = profissional.Nome,
                    value = lancamentos.Count()
                });
            }

            return Json(stats, JsonRequestBehavior.AllowGet);
        }
    }
}