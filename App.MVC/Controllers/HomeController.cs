using App.MVC.Util;
using App.MVC.ViewModels;
using AutoMapper;
using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Infra.Cross.Security;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Enum = ControleDeSalao.Domain.Enums.Enum;

namespace App.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProfissionalAppService _profissionalAppService;
        private readonly IEmpresaAppService _empresaAppService;
        private readonly IServicoAppService _servicoAppService;
        private readonly IProdutoAppService _produtoAppService;
        private readonly IClienteAppService _clienteAppService;
        private readonly IMensalidadeAppService _mensalidadeAppService;
        private readonly IPlanoAppService _planoAppService;
        private readonly IComandaAppService _comandaAppService;

        public HomeController(IProfissionalAppService profissionalAppService, IEmpresaAppService empresaAppService, IServicoAppService servicoAppService, IProdutoAppService produtoAppService, IClienteAppService clienteAppService, IMensalidadeAppService mensalidadeAppService, IPlanoAppService planoAppService, IComandaAppService comandaAppService)
        {
            _profissionalAppService = profissionalAppService;
            _empresaAppService = empresaAppService;
            _servicoAppService = servicoAppService;
            _produtoAppService = produtoAppService;
            _clienteAppService = clienteAppService;
            _mensalidadeAppService = mensalidadeAppService;
            _planoAppService = planoAppService;
            _comandaAppService = comandaAppService;
        }

        public async Task<ActionResult> Index()
        {
            if (!Cookies.IdUsuarioLogado.HasValue)
                return RedirectToAction("Login", "Usuario");

            if (Cookies.DataBloqueio <= DateTime.Today)
                return RedirectToAction("Bloqueio", "Home");

            var profissionais = await _profissionalAppService.GetAll();
            var servicos = await _servicoAppService.GetAll();
            var empresa = Mapper.Map<Empresa, EmpresaViewModel>(await _empresaAppService.GetById(Cookies.IdEmpresaLogada.Value));
            var produtos = await _produtoAppService.GetAll();
            var clientes = await _clienteAppService.GetAll();

            ViewBag.QtdProfissionais = profissionais.Count();
            ViewBag.QtdServicos = servicos.Count();
            ViewBag.QtdProdutos = produtos.Count();
            ViewBag.QtdClientes = clientes.Count();
            ViewBag.TemCpf = !string.IsNullOrEmpty(empresa.CPFResponsavel);

            if (ViewBag.QtdProdutos > 0 && ViewBag.QtdServicos > 5 && ViewBag.QtdProfissionais > 0 && Cookies.DireitoUsuarioLogado.Contains("SOPEDASH"))
                return RedirectToAction("Operacional","Dashboard");

            return View(empresa);
        }

        public FileResult DownloadAtalho()
        {
            var diretorio = Server.MapPath("/Content/");
            const string arquivo = "Controle De Salao.url";
            var stream = new FileStream(diretorio + arquivo, FileMode.Open);
            return File(stream, "baixar", arquivo);
        }

        public async Task<ActionResult> Bloqueio()
        {
            var empresa = Mapper.Map<Empresa, EmpresaViewModel>(await _empresaAppService.GetById(Cookies.IdEmpresaLogada.Value));

            if (empresa.PlanoId.HasValue)
            {
                var mensalidades = await _mensalidadeAppService.GetByEmpresaId(empresa.Id);
                ViewBag.MensalidadeAberta = mensalidades.FirstOrDefault(m => !m.ValorPago.HasValue);
            }

            return View(empresa);
        }

        [HttpPost]
        public async Task<ActionResult> Bloqueio(EmpresaViewModel model)
        {
            var empresa = await _empresaAppService.GetById(Cookies.IdEmpresaLogada.Value);
            var comandas = await _comandaAppService.GetByStatusAndPeriodo(Enum.StatusComanda.Faturada, DateTime.Today.AddMonths(-1), DateTime.Today);
            var profissionais = comandas.GroupBy(m => m.ProfissionalId).Count();
            var planos =  await _planoAppService.GetAll();
            var plano = planos.Where(m => m.QtdProfissionais >= profissionais).OrderBy(m => m.QtdProfissionais).Take(1).FirstOrDefault();


            var mensalidade = new Mensalidade();
            mensalidade.SacadoNome = empresa.NomeResponsavel;
            mensalidade.SacadoCPF = empresa.CPFResponsavel;
            mensalidade.EmpresaId = empresa.Id;
            mensalidade.DataVencimento = DateTime.Today.AddDays(2);
            mensalidade.Valor = plano != null ? plano.Valor : 50;
            await _mensalidadeAppService.Save(mensalidade);
            
            empresa.DataAlerta = DateTime.Today;
            empresa.DataBloqueio = DateTime.Today.AddDays(2);
            empresa.PlanoId = plano != null ? plano.Id : 1;
            await _empresaAppService.Save(empresa);

            Cookies.DataBloqueio = empresa.DataBloqueio;
            TempData["swal"] = SweetAlert.MensagemSucesso("Parabéns, sua assinatura está ativa, verifique as suas mensalidades em: Configurações > Mensalidades.");
            return RedirectToAction("Index");
        }
    }
}