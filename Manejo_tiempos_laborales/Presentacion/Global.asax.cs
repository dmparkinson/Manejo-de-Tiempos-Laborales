using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Presentacion
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();

            HttpException httpException = exception as HttpException;

            int error = httpException != null ? httpException.GetHttpCode() : 0;
            string ruta = "";
            switch (error)
            {
                case 505:
                    ruta = "Error";
                    break;
                case 500:
                    ruta = "Error";
                    break;
                case 403:
                    ruta = "Error403";
                    break;

                case 404:
                    ruta = "Error404";
                    break;

                default:
                    ruta = "Unknown";
                    break;
            }
            Server.ClearError();
            Response.Redirect("~/Error/"+ruta);




        }


    }
}
