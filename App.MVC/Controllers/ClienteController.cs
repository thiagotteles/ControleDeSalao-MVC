using System;
using System.Collections.Generic;
using App.MVC.Util;
using App.MVC.ViewModels;
using AutoMapper;
using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ControleDeSalao.Infra.Cross.Security;

namespace App.MVC.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteAppService _clienteAppService;
        

        public ClienteController(IClienteAppService clienteAppService)
        {
            _clienteAppService = clienteAppService;
            
        }

        // GET: Cliente
        public async Task<ActionResult> Index()
        {
            try
            {
                
                if (!Cookies.IdUsuarioLogado.HasValue)
                    return RedirectToAction("Login", "Usuario");

                if (Cookies.DataBloqueio <= DateTime.Today)
                    return RedirectToAction("Bloqueio", "Home");

                var clienteViewModel = Mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteViewModel>>(await _clienteAppService.GetAll());

                return View(clienteViewModel);
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<ActionResult> ClienteAutoComplete(string id)
        {
            //var clientes = await _clienteAppService.AutoComplete(id.ToLower());
            var clientes = await _clienteAppService.GetAll();
            var routeList = clientes.Select(r => new { id = r.Id, label = r.Nome, value = "NomeCliente" }).OrderBy(o => o.label);
            return Json(routeList, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetAll()
        {
            //var clientes = await _clienteAppService.AutoComplete(id.ToLower());
            var clientes = await _clienteAppService.GetAll();
            var routeList = clientes.Select(r => new { r.Nome }).OrderBy(o => o.Nome);
            return Json(routeList, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ResgataCliente(string nome)
        {
            var clienteViewModel = Mapper.Map<Cliente, ClienteViewModel>(await _clienteAppService.GetByNome(nome)) ?? new ClienteViewModel();
            return Json(clienteViewModel, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Detalhe(int id)
        {
            try
            {
                var cli = Mapper.Map<Cliente, ClienteViewModel>(await _clienteAppService.GetById(id));
                cli.SDataNascimento = cli.DataNascimento.HasValue ? cli.DataNascimento.Value.ToString("dd/MM/yyyy") : string.Empty;
                return PartialView(cli);
            }
            catch (Exception ex)
            {   
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return View("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Detalhe(ClienteViewModel clienteViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    clienteViewModel.DataNascimento = string.IsNullOrEmpty(clienteViewModel.SDataNascimento) ? (DateTime?)null : Convert.ToDateTime(clienteViewModel.SDataNascimento);
                    var cliente = Mapper.Map<ClienteViewModel, Cliente>(clienteViewModel);
                    
                    await _clienteAppService.Save(cliente);
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
                var cliente = await _clienteAppService.GetById(id);
                cliente.Status = false;
                await _clienteAppService.Save(cliente);

                return Json(cliente, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
    }
}