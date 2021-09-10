using App.MVC.Util;
using App.MVC.ViewModels;
using AutoMapper;
using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Infra.Cross.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace App.MVC.Controllers
{
    public class AgendaController : Controller
    {
        private readonly IProfissionalAppService _profissionalAppService;
        private readonly IAgendaAppService _agendaAppService;
        private readonly IClienteAppService _clienteAppService;
        private readonly IServicoAppService _servicoAppService;

        private readonly IEmpresaAppService _empresaAppService;

        public AgendaController(IProfissionalAppService profissionalAppService, IAgendaAppService agendaAppService, IClienteAppService clienteAppService, IServicoAppService servicoAppService, IProfissionalCategoriaAppService profissionalCategoriaAppService, IEmpresaAppService empresaAppService)
        {
            _profissionalAppService = profissionalAppService;
            _agendaAppService = agendaAppService;
            _clienteAppService = clienteAppService;
            _servicoAppService = servicoAppService;
            _empresaAppService = empresaAppService;
        }

        // GET: Agenda
        public async Task<ActionResult> Index()
        {
            try
            {
                if (!Cookies.IdUsuarioLogado.HasValue)
                    return RedirectToAction("Login", "Usuario");

                if (Cookies.DataBloqueio <= DateTime.Today)
                    return RedirectToAction("Bloqueio", "Home");

                var profissionais = (List<ProfissionalViewModel>)Session["TodosProfissionais"] ?? new List<ProfissionalViewModel>();
                if (profissionais.Count <= 0)
                {
                    if (!Cookies.IdProfissionalLogado.HasValue)
                    {
                        profissionais = Mapper.Map<IEnumerable<Profissional>, IEnumerable<ProfissionalViewModel>>(await _profissionalAppService.GetAll()).ToList();

                        foreach (var profissional in profissionais)
                        {
                            profissional.HorarioMarcado = Mapper.Map<IEnumerable<Agenda>, IEnumerable<AgendaViewModel>>(await _agendaAppService.GetByProfissionalIdAndPeriodo(profissional.Id, DateTime.Today, DateTime.Today.AddDays(6))).ToList();
                        }
                        Session["TodosProfissionais"] = profissionais;
                    }
                    else
                    {
                        var profissional = Mapper.Map<Profissional, ProfissionalViewModel>(await _profissionalAppService.GetById(Cookies.IdProfissionalLogado.Value));
                        profissional.HorarioMarcado = Mapper.Map<IEnumerable<Agenda>, IEnumerable<AgendaViewModel>>(await _agendaAppService.GetByProfissionalIdAndPeriodo(profissional.Id, DateTime.Today, DateTime.Today.AddDays(6))).ToList();
                        profissionais.Add(profissional);
                        Session["TodosProfissionais"] = profissionais;
                    }
                }

                Session["TodosServicos"] = Session["TodosServicos"] ?? Mapper.Map<IEnumerable<Servico>, IEnumerable<ServicoViewModel>>(await _servicoAppService.GetAll()).OrderBy(m => m.CategoriaId);
                var empresa = Mapper.Map<Empresa, EmpresaViewModel>(await _empresaAppService.GetById(Cookies.IdEmpresaLogada.Value));
                ViewBag.QtdSMS = empresa.QtdSMSPago + empresa.QtdSMSBonus;
                return View();
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Detalhe(AgendaViewModel model)
        {
            try
            {
                return await SalvarAgenda(model);
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return View(model);
            }
        }

        private async Task<ActionResult> SalvarAgenda(AgendaViewModel model)
        {
            var empresa =await _empresaAppService.GetById(Cookies.IdEmpresaLogada.Value);
            var age = await _agendaAppService.GetById(model.Id);
            var cli = await _clienteAppService.GetByNome(model.NomeCliente);
            if (cli == null)
            {
                cli = new Cliente
                {
                    Nome = model.NomeCliente,
                    Celular = model.CelularCliente
                };
                cli.Id = await _clienteAppService.Save(cli);
            }

            if (!string.IsNullOrEmpty(model.CelularCliente) && string.IsNullOrEmpty(cli.Celular))
            {
                cli.Celular = model.CelularCliente;
                await _clienteAppService.Save(cli);
            }

            var ser = await _servicoAppService.GetByNome(model.NomeServico);
            if (ser.Valor == 0)
            {
                ser.Valor = model.Valor;
                ser.HoraDuracao = Convert.ToInt32(model.Duracao.Split(':')[0]);// model.HoraDuracao;
                ser.MinutoDuracao = Convert.ToInt32(model.Duracao.Split(':')[1]);// model.MinutoDuracao;
                ser.ValorParaProfissional = Convert.ToDouble(model.NovaComissao);
                await _servicoAppService.Save(ser);
            }

            if (age == null)
                age = new Agenda();

            age.ProfissionalId = Convert.ToInt32(model.Parametros.Split('|')[1]);
            age.ServicoId = ser.Id;
            age.ClienteId = cli.Id;
            age.Data = Convert.ToDateTime(model.Parametros.Substring(8, 2) + model.Parametros.Substring(4, 3) + model.Parametros.Substring(11, 4)); //Convert.ToDateTime(model.Parametros.Split('|')[3]);
            age.Valor = model.Valor;
            age.HoraInicial = Convert.ToInt32(model.Parametros.Substring(16, 2) + model.Parametros.Substring(19, 2));
            age.HoraFinal = UtilAgenda.CalcularHoraFinal(age.HoraInicial, Convert.ToInt32(model.Duracao.Split(':')[0]), Convert.ToInt32(model.Duracao.Split(':')[1]));
            age.Observacao = model.Observacao;
            age.EnviarSMS = model.EnviarSMS;
            age.EnviouSMS = false;

            var emp = await _empresaAppService.GetById(Cookies.IdEmpresaLogada.Value);
            var msg = emp.SmsAgenda.Replace("[Cliente]", cli.Nome)
                    .Replace("[Salao]", Cookies.NomeEmpresaLogada)
                    .Replace("[Dia]", age.Data.ToString("dd/MM/yyyy"))
                    .Replace("[Horario]", age.HoraInicial.ToString("00:00"));

            if (age.EnviarSMS.HasValue && age.EnviarSMS.Value && age.Data == DateTime.Today &&
                !string.IsNullOrEmpty(model.CelularCliente) && model.CelularCliente.Length >= 14)
            {
                age.EnviouSMS = UtilAgenda.EnviarSMS(msg, model.CelularCliente, string.Empty, string.Empty);
                if (empresa.QtdSMSBonus > 0)
                    empresa.QtdSMSBonus -= 1;
                else if (empresa.QtdSMSPago > 0)
                    empresa.QtdSMSPago -= 1;

                await _empresaAppService.Save(empresa);
            }
            else if (age.EnviarSMS.HasValue && age.EnviarSMS.Value && age.Data > DateTime.Today)
            {
                age.EnviouSMS = UtilAgenda.EnviarSMS(msg, model.CelularCliente, age.Data.ToString("dd-MM-yyyy"), "08:30");
                if (empresa.QtdSMSBonus > 0)
                    empresa.QtdSMSBonus -= 1;
                else if (empresa.QtdSMSPago > 0)
                    empresa.QtdSMSPago -= 1;

                await _empresaAppService.Save(empresa);
            }

            await _agendaAppService.Save(age);


            return RedirectToAction("Index");
        }

        public async Task<ActionResult> ResgataAgenda(int id, string start, string end)
        {
            try
            {
                var agendas = Mapper.Map<IEnumerable<Agenda>, IEnumerable<AgendaViewModel>>(await _agendaAppService.GetByProfissionalIdAndPeriodo(id, Convert.ToDateTime(start.Substring(8, 2) + start.Substring(4, 3) + start.Substring(11, 4)), Convert.ToDateTime(end.Substring(8, 2) + end.Substring(4, 3) + end.Substring(11, 4))));
                return Json(agendas, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> CancelarAgenda(int id)
        {
            try
            {
                var agenda = await _agendaAppService.GetById(id);

                if (agenda != null)
                {
                    agenda.Status = false;
                    await _agendaAppService.Save(agenda);
                }
                return Json(agenda, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> AtualizarAgenda(int id, string start, string end)
        {
            try
            {
                var agenda = await _agendaAppService.GetById(id);

                if (agenda != null)
                {
                    agenda.Data = Convert.ToDateTime(start.Substring(8, 2) + start.Substring(4, 3) + start.Substring(11, 4));
                    agenda.HoraInicial = Convert.ToInt32(start.Substring(16, 2) + start.Substring(19, 2));
                    agenda.HoraFinal = Convert.ToInt32(end.Substring(16, 2) + end.Substring(19, 2));

                    await _agendaAppService.Save(agenda);
                }
                return Json(agenda, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
    }
}