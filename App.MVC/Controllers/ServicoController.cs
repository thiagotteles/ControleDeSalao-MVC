using System;
using System.Collections.Generic;
using System.Linq;
using App.MVC.Util;
using App.MVC.ViewModels;
using AutoMapper;
using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using System.Threading.Tasks;
using System.Web.Mvc;
using ControleDeSalao.Infra.Cross.Security;

namespace App.MVC.Controllers
{
    public class ServicoController : Controller
    {
        private readonly IServicoAppService _servicoAppService;
        private readonly ICategoriaAppService _categoriaAppService;
        private readonly IProfissionalAppService _profissionalAppService;
        private readonly IServicoPreCadastradoAppService _servicoPreCadastradoAppService;
        private readonly IComissaoPersonalizadaAppService _comissaoPersonalizadaAppService;
        private readonly IProfissionalCategoriaAppService _profissionalCategoriaAppService;
        

        public ServicoController(IServicoAppService iServicoAppService, ICategoriaAppService categoriaAppService, IServicoPreCadastradoAppService servicoPreCadastradoAppService, IComissaoPersonalizadaAppService comissaoPersonalizadaAppService, IProfissionalCategoriaAppService profissionalCategoriaAppService, IProfissionalAppService profissionalAppService)
        {
            _servicoAppService = iServicoAppService;
            _categoriaAppService = categoriaAppService;
            _servicoPreCadastradoAppService = servicoPreCadastradoAppService;
            _comissaoPersonalizadaAppService = comissaoPersonalizadaAppService;
            _profissionalCategoriaAppService = profissionalCategoriaAppService;
            _profissionalAppService = profissionalAppService;
            
        }


        // GET: Servico
        public async Task<ActionResult> Index()
        {
            try
            {
                if (!Cookies.IdUsuarioLogado.HasValue)
                    return RedirectToAction("Login", "Usuario");

                if (Cookies.DataBloqueio <= DateTime.Today)
                    return RedirectToAction("Bloqueio", "Home");

                var servicosViewModels = Mapper.Map<IEnumerable<Servico>, IEnumerable<ServicoViewModel>>(await _servicoAppService.GetAll());
                ViewBag.Categorias = Mapper.Map<IEnumerable<Categoria>, IEnumerable<CategoriaViewModel>>(await _categoriaAppService.GetAll());
                ViewBag.ServicosPreCadastrados = Mapper.Map<IEnumerable<ServicoPreCadastrado>, IEnumerable<ServicoPreCadastradoViewModel>>(await _servicoPreCadastradoAppService.GetAll());
                ViewBag.idServicosPre = servicosViewModels.Where(m => m.ServicoPreCadastradoId.HasValue).Select(m => m.ServicoPreCadastradoId).ToArray();

                return View(servicosViewModels);
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<ActionResult> ResgataServico(string servico)
        {
            var ser = Mapper.Map<Servico, ServicoViewModel>(await _servicoAppService.GetByNome(servico));
            return Json(ser, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Detalhe(int id)
        {
            try
            {
                var ser = Mapper.Map<Servico, ServicoViewModel>(await _servicoAppService.GetById(id));
                ser.Valor = ser.Valor > 0 ? ser.Valor : (double?)null;
                ser.Duracao = ser.TempoDuracao;
                return PartialView(ser);
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return View("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Detalhe(ServicoViewModel model)
        {
            try
            {
                Session["TodosServicos"] = null;

                var servico = Mapper.Map<ServicoViewModel, Servico>(model);
                servico.HoraDuracao = Convert.ToInt32(model.Duracao.Split(':')[0]);// model.HoraDuracao;
                servico.MinutoDuracao = Convert.ToInt32(model.Duracao.Split(':')[1]);// model.MinutoDuracao;
                await _servicoAppService.Save(servico);
                TempData["swal"] = SweetAlert.MensagemSucesso("As informações foram salvas!");
                ModelState.Clear();

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
                var servico = await _servicoAppService.GetById(id);
                servico.Status = false;
                await _servicoAppService.Save(servico);

                return Json(servico, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<ActionResult> AddServicoPre(int id)
        {
            try
            {
                var idServicoCadastrado = Convert.ToInt32(id) * -1;
                var servicoPreCadastrado = await _servicoPreCadastradoAppService.GetById(idServicoCadastrado);
                var servicos = await _servicoAppService.GetAll();

                var ser = new Servico();
                if (servicoPreCadastrado != null && servicos.Where(m => m.ServicoPreCadastradoId == idServicoCadastrado).Count() <= 0)
                {
                    Session["TodosServicos"] = null;
                    ser.Descricao = servicoPreCadastrado.Descricao;
                    ser.CategoriaId = servicoPreCadastrado.CategoriaId;
                    ser.ServicoPreCadastradoId = idServicoCadastrado;
                    ser.Valor = 0;
                    ser.HoraDuracao = 0;
                    ser.MinutoDuracao = 0;
                    await _servicoAppService.Save(ser);
                }

                return Json(ser, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

    }
}