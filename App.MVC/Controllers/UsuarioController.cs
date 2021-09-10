using App.MVC.Util;
using App.MVC.ViewModels;
using AutoMapper;
using ControleDeSalao.Application.Interface;
using ControleDeSalao.Domain.Entities;
using ControleDeSalao.Infra.Cross.Security;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace App.MVC.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioAppService _usuarioAppService;
        private readonly IEmpresaAppService _empresaAppService;
        private readonly IPlanoDeContaAppService _planoDeContaAppService;
        private readonly IProfissionalAppService _profissionalAppService;
        private readonly IServicoAppService _servicoAppService;
        private readonly IClienteAppService _clienteAppService;
        private readonly IMensalidadeAppService _mensalidadeAppService;

        public UsuarioController(IUsuarioAppService usuarioAppService, IEmpresaAppService empresaAppService, IPlanoDeContaAppService planoDeContaAppService, IProfissionalAppService profissionalAppService, IServicoAppService servicoAppService, IClienteAppService clienteAppService, IMensalidadeAppService mensalidadeAppService)
        {
            _usuarioAppService = usuarioAppService;
            _empresaAppService = empresaAppService;
            _planoDeContaAppService = planoDeContaAppService;
            _profissionalAppService = profissionalAppService;
            _servicoAppService = servicoAppService;
            _clienteAppService = clienteAppService;
            _mensalidadeAppService = mensalidadeAppService;
        }


        // GET: Usuario
        public async Task<ActionResult> Index()
        {
            try
            {
                if (!Cookies.IdUsuarioLogado.HasValue)
                    return RedirectToAction("Login", "Usuario");

                if (Cookies.DataBloqueio <= DateTime.Today)
                    return RedirectToAction("Bloqueio", "Home");

                var profissionais = Mapper.Map<IEnumerable<Profissional>, IEnumerable<ProfissionalViewModel>>(await _profissionalAppService.GetAll());
                var listItems = new List<SelectListItem>();
                listItems.Add(new SelectListItem { Text = " ", Value = "0" });
                foreach (var profissional in profissionais)
                {
                    listItems.Add(new SelectListItem { Text = profissional.Nome, Value = profissional.Id.ToString(CultureInfo.InvariantCulture) });
                }
                Session["lstUsuProfissionais"] = listItems;

                var usus = Mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioViewModel>>(await _usuarioAppService.GetAll());

                return View(usus);
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return RedirectToAction("Index", "Home");
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> Login(string usuario, string password)
        {
            if ((!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(password)) ^ (Cookies.IdUsuarioLogado.HasValue))
            {
                var usuarioLogado = Cookies.IdUsuarioLogado.HasValue ? await _usuarioAppService.GetById(Cookies.IdUsuarioLogado.Value) : await _usuarioAppService.IsValid(usuario, password);

                if (usuarioLogado != null)
                {
                    usuarioLogado.DtUltLogin = DatetimeBrasil.HorarioDeBrasilia();
                    await _usuarioAppService.Save(usuarioLogado);
                    var empresalogada = await _empresaAppService.GetById(usuarioLogado.EmpresaId);
                    Session["UsuarioLogado"] = usuarioLogado;
                    Session["EmpresaLogada"] = empresalogada;
                    Cookies.IdUsuarioLogado = usuarioLogado.Id;
                    Cookies.IdProfissionalLogado = usuarioLogado.ProfissionalId;
                    Cookies.EmailUsuarioLogado = usuarioLogado.Email;
                    Cookies.LoginUsuarioLogado = usuarioLogado.Login;
                    Cookies.UrlAvatarUsuarioLogado = usuarioLogado.UrlAvatar;
                    Cookies.CargoUsuarioLogado = usuarioLogado.Cargo;
                    Cookies.DireitoUsuarioLogado = usuarioLogado.Direitos;
                    Cookies.IdEmpresaLogada = empresalogada.Id;
                    Cookies.NomeEmpresaLogada = empresalogada.Nome;
                    Cookies.DataBloqueio = empresalogada.DataBloqueio;
                    FormsAuthentication.SetAuthCookie(usuario, true);
                    return RedirectToAction("Index", "Home");
                }

                TempData["msg_warning"] = "Usuário ou senha inválidos";
                return RedirectToAction("Login", "Usuario");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuarioLogado = await _usuarioAppService.IsValid(model.Login, model.Password);

                if (usuarioLogado != null)
                {
                    usuarioLogado.DtUltLogin = DatetimeBrasil.HorarioDeBrasilia();
                    await _usuarioAppService.Save(usuarioLogado);
                    var empresalogada = await _empresaAppService.GetById(usuarioLogado.EmpresaId);
                    Session["UsuarioLogado"] = usuarioLogado;
                    Session["EmpresaLogada"] = empresalogada;
                    Cookies.IdUsuarioLogado = usuarioLogado.Id;
                    Cookies.IdProfissionalLogado = usuarioLogado.ProfissionalId;
                    Cookies.EmailUsuarioLogado = usuarioLogado.Email;
                    Cookies.LoginUsuarioLogado = usuarioLogado.Login;
                    Cookies.UrlAvatarUsuarioLogado = usuarioLogado.UrlAvatar;
                    Cookies.CargoUsuarioLogado = usuarioLogado.Cargo;
                    Cookies.DireitoUsuarioLogado = usuarioLogado.Direitos;
                    Cookies.IdEmpresaLogada = empresalogada.Id;
                    Cookies.NomeEmpresaLogada = empresalogada.Nome;
                    Cookies.DataBloqueio = empresalogada.DataBloqueio;
                    FormsAuthentication.SetAuthCookie(model.Login, true);
                    return RedirectToAction("Index", "Home");
                }

                TempData["msg_warning"] = "Usuário ou senha inválidos";
                return RedirectToAction("Login", "Usuario");
            }

            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> Register(string nomeSalao, string email, string login, string senha)
        {
            var model = new RegisterViewModel();
            model.Nome = nomeSalao;
            model.Login = Server.HtmlDecode(Request.QueryString["login"]);
            model.Password = senha;
            model.ConfirmPassword = senha;
            model.Email = email;

            if (!string.IsNullOrEmpty(nomeSalao) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(senha))
            {
                model.Login = email.Split('@')[0];

                var usu = await _usuarioAppService.GetByEmail(model.Email);
                if (usu != null)
                {
                    TempData["msg_warning"] = "E-mail já está em uso";
                    return View("Login");
                }

                var empresa = new Empresa()
                {
                    Nome = nomeSalao,
                    Email = model.Email,
                    PassoWizard = 1,
                    DataBloqueio = DateTime.Today.AddDays(15),
                    DataAlerta = DateTime.Today.AddDays(16),
                    CodigoPromocional = model.CodigoPromocional,
                    SmsAgenda = "Oi [Cliente] tudo bem? queremos te lembrar de um agendamento para hoje as: [Horario] no [Salao]",
                    SmsAniversario = "Feliz Aniversário [Cliente], nós da equipe [Salao] queremos te desejar toda felicidade do mundo neste dia muito especial"
                };

                var idEmpresa = await _empresaAppService.Save(empresa);

                var usuario = new Usuario
                {
                    EmpresaId = idEmpresa,
                    Email = model.Email,
                    Login = model.Login,
                    Direitos = _usuarioAppService.GetAllDireitos(),
                    Password = Password.Cryptography(model.Password)
                };

                usuario.Id = await _usuarioAppService.Save(usuario);
                await _planoDeContaAppService.AddAllDefaults(idEmpresa, usuario.Id);
                await _servicoAppService.AddAllDefaults(idEmpresa, usuario.Id);
                Cookies.IdUsuarioLogado = usuario.Id;
                Cookies.IdProfissionalLogado = usuario.ProfissionalId;
                Cookies.EmailUsuarioLogado = usuario.Email;
                Cookies.LoginUsuarioLogado = usuario.Login;
                Cookies.UrlAvatarUsuarioLogado = usuario.UrlAvatar;
                Cookies.CargoUsuarioLogado = usuario.Cargo;
                Cookies.DireitoUsuarioLogado = usuario.Direitos;
                Cookies.IdEmpresaLogada = empresa.Id;
                Cookies.NomeEmpresaLogada = empresa.Nome;
                Cookies.DataBloqueio = empresa.DataBloqueio;
                return RedirectToAction("Login", "Usuario");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> EsqueciSenha(LoginViewModel model)
        {
            try
            {
                var usuario = await _usuarioAppService.GetByEmail(model.Login);
                if (usuario != null)
                {
                    var msg = string.Format("<img src='http://app.controledesalao.com.br/assets/pages/img/login/login-invert.png' style='width: 300px' /><br/><p style='font: black; font-size: 35pt'>Olá {0} sua senha é: <strong>{1}</strong><p><hr><p style='font: gray;'>Qualquer dúvida entre em contato em nosso chat: (11) 95329-2131<p>", usuario.Login, Password.Decryption(usuario.Password));
                    Email.EnviarEmail(model.Login, "Recuperação de senha!", msg);
                    TempData["swal"] = SweetAlert.MensagemSucesso("Enviamos a sua senha, verifique o seu email.");

                    return RedirectToAction("Login");
                }
                TempData["swal"] = SweetAlert.MensagemErro("Este email não está cadastrado.");
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
            }

            return RedirectToAction("Login");
        }

        public async Task<ActionResult> Detalhe(int id)
        {
            try
            {
                var usu = Mapper.Map<Usuario, UsuarioViewModel>(await _usuarioAppService.GetById(id));
                usu.Acesso = Acesso.RetornaAcesso(usu.Direitos);
                usu.Password = string.Empty;
                return PartialView(usu);
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return View("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Detalhe(UsuarioViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Id <= 0)
                    {
                        var usuario = await _usuarioAppService.GetByEmail(model.Email);
                        if (usuario != null)
                        {
                            TempData["swal"] = SweetAlert.MensagemErro("Email já está em uso");
                            return PartialView(model);
                        }

                        if (model.Password.Length < 6)
                        {
                            TempData["swal"] = SweetAlert.MensagemErro("O Password deve ter no mínimo 6 caracteres");
                            return PartialView(model);
                        }

                        var usu = new Usuario();

                        usu.Password = Password.Cryptography(model.Password);
                        usu.Email = model.Email;
                        usu.Login = model.Email.Split('@')[0];
                        usu.ProfissionalId = model.ProfissionalId > 0 ? model.ProfissionalId : null;
                        usu.Direitos = Acesso.PreencherDireitos(model.Acesso);
                        
                        await _usuarioAppService.Save(usu);
                        TempData["swal"] = SweetAlert.MensagemSucesso("As informações foram salvas!");
                        ModelState.Clear();
                    }
                    else
                    {
                        var usu = await _usuarioAppService.GetById(model.Id);
                        usu.Email = model.Email;
                        usu.ProfissionalId = model.ProfissionalId > 0 ? model.ProfissionalId : null;
                        usu.Password = !string.IsNullOrEmpty(model.Password) ? Password.Cryptography(model.Password) : usu.Password;
                        usu.Direitos = Acesso.PreencherDireitos(model.Acesso);
                        await _usuarioAppService.Save(usu);
                        TempData["swal"] = SweetAlert.MensagemSucesso("As informações foram salvas!");
                        ModelState.Clear();
                    }

                }

                return RedirectPermanent("Index");
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<ActionResult> Perfil()
        {
            try
            {
                if (!Cookies.IdUsuarioLogado.HasValue)
                    return RedirectToAction("Login", "Usuario");

                if (Cookies.DataBloqueio <= DateTime.Today)
                    return RedirectToAction("Bloqueio", "Home");

                var usu = Mapper.Map<Usuario, UsuarioViewModel>(await _usuarioAppService.GetById(Cookies.IdUsuarioLogado.Value));
                usu.Acesso = Acesso.RetornaAcesso(usu.Direitos);
                usu.Empresa = Mapper.Map<Empresa, EmpresaViewModel>(await _empresaAppService.GetById(Cookies.IdEmpresaLogada.Value));
                Session["TodasMensalidades"] = Session["TodasMensalidades"] ?? Mapper.Map<IEnumerable<Mensalidade>, IEnumerable<MensalidadeViewModel>>(await _mensalidadeAppService.GetByEmpresaId(usu.EmpresaId));
                usu.Password = string.Empty;
                ViewBag.Profissionais = await _profissionalAppService.GetAll();
                ViewBag.Clientes = await _clienteAppService.GetAll();
                ViewBag.Servicos = await _servicoAppService.GetAll();
                return View(usu);
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return View("Index");
            }
        }

        public async Task<ActionResult> Ajuda()
        {
            try
            {
                if (!Cookies.IdUsuarioLogado.HasValue)
                    return RedirectToAction("Login", "Usuario");

                if (Cookies.DataBloqueio <= DateTime.Today)
                    return RedirectToAction("Bloqueio", "Home");

                var usu = Mapper.Map<Usuario, UsuarioViewModel>(await _usuarioAppService.GetById(Cookies.IdUsuarioLogado.Value));
                usu.Acesso = Acesso.RetornaAcesso(usu.Direitos);
                usu.Empresa = Mapper.Map<Empresa, EmpresaViewModel>(await _empresaAppService.GetById(Cookies.IdEmpresaLogada.Value));
                usu.Password = string.Empty;
                ViewBag.Profissionais = await _profissionalAppService.GetAll();
                ViewBag.Clientes = await _clienteAppService.GetAll();
                ViewBag.Servicos = await _servicoAppService.GetAll();
                return View(usu);
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return View("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Configurar(UsuarioViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuario = await _usuarioAppService.GetById(Cookies.IdUsuarioLogado.Value);
                    if (usuario != null)
                    {
                        usuario.Cargo = model.Cargo;
                        usuario.Login = model.Login;
                        await _usuarioAppService.Save(usuario);
                        Cookies.LoginUsuarioLogado = model.Login;
                        Cookies.CargoUsuarioLogado = model.Cargo;
                        TempData["swal"] = SweetAlert.MensagemSucesso("As informações foram salvas!");

                        ModelState.Clear();
                    }

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
        public async Task<ActionResult> AlterarSenha(string senhaAtual, string novaSenha, string reNovaSenha)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuario = await _usuarioAppService.GetById(Cookies.IdUsuarioLogado.Value);

                    if (usuario != null)
                    {
                        if (senhaAtual != Password.Decryption(usuario.Password))
                        {
                            TempData["swal"] = SweetAlert.MensagemErro("A Senha atual está errada, tente novamente.");
                            return RedirectToActionPermanent("Perfil");
                        }

                        if (novaSenha != reNovaSenha)
                        {
                            TempData["swal"] = SweetAlert.MensagemErro("As senhas novas não iguais, tente novamente.");
                            return RedirectToActionPermanent("Perfil");
                        }

                        if (novaSenha.Length < 6)
                        {
                            TempData["swal"] = SweetAlert.MensagemErro("Para sua segurança, digite uma senha com 6 ou mais caracteres.");
                            return RedirectToActionPermanent("Perfil");
                        }


                        usuario.Password = Password.Cryptography(novaSenha);

                        await _usuarioAppService.Save(usuario);
                        TempData["swal"] = SweetAlert.MensagemSucesso("As novas senhas foram salvas!");

                        ModelState.Clear();
                    }
                }

                return RedirectToActionPermanent("Perfil");
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<ActionResult> AtualizarAvatar(string id)
        {
            try
            {
                var usuario = await _usuarioAppService.GetById(Cookies.IdUsuarioLogado.Value);

                if (usuario != null)
                {
                    usuario.UrlAvatar = string.Format("../../assets/pages/media/profile/{0}.png", id);
                    Cookies.UrlAvatarUsuarioLogado = usuario.UrlAvatar;
                    await _usuarioAppService.Save(usuario);
                }
                return Json(usuario, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Logout()
        {
            try
            {
                Cookies.IdUsuarioLogado = null;
                Cookies.IdProfissionalLogado = null;
                Cookies.EmailUsuarioLogado = null;
                Cookies.LoginUsuarioLogado = null;
                Cookies.DireitoUsuarioLogado = null;
                Cookies.CargoUsuarioLogado = null;
                Cookies.UrlAvatarUsuarioLogado = null;
                Cookies.IdEmpresaLogada = null;
                Cookies.NomeEmpresaLogada = null;
                Cookies.DataBloqueio = DateTime.MinValue;

                return RedirectToActionPermanent("Login");
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return RedirectToActionPermanent("Index", "Home");
            }
        }

        public async Task<ActionResult> Lock()
        {
            try
            {
                if (System.Web.HttpContext.Current == null)
                    return RedirectToAction("Index", "Home");

                if (!Cookies.IdUsuarioLogado.HasValue)
                    return RedirectToAction("Login", "Usuario");

                var usu = Mapper.Map<Usuario, UsuarioViewModel>(await _usuarioAppService.GetById(Cookies.IdUsuarioLogado.Value));
                Cookies.IdUsuarioLogado = null;
                Cookies.IdProfissionalLogado = null;
                Cookies.EmailUsuarioLogado = null;
                Cookies.LoginUsuarioLogado = null;
                Cookies.DireitoUsuarioLogado = null;
                Cookies.CargoUsuarioLogado = null;
                Cookies.UrlAvatarUsuarioLogado = null;
                Cookies.IdEmpresaLogada = null;
                Cookies.NomeEmpresaLogada = null;
                Cookies.DataBloqueio = DateTime.MinValue;
                return View(usu);
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Usuario");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Lock(UsuarioViewModel model)
        {
            try
            {

                var usuarioLogado = await _usuarioAppService.IsValid(model.Email, model.Password);

                if (usuarioLogado != null)
                {
                    usuarioLogado.DtUltLogin = DatetimeBrasil.HorarioDeBrasilia();
                    await _usuarioAppService.Save(usuarioLogado);
                    var empresalogada = await _empresaAppService.GetById(usuarioLogado.EmpresaId);
                    Session["UsuarioLogado"] = usuarioLogado;
                    Session["EmpresaLogada"] = empresalogada;
                    Cookies.IdUsuarioLogado = usuarioLogado.Id;
                    Cookies.IdProfissionalLogado = usuarioLogado.ProfissionalId;
                    Cookies.EmailUsuarioLogado = usuarioLogado.Email;
                    Cookies.LoginUsuarioLogado = usuarioLogado.Login;
                    Cookies.UrlAvatarUsuarioLogado = usuarioLogado.UrlAvatar;
                    Cookies.CargoUsuarioLogado = usuarioLogado.Cargo;
                    Cookies.DireitoUsuarioLogado = usuarioLogado.Direitos;
                    Cookies.IdEmpresaLogada = empresalogada.Id;
                    Cookies.NomeEmpresaLogada = empresalogada.Nome;
                    Cookies.DataBloqueio = empresalogada.DataBloqueio;
                    FormsAuthentication.SetAuthCookie(model.Login, true);
                    return RedirectToAction("Index", "Home");

                }

                TempData["msg_warning"] = "Usuário ou senha inválidos";
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["swal"] = SweetAlert.MensagemErroNaoTratado(ex.Message);
                return View("Index");
            }
        }

        public ActionResult keepAlive()
        {
            if (Cookies.IdUsuarioLogado.HasValue)
                return new JsonResult { Data = "Success" };

            return null;
        }

    }
}