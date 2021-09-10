using App.MVC.ViewModels;
using App.MVC.ViewModels.Relatorios;
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

namespace ControleDeSalao.MVC.Controllers
{
    public class RelatorioController : Controller
    {
        private readonly IAgendaAppService _agendaAppService;
        private readonly IClienteAppService _clienteAppService;
        private readonly IComandaAppService _comandaAppService;
        private readonly IProfissionalAppService _profissionalAppService;
        private readonly IServicoComandaAppService _servicoComandaAppService;
        private readonly IProdutoComandaAppService _produtoComandaAppService;
        private readonly IProdutoAppService _produtoAppService;
        private readonly IServicoAppService _servicoAppService;
        private readonly IComissaoAppService _comissaoAppService;
        private readonly IFornecedorAppService _fornecedorAppService;
        private readonly ICompraAppService _compraAppService;
        private readonly ICompraDetalheAppService _compraDetalheAppService;
        private readonly IFinanceiroAppService _financeiroAppService;
        private readonly IPlanoDeContaAppService _planoDeContaAppService;

        public RelatorioController(IAgendaAppService agendaAppService, IClienteAppService clienteAppService, IComandaAppService comandaAppService, IProfissionalAppService profissionalAppService, IServicoComandaAppService servicoComandaAppService, IProdutoComandaAppService produtoComandaAppService, IProdutoAppService produtoAppService, IServicoAppService servicoAppService, IComissaoAppService comissaoAppService, IFornecedorAppService fornecedorAppService, ICompraAppService compraAppService, ICompraDetalheAppService compraDetalheAppService, IFinanceiroAppService financeiroAppService, IPlanoDeContaAppService planoDeContaAppService)
        {
            _agendaAppService = agendaAppService;
            _clienteAppService = clienteAppService;
            _comandaAppService = comandaAppService;
            _profissionalAppService = profissionalAppService;
            _servicoComandaAppService = servicoComandaAppService;
            _produtoComandaAppService = produtoComandaAppService;
            _produtoAppService = produtoAppService;
            _servicoAppService = servicoAppService;
            _comissaoAppService = comissaoAppService;
            _fornecedorAppService = fornecedorAppService;
            _compraAppService = compraAppService;
            _compraDetalheAppService = compraDetalheAppService;
            _financeiroAppService = financeiroAppService;
            _planoDeContaAppService = planoDeContaAppService;
        }

        // GET: Relatorio
        public ActionResult ClienteAgendamento()
        {
            if (!Cookies.IdUsuarioLogado.HasValue)
                return RedirectToAction("Login", "Usuario");

            if (Cookies.DataBloqueio <= DateTime.Today)
                return RedirectToAction("Bloqueio", "Home");

            var dtInicial = Session["rcli_dtInicial"] == null ? DateTime.Today : Convert.ToDateTime(Session["rcli_dtInicial"]);
            var dtFinal = Session["rcli_dtFinal"] == null ? DateTime.Today : Convert.ToDateTime(Session["rcli_dtFinal"]);
            Session["rcli_dtInicial"] = dtInicial;
            Session["rcli_dtFinal"] = dtFinal;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ClienteAgendamento(string nomeCliente, string dataInicial, string dataFinal)
        {
            var dtInicial = Session["rcli_dtInicial"] == null ? DateTime.Today : Convert.ToDateTime(Session["rcli_dtInicial"]);
            var dtFinal = Session["rcli_dtFinal"] == null ? DateTime.Today : Convert.ToDateTime(Session["rcli_dtFinal"]);

            if (!string.IsNullOrEmpty(dataInicial) && !string.IsNullOrEmpty(dataFinal))
            {
                dtInicial = Convert.ToDateTime(dataInicial);
                dtFinal = Convert.ToDateTime(dataFinal);
            }

            Session["rcli_dtInicial"] = dtInicial;
            Session["rcli_dtFinal"] = dtFinal;

            var cliente = await _clienteAppService.GetByNome(nomeCliente);
            var lstAgendas = await _agendaAppService.GetByPeriodo(dtInicial, dtFinal);
            return View("_ClienteAgendamento", Mapper.Map<IEnumerable<Agenda>, IEnumerable<AgendaViewModel>>(lstAgendas.Where(m => m.ClienteId == cliente.Id).ToList()));
        }

        public ActionResult ClienteRanking()
        {
            if (!Cookies.IdUsuarioLogado.HasValue)
                return RedirectToAction("Login", "Usuario");

            if (Cookies.DataBloqueio <= DateTime.Today)
                return RedirectToAction("Bloqueio", "Home");


            var dtInicial = Session["ranqcli_dtInicial"] == null ? DateTime.Today : Convert.ToDateTime(Session["ranqcli_dtInicial"]);
            var dtFinal = Session["ranqcli_dtFinal"] == null ? DateTime.Today : Convert.ToDateTime(Session["ranqcli_dtFinal"]);
            Session["ranqcli_dtInicial"] = dtInicial;
            Session["ranqcli_dtFinal"] = dtFinal;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ClienteRanking(string dataInicial, string dataFinal)
        {
            var dtInicial = Session["ranqcli_dtInicial"] == null ? DateTime.Today : Convert.ToDateTime(Session["ranqcli_dtInicial"]);
            var dtFinal = Session["ranqcli_dtFinal"] == null ? DateTime.Today : Convert.ToDateTime(Session["ranqcli_dtFinal"]);

            if (!string.IsNullOrEmpty(dataInicial) && !string.IsNullOrEmpty(dataFinal))
            {
                dtInicial = Convert.ToDateTime(dataInicial);
                dtFinal = Convert.ToDateTime(dataFinal);
            }

            Session["ranqcli_dtInicial"] = dtInicial;
            Session["ranqcli_dtFinal"] = dtFinal;

            var clientes = await _clienteAppService.GetAll();
            var ranking = new List<RelClienteRanking>();

            foreach (var cliente in clientes)
            {
                var com = await _comandaAppService.GetByClienteIdStatusAndPeriodo(cliente.Id, Enum.StatusComanda.Faturada, dtInicial, dtFinal);

                if (com.Any())
                {
                    ranking.Add(new RelClienteRanking
                    {
                        NomeCliente = cliente.Nome,
                        Quantidade = com.Count(),
                        Valor = com.Sum(m => m.ValorTotal)
                    });
                }
            }

            return View("_ClienteRanking", ranking.OrderByDescending(m => m.Valor));
        }

        public ActionResult ClienteComanda()
        {
            if (!Cookies.IdUsuarioLogado.HasValue)
                return RedirectToAction("Login", "Usuario");

            if (Cookies.DataBloqueio <= DateTime.Today)
                return RedirectToAction("Bloqueio", "Home");


            var dtInicial = Session["comcli_dtInicial"] == null ? DateTime.Today : Convert.ToDateTime(Session["comcli_dtInicial"]);
            var dtFinal = Session["comcli_dtFinal"] == null ? DateTime.Today : Convert.ToDateTime(Session["comcli_dtFinal"]);
            Session["comcli_dtInicial"] = dtInicial;
            Session["comcli_dtFinal"] = dtFinal;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ClienteComanda(string nomeCliente, string dataInicial, string dataFinal)
        {
            var dtInicial = Session["comcli_dtInicial"] == null ? DateTime.Today : Convert.ToDateTime(Session["comcli_dtInicial"]);
            var dtFinal = Session["comcli_dtFinal"] == null ? DateTime.Today : Convert.ToDateTime(Session["comcli_dtFinal"]);

            if (!string.IsNullOrEmpty(dataInicial) && !string.IsNullOrEmpty(dataFinal))
            {
                dtInicial = Convert.ToDateTime(dataInicial);
                dtFinal = Convert.ToDateTime(dataFinal);
            }

            Session["comcli_dtInicial"] = dtInicial;
            Session["comcli_dtFinal"] = dtFinal;

            var cli = Mapper.Map<Cliente, ClienteViewModel>(await _clienteAppService.GetByNome(nomeCliente));

            var comandas = Mapper.Map<IEnumerable<Comanda>, IEnumerable<ComandaViewModel>>(await _comandaAppService.GetByClienteIdStatusAndPeriodo(cli.Id, Enum.StatusComanda.Faturada, dtInicial, dtFinal));

            foreach (var comanda in comandas)
            {
                comanda.Cliente = cli;
                comanda.Profissional = comanda.ProfissionalId.HasValue ? Mapper.Map<Profissional, ProfissionalViewModel>(await _profissionalAppService.GetById(comanda.ProfissionalId.Value)) : null;
                comanda.Servicos = Mapper.Map<IEnumerable<ServicoComanda>, IEnumerable<ServicoComandaViewModel>>(await _servicoComandaAppService.GetByComandaId(comanda.Id)).ToList();
                comanda.Produtos = Mapper.Map<IEnumerable<ProdutoComanda>, IEnumerable<ProdutoComandaViewModel>>(await _produtoComandaAppService.GetByComandaId(comanda.Id)).ToList();
            }

            return View("_ClienteComanda", comandas);
        }

        public ActionResult ProdutoEstoque()
        {
            if (!Cookies.IdUsuarioLogado.HasValue)
                return RedirectToAction("Login", "Usuario");

            if (Cookies.DataBloqueio <= DateTime.Today)
                return RedirectToAction("Bloqueio", "Home");


            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ProdutoEstoque(string nomeProduto)
        {
            var produtos = new List<Produto>();
            if (string.IsNullOrEmpty(nomeProduto))
                produtos = (await _produtoAppService.GetAll()).ToList();
            else
                produtos.Add(await _produtoAppService.GetByNome(nomeProduto));

            return View("_ProdutoEstoque", Mapper.Map<IEnumerable<Produto>, IEnumerable<ProdutoViewModel>>(produtos).ToList());
        }

        public ActionResult ProdutoRanking()
        {
            if (!Cookies.IdUsuarioLogado.HasValue)
                return RedirectToAction("Login", "Usuario");

            if (Cookies.DataBloqueio <= DateTime.Today)
                return RedirectToAction("Bloqueio", "Home");


            var dtInicial = Session["ranqprd_dtInicial"] == null ? DateTime.Today : Convert.ToDateTime(Session["ranqprd_dtInicial"]);
            var dtFinal = Session["ranqprd_dtFinal"] == null ? DateTime.Today : Convert.ToDateTime(Session["ranqprd_dtFinal"]);
            Session["ranqprd_dtInicial"] = dtInicial;
            Session["ranqprd_dtFinal"] = dtFinal;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ProdutoRanking(string dataInicial, string dataFinal)
        {
            var dtInicial = Session["ranqprd_dtInicial"] == null ? DateTime.Today : Convert.ToDateTime(Session["ranqprd_dtInicial"]);
            var dtFinal = Session["ranqprd_dtFinal"] == null ? DateTime.Today : Convert.ToDateTime(Session["ranqprd_dtFinal"]);

            if (!string.IsNullOrEmpty(dataInicial) && !string.IsNullOrEmpty(dataFinal))
            {
                dtInicial = Convert.ToDateTime(dataInicial);
                dtFinal = Convert.ToDateTime(dataFinal);
            }

            Session["ranqprd_dtInicial"] = dtInicial;
            Session["ranqprd_dtFinal"] = dtFinal;

            var produtos = await _produtoAppService.GetAll();

            var ranking = produtos.Select(prd => new RelProdutoRanking
            {
                ProdutoId = prd.Id,
                NomeProduto = prd.Descricao,
                Unidade = prd.Unidade
            }).ToList();

            var comandas = Mapper.Map<IEnumerable<Comanda>, IEnumerable<ComandaViewModel>>(await _comandaAppService.GetByStatusAndPeriodo(Enum.StatusComanda.Faturada, dtInicial, dtFinal));

            foreach (var com in comandas)
            {
                com.Produtos = Mapper.Map<IEnumerable<ProdutoComanda>, IEnumerable<ProdutoComandaViewModel>>(await _produtoComandaAppService.GetByComandaId(com.Id)).ToList();

                foreach (var prd in com.Produtos)
                {
                    var rnk = ranking.FirstOrDefault(m => m.ProdutoId == prd.ProdutoId);
                    if (rnk != null)
                    {
                        rnk.Quantidade += prd.Quantidade;
                        rnk.Valor += prd.ValorTotal;
                    }
                }
            }

            return View("_ProdutoRanking", ranking.OrderByDescending(m => m.Valor));
        }

        public ActionResult ServicoRanking()
        {
            if (!Cookies.IdUsuarioLogado.HasValue)
                return RedirectToAction("Login", "Usuario");

            if (Cookies.DataBloqueio <= DateTime.Today)
                return RedirectToAction("Bloqueio", "Home");


            var dtInicial = Session["ranqser_dtInicial"] == null ? DateTime.Today : Convert.ToDateTime(Session["ranqser_dtInicial"]);
            var dtFinal = Session["ranqser_dtFinal"] == null ? DateTime.Today : Convert.ToDateTime(Session["ranqser_dtFinal"]);
            Session["ranqser_dtInicial"] = dtInicial;
            Session["ranqser_dtFinal"] = dtFinal;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ServicoRanking(string dataInicial, string dataFinal)
        {
            var dtInicial = Session["ranqser_dtInicial"] == null ? DateTime.Today : Convert.ToDateTime(Session["ranqser_dtInicial"]);
            var dtFinal = Session["ranqser_dtFinal"] == null ? DateTime.Today : Convert.ToDateTime(Session["ranqser_dtFinal"]);

            if (!string.IsNullOrEmpty(dataInicial) && !string.IsNullOrEmpty(dataFinal))
            {
                dtInicial = Convert.ToDateTime(dataInicial);
                dtFinal = Convert.ToDateTime(dataFinal);
            }

            Session["ranqser_dtInicial"] = dtInicial;
            Session["ranqser_dtFinal"] = dtFinal;

            var servicos = await _servicoAppService.GetAll();

            var ranking = servicos.Select(ser => new RelServicoRanking()
            {
                ServicoId = ser.Id,
                NomeServico = ser.Descricao
            }).ToList();

            var comandas = Mapper.Map<IEnumerable<Comanda>, IEnumerable<ComandaViewModel>>(await _comandaAppService.GetByStatusAndPeriodo(Enum.StatusComanda.Faturada, dtInicial, dtFinal));

            foreach (var com in comandas)
            {
                com.Servicos = Mapper.Map<IEnumerable<ServicoComanda>, IEnumerable<ServicoComandaViewModel>>(await _servicoComandaAppService.GetByComandaId(com.Id)).ToList();

                foreach (var ser in com.Servicos)
                {
                    var rnk = ranking.FirstOrDefault(m => m.ServicoId == ser.ServicoId);
                    if (rnk == null) continue;
                    rnk.Quantidade += 1;
                    rnk.Valor += ser.Valor;
                }
            }

            return View("_ServicoRanking", ranking.OrderByDescending(m => m.Valor));
        }

        public async Task<ActionResult> ProfissionalComissao()
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
                    profissionais = Mapper.Map<IEnumerable<Profissional>, IEnumerable<ProfissionalViewModel>>(await _profissionalAppService.GetComAgenda()).ToList();

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


            var dtInicial = Session["compro_dtInicial"] == null ? DateTime.Today : Convert.ToDateTime(Session["compro_dtInicial"]);
            var dtFinal = Session["compro_dtFinal"] == null ? DateTime.Today : Convert.ToDateTime(Session["compro_dtFinal"]);
            Session["compro_dtInicial"] = dtInicial;
            Session["compro_dtFinal"] = dtFinal;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ProfissionalComissao(string nomeProfissional, string dataInicial, string dataFinal)
        {
            var dtInicial = Session["compro_dtInicial"] == null ? DateTime.Today : Convert.ToDateTime(Session["compro_dtInicial"]);
            var dtFinal = Session["compro_dtFinal"] == null ? DateTime.Today : Convert.ToDateTime(Session["compro_dtFinal"]);

            if (!string.IsNullOrEmpty(dataInicial) && !string.IsNullOrEmpty(dataFinal))
            {
                dtInicial = Convert.ToDateTime(dataInicial);
                dtFinal = Convert.ToDateTime(dataFinal);
            }

            Session["compro_dtInicial"] = dtInicial;
            Session["compro_dtFinal"] = dtFinal;

            var pro = await _profissionalAppService.GetByNome(nomeProfissional);

            var comissoes = Mapper.Map<IEnumerable<Comissao>, IEnumerable<ComissaoViewModel>>(await _comissaoAppService.GetByProfissinalAndDate(pro.Id, dtInicial, dtFinal));

            return View("_ProfissionalComissao", comissoes.ToList());
        }

        public async Task<ActionResult> ProfissionalAgenda()
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
                    profissionais = Mapper.Map<IEnumerable<Profissional>, IEnumerable<ProfissionalViewModel>>(await _profissionalAppService.GetComAgenda()).ToList();

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

            var dtInicial = Session["agepro_dtInicial"] == null ? DateTime.Today : Convert.ToDateTime(Session["agepro_dtInicial"]);
            var dtFinal = Session["agepro_dtFinal"] == null ? DateTime.Today : Convert.ToDateTime(Session["agepro_dtFinal"]);
            Session["agepro_dtInicial"] = dtInicial;
            Session["agepro_dtFinal"] = dtFinal;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ProfissionalAgenda(string nomeProfissional, string dataInicial, string dataFinal)
        {
            var dtInicial = Session["agepro_dtInicial"] == null ? DateTime.Today : Convert.ToDateTime(Session["agepro_dtInicial"]);
            var dtFinal = Session["agepro_dtFinal"] == null ? DateTime.Today : Convert.ToDateTime(Session["agepro_dtFinal"]);

            if (!string.IsNullOrEmpty(dataInicial) && !string.IsNullOrEmpty(dataFinal))
            {
                dtInicial = Convert.ToDateTime(dataInicial);
                dtFinal = Convert.ToDateTime(dataFinal);
            }

            Session["agepro_dtInicial"] = dtInicial;
            Session["agepro_dtFinal"] = dtFinal;

            var pro = await _profissionalAppService.GetByNome(nomeProfissional);

            var agendas = Mapper.Map<IEnumerable<Agenda>, IEnumerable<AgendaViewModel>>(await _agendaAppService.GetByProfissionalIdAndPeriodo(pro.Id, dtInicial, dtFinal));

            return View("_ProfissionalAgenda", agendas);
        }

        public ActionResult ProfissionaisRanking()
        {
            if (!Cookies.IdUsuarioLogado.HasValue)
                return RedirectToAction("Login", "Usuario");

            if (Cookies.DataBloqueio <= DateTime.Today)
                return RedirectToAction("Bloqueio", "Home");


            var dtInicial = Session["ranqpro_dtInicial"] == null ? DateTime.Today : Convert.ToDateTime(Session["ranqpro_dtInicial"]);
            var dtFinal = Session["ranqpro_dtFinal"] == null ? DateTime.Today : Convert.ToDateTime(Session["ranqpro_dtFinal"]);
            Session["ranqpro_dtInicial"] = dtInicial;
            Session["ranqpro_dtFinal"] = dtFinal;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ProfissionaisRanking(string dataInicial, string dataFinal)
        {
            var dtInicial = Session["ranqpro_dtInicial"] == null ? DateTime.Today : Convert.ToDateTime(Session["ranqpro_dtInicial"]);
            var dtFinal = Session["ranqpro_dtFinal"] == null ? DateTime.Today : Convert.ToDateTime(Session["ranqpro_dtFinal"]);

            if (!string.IsNullOrEmpty(dataInicial) && !string.IsNullOrEmpty(dataFinal))
            {
                dtInicial = Convert.ToDateTime(dataInicial);
                dtFinal = Convert.ToDateTime(dataFinal);
            }

            Session["ranqpro_dtInicial"] = dtInicial;
            Session["ranqpro_dtFinal"] = dtFinal;

            var profissionais = await _profissionalAppService.GetAll();
            var ranking = new List<RelProfissionalRanking>();

            foreach (var profissional in profissionais)
            {
                var com = await _comandaAppService.GetByProfissionalIdStatusAndPeriodo(profissional.Id, Enum.StatusComanda.Faturada, dtInicial, dtFinal);

                if (com.Any())
                {
                    ranking.Add(new RelProfissionalRanking
                    {
                        NomeProfissional = profissional.Nome,
                        Quantidade = com.Count(),
                        Valor = com.Sum(m => m.ValorTotal)
                    });
                }
            }

            return View("_ProfissionaisRanking", ranking.OrderByDescending(m => m.Valor));
        }

        public ActionResult ComprasFornecedor()
        {
            if (!Cookies.IdUsuarioLogado.HasValue)
                return RedirectToAction("Login", "Usuario");

            if (Cookies.DataBloqueio <= DateTime.Today)
                return RedirectToAction("Bloqueio", "Home");


            var dtInicial = Session["comfor_dtInicial"] == null ? DateTime.Today : Convert.ToDateTime(Session["comfor_dtInicial"]);
            var dtFinal = Session["comfor_dtFinal"] == null ? DateTime.Today : Convert.ToDateTime(Session["comfor_dtFinal"]);
            Session["comfor_dtInicial"] = dtInicial;
            Session["comfor_dtFinal"] = dtFinal;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ComprasFornecedor(string nomeFornecedor, string dataInicial, string dataFinal)
        {
            var dtInicial = Session["comfor_dtInicial"] == null ? DateTime.Today : Convert.ToDateTime(Session["comfor_dtInicial"]);
            var dtFinal = Session["comfor_dtFinal"] == null ? DateTime.Today : Convert.ToDateTime(Session["comfor_dtFinal"]);

            if (!string.IsNullOrEmpty(dataInicial) && !string.IsNullOrEmpty(dataFinal))
            {
                dtInicial = Convert.ToDateTime(dataInicial);
                dtFinal = Convert.ToDateTime(dataFinal);
            }

            Session["comfor_dtInicial"] = dtInicial;
            Session["comfor_dtFinal"] = dtFinal;

            var forn = await _fornecedorAppService.GetByNome(nomeFornecedor);

            var compras = Mapper.Map<IEnumerable<Compra>, IEnumerable<CompraViewModel>>(await _compraAppService.GetByFornecedorIdAndPeriodo(forn.Id, dtInicial, dtFinal));

            foreach (var compra in compras)
            {
                compra.Detalhes = Mapper.Map<IEnumerable<CompraDetalhe>, IEnumerable<CompraDetalheViewModel>>(await _compraDetalheAppService.GetByCompraId(compra.Id)).ToList();
            }

            return View("_ComprasFornecedor", compras);
        }

        public ActionResult DescontoComanda()
        {
            if (!Cookies.IdUsuarioLogado.HasValue)
                return RedirectToAction("Login", "Usuario");

            if (Cookies.DataBloqueio <= DateTime.Today)
                return RedirectToAction("Bloqueio", "Home");


            var dtInicial = Session["descom_dtInicial"] == null ? DateTime.Today : Convert.ToDateTime(Session["descom_dtInicial"]);
            var dtFinal = Session["descom_dtFinal"] == null ? DateTime.Today : Convert.ToDateTime(Session["descom_dtFinal"]);
            Session["descom_dtInicial"] = dtInicial;
            Session["descom_dtFinal"] = dtFinal;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> DescontoComanda(string dataInicial, string dataFinal)
        {
            var dtInicial = Session["descom_dtInicial"] == null ? DateTime.Today : Convert.ToDateTime(Session["descom_dtInicial"]);
            var dtFinal = Session["descom_dtFinal"] == null ? DateTime.Today : Convert.ToDateTime(Session["descom_dtFinal"]);

            if (!string.IsNullOrEmpty(dataInicial) && !string.IsNullOrEmpty(dataFinal))
            {
                dtInicial = Convert.ToDateTime(dataInicial);
                dtFinal = Convert.ToDateTime(dataFinal);
            }

            Session["descom_dtInicial"] = dtInicial;
            Session["descom_dtFinal"] = dtFinal;

            var comandas = Mapper.Map<IEnumerable<Comanda>, IEnumerable<ComandaViewModel>>(await _comandaAppService.GetDescontoByStatusAndPeriodo(Enum.StatusComanda.Faturada, dtInicial, dtFinal));
            foreach (var comanda in comandas)
            {
                if (comanda.ClienteId.HasValue && comanda.ClienteId > 0)
                    comanda.Cliente = Mapper.Map<Cliente, ClienteViewModel>(await _clienteAppService.GetById(comanda.ClienteId.Value));

                if (comanda.ProfissionalId.HasValue && comanda.ProfissionalId > 0)
                    comanda.Profissional = Mapper.Map<Profissional, ProfissionalViewModel>(await _profissionalAppService.GetById(comanda.ProfissionalId.Value));
            }

            return View("_DescontoComanda", comandas.ToList().OrderByDescending(m => m.Data));
        }

        public ActionResult Recebimentos()
        {
            if (!Cookies.IdUsuarioLogado.HasValue)
                return RedirectToAction("Login", "Usuario");

            if (Cookies.DataBloqueio <= DateTime.Today)
                return RedirectToAction("Bloqueio", "Home");

            var dtInicial = Session["recb_dtInicial"] == null ? DateTime.Today : Convert.ToDateTime(Session["recb_dtInicial"]);
            var dtFinal = Session["recb_dtFinal"] == null ? DateTime.Today : Convert.ToDateTime(Session["recb_dtFinal"]);
            Session["recb_dtInicial"] = dtInicial;
            Session["recb_dtFinal"] = dtFinal;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Recebimentos(string dataInicial, string dataFinal)
        {
            var dtInicial = Session["recb_dtInicial"] == null ? DateTime.Today : Convert.ToDateTime(Session["recb_dtInicial"]);
            var dtFinal = Session["recb_dtFinal"] == null ? DateTime.Today : Convert.ToDateTime(Session["recb_dtFinal"]);

            if (!string.IsNullOrEmpty(dataInicial) && !string.IsNullOrEmpty(dataFinal))
            {
                dtInicial = Convert.ToDateTime(dataInicial);
                dtFinal = Convert.ToDateTime(dataFinal);
            }

            Session["recb_dtInicial"] = dtInicial;
            Session["recb_dtFinal"] = dtFinal;

            var lancamentos = Mapper.Map<IEnumerable<Financeiro>, IEnumerable<FinanceiroViewModel>>(await _financeiroAppService.GetByVencimento(Enum.TipoCreditoDebito.Crédito, dtInicial, dtFinal));

            return View("_Recebimentos", lancamentos.ToList().OrderBy(m => m.DataVencimento));
        }

        public ActionResult Despesas()
        {
            if (!Cookies.IdUsuarioLogado.HasValue)
                return RedirectToAction("Login", "Usuario");

            if (Cookies.DataBloqueio <= DateTime.Today)
                return RedirectToAction("Bloqueio", "Home");


            var dtInicial = Session["desp_dtInicial"] == null ? DateTime.Today : Convert.ToDateTime(Session["desp_dtInicial"]);
            var dtFinal = Session["desp_dtFinal"] == null ? DateTime.Today : Convert.ToDateTime(Session["desp_dtFinal"]);
            Session["desp_dtInicial"] = dtInicial;
            Session["desp_dtFinal"] = dtFinal;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Despesas(string dataInicial, string dataFinal)
        {
            var dtInicial = Session["desp_dtInicial"] == null ? DateTime.Today : Convert.ToDateTime(Session["desp_dtInicial"]);
            var dtFinal = Session["desp_dtFinal"] == null ? DateTime.Today : Convert.ToDateTime(Session["desp_dtFinal"]);

            if (!string.IsNullOrEmpty(dataInicial) && !string.IsNullOrEmpty(dataFinal))
            {
                dtInicial = Convert.ToDateTime(dataInicial);
                dtFinal = Convert.ToDateTime(dataFinal);
            }

            Session["desp_dtInicial"] = dtInicial;
            Session["desp_dtFinal"] = dtFinal;

            var lancamentos = Mapper.Map<IEnumerable<Financeiro>, IEnumerable<FinanceiroViewModel>>(await _financeiroAppService.GetByVencimento(Enum.TipoCreditoDebito.Débito, dtInicial, dtFinal));

            return View("_Despesas", lancamentos.ToList().OrderBy(m => m.DataVencimento));
        }

        public ActionResult Fechamento()
        {
            if (!Cookies.IdUsuarioLogado.HasValue)
                return RedirectToAction("Login", "Usuario");

            if (Cookies.DataBloqueio <= DateTime.Today)
                return RedirectToAction("Bloqueio", "Home");


            var dt = Session["fech_dt"] == null ? DateTime.Today : Convert.ToDateTime(Session["fech_dt"]);
            Session["fech_dt"] = dt;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Fechamento(string data)
        {
            var dt = Session["fech_dt"] == null ? DateTime.Today : Convert.ToDateTime(Session["fech_dt"]);

            if (!string.IsNullOrEmpty(data) && !string.IsNullOrEmpty(data))
            {
                dt = Convert.ToDateTime(data);
            }

            Session["fech_dt"] = dt;

            var lancamentos = Mapper.Map<IEnumerable<Financeiro>, IEnumerable<FinanceiroViewModel>>(await _financeiroAppService.GetByPagamento(null, dt, dt)).ToList().OrderBy(m => m.DataVencimento).ThenBy(m => m.Status);
            foreach (var lancamento in lancamentos)
            {
                lancamento.PlanoDeConta = Mapper.Map<PlanoDeConta, PlanoDeContaViewModel>(await _planoDeContaAppService.GetById(lancamento.PlanoDeContaId));
            }
            return View("_Fechamento", lancamentos);
        }

        public ActionResult Movimentacao()
        {
            if (!Cookies.IdUsuarioLogado.HasValue)
                return RedirectToAction("Login", "Usuario");

            if (Cookies.DataBloqueio <= DateTime.Today)
                return RedirectToAction("Bloqueio", "Home");


            var dtInicial = Session["mov_dtInicial"] == null ? DateTime.Today : Convert.ToDateTime(Session["mov_dtInicial"]);
            var dtFinal = Session["mov_dtFinal"] == null ? DateTime.Today : Convert.ToDateTime(Session["mov_dtFinal"]);
            Session["mov_dtInicial"] = dtInicial;
            Session["mov_dtFinal"] = dtFinal;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Movimentacao(string dataInicial, string dataFinal)
        {
            var dtInicial = Session["mov_dtInicial"] == null ? DateTime.Today : Convert.ToDateTime(Session["mov_dtInicial"]);
            var dtFinal = Session["mov_dtFinal"] == null ? DateTime.Today : Convert.ToDateTime(Session["mov_dtFinal"]);

            if (!string.IsNullOrEmpty(dataInicial) && !string.IsNullOrEmpty(dataFinal))
            {
                dtInicial = Convert.ToDateTime(dataInicial);
                dtFinal = Convert.ToDateTime(dataFinal);
            }

            Session["mov_dtInicial"] = dtInicial;
            Session["mov_dtFinal"] = dtFinal;

            var lancamentos = Mapper.Map<IEnumerable<Financeiro>, IEnumerable<FinanceiroViewModel>>(await _financeiroAppService.GetByPagamento(null, dtInicial, dtFinal)).ToList().OrderBy(m => m.DataVencimento).ThenBy(m => m.Status);
            foreach (var lancamento in lancamentos)
            {
                lancamento.PlanoDeConta = Mapper.Map<PlanoDeConta, PlanoDeContaViewModel>(await _planoDeContaAppService.GetById(lancamento.PlanoDeContaId));
            }
            return View("_Movimentacao", lancamentos);
        }
    }
}