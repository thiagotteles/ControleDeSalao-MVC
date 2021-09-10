using AutoMapper;
using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Infra.Cross.Security;
using App.MVC.ViewModels;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Ninject;
using App.MVC.Util;
using System.Collections.Generic;
using System.Globalization;
using ControleDeSalao.MVC.Util;
namespace App.MVC.Controllers
{
    public class EmpresaController : Controller
    {
        private readonly IEmpresaAppService _empresaAppService;
        private readonly IHorarioFuncionamentoAppService _horarioFuncionamentoAppService;
        
        private readonly IUsuarioAppService _usuarioAppService;
        private readonly IProfissionalAppService _profissionalAppService;
        private readonly IMensalidadeAppService _mensalidadeAppService;
        private readonly IClienteAppService _clienteAppService;
        private readonly IServicoAppService _servicoAppService;

        public EmpresaController(IEmpresaAppService empresaAppService, IHorarioFuncionamentoAppService horarioFuncionamentoAppService,  IUsuarioAppService usuarioAppService, IProfissionalAppService profissionalAppService, IMensalidadeAppService mensalidadeAppService, IClienteAppService clienteAppService, IServicoAppService servicoAppService)
        {
            _empresaAppService = empresaAppService;
            _horarioFuncionamentoAppService = horarioFuncionamentoAppService;
            
            _usuarioAppService = usuarioAppService;
            _profissionalAppService = profissionalAppService;
            _mensalidadeAppService = mensalidadeAppService;
            _clienteAppService = clienteAppService;
            _servicoAppService = servicoAppService;
        }

        // GET: Empresa
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Perfil()
        {
            try
            {
                if (!Cookies.IdUsuarioLogado.HasValue)
                    return RedirectToAction("Login", "Usuario");

                if (Cookies.DataBloqueio <= DateTime.Today)
                    return RedirectToAction("Bloqueio", "Home");

                var profissionais = Mapper.Map<IEnumerable<Profissional>, IEnumerable<ProfissionalViewModel>>(await _profissionalAppService.GetComAgenda());
                var listItems = new List<SelectListItem>();
                listItems.Add(new SelectListItem { Text = " ", Value = "0" });
                foreach (var profissional in profissionais)
                {
                    listItems.Add(new SelectListItem { Text = profissional.Nome, Value = profissional.Id.ToString(CultureInfo.InvariantCulture) });
                }
                Session["lstUsuProfissionais"] = listItems;

                var empresa = Mapper.Map<Empresa, EmpresaViewModel>(await _empresaAppService.GetById(Cookies.IdEmpresaLogada.Value));
                empresa.Usuarios = Mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioViewModel>>(await _usuarioAppService.GetAll());
                Session["TodasMensalidades"] = Session["TodasMensalidades"] ?? Mapper.Map<IEnumerable<Mensalidade>, IEnumerable<MensalidadeViewModel>>(await _mensalidadeAppService.GetByEmpresaId(empresa.Id));
                ViewBag.Profissionais = await _profissionalAppService.GetAll();
                ViewBag.Clientes = await _clienteAppService.GetAll();
                ViewBag.Servicos = await _servicoAppService.GetAll();
                return View(empresa);
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        public async Task<ActionResult> Configurar(EmpresaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _empresaAppService.Save(Mapper.Map<EmpresaViewModel, Empresa>(model));
                    TempData["swal"] = SweetAlert.MensagemSucesso("As informações foram salvas!");
                    ModelState.Clear();
                    return RedirectToActionPermanent("Perfil");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<ActionResult> ConfigurarSMS(EmpresaViewModel model)
        {
            try
            {
                var empresa = await _empresaAppService.GetById(Cookies.IdEmpresaLogada.Value);
                empresa.SmsAgenda = model.SmsAgenda;
                empresa.SmsAniversario = model.SmsAniversario;
                await _empresaAppService.Save(empresa);
                TempData["swal"] = SweetAlert.MensagemSucesso("As informações foram salvas!");
                ModelState.Clear();
                return RedirectToActionPermanent("Perfil");
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<ActionResult> VisualizarBoleto(int id)
        {
            try
            {
                var boletoMensalidade = new BoletoMensalidade();
                var mensalidade = await _mensalidadeAppService.GetById(id);
                ViewBag.Boleto = boletoMensalidade.Bradesco(mensalidade);

                return View();
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return RedirectToAction("Perfil");
            }
        }
    }
}