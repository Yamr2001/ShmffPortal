using ShmffPortal.BLL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ShmffPortal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ClientDataTypeModelValidatorProvider.ResourceClassKey = "MyResources";
            DefaultModelBinder.ResourceClassKey = "MyResources";
            MyErrorMessageProvider.SetResourceClassKey("MyResources");
            //var cultureInfo = new CultureInfo("en");
            //cultureInfo.DateTimeFormat.DateSeparator = "/";
            //cultureInfo.DateTimeFormat.ShortDatePattern = "MM/dd/yyyy";
            //cultureInfo.DateTimeFormat.LongDatePattern = "MM/dd/yyyy hh:mm:ss tt";
            //Thread.CurrentThread.CurrentUICulture = cultureInfo;
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
            //DateTimeFormatInfo englishdateformate = new CultureInfo("en-CA").DateTimeFormat;
            //Thread.CurrentThread.CurrentCulture.DateTimeFormat = englishdateformate;

            //HttpCookie langCookie = new HttpCookie("culture", "en");
            //langCookie.Expires = DateTime.Now.AddYears(1);
            //HttpContext.Current.Response.Cookies.Add(langCookie);
        }

        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            // TODO do whatever you want with exception, such as logging, set errorMessage, etc.
            LogExceptions logExceptions = new LogExceptions();
            logExceptions.LogError(exception);

        }

      

    }

}
