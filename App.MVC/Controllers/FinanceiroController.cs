using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using App.MVC.Util;
using App.MVC.ViewModels;
using AutoMapper;
using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Infra.Cross.Security;
using Enum = ControleDeSalao.Domain.Enums.Enum;

namespace App.MVC.Controllers
{
    public class FinanceiroController : Controller
    {
        private readonly IFinanceiroAppService _financeiroAppService;
        private readonly IPlanoDeContaAppService _planodecontaAppService;
        private readonly IClienteAppService _clienteAppService;
        private readonly IFornecedorAppService _fornecedorAppService;
        
        private List<PlanoDeContaViewModel> _planoDeContas;

        public FinanceiroController(IFinanceiroAppService financeiroAppService, IPlanoDeContaAppService planodecontaAppService, IClienteAppService clienteAppService, IFornecedorAppService fornecedorAppService)
        {
            _financeiroAppService = financeiroAppService;
            _planodecontaAppService = planodecontaAppService;
            _clienteAppService = clienteAppService;
            _fornecedorAppService = fornecedorAppService;
            
        }

        // GET: Financeiro
        public async Task<ActionResult> Index(string dataInicial, string dataFinal)
        {
            try
            {
                if (!Cookies.IdUsuarioLogado.HasValue)
                    return RedirectToAction("Login", "Usuario");

                if (Cookies.DataBloqueio <= DateTime.Today)
                    return RedirectToAction("Bloqueio", "Home");

                var dtInicial = Session["fin_dtInicial"] == null ? DateTime.Today : Convert.ToDateTime(Session["fin_dtInicial"]);
                var dtFinal = Session["fin_dtFinal"] == null ? DateTime.Today : Convert.ToDateTime(Session["fin_dtFinal"]);

                _planoDeContas = _planoDeContas ?? Mapper.Map<IEnumerable<PlanoDeConta>, IEnumerable<PlanoDeContaViewModel>>(await _planodecontaAppService.GetAll()).ToList();
                if (Session["fin_plDeContasDeb"] == null || Session["fin_plDeContasCre"] == null)
                {
                    var listDeb = new List<SelectListItem>();
                    var listCre = new List<SelectListItem>();

                    foreach (var planoDeConta in _planoDeContas)
                    {
                        if (planoDeConta.Tipo == ControleDeSalao.Domain.Enums.Enum.TipoCreditoDebito.Débito)
                            listDeb.Add(new SelectListItem { Text = planoDeConta.Nome, Value = planoDeConta.Id.ToString(CultureInfo.InvariantCulture) });
                        else
                            listCre.Add(new SelectListItem { Text = planoDeConta.Nome, Value = planoDeConta.Id.ToString(CultureInfo.InvariantCulture) });
                    }
                    Session["fin_plDeContasDeb"] = listDeb;
                    Session["fin_plDeContasCre"] = listCre;
                }

                if (!string.IsNullOrEmpty(dataInicial) && !string.IsNullOrEmpty(dataFinal))
                {
                    dtInicial = Convert.ToDateTime(dataInicial);
                    dtFinal = Convert.ToDateTime(dataFinal);
                }

                Session["fin_dtInicial"] = dtInicial;
                Session["fin_dtFinal"] = dtFinal;

                var lancamentos = Mapper.Map<IEnumerable<Financeiro>, IEnumerable<FinanceiroViewModel>>(await _financeiroAppService.GetByVencimento(null, dtInicial, dtFinal)).ToList();

                foreach (var lancamento in lancamentos)
                {
                    lancamento.PlanoDeConta = _planoDeContas.FirstOrDefault(m => m.Id == lancamento.PlanoDeContaId);
                }

                ViewBag.Saldo =
                    (lancamentos.Where(m => m.Tipo == ControleDeSalao.Domain.Enums.Enum.TipoCreditoDebito.Crédito)
                        .Sum(m => m.ValorPago)
                        .Value -
                     lancamentos.Where(m => m.Tipo == ControleDeSalao.Domain.Enums.Enum.TipoCreditoDebito.Débito)
                         .Sum(m => m.ValorPago)
                         .Value).ToString("C").Replace(",", ".");

                ViewBag.SaldoPrevisto =
                    (lancamentos.Where(m => m.Tipo == ControleDeSalao.Domain.Enums.Enum.TipoCreditoDebito.Crédito)
                        .Sum(m => m.Valor)
                        .Value -
                     lancamentos.Where(m => m.Tipo == ControleDeSalao.Domain.Enums.Enum.TipoCreditoDebito.Débito)
                         .Sum(m => m.Valor)
                         .Value).ToString("C").Replace(",", ".");

                return View(lancamentos);
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Recebimento(FinanceiroViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrEmpty(model.NomeQuem))
                    {
                        var cliente = await _clienteAppService.GetByNome(model.NomeQuem);
                        model.ClienteId = cliente != null ? cliente.Id : (int?)null;
                    }

                    _planoDeContas = _planoDeContas ?? Mapper.Map<IEnumerable<PlanoDeConta>, IEnumerable<PlanoDeContaViewModel>>(await _planodecontaAppService.GetAll()).ToList();
                    model.DataVencimento = Convert.ToDateTime(model.SDtVcto);
                    model.Tipo = _planoDeContas.FirstOrDefault(m => m.Id == model.PlanoDeContaId).Tipo;

                    if (model.IncluirComoPago)
                    {
                        model.DataPagamento = model.DataVencimento;
                        model.ValorPago = model.Valor;
                        model.Status = Enum.StatusFinanceiro.Pago;
                    }

                    await _financeiroAppService.Save(Mapper.Map<FinanceiroViewModel, Financeiro>(model));

                    TempData["swal"] = SweetAlert.MensagemSucesso("Receita incluída com sucesso.");
                }


                return RedirectPermanent("Index");
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return RedirectPermanent("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Despesa(FinanceiroViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.NomeQuem))
                {
                    var fornecedor = await _fornecedorAppService.GetByNome(model.NomeQuem);
                    model.FornecedorId = fornecedor != null ? fornecedor.Id : (int?)null;
                }

                _planoDeContas = _planoDeContas ?? Mapper.Map<IEnumerable<PlanoDeConta>, IEnumerable<PlanoDeContaViewModel>>(await _planodecontaAppService.GetAll()).ToList();
                model.DataVencimento = Convert.ToDateTime(model.SDtVcto);
                model.Tipo = _planoDeContas.FirstOrDefault(m => m.Id == model.PlanoDeContaId).Tipo;

                model.Status = Enum.StatusFinanceiro.Aberto;
                for (int i = 0; i < model.QtdMeses; i++)
                {
                    var lancamento = new Financeiro();

                    lancamento.EmpresaId = model.EmpresaId;
                    lancamento.PlanoDeContaId = model.PlanoDeContaId;
                    lancamento.FornecedorId = model.FornecedorId;
                    lancamento.NomeQuem = model.NomeQuem;
                    lancamento.Tipo = model.Tipo;
                    lancamento.Status = model.Status;
                    if (i == 0 && model.IncluirComoPago)
                    {
                        lancamento.DataPagamento = model.DataVencimento;
                        lancamento.ValorPago = model.Valor;
                        lancamento.Status = Enum.StatusFinanceiro.Pago;
                    }
                    lancamento.DataVencimento = model.DataVencimento.AddMonths(i);
                    lancamento.Valor = model.Valor;
                    lancamento.Descricao = model.Descricao;
                    lancamento.FormaDePagamento = model.FormaDePagamento;

                    await _financeiroAppService.Save(lancamento);
                }
                TempData["swal"] = SweetAlert.MensagemSucesso("Despesa incluída com sucesso.");
            }

            return RedirectPermanent("Index");
        }

        public async Task<ActionResult> PagarReceber(int id)
        {
            var lancamento = Mapper.Map<Financeiro, FinanceiroViewModel>(await _financeiroAppService.GetById(id));
            return PartialView(lancamento);
        }

        [HttpPost]
        public async Task<ActionResult> PagarReceber(FinanceiroViewModel model)
        {
            try
            {
                var lancamento = await _financeiroAppService.GetById(model.Id);
                lancamento.DataPagamento = Convert.ToDateTime(model.SDtVcto);
                lancamento.ValorPago = Convert.ToDouble(model.SValor);
                lancamento.Status = Enum.StatusFinanceiro.Pago;
                await _financeiroAppService.Save(lancamento);

                TempData["swal"] = SweetAlert.MensagemSucesso(string.Format("Lançamento {0} com sucesso!", lancamento.Tipo == Enum.TipoCreditoDebito.Crédito ? "recebido" : "pago"));
                return RedirectPermanent("Index");
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return RedirectPermanent("Index");
            }
        }

        public async Task<ActionResult> Excluir(int id)
        {
            try
            {
                var lancamento = await _financeiroAppService.GetById(id);
                lancamento.Status = Enum.StatusFinanceiro.Excluida;
                await _financeiroAppService.Save(lancamento);

                return Json(lancamento, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
    }
}