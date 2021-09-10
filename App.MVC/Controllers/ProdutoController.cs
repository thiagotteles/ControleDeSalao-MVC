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
    public class ProdutoController : Controller
    {
        private readonly IProdutoAppService _produtoAppService;
        

        public ProdutoController(IProdutoAppService produtoAppService)
        {
            _produtoAppService = produtoAppService;
            
        }

        // GET: Produto
        public async Task<ActionResult> Index()
        {
            try
            {
                
                if (!Cookies.IdUsuarioLogado.HasValue)
                    return RedirectToAction("Login", "Usuario");

                if (Cookies.DataBloqueio <= DateTime.Today)
                    return RedirectToAction("Bloqueio", "Home");

                var produtoViewModel = Mapper.Map<IEnumerable<Produto>, IEnumerable<ProdutoViewModel>>(await _produtoAppService.GetAll());

                return View(produtoViewModel);
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<ActionResult> ResgataProduto(string produto)
        {
            var produtoViewModel = Mapper.Map<Produto, ProdutoViewModel>(await _produtoAppService.GetByNome(produto)) ?? new ProdutoViewModel();
            return Json(produtoViewModel, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Detalhe(int id)
        {
            try
            {
                var prod = Mapper.Map<Produto, ProdutoViewModel>(await _produtoAppService.GetById(id));
                return PartialView(prod);
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return View("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Detalhe(ProdutoViewModel model)
        {
            try
            {
                var produto = Mapper.Map<ProdutoViewModel, Produto>(model);

                await _produtoAppService.Save(produto);
                TempData["swal"] = SweetAlert.MensagemSucesso("As informações foram salvas!");
                ModelState.Clear();
                Session["TodosProdutos"] = null;
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
                var produto = await _produtoAppService.GetById(id);
                produto.Status = false;
                await _produtoAppService.Save(produto);

                return Json(produto, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
    }
}