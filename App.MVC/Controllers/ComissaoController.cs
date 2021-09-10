using System;
using System.Collections;
using System.Collections.Generic;
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
    public class ComissaoController : Controller
    {
        private readonly IProfissionalAppService _profissionalAppService;
        
        private readonly IComissaoAppService _comissaoAppService;
        private readonly IFinanceiroAppService _financeiroAppService;
        private readonly IPlanoDeContaAppService _planoDeContaAppService;

        public ComissaoController(IProfissionalAppService profissionalAppService,  IComissaoAppService comissaoAppService, IFinanceiroAppService financeiroAppService, IPlanoDeContaAppService planoDeContaAppService)
        {
            _profissionalAppService = profissionalAppService;
            
            _comissaoAppService = comissaoAppService;
            _financeiroAppService = financeiroAppService;
            _planoDeContaAppService = planoDeContaAppService;
        }

        // GET: Comissao
        public async Task<ActionResult> Index()
        {
            if (!Cookies.IdUsuarioLogado.HasValue)
                return RedirectToAction("Login", "Usuario");

            if (Cookies.DataBloqueio <= DateTime.Today)
                return RedirectToAction("Bloqueio", "Home");

            //var profissionalViewModels = Mapper.Map<IEnumerable<Profissional>, IEnumerable<ProfissionalViewModel>>(await _profissionalAppService.GetAll());
            var profissionais = new List<ProfissionalViewModel>();
            if (profissionais.Count <= 0)
            {
                if (!Cookies.IdProfissionalLogado.HasValue)
                {
                    profissionais = Mapper.Map<IEnumerable<Profissional>, IEnumerable<ProfissionalViewModel>>(await _profissionalAppService.GetAll()).ToList();
                    Session["TodosProfissionais"] = profissionais;
                }
                else
                {
                    var profissional = Mapper.Map<Profissional, ProfissionalViewModel>(await _profissionalAppService.GetById(Cookies.IdProfissionalLogado.Value));
                    profissionais.Add(profissional);
                    Session["TodosProfissionais"] = profissionais;
                }
            }
            var comissoesViewModel = Mapper.Map<IEnumerable<Comissao>, IEnumerable<ComissaoViewModel>>(await _comissaoAppService.GetByStatus(Enum.StatusComissao.Aberto)).ToList();

            var viewModels = profissionais as IList<ProfissionalViewModel> ?? profissionais.ToList();
            foreach (var item in viewModels)
            {
                item.Comissoes = comissoesViewModel.Where(m => m.ProfissionalId == item.Id).ToList();

            }

            return View(profissionais);
        }

        public async Task<ActionResult> Detalhe(int id)
        {
            try
            {
                var profissional = Mapper.Map<Profissional, ProfissionalViewModel>(await _profissionalAppService.GetById(id));
                profissional.Comissoes = Mapper.Map<IEnumerable<Comissao>, IEnumerable<ComissaoViewModel>>(await _comissaoAppService.GetByProfissionalIdAndSatus(id, Enum.StatusComissao.Aberto)).ToList();
                return PartialView(profissional);
            }
            catch (Exception ex)
            {

                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return View("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Detalhe(ProfissionalViewModel model)
        {
            try
            {
                var planoDeContas = Mapper.Map<IEnumerable<PlanoDeConta>, IEnumerable<PlanoDeContaViewModel>>(await _planoDeContaAppService.GetByTipo(Enum.TipoCreditoDebito.Débito)).ToList();
                var plano = planoDeContas.FirstOrDefault(m => m.TelaPadrao == Enum.TelaPadrao.Comissão);
                var pagarComissoes = model.Comissoes.Where(m => m.Selecionada);
                var dtPagamento = string.IsNullOrEmpty(model.SDataPagamento) ? DateTime.Today : Convert.ToDateTime(model.SDataPagamento);

                Session["comissaoPagaProfissional"] = model.Id;
                Session["comissaoPagaData"] = dtPagamento;

                foreach (var com in pagarComissoes)
                {
                    var comissao = Mapper.Map<Comissao, ComissaoViewModel>(await _comissaoAppService.GetById(com.Id));

                    var financeiro = new Financeiro();
                    if (DateTime.Today == dtPagamento)
                    {
                        financeiro.ValorPago = comissao.Valor;
                        financeiro.DataVencimento = DateTime.Today;
                        financeiro.DataPagamento = DateTime.Today;
                        financeiro.Status = Enum.StatusFinanceiro.Pago;
                    }
                    else
                    {
                        financeiro.DataVencimento = dtPagamento;
                        financeiro.Status = Enum.StatusFinanceiro.Aberto;
                    }

                    financeiro.ProfissionalId = model.Id;
                    financeiro.PlanoDeContaId = plano.Id;
                    financeiro.FormaDePagamento = model.FormaDePagamento;
                    financeiro.Valor = comissao.Valor;
                    financeiro.Tipo = plano.Tipo;
                    financeiro.NomeQuem = model.Nome;
                    financeiro.Descricao = comissao.Descricao;
                    await _financeiroAppService.Save(financeiro);
                    comissao.Status = Enum.StatusComissao.Pago;
                    comissao.DataPagamento = dtPagamento;
                    await _comissaoAppService.Save(Mapper.Map<ComissaoViewModel, Comissao>(comissao));

                }
                TempData["swal"] = SweetAlert.MensagemSucessoComissao("As comissões foram pagas com sucesso! Os valores já foram automaticamente lançados no seu financeiro.");

                ModelState.Clear();
                return RedirectPermanent("Index");
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return RedirectPermanent("Index");
            }
        }

        public async Task<ActionResult> Recibo()
        {
            var profissionalId = Convert.ToInt32(Session["comissaoPagaProfissional"]);
            var dtPagamento = Convert.ToDateTime(Session["comissaoPagaData"]);
            var comissoesViewModel = Mapper.Map<IEnumerable<Comissao>, IEnumerable<ComissaoViewModel>>(await _comissaoAppService.GetByProfissinalAndPagamento(profissionalId, dtPagamento)).ToList();

            return View("_ReciboComissao", comissoesViewModel);
        }
    }
}