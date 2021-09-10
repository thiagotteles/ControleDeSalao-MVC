using App.MVC.Util;
using App.MVC.ViewModels;
using AutoMapper;
using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Infra.Cross.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Enum = ControleDeSalao.Domain.Enums.Enum;

namespace App.MVC.Controllers
{
    public class ComandaController : Controller
    {
        private readonly IComandaAppService _comandaAppService;
        private readonly IClienteAppService _clienteAppService;
        private readonly IProfissionalAppService _profissionalAppService;
        private readonly IProdutoComandaAppService _produtoComandaAppService;
        private readonly IServicoComandaAppService _servicoComandaAppService;
        private readonly IServicoAppService _servicoAppService;
        private readonly IProdutoAppService _produtoAppService;
        private readonly IAgendaAppService _agendaAppService;
        private readonly IComandaPagamentoAppService _comandaPagamentoAppService;
        private readonly IComandaPagamentoParcelaAppService _comandaPagamentoParcelaAppService;
        private readonly IPlanoDeContaAppService _planoDeContaAppService;
        private readonly IFinanceiroAppService _financeiroAppService;
        private readonly IComissaoPersonalizadaAppService _comissaoPersonalizadaAppService;
        private readonly IComissaoAppService _comissaoAppService;


        public ComandaController(IComandaAppService comandaAppService, IClienteAppService clienteAppService, IProfissionalAppService profissionalAppService, IProdutoComandaAppService produtoComandaAppService, IServicoComandaAppService servicoComandaAppService, IServicoAppService servicoAppService, IProdutoAppService produtoAppService, IAgendaAppService agendaAppService, IComandaPagamentoAppService comandaPagamentoAppService, IComandaPagamentoParcelaAppService comandaPagamentoParcelaAppService, IPlanoDeContaAppService planoDeContaAppService, IFinanceiroAppService financeiroAppService, IComissaoPersonalizadaAppService comissaoPersonalizadaAppService, IComissaoAppService comissaoAppService)
        {
            _comandaAppService = comandaAppService;
            _clienteAppService = clienteAppService;
            _profissionalAppService = profissionalAppService;
            _produtoComandaAppService = produtoComandaAppService;
            _servicoComandaAppService = servicoComandaAppService;
            _servicoAppService = servicoAppService;
            _produtoAppService = produtoAppService;
            _agendaAppService = agendaAppService;
            _comandaPagamentoAppService = comandaPagamentoAppService;
            _comandaPagamentoParcelaAppService = comandaPagamentoParcelaAppService;
            _planoDeContaAppService = planoDeContaAppService;
            _financeiroAppService = financeiroAppService;
            _comissaoPersonalizadaAppService = comissaoPersonalizadaAppService;
            _comissaoAppService = comissaoAppService;

            _comissaoPersonalizadaAppService = comissaoPersonalizadaAppService;
        }


        // GET: Comanda
        public async Task<ActionResult> Index(string data, string status)
        {
            try
            {
                TempData["swal"] = null;
                if (!Cookies.IdUsuarioLogado.HasValue)
                    return RedirectToAction("Login", "Usuario");

                if (Cookies.DataBloqueio <= DateTime.Today)
                    return RedirectToAction("Bloqueio", "Home");

                Session["dt_sel_com"] = data;
                Session["sts_sel_com"] = status;
                Session["det_com"] = null;
                Session["pagto_compra"] = null;
                var dt = !string.IsNullOrEmpty(data) ? Convert.ToDateTime(data) : (DateTime?)null;

                Enum.StatusComanda sts;
                if (string.IsNullOrEmpty(status))
                    sts = Enum.StatusComanda.Aberta;
                else
                    System.Enum.TryParse(status, out sts);

                var comandas = Cookies.IdProfissionalLogado.HasValue ? Mapper.Map<IEnumerable<Comanda>, IEnumerable<ComandaViewModel>>(await _comandaAppService.GetByProfissionalID(Cookies.IdProfissionalLogado.Value)).Where(m => m.Status == Enum.StatusComanda.Aberta).OrderByDescending(m => m.Data) : Mapper.Map<IEnumerable<Comanda>, IEnumerable<ComandaViewModel>>(await _comandaAppService.GetByStatusAndData(sts, dt)).OrderByDescending(m => m.Data);
                foreach (var comanda in comandas)
                {
                    if (comanda.ClienteId.HasValue)
                        comanda.Cliente = Mapper.Map<Cliente, ClienteViewModel>(await _clienteAppService.GetById(comanda.ClienteId.Value));

                    if (comanda.ProfissionalId.HasValue)
                        comanda.Profissional = Mapper.Map<Profissional, ProfissionalViewModel>(await _profissionalAppService.GetById(comanda.ProfissionalId.Value));
                }

                return View(comandas);
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return View("Index");
            }
        }

        public async Task<ActionResult> Novo()
        {
            try
            {
                Session["det_com"] = null;
                var comanda = new ComandaViewModel { Data = DateTime.Today, Servicos = new List<ServicoComandaViewModel>(), Produtos = new List<ProdutoComandaViewModel>() };
                comanda.Id = await _comandaAppService.Save(Mapper.Map<ComandaViewModel, Comanda>(comanda));
                Session["det_com"] = comanda;

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
                Session["TodosServicos"] = Session["TodosServicos"] ?? Mapper.Map<IEnumerable<Servico>, IEnumerable<ServicoViewModel>>(await _servicoAppService.GetAll());
                Session["TodosProdutos"] = Session["TodosProdutos"] ?? Mapper.Map<IEnumerable<Produto>, IEnumerable<ProdutoViewModel>>(await _produtoAppService.GetAll());
                return View("Detalhe", comanda);
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return RedirectToAction("Index");
            }
        }

        public async Task<ActionResult> Incluir(int id)
        {
            try
            {
                var agenda = await _agendaAppService.GetById(id);
                if (agenda != null)
                {
                    ComandaViewModel comanda = null;

                    if (agenda.ComandaId.HasValue)
                        comanda = Mapper.Map<Comanda, ComandaViewModel>(await _comandaAppService.GetById(agenda.ComandaId.Value));

                    if (comanda != null)
                    {
                        if (comanda.ClienteId.HasValue)
                            comanda.Cliente = Mapper.Map<Cliente, ClienteViewModel>(await _clienteAppService.GetById(comanda.ClienteId.Value));

                        if (comanda.ProfissionalId.HasValue)
                            comanda.Profissional = Mapper.Map<Profissional, ProfissionalViewModel>(await _profissionalAppService.GetById(comanda.ProfissionalId.Value));

                        comanda.NomeCliente = comanda.Cliente.Nome;
                        comanda.NomeProfissional = comanda.Profissional.Nome;
                        Session["det_com"] = comanda;
                        if (comanda.Status == Enum.StatusComanda.Aberta)
                            return RedirectToAction("Detalhe", new { id = comanda.Id });

                        return RedirectToAction("Visualizar", new { id = comanda.Id });
                    }

                    var com = (await _comandaAppService.GetByClienteIdStatusAndPeriodo(agenda.ClienteId.Value, Enum.StatusComanda.Aberta, agenda.Data, agenda.Data)).ToList();
                    if (com.Any())
                    {
                        comanda = Mapper.Map<Comanda, ComandaViewModel>(com[0]);
                        comanda.ValorTotal += agenda.Valor;
                        await _comandaAppService.Save(Mapper.Map<ComandaViewModel, Comanda>(comanda));

                        var serv = new ServicoComanda();
                        serv.ComandaId = comanda.Id;
                        serv.ServicoId = agenda.ServicoId;
                        serv.Valor = agenda.Valor;
                        await _servicoComandaAppService.Save(serv);

                        agenda.ComandaId = comanda.Id;
                        await _agendaAppService.Save(agenda);

                        Session["det_com"] = comanda;
                        return RedirectToAction("Detalhe", new { id = comanda.Id });
                    }


                    comanda = new ComandaViewModel();
                    comanda.ProfissionalId = agenda.ProfissionalId;
                    comanda.ClienteId = agenda.ClienteId;
                    comanda.NomeCliente = agenda.Cliente.Nome;
                    comanda.NomeProfissional = agenda.Profissional.Nome;
                    comanda.Data = agenda.Data;
                    comanda.ValorTotal = agenda.Valor;
                    comanda.Observacao = agenda.Observacao;
                    comanda.Id = await _comandaAppService.Save(Mapper.Map<ComandaViewModel, Comanda>(comanda));

                    var servicoComanda = new ServicoComanda();
                    servicoComanda.ComandaId = comanda.Id;
                    servicoComanda.ServicoId = agenda.ServicoId;
                    servicoComanda.Valor = agenda.Valor;
                    await _servicoComandaAppService.Save(servicoComanda);

                    agenda.ComandaId = comanda.Id;
                    await _agendaAppService.Save(agenda);

                    Session["det_com"] = comanda;
                    return RedirectToAction("Detalhe", new { id = comanda.Id });
                }

                return View("Index");
            }
            catch (Exception ex)
            {

                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return RedirectToAction("Index", "Agenda");
            }
        }

        public async Task<ActionResult> Detalhe(int id)
        {
            try
            {
                Session["det_com"] = null;
                TempData["swal"] = null;
                var comanda = Mapper.Map<Comanda, ComandaViewModel>(await _comandaAppService.GetById(id));
                if (comanda.ClienteId.HasValue)
                    comanda.Cliente = Mapper.Map<Cliente, ClienteViewModel>(await _clienteAppService.GetById(comanda.ClienteId.Value));

                if (comanda.ProfissionalId.HasValue)
                    comanda.Profissional = Mapper.Map<Profissional, ProfissionalViewModel>(await _profissionalAppService.GetById(comanda.ProfissionalId.Value));

                comanda.Servicos = Mapper.Map<IEnumerable<ServicoComanda>, IEnumerable<ServicoComandaViewModel>>(await _servicoComandaAppService.GetByComandaId(id)).ToList();
                comanda.Produtos = Mapper.Map<IEnumerable<ProdutoComanda>, IEnumerable<ProdutoComandaViewModel>>(await _produtoComandaAppService.GetByComandaId(id)).ToList();

                comanda.NomeCliente = comanda.Cliente != null ? comanda.Cliente.Nome : string.Empty;
                comanda.NomeProfissional = comanda.Profissional != null ? comanda.Profissional.Nome : string.Empty;

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
                Session["TodosServicos"] = Session["TodosServicos"] ?? Mapper.Map<IEnumerable<Servico>, IEnumerable<ServicoViewModel>>(await _servicoAppService.GetAll());
                Session["TodosProdutos"] = Session["TodosProdutos"] ?? Mapper.Map<IEnumerable<Produto>, IEnumerable<ProdutoViewModel>>(await _produtoAppService.GetAll());

                Session["det_com"] = comanda;
                return View("Detalhe", comanda);
            }
            catch (Exception ex)
            {

                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return View("Index");
            }
        }

        public async Task<ActionResult> AddServico(ComandaViewModel model)
        {
            try
            {
                TempData["swal"] = null;
                var comanda = (ComandaViewModel)Session["det_com"];

                if (string.IsNullOrEmpty(model.NovoServico) || string.IsNullOrEmpty(model.NovoValor))
                    return View("Detalhe", comanda);

                var ser = await _servicoAppService.GetByNome(model.NovoServico);
                if (!string.IsNullOrEmpty(model.NovaComissao) && !string.IsNullOrEmpty(model.NovaHoraDuracao))
                {
                    ser.Valor = Convert.ToDouble(model.NovoValor);
                    ser.ValorParaProfissional = Convert.ToDouble(model.NovaComissao);
                    ser.HoraDuracao = Convert.ToInt32(model.NovaHoraDuracao.Split(':')[0]);
                    ser.MinutoDuracao = Convert.ToInt32(model.NovaHoraDuracao.Split(':')[1]);

                    await _servicoAppService.Save(ser);
                }

                var servicoComanda = new ServicoComanda
                {
                    ComandaId = comanda.Id,
                    ServicoId = ser.Id,
                    Valor = Convert.ToDouble(model.NovoValor),
                };

                if (comanda.Servicos == null)
                    comanda.Servicos = new List<ServicoComandaViewModel>();

                comanda.Servicos.Add(Mapper.Map<ServicoComanda, ServicoComandaViewModel>(servicoComanda));

                await _servicoComandaAppService.Save(servicoComanda);

                var com = await _comandaAppService.GetById(comanda.Id);
                com.ValorTotal = (comanda.Servicos.Sum(m => m.Valor) + comanda.Produtos.Sum(m => m.ValorTotal)) - (comanda.ValorDesconto.HasValue ? comanda.ValorDesconto.Value : 0);
                await _comandaAppService.Save(com);

                ModelState.Remove("NovoServico");
                ModelState.Remove("NovoValor");
                Session["det_com"] = comanda;
                return RedirectToAction("Detalhe", new { id = comanda.Id });
            }
            catch (Exception ex)
            {

                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return View("Detalhe", model);
            }
        }

        public async Task<ActionResult> DelServico(int? id)
        {
            try
            {
                TempData["swal"] = null;
                var comanda = (ComandaViewModel)Session["det_com"];

                for (int i = 0; i < comanda.Servicos.Count; i++)
                {
                    if (comanda.Servicos[i].Id == id)
                    {
                        comanda.Servicos.RemoveAt(i);
                        break;
                    }
                }

                if (id.HasValue && id.Value > 0)
                {
                    var servicoComanda = await _servicoComandaAppService.GetById(id.Value);
                    if (servicoComanda != null)
                    {
                        await _servicoComandaAppService.Remove(servicoComanda);

                        var com = await _comandaAppService.GetById(servicoComanda.ComandaId);
                        com.ValorTotal = (comanda.Servicos.Sum(m => m.Valor) + comanda.Produtos.Sum(m => m.ValorTotal)) - (comanda.ValorDesconto.HasValue ? comanda.ValorDesconto.Value : 0);
                        await _comandaAppService.Save(com);
                    }
                }
                Session["det_com"] = comanda;
                return View("Detalhe", ((ComandaViewModel)Session["det_com"]));
            }
            catch (Exception ex)
            {

                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return View("Detalhe", ((ComandaViewModel)Session["det_com"]));
            }
        }

        public async Task<ActionResult> AddProduto(ComandaViewModel model)
        {
            try
            {
                TempData["swal"] = null;
                var comanda = (ComandaViewModel)Session["det_com"];

                if (string.IsNullOrEmpty(model.NovoProduto) || string.IsNullOrEmpty(model.NovoValorProduto) || string.IsNullOrEmpty(model.NovaQtdProduto))
                    return View("Detalhe", comanda);

                var prod = await _produtoAppService.GetByNome(model.NovoProduto);

                var produtoComanda = new ProdutoComanda()
                {
                    ComandaId = comanda.Id,
                    ProdutoId = prod.Id,
                    Quantidade = Convert.ToDouble(model.NovaQtdProduto),
                    ValorProduto = Convert.ToDouble(model.NovoValorProduto),
                    ValorTotal = Convert.ToDouble(model.NovoValorProduto) * Convert.ToDouble(model.NovaQtdProduto),
                };

                await _produtoComandaAppService.Save(produtoComanda);

                if (comanda.Produtos == null)
                    comanda.Produtos = new List<ProdutoComandaViewModel>();

                comanda.Produtos.Add(Mapper.Map<ProdutoComanda, ProdutoComandaViewModel>(produtoComanda));

                var com = await _comandaAppService.GetById(comanda.Id);
                com.ValorTotal = (comanda.Servicos.Sum(m => m.Valor) + comanda.Produtos.Sum(m => m.ValorTotal)) - (comanda.ValorDesconto.HasValue ? comanda.ValorDesconto.Value : 0);
                await _comandaAppService.Save(com);

                ModelState.Remove("NovoProduto");
                ModelState.Remove("NovaQtdProduto");
                ModelState.Remove("NovaUnidade");
                ModelState.Remove("NovoValorProduto");
                Session["det_com"] = comanda;
                return RedirectToAction("Detalhe", new { id = comanda.Id });
            }
            catch (Exception ex)
            {

                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return View("Detalhe", model);
            }
        }

        public async Task<ActionResult> DelProduto(int? id)
        {
            try
            {
                TempData["swal"] = null;
                var comanda = ((ComandaViewModel)Session["det_com"]);
                for (int i = 0; i < comanda.Produtos.Count; i++)
                {
                    if (comanda.Produtos[i].Id == id)
                    {
                        comanda.Produtos.RemoveAt(i);
                        break;
                    }
                }

                if (id.HasValue && id.Value > 0)
                {
                    var produtosComanda = await _produtoComandaAppService.GetById(id.Value);
                    if (produtosComanda != null)
                    {
                        await _produtoComandaAppService.Remove(produtosComanda);

                        var com = await _comandaAppService.GetById(produtosComanda.ComandaId);
                        com.ValorTotal = (comanda.Servicos.Sum(m => m.Valor) + comanda.Produtos.Sum(m => m.ValorTotal)) - (comanda.ValorDesconto.HasValue ? comanda.ValorDesconto.Value : 0);
                        await _comandaAppService.Save(com);
                    }
                }

                Session["det_com"] = comanda;

            }
            catch (Exception ex)
            {

                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
            }

            return View("Detalhe", ((ComandaViewModel)Session["det_com"]));
        }

        public async Task<ActionResult> SalvarProfissional(string profissional, string id)
        {
            try
            {
                var ser = await _profissionalAppService.GetByNome(profissional) ?? new Profissional();
                var comanda = Mapper.Map<Comanda, ComandaViewModel>(await _comandaAppService.GetById(Convert.ToInt32(id)));
                comanda.ProfissionalId = ser.Id;
                if (comanda.ClienteId.HasValue)
                    comanda.Cliente = Mapper.Map<Cliente, ClienteViewModel>(await _clienteAppService.GetById(comanda.ClienteId.Value));
                comanda.NomeCliente = comanda.Cliente != null ? comanda.Cliente.Nome : string.Empty;
                comanda.NomeProfissional = ser.Nome;
                comanda.Servicos = Mapper.Map<IEnumerable<ServicoComanda>, IEnumerable<ServicoComandaViewModel>>(await _servicoComandaAppService.GetByComandaId(comanda.Id)).ToList();
                comanda.Produtos = Mapper.Map<IEnumerable<ProdutoComanda>, IEnumerable<ProdutoComandaViewModel>>(await _produtoComandaAppService.GetByComandaId(comanda.Id)).ToList();

                await _comandaAppService.Save(Mapper.Map<ComandaViewModel, Comanda>(comanda));
                Session["det_com"] = comanda;

                return Json(ser, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> SalvarCliente(string cliente, string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(cliente))
                {
                    var comanda = Mapper.Map<Comanda, ComandaViewModel>(await _comandaAppService.GetById(Convert.ToInt32(id)));
                    var cli = await _clienteAppService.GetByNome(cliente);
                    if (cli == null)
                    {
                        cli = new Cliente { Nome = cliente };
                        cli.Id = await _clienteAppService.Save(cli);
                    }

                    comanda.ClienteId = cli.Id;

                    if (comanda.ProfissionalId.HasValue)
                        comanda.Profissional = Mapper.Map<Profissional, ProfissionalViewModel>(await _profissionalAppService.GetById(comanda.ProfissionalId.Value));

                    comanda.NomeProfissional = comanda.Profissional != null ? comanda.Profissional.Nome : string.Empty;
                    comanda.NomeCliente = cli.Nome;
                    comanda.Servicos = Mapper.Map<IEnumerable<ServicoComanda>, IEnumerable<ServicoComandaViewModel>>(await _servicoComandaAppService.GetByComandaId(comanda.Id)).ToList();
                    comanda.Produtos = Mapper.Map<IEnumerable<ProdutoComanda>, IEnumerable<ProdutoComandaViewModel>>(await _produtoComandaAppService.GetByComandaId(comanda.Id)).ToList();

                    await _comandaAppService.Save(Mapper.Map<ComandaViewModel, Comanda>(comanda));
                    Session["det_com"] = comanda;
                    return Json(cli, JsonRequestBehavior.AllowGet);
                }

                return Json(new Cliente(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> SalvarData(string data, string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(data))
                {
                    var comanda = Mapper.Map<Comanda, ComandaViewModel>(await _comandaAppService.GetById(Convert.ToInt32(id)));

                    comanda.SData = data;
                    comanda.Data = Convert.ToDateTime(comanda.SData);

                    if (comanda.ProfissionalId.HasValue)
                        comanda.Profissional = Mapper.Map<Profissional, ProfissionalViewModel>(await _profissionalAppService.GetById(comanda.ProfissionalId.Value));
                    
                    comanda.NomeCliente = comanda.Cliente != null ? comanda.Cliente.Nome : string.Empty;
                    comanda.NomeProfissional = comanda.Profissional != null ? comanda.Profissional.Nome : string.Empty;
                    comanda.Servicos = Mapper.Map<IEnumerable<ServicoComanda>, IEnumerable<ServicoComandaViewModel>>(await _servicoComandaAppService.GetByComandaId(comanda.Id)).ToList();
                    comanda.Produtos = Mapper.Map<IEnumerable<ProdutoComanda>, IEnumerable<ProdutoComandaViewModel>>(await _produtoComandaAppService.GetByComandaId(comanda.Id)).ToList();

                    await _comandaAppService.Save(Mapper.Map<ComandaViewModel, Comanda>(comanda));
                    Session["det_com"] = comanda;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }

                return Json(new Cliente(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Salvar(ComandaViewModel model)
        {
            try
            {
                var comanda = (ComandaViewModel)Session["det_com"];

                if (comanda.Cliente == null && !string.IsNullOrEmpty(model.NomeCliente))
                {
                    var cli = await _clienteAppService.GetByNome(model.NomeCliente);
                    if (cli == null)
                    {
                        cli = new Cliente { Nome = model.NomeCliente };
                        cli.Id = await _clienteAppService.Save(cli);
                    }

                    comanda.ClienteId = cli.Id;
                }
                else if (comanda.Cliente != null && comanda.Cliente.Nome != model.NomeCliente)
                {
                    var cli = await _clienteAppService.GetByNome(model.NomeCliente);
                    if (cli == null)
                    {
                        cli = new Cliente { Nome = model.NomeCliente };
                        cli.Id = await _clienteAppService.Save(cli);
                    }

                    comanda.ClienteId = cli.Id;
                }

                if (comanda.Profissional == null)
                {
                    var pro = await _profissionalAppService.GetByNome(model.NomeProfissional);
                    if (pro != null)
                        comanda.ProfissionalId = pro.Id;
                }
                else if (comanda.Profissional != null && comanda.Profissional.Nome != model.NomeProfissional)
                {
                    var pro = await _profissionalAppService.GetByNome(model.NomeProfissional);
                    if (pro != null)
                        comanda.ProfissionalId = pro.Id;
                }

                comanda.ValorDesconto = model.ValorDesconto;
                comanda.Observacao = model.Observacao;
                comanda.Data = Convert.ToDateTime(model.SData);
                comanda.ValorTotal = (comanda.Servicos.Sum(m => m.Valor) + comanda.Produtos.Sum(m => m.ValorTotal)) - (model.ValorDesconto.HasValue ? model.ValorDesconto.Value : 0);
                comanda.NomeCliente = comanda.Cliente != null ? comanda.Cliente.Nome : string.Empty;
                comanda.NomeProfissional = comanda.Profissional != null ? comanda.Profissional.Nome : string.Empty;
                await _comandaAppService.Save(Mapper.Map<ComandaViewModel, Comanda>(comanda));
                SweetAlert.MensagemSucesso("Comanda Salva com sucesso");
            }
            catch (Exception ex)
            {

                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return View("Detalhe", model);
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Excluir(int id)
        {
            try
            {
                var comanda = await _comandaAppService.GetById(id);
                if (comanda != null)
                {
                    comanda.Status = Enum.StatusComanda.Excluida;
                    comanda.IdUsuarioAlteracao = Cookies.IdUsuarioLogado;
                    comanda.DataAlteracao = DatetimeBrasil.HorarioDeBrasilia();
                    await _comandaAppService.Save(comanda);
                }

                return Json(comanda, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> Faturar(int id, string dinheiro, string credito, string debito, string cheque, string promissoria, string pacote)
        {
            try
            {
                var comanda = Mapper.Map<Comanda, ComandaViewModel>(await _comandaAppService.GetById(id));
                if (comanda != null)
                {
                    if (comanda.ClienteId.HasValue && comanda.ClienteId > 0)
                        comanda.Cliente = Mapper.Map<Cliente, ClienteViewModel>(await _clienteAppService.GetById(comanda.ClienteId.Value));

                    if (comanda.ProfissionalId.HasValue && comanda.ProfissionalId > 0)
                        comanda.Profissional = Mapper.Map<Profissional, ProfissionalViewModel>(await _profissionalAppService.GetById(comanda.ProfissionalId.Value));

                    comanda.Servicos = Mapper.Map<IEnumerable<ServicoComanda>, IEnumerable<ServicoComandaViewModel>>(await _servicoComandaAppService.GetByComandaId(id)).ToList();
                    comanda.Produtos = Mapper.Map<IEnumerable<ProdutoComanda>, IEnumerable<ProdutoComandaViewModel>>(await _produtoComandaAppService.GetByComandaId(id)).ToList();

                    var planoDeConta = Mapper.Map<PlanoDeConta, PlanoDeContaViewModel>(await _planoDeContaAppService.GetByTelaPadrao(Enum.TelaPadrao.Comandas));
                    double vlrDinheiro = string.IsNullOrEmpty(dinheiro) ? 0 : Convert.ToDouble(dinheiro);
                    double vlrCredito = string.IsNullOrEmpty(credito) ? 0 : Convert.ToDouble(credito);
                    double vlrDebito = string.IsNullOrEmpty(debito) ? 0 : Convert.ToDouble(debito);
                    double vlrCheque = string.IsNullOrEmpty(cheque) ? 0 : Convert.ToDouble(cheque);
                    double vlrPromissoria = string.IsNullOrEmpty(promissoria) ? 0 : Convert.ToDouble(promissoria);
                    double vlrPacote = string.IsNullOrEmpty(pacote) ? 0 : Convert.ToDouble(pacote);

                    if (comanda.ValorTotal < (vlrDinheiro + vlrCredito + vlrDebito + vlrCheque + vlrPromissoria + vlrPacote))
                    {
                        double vlrTroco = ((vlrDinheiro + vlrCredito + vlrDebito + vlrCheque + vlrPromissoria + vlrPacote) - comanda.ValorTotal);
                        vlrDinheiro = vlrDinheiro - vlrTroco;
                    }

                    var pagamento = new ComandaPagamentoViewModel();
                    pagamento.Valor = comanda.ValorTotal;
                    pagamento.Parcelas = new List<ComandaPagamentoParcelaViewModel>();

                    if (vlrDinheiro > 0)
                    {
                        var parcela = new ComandaPagamentoParcela();
                        parcela.DataVencimento = comanda.Data;
                        parcela.FormaDePagamento = Enum.FormaDePagamento.Dinheiro;
                        parcela.PlanoDeContaId = planoDeConta.Id;
                        parcela.Valor = vlrDinheiro;
                        pagamento.Parcelas.Add(Mapper.Map<ComandaPagamentoParcela, ComandaPagamentoParcelaViewModel>(parcela));
                    }

                    if (vlrCredito > 0)
                    {
                        var parcela = new ComandaPagamentoParcela();
                        parcela.DataVencimento = comanda.Data;
                        parcela.FormaDePagamento = Enum.FormaDePagamento.Crédito;
                        parcela.PlanoDeContaId = planoDeConta.Id;
                        parcela.Valor = vlrCredito;
                        pagamento.Parcelas.Add(Mapper.Map<ComandaPagamentoParcela, ComandaPagamentoParcelaViewModel>(parcela));
                    }

                    if (vlrDebito > 0)
                    {
                        var parcela = new ComandaPagamentoParcela();
                        parcela.DataVencimento = comanda.Data;
                        parcela.FormaDePagamento = Enum.FormaDePagamento.Débito;
                        parcela.PlanoDeContaId = planoDeConta.Id;
                        parcela.Valor = vlrDebito;
                        pagamento.Parcelas.Add(Mapper.Map<ComandaPagamentoParcela, ComandaPagamentoParcelaViewModel>(parcela));
                    }

                    if (vlrCheque > 0)
                    {
                        var parcela = new ComandaPagamentoParcela();
                        parcela.DataVencimento = comanda.Data;
                        parcela.FormaDePagamento = Enum.FormaDePagamento.Cheque;
                        parcela.PlanoDeContaId = planoDeConta.Id;
                        parcela.Valor = vlrCheque;
                        pagamento.Parcelas.Add(Mapper.Map<ComandaPagamentoParcela, ComandaPagamentoParcelaViewModel>(parcela));
                    }

                    if (vlrPromissoria > 0)
                    {
                        var parcela = new ComandaPagamentoParcela();
                        parcela.DataVencimento = comanda.Data;
                        parcela.FormaDePagamento = Enum.FormaDePagamento.Promissória;
                        parcela.PlanoDeContaId = planoDeConta.Id;
                        parcela.Valor = vlrPromissoria;
                        pagamento.Parcelas.Add(Mapper.Map<ComandaPagamentoParcela, ComandaPagamentoParcelaViewModel>(parcela));
                    }

                    if (vlrPacote > 0)
                    {
                        var parcela = new ComandaPagamentoParcela();
                        parcela.DataVencimento = comanda.Data;
                        parcela.FormaDePagamento = Enum.FormaDePagamento.Pacote;
                        parcela.PlanoDeContaId = planoDeConta.Id;
                        parcela.Valor = vlrPacote;
                        pagamento.Parcelas.Add(Mapper.Map<ComandaPagamentoParcela, ComandaPagamentoParcelaViewModel>(parcela));
                    }

                    foreach (var detalhe in comanda.Produtos)
                    {
                        var prd = await _produtoAppService.GetById(detalhe.ProdutoId);
                        prd.QtdEstoque = prd.QtdEstoque.HasValue ? prd.QtdEstoque - detalhe.Quantidade : 0;
                        await _produtoAppService.Save(prd);
                    }

                    var comandaPagamento = new ComandaPagamento();
                    comandaPagamento.ComandaId = comanda.Id;
                    comandaPagamento.QtdParcelas = pagamento.Parcelas.Count();
                    comandaPagamento.Valor = pagamento.Parcelas.Sum(m => m.Valor);
                    comandaPagamento.Id = await _comandaPagamentoAppService.Save(comandaPagamento);

                    foreach (var parcela in pagamento.Parcelas)
                    {
                        var plano = await _planoDeContaAppService.GetById(parcela.PlanoDeContaId);

                        var financeiro = new Financeiro();
                        financeiro.Descricao = string.Format("Comanda por: {0}, Cliente: {1}", comanda.Profissional.Nome, comanda.Cliente.Nome);
                        financeiro.PlanoDeContaId = parcela.PlanoDeContaId;
                        financeiro.NomeQuem = comanda.Cliente != null ? comanda.Cliente.Nome : string.Empty;
                        financeiro.Tipo = plano.Tipo;
                        financeiro.DataVencimento = parcela.DataVencimento;
                        financeiro.Valor = parcela.Valor;
                        financeiro.ValorPago = parcela.Valor;
                        financeiro.DataPagamento = parcela.DataVencimento;
                        financeiro.FormaDePagamento = parcela.FormaDePagamento;
                        financeiro.Status = Enum.StatusFinanceiro.Pago;
                        parcela.FinanceiroId = await _financeiroAppService.Save(financeiro);
                        parcela.ComandaPagamentoId = comandaPagamento.Id;
                        await _comandaPagamentoParcelaAppService.Save(Mapper.Map<ComandaPagamentoParcelaViewModel, ComandaPagamentoParcela>(parcela));
                    }


                    if (comanda.ProfissionalId.HasValue && comanda.ProfissionalId > 0)
                    {
                        foreach (var servico in comanda.Servicos)
                        {
                            var agendas = await _agendaAppService.GetByComandaId(comanda.Id);
                            var agenda = agendas.FirstOrDefault(m => m.ServicoId == servico.ServicoId);
                            var prof = agenda != null ? agenda.ProfissionalId : (int?)null;
                            var comPer = Mapper.Map<ComissaoPersonalizada, ComissaoPersonalizadaViewModel>(await _comissaoPersonalizadaAppService.GetByServicoIdAndProfissionalId(servico.ServicoId.HasValue ? servico.ServicoId.Value : 0, comanda.ProfissionalId.Value));

                            if ((servico.Servico.ValorParaProfissional.HasValue && servico.Servico.ValorParaProfissional > 0) ||
                                (comanda.Profissional.Comissao.HasValue && comanda.Profissional.Comissao.Value > 0) ||
                                (comPer != null && comPer.Comissao > 0))
                            {
                                var porcentagem = comPer != null ? comPer.Comissao : comanda.Profissional.Comissao.HasValue
                                    ? comanda.Profissional.Comissao.Value
                                    : servico.Servico.ValorParaProfissional.Value > 0
                                        ? servico.Servico.ValorParaProfissional.Value
                                        : 0;

                                var comissao = new Comissao();
                                comissao.DataLancamento = comanda.Data;
                                comissao.Descricao = string.Format("{0}, cliente {1}", servico.Servico.Descricao, comanda.Cliente != null ? comanda.Cliente.Nome : string.Empty);
                                comissao.Valor = servico.Valor * porcentagem / 100;
                                comissao.ProfissionalId = prof.HasValue ? prof.Value : comanda.ProfissionalId.Value;

                                await _comissaoAppService.Save(comissao);
                            }
                        }

                        foreach (var produto in comanda.Produtos)
                        {
                            if ((comanda.ProfissionalId.HasValue && comanda.ProfissionalId > 0 && produto.Produto.ValorParaProfissional.HasValue && produto.Produto.ValorParaProfissional > 0) ||
                                (comanda.Profissional.Comissao.HasValue && comanda.Profissional.Comissao.Value > 0))
                            {
                                var porcentagem = comanda.Profissional.Comissao.HasValue
                                    ? comanda.Profissional.Comissao.Value
                                    : produto.Produto.ValorParaProfissional.Value > 0
                                        ? produto.Produto.ValorParaProfissional.Value
                                        : 0;
                                var comissao = new Comissao
                                {
                                    DataLancamento = comanda.Data,
                                    Descricao = string.Format("Venda de {0} para o cliente {1}", produto.Produto.Descricao, comanda.Cliente != null ? comanda.Cliente.Nome : string.Empty),
                                    Valor = produto.ValorTotal * porcentagem / 100,
                                    ProfissionalId = comanda.ProfissionalId.Value
                                };

                                await _comissaoAppService.Save(comissao);
                            }
                        }
                    }

                    comanda.Status = Enum.StatusComanda.Faturada;
                    await _comandaAppService.Save(Mapper.Map<ComandaViewModel, Comanda>(comanda));
                }
                return Json(comanda, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> SalvarDesconto(int id, string desconto)
        {
            try
            {
                var comanda = await _comandaAppService.GetById(id);
                if (comanda != null)
                {
                    comanda.ValorDesconto = string.IsNullOrEmpty(desconto) ? 0 : Convert.ToDouble(desconto);
                    comanda.ValorTotal = (((ComandaViewModel)Session["det_com"]).Servicos.Sum(m => m.Valor) + ((ComandaViewModel)Session["det_com"]).Produtos.Sum(m => m.ValorTotal)) - (comanda.ValorDesconto.Value);
                    //comanda.ValorTotal = comanda.ValorTotal - comanda.ValorDesconto.Value;
                    await _comandaAppService.Save(comanda);
                    ((ComandaViewModel)Session["det_com"]).ValorDesconto = comanda.ValorDesconto;
                    ((ComandaViewModel)Session["det_com"]).ValorTotal = comanda.ValorTotal;
                }
                return Json(comanda, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> Visualizar(int id)
        {
            try
            {
                var comandaPagamento = Mapper.Map<ComandaPagamento, ComandaPagamentoViewModel>(await _comandaPagamentoAppService.GetByComandaId(id));
                comandaPagamento.Parcelas = Mapper.Map<IEnumerable<ComandaPagamentoParcela>, IEnumerable<ComandaPagamentoParcelaViewModel>>(await _comandaPagamentoParcelaAppService.GetByComandaPagamentoId(comandaPagamento.Id)).ToList();
                comandaPagamento.Comanda.Servicos = Mapper.Map<IEnumerable<ServicoComanda>, IEnumerable<ServicoComandaViewModel>>(await _servicoComandaAppService.GetByComandaId(id)).ToList();
                comandaPagamento.Comanda.Produtos = Mapper.Map<IEnumerable<ProdutoComanda>, IEnumerable<ProdutoComandaViewModel>>(await _produtoComandaAppService.GetByComandaId(id)).ToList();
                if (comandaPagamento.Comanda.ClienteId.HasValue)
                    comandaPagamento.Comanda.Cliente = Mapper.Map<Cliente, ClienteViewModel>(await _clienteAppService.GetById(comandaPagamento.Comanda.ClienteId.Value));

                if (comandaPagamento.Comanda.ProfissionalId.HasValue)
                    comandaPagamento.Comanda.Profissional = Mapper.Map<Profissional, ProfissionalViewModel>(await _profissionalAppService.GetById(comandaPagamento.Comanda.ProfissionalId.Value));

                return View(comandaPagamento);
            }
            catch (Exception ex)
            {

                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return RedirectToAction("Index", "Home");
            }
        }
    }
}