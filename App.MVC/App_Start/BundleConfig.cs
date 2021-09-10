using System.Web;
using System.Web.Optimization;

namespace App.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region views

            bundles.Add(new ScriptBundle("~/bundles/Comanda-Detalhe").Include(
                    "~/assets/views/Comanda-Detalhe.js"));

            bundles.Add(new ScriptBundle("~/bundles/Comanda-Faturar").Include(
                    "~/assets/views/Comanda-Faturar.js"));

            bundles.Add(new ScriptBundle("~/bundles/Comanda-Index").Include(
                    "~/assets/views/Comanda-Index.js"));

            bundles.Add(new ScriptBundle("~/bundles/Comanda-Produto").Include(
                    "~/assets/views/Comanda-Produto.js"));

            bundles.Add(new ScriptBundle("~/bundles/Comanda-Servico").Include(
                    "~/assets/views/Comanda-Servico.js"));

            bundles.Add(new ScriptBundle("~/bundles/Agenda-Index").Include(
                    "~/assets/views/Agenda-Index.js"));

            bundles.Add(new ScriptBundle("~/bundles/Agenda-Detalhe").Include(
                    "~/assets/views/Agenda-Detalhe.js"));

            bundles.Add(new ScriptBundle("~/bundles/Cliente-Index").Include(
                    "~/assets/views/Cliente-Index.js"));

            bundles.Add(new ScriptBundle("~/bundles/Financeiro-Index").Include(
                    "~/assets/views/Financeiro-Index.js"));

            bundles.Add(new ScriptBundle("~/bundles/Financeiro-Recebimento").Include(
                    "~/assets/views/Financeiro-Recebimento.js"));

            bundles.Add(new ScriptBundle("~/bundles/Financeiro-Recebimento").Include(
                    "~/assets/views/Financeiro-Despesa.js"));

            bundles.Add(new ScriptBundle("~/bundles/Produto-Index").Include(
                    "~/assets/views/Produto-Index.js"));

            bundles.Add(new ScriptBundle("~/bundles/Profissional-Index").Include(
                    "~/assets/views/Profissional-Index.js"));

            bundles.Add(new ScriptBundle("~/bundles/Servico-Index").Include(
                    "~/assets/views/Servico-Index.js"));

            bundles.Add(new ScriptBundle("~/bundles/Usuario-Index").Include(
                    "~/assets/views/Usuario-Index.js"));

            bundles.Add(new ScriptBundle("~/bundles/Relatorios").Include(
                    "~/assets/views/Relatorios.js"));

            bundles.Add(new ScriptBundle("~/bundles/Empresa-Perfil").Include(
                    "~/assets/views/Empresa-Perfil.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard-Gerencial").Include(
                     "~/assets/views/Dashboard-Gerencial.js"));

            bundles.Add(new ScriptBundle("~/bundles/Home-Index").Include(
                    "~/assets/views/Home-Index.js"));


            #endregion

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      "~/assets/global/plugins/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/assets/global/plugins/bootstrap/js/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/cookie").Include(
                      "~/assets/global/plugins/js.cookie.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-hover-dropdown").Include(
                      "~/assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-switch").Include(
                      "~/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                      "~/assets/global/scripts/app.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/layout").Include(
                      "~/assets/layouts/layout/scripts/layout.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/demo").Include(
                      "~/assets/layouts/layout/scripts/demo.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/quick-sidebar").Include(
                      "~/assets/layouts/global/scripts/quick-sidebar.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/sweetalert").Include(
                      "~/assets/global/plugins/sweetalert/sweetalert.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-migrate").Include(
                      "~/assets/global/plugins/jquery-migrate-1.2.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                      "~/assets/global/plugins/jquery-ui-1.11.4/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/weekcalendar").Include(
                      "~/assets/global/plugins/weekcalendar/jquery.weekcalendar.js",
                      "~/assets/global/plugins/weekcalendar/date.js"));

            bundles.Add(new ScriptBundle("~/bundles/typeahead").Include(
                      "~/assets/global/plugins/typeahead/typeahead.bundle.min.js",
                      "~/assets/global/plugins/typeahead/handlebars.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-switch").Include(
                      "~/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
                      "~/assets/pages/scripts/components-bootstrap-switch.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-select").Include(
                      "~/assets/global/plugins/bootstrap-select/js/bootstrap-select.min.js",
                      "~/assets/pages/scripts/components-bootstrap-select.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/mascaras").Include(
                    "~/assets/global/plugins/Mascaras.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-timepicker").Include(
                    "~/assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-material-datetimepicker").Include(
                    "~/assets/global/plugins/bootstrap-material-datetimepicker/js/bootstrap-material-datetimepicker.js",
                    "~/assets/pages/scripts/components-date-time-pickers.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                    "~/assets/global/scripts/datatable.js",
                    "~/assets/global/plugins/datatables/datatables.min.js",
                    "~/assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery.validate").Include(
                    "~/assets/global/plugins/jquery-validation/js/jquery.validate.min.js",
                    "~/assets/global/plugins/jquery-validation/js/additional-methods.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/nouislider").Include(
                    "~/assets/global/plugins/nouislider/wNumb.min.js",
                    "~/assets/global/plugins/nouislider/nouislider.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/sparkline").Include(
                    "~/assets/global/plugins/jquery.sparkline.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/profile").Include(
                    "~/assets/pages/scripts/profile.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/amcharts").Include(
                    "~/assets/global/plugins/amcharts/amcharts/amcharts.js",
                    "~/assets/global/plugins/amcharts/amcharts/serial.js",
                    "~/assets/global/plugins/amcharts/amcharts/pie.js",
                    "~/assets/global/plugins/amcharts/amcharts/radar.js",
                    "~/assets/global/plugins/amcharts/amcharts/themes/light.js",
                    "~/assets/global/plugins/amcharts/amstockcharts/amstock.js"));

            bundles.Add(new ScriptBundle("~/bundles/tutorialize").Include(
                    "~/assets/global/plugins/tutorialize/jquery.tutorialize.sec.js"));

            /*********************************************** CSS **********************************************************/

            bundles.Add(new StyleBundle("~/Content/tutorialize").Include(
                     "~/assets/global/plugins/tutorialize/tutorialize.css"));


            bundles.Add(new StyleBundle("~/Content/nouislider").Include(
                     "~/assets/global/plugins/nouislider/nouislider.min.css",
                     "~/assets/global/plugins/nouislider/nouislider.pips.css"));

            bundles.Add(new StyleBundle("~/Content/datatables").Include(
                     "~/assets/global/plugins/datatables/datatables.min.css",
                     "~/assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/profile").Include(
                     "~/assets/pages/css/profile.min.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-material-datetimepicker").Include(
                     "~/assets/global/plugins/bootstrap-material-datetimepicker/css/bootstrap-material-datetimepicker.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-timepicker").Include(
                     "~/assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-select").Include(
                     "~/assets/global/plugins/bootstrap-select/css/bootstrap-select.min.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-switch").Include(
                     "~/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css"));

            bundles.Add(new StyleBundle("~/Content/typeahead").Include(
                     "~/assets/global/plugins/typeahead/typeahead.css"));

            bundles.Add(new StyleBundle("~/Content/font-awesome").Include(
                       "~/assets/global/plugins/font-awesome/css/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                       "~/assets/global/plugins/bootstrap/css/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/uniform.default").Include(
                       "~/assets/global/plugins/uniform/css/uniform.default.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-switch").Include(
                       "~/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css"));

            bundles.Add(new StyleBundle("~/Content/components-md").Include(
                       "~/assets/global/css/components-md.min.css"));

            bundles.Add(new StyleBundle("~/Content/plugins-md").Include(
                       "~/assets/global/css/plugins-md.min.css"));

            bundles.Add(new StyleBundle("~/Content/custom").Include(
                       "~/assets/layouts/layout/css/custom.min.css"));

            bundles.Add(new StyleBundle("~/Content/layout").Include(
                       "~/assets/layouts/layout/css/layout.min.css"));

            bundles.Add(new StyleBundle("~/Content/sweetalert").Include(
                       "~/assets/global/plugins/sweetalert/sweetalert.cs"));

            bundles.Add(new StyleBundle("~/Content/jquery-ui-1.8.11.custom").Include(
                       "~/assets/global/plugins/weekcalendar/css/smoothness/jquery-ui-1.8.11.custom.css"));

            bundles.Add(new StyleBundle("~/Content/weekcalendar").Include(
                       "~/assets/global/plugins/weekcalendar/jquery.weekcalendar.css"));

            bundles.Add(new StyleBundle("~/Content/gcalendar").Include(
                       "~/assets/global/plugins/weekcalendar/skins/gcalendar.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
