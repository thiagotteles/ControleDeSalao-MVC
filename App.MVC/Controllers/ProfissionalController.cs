using System;
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

namespace App.MVC.Controllers
{
    public class ProfissionalController : Controller
    {
        private readonly IProfissionalAppService _profissionalAppService;
        

        public ProfissionalController(IProfissionalAppService profissionalAppService)
        {
            _profissionalAppService = profissionalAppService;
            
        }

        // GET: Profissional
        public async Task<ActionResult> Index()
        {
            try
            {
                if (!Cookies.IdUsuarioLogado.HasValue)
                    return RedirectToAction("Login", "Usuario");

                if (Cookies.DataBloqueio <= DateTime.Today)
                    return RedirectToAction("Bloqueio", "Home");

                var profissionalViewModels = Mapper.Map<IEnumerable<Profissional>, IEnumerable<ProfissionalViewModel>>(await _profissionalAppService.GetAll());

                return View(profissionalViewModels);
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<ActionResult> Detalhe(int id)
        {
            try
            {
                var pro = Mapper.Map<Profissional, ProfissionalViewModel>(await _profissionalAppService.GetById(id));
                pro.SDataContratacao = pro.DataContratacao.HasValue ? pro.DataContratacao.Value.ToString("dd/MM/yyyy") : string.Empty;
                return PartialView(pro);
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
                if (ModelState.IsValid)
                {
                    Session["TodosProfissionais"] = null;
                    if (!string.IsNullOrEmpty(model.SDataContratacao))
                    {
                        DateTime dtContratacao;
                        DateTime.TryParse(model.SDataContratacao, out dtContratacao);
                        model.DataContratacao = dtContratacao > DateTime.MinValue ? dtContratacao : (DateTime?)null;
                    }

                    var profissional = Mapper.Map<ProfissionalViewModel, Profissional>(model);

                    await _profissionalAppService.Save(profissional);
                    TempData["swal"] = SweetAlert.MensagemSucesso("As informações foram salvas!");
                    ModelState.Clear();
                    
                    return RedirectPermanent("Index");
                }
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
                var profissional = await _profissionalAppService.GetById(id);
                profissional.Status = false;
                await _profissionalAppService.Save(profissional);
                Session["TodosProfissionais"] = null;
                return Json(profissional, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
    }
}