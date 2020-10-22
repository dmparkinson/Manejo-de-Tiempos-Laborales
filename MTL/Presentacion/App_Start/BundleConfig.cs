using System.Web;
using System.Web.Optimization;

namespace Presentacion
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
          //  bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
           //             "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

          //  bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
           //           "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));




            bundles.Add(new ScriptBundle("~/bundles/ajax").Include(
                      "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/plugins/bootstrap/js/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/plugins/jquery/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/pooper").Include(
                        "~/plugins/pooper/pooper.min.js"));

            bundles.Add(new StyleBundle("~/Content/adminltecss").Include(
                      "~/dist/css/adminlte.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/adminltejs").Include(
                        "~/dist/js/adminlte.min.js"));

            bundles.Add(new StyleBundle("~/Content/datatables").Include(
                      "~/plugins/datatables-bs4/css/dataTables.bootstrap4.min.css"));

            bundles.Add(new StyleBundle("~/Content/responsive.datatables").Include(
                      "~/plugins/datatables-responsive/css/responsive.bootstrap4.min.css"));


            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                      "~/plugins/datatables/jquery.dataTables.min.js",
                      "~/plugins/datatables-bs4/js/dataTables.bootstrap4.min.js",
                      "~/plugins/datatables-responsive/js/dataTables.responsive.min.js",
                      "~/plugins/datatables-responsive/js/responsive.bootstrap4.min.js",
                      "~/Scripts/js/scriptsTables.js"));


            bundles.Add(new StyleBundle("~/Content/sweetalert2").Include(
                      "~/plugins/sweetalert2/sweetalert2.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/sweetalert2").Include(
                      "~/plugins/sweetalert2/sweetalert2.min.js"));

            bundles.Add(new StyleBundle("~/Content/fontawesome").Include(
                      "~/plugins/fontawesome-free/css/all.min.css"));


            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                      "~/Scripts/js/scriptsCatalogoAusencias.js",
                      "~/Scripts/js/scriptsCatalogoCircuitos.js",
                      "~/Scripts/js/scriptsCatalogoOficina.js",
                      "~/Scripts/js/scriptsCatalogoPuesto.js", 
                      "~/Scripts/js/scriptsCatalogoRoles.js",
                      "~/Scripts/js/scriptsCatalogoTiempoL.js",
                      "~/Scripts/js/scriptsAusencias.js",
                      "~/Scripts/js/scriptsHorarios.js",
                      "~/Scripts/js/scriptsEmpleados.js",
                      "~/Scripts/js/scriptsHistoricoAusencias.js",
                      "~/Scripts/js/scriptsHistoricoHorarios.js"));


            
        }
    }
}
