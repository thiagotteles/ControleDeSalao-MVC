using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Panco.MVC.AutoMapper;

namespace App.MVC
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutoMapperConfig.RegisterMapping();
        }

        void Application_Error(Object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            if (exception == null)
                return;
            if (exception is ArgumentNullException)
                Response.Redirect("Usuario/Login");

            Server.ClearError();
            var urlReferrer = ((HttpApplication)(sender)).Request.UrlReferrer;
            if (urlReferrer != null)
                Response.Redirect(urlReferrer.AbsolutePath);
        }
    }
}
