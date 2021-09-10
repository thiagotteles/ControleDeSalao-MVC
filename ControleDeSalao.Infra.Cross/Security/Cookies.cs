using System;
using System.Net;
using System.Web;
using ControleDeSalao.Domain.Entities;

namespace ControleDeSalao.Infra.Cross.Security
{
    public class Cookies
    {
        private static int? _idUsuarioLogado;
        public static int? IdUsuarioLogado
        {
            get
            {
                var cookie = HttpContext.Current.Request.Cookies["idUsuLog"];
                if (cookie == null && HttpContext.Current.Session["UsuarioLogado"] != null)
                {
                    _idUsuarioLogado = ((Usuario) HttpContext.Current.Session["UsuarioLogado"]).Id;
                    cookie = new HttpCookie("idUsuLog");
                    cookie.Value = _idUsuarioLogado.ToString();
                    cookie.Expires = DatetimeBrasil.HorarioDeBrasilia().AddHours(12);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                }

                return cookie != null && !string.IsNullOrEmpty(cookie.Value) ? Convert.ToInt32(cookie.Value) : (int?)null;
            }
            set
            {
                _idUsuarioLogado = value;
                var cookie = new HttpCookie("idUsuLog");
                cookie.Value = _idUsuarioLogado.ToString();
                cookie.Expires = DatetimeBrasil.HorarioDeBrasilia().AddHours(12);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }


        private static int? _idProfissionalLogado;
        public static int? IdProfissionalLogado
        {
            get
            {
                var cookie = HttpContext.Current.Request.Cookies["idProfLog"];
                return cookie != null && !string.IsNullOrEmpty(cookie.Value) ? Convert.ToInt32(cookie.Value) : (int?)null;
            }
            set
            {
                _idProfissionalLogado = value;
                var cookie = new HttpCookie("idProfLog");
                cookie.Value = _idProfissionalLogado.ToString();
                cookie.Expires = DatetimeBrasil.HorarioDeBrasilia().AddHours(12);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }


        private static string _emailUsuarioLogado;
        public static string EmailUsuarioLogado
        {
            get
            {
                var cookie = HttpContext.Current.Request.Cookies["emailUsuLog"];
                return cookie != null && !string.IsNullOrEmpty(cookie.Value) ? cookie.Value : string.Empty;
            }
            set
            {
                _emailUsuarioLogado = value;
                var cookie = new HttpCookie("emailUsuLog");
                cookie.Value = _emailUsuarioLogado;
                cookie.Expires = DatetimeBrasil.HorarioDeBrasilia().AddHours(12);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }


        private static string _loginUsuarioLogado;
        public static string LoginUsuarioLogado
        {
            get
            {
                var cookie = HttpContext.Current.Request.Cookies["loginUsuLog"];
                return cookie != null && !string.IsNullOrEmpty(cookie.Value) ? cookie.Value : string.Empty;
            }
            set
            {
                _loginUsuarioLogado = value;
                var cookie = new HttpCookie("loginUsuLog");
                cookie.Value = _loginUsuarioLogado;
                cookie.Expires = DatetimeBrasil.HorarioDeBrasilia().AddHours(12);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }


        private static string _direitosUsuarioLogado;
        public static string DireitoUsuarioLogado
        {
            get
            {
                var cookie = HttpContext.Current.Request.Cookies["dirUsuLog"];
                return cookie != null && !string.IsNullOrEmpty(cookie.Value) ? cookie.Value : string.Empty;
            }
            set
            {
                _direitosUsuarioLogado = value;
                var cookie = new HttpCookie("dirUsuLog");
                cookie.Value = _direitosUsuarioLogado;
                cookie.Expires = DatetimeBrasil.HorarioDeBrasilia().AddHours(12);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }


        private static string _cargoUsuarioLogado;
        public static string CargoUsuarioLogado
        {
            get
            {
                var cookie = HttpContext.Current.Request.Cookies["cargoUsuLog"];
                return cookie != null && !string.IsNullOrEmpty(cookie.Value) ? cookie.Value : string.Empty;
            }
            set
            {
                _cargoUsuarioLogado = value;
                var cookie = new HttpCookie("cargoUsuLog");
                cookie.Value = _cargoUsuarioLogado;
                cookie.Expires = DatetimeBrasil.HorarioDeBrasilia().AddHours(12);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }


        private static string _urlAvatarUsuarioLogado;
        public static string UrlAvatarUsuarioLogado
        {
            get
            {
                var cookie = HttpContext.Current.Request.Cookies["urlAvaUsuLog"];
                return cookie != null && !string.IsNullOrEmpty(cookie.Value) ? cookie.Value : "../../assets/pages/media/profile/profile_user.png";
            }
            set
            {
                _urlAvatarUsuarioLogado = value;
                var cookie = new HttpCookie("urlAvaUsuLog");
                cookie.Value = _urlAvatarUsuarioLogado;
                cookie.Expires = DatetimeBrasil.HorarioDeBrasilia().AddHours(12);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }


        private static int? _idEmpresaLogada;
        public static int? IdEmpresaLogada
        {
            get
            {
                if (HttpContext.Current == null)
                    return (int?) null;

                var cookie = HttpContext.Current.Request.Cookies["idEmpLog"];
                if (cookie == null && HttpContext.Current.Session["EmpresaLogada"] != null)
                {
                    _idEmpresaLogada = ((Usuario)HttpContext.Current.Session["EmpresaLogada"]).Id;
                    cookie = new HttpCookie("idEmpLog");
                    cookie.Value = _idEmpresaLogada.ToString();
                    cookie.Expires = DatetimeBrasil.HorarioDeBrasilia().AddHours(12);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                }

                return cookie != null && !string.IsNullOrEmpty(cookie.Value) ? Convert.ToInt32(cookie.Value) : (int?)null;
            }
            set
            {
                _idEmpresaLogada = value;
                var cookie = new HttpCookie("idEmpLog");
                cookie.Value = _idEmpresaLogada.ToString();
                cookie.Expires = DatetimeBrasil.HorarioDeBrasilia().AddHours(12);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }


        private static string _nomeEmpresaLogada;
        public static string NomeEmpresaLogada
        {
            get
            {
                var cookie = HttpContext.Current.Request.Cookies["nomeEmpLog"];
                return cookie != null && !string.IsNullOrEmpty(cookie.Value) ? cookie.Value : string.Empty;
            }
            set
            {
                _nomeEmpresaLogada = value;
                var cookie = new HttpCookie("nomeEmpLog");
                cookie.Value = _nomeEmpresaLogada;
                cookie.Expires = DatetimeBrasil.HorarioDeBrasilia().AddHours(12);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }


        private static DateTime _dataBloqueio;
        public static DateTime DataBloqueio
        {
            get
            {
                var cookie = HttpContext.Current.Request.Cookies["dtBloqEmpLog"];
                return cookie != null && !string.IsNullOrEmpty(cookie.Value) ? Convert.ToDateTime(cookie.Value) : DateTime.MinValue;
            }
            set
            {
                _dataBloqueio = value;
                var cookie = new HttpCookie("dtBloqEmpLog");
                cookie.Value = _dataBloqueio.ToString("dd/MM/yyyy");
                cookie.Expires = DatetimeBrasil.HorarioDeBrasilia().AddHours(12);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

    }
}
