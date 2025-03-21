using ShmffPortal.Infrastuture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShmffPortal.Filters
{
    public class RegFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (OurConfiguration.allReg.ToString()=="false")
            {
                if (HttpContext.Current.Session["Token"] == null)
                {
                    //var Token = HttpContext.Current.Session["Token"].ToString();

                    //filterContext.Result = new RedirectToRouteResult(
                    //new RouteValueDictionary(new { controller = "Account", action = "Login" })
                    //    ); 
                    //filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);

                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}