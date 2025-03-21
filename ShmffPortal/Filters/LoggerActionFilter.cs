using Newtonsoft.Json;
using ShmffPortal.DAL;
using ShmffPortal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace ShmffPortal.Filters
{
    public class LoggerActionFilter : ActionFilterAttribute
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
       // private NewPortalDBEntities NewPortalDB;// = new ERPEntities();
       //// DataAccessCls dataAccessCls;
       // public NewPortalDBEntities entity
       // {
       //     get
       //     {
       //         if (NewPortalDB == null)
       //         {
       //             NewPortalDB = new NewPortalDBEntities();
       //         }
       //         return NewPortalDB;
       //     }
       // }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                string actionName = filterContext.ActionDescriptor.ActionName;
                string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                var parameters = filterContext.ActionDescriptor.GetParameters();
                string browser;

                var Item = new List<KeyValuePair<string, string>>();
                foreach (var parameter in filterContext.ActionParameters)
                {
                    Item.Add(new KeyValuePair<string, string>(parameter.Key.ToString(), parameter.Value == null ? "" : parameter.Value.ToString()));
                }

                var formCollection = filterContext.Controller.ControllerContext.HttpContext.Request.Form;//key/value
                var keys = formCollection.Keys;//get keys of formCollection

                foreach (var key in formCollection.AllKeys)
                {
                    Item.Add(new KeyValuePair<string, string>(key, formCollection[key]));
                }
                //foreach (var key in keys)
                //{
                //    parmeterslist.Add(key.ToString(), formCollection..ToString());
                //   // var dataValue = formCollection.GetValues(key.ToString());//get data value from form
                //}

                browser = (HttpContext.Current.Request.UserAgent.ToString().Contains("Chrome")) ? "Chrome" : HttpContext.Current.Request.Browser.Browser.ToString();


                browser = ((browser == "Chrome") || (browser == "AppleMAC-Safari")) ? browser : browser + "-" + HttpContext.Current.Request.Browser.Version.ToString();
                string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ip))
                {
                    ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                string UserName = HttpContext.Current.User.Identity.Name;

                if (HttpContext.Current.Session["Token"] != null)
                {
                    var Token = HttpContext.Current.Session["Token"].ToString();
                    using (var entity=new NewPortalDBEntities())
                    {
                        var userlogged = entity.RegisterSSNs.Where(a => a.Token == Token && a.SSN == UserName).FirstOrDefault();
                        if (userlogged == null)
                        {
                            FormsAuthentication.SignOut();
                            // Second we clear the principal to ensure the user does not retain any authentication
                            //required NameSpace: using System.Security.Principal;
                            //HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
                            HttpContext.Current.Session.Clear();
                            System.Web.HttpContext.Current.Session.RemoveAll();

                            filterContext.Result = new RedirectToRouteResult(
                                new RouteValueDictionary(new { controller = "Account", action = "Login" })
                                    );
                            filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
                        }
                    }  
                    
                }

                using (var entity = new NewPortalDBEntities())
                {
                    //var Userid = userBll.GETUSERID(UserName);
                    //Generate an audit
                    Portal_Logger aLogger = new Portal_Logger()
                    {
                        //Your Logger Identifier     
                        loggerID = Guid.NewGuid(),
                        //Logged On User Id
                        USERNAME = UserName,
                        //The IP Address of the Request
                        IPADDRESS = ip,// Convert.ToString(ipHostInfo.AddressList.FirstOrDefault(address => address.AddressFamily == AddressFamily.InterNetwork)),
                                       //Get the Web Page name(from the URL that was accessed)
                                       //AreaAccessed = UserActivityUtility.GetWebPageName(request.RawUrl == "/" ? "/Home/UserLandingPage" : request.RawUrl),
                        CONTROLLERNAME = controllerName,
                        ACTIONAME = actionName,
                        //Creates our Timestamp
                        Timestamp = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE),
                        browser = browser,
                        PARAMETERSFORM = JsonConvert.SerializeObject(Item)
                    };
                    // Stores the Logger in the Database
                    entity.Portal_Logger.Add(aLogger);
                    entity.SaveChanges();
                }
            }
            catch
            {

            }
            //Finishes executing the Logger as normal 
            base.OnActionExecuting(filterContext);
        }
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    try
        //    {
        //        dataAccessCls = new DataAccessCls();
        //        string actionName = filterContext.ActionDescriptor.ActionName;
        //        string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
        //        var parameters = filterContext.ActionDescriptor.GetParameters();
        //        string browser;

        //        var Item = new List<KeyValuePair<string, string>>();
        //        foreach (var parameter in filterContext.ActionParameters)
        //        {
        //            Item.Add(new KeyValuePair<string, string>(parameter.Key.ToString(), parameter.Value == null ? "" : parameter.Value.ToString()));
        //        }

        //        var formCollection = filterContext.Controller.ControllerContext.HttpContext.Request.Form;//key/value
        //        var keys = formCollection.Keys;//get keys of formCollection

        //        foreach (var key in formCollection.AllKeys)
        //        {
        //            Item.Add(new KeyValuePair<string, string>(key, formCollection[key]));
        //        }
        //        //foreach (var key in keys)
        //        //{
        //        //    parmeterslist.Add(key.ToString(), formCollection..ToString());
        //        //   // var dataValue = formCollection.GetValues(key.ToString());//get data value from form
        //        //}

        //        browser = (HttpContext.Current.Request.UserAgent.ToString().Contains("Chrome")) ? "Chrome" : HttpContext.Current.Request.Browser.Browser.ToString();


        //        browser = ((browser == "Chrome") || (browser == "AppleMAC-Safari")) ? browser : browser + "-" + HttpContext.Current.Request.Browser.Version.ToString();
        //        string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        //        if (string.IsNullOrEmpty(ip))
        //        {
        //            ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        //        }
        //        string UserName = HttpContext.Current.User.Identity.Name;

        //        if (HttpContext.Current.Session["Token"] != null)
        //        {
        //            var Token = HttpContext.Current.Session["Token"].ToString();

        //            var loggedinDt = dataAccessCls.ExcuteQuery("select * from RegisterSSN where Token='" + Token + "' and ssn='" + UserName + "'");
        //            if (loggedinDt.Rows.Count == 0)
        //            {
        //                FormsAuthentication.SignOut();
        //                // Second we clear the principal to ensure the user does not retain any authentication
        //                //required NameSpace: using System.Security.Principal;
        //                //HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
        //                HttpContext.Current.Session.Clear();
        //                System.Web.HttpContext.Current.Session.RemoveAll();

        //                filterContext.Result = new RedirectToRouteResult(
        //                    new RouteValueDictionary(new { controller = "Account", action = "Login" })
        //                        );
        //                filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
        //            }
        //            //        var userlogged = entity.RegisterSSNs.Where(a => a.Token == Token && a.SSN == UserName).FirstOrDefault();
        //            //if (userlogged == null)
        //            //{
        //            //    FormsAuthentication.SignOut();
        //            //    // Second we clear the principal to ensure the user does not retain any authentication
        //            //    //required NameSpace: using System.Security.Principal;
        //            //    //HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
        //            //    HttpContext.Current.Session.Clear();
        //            //    System.Web.HttpContext.Current.Session.RemoveAll();

        //            //    filterContext.Result = new RedirectToRouteResult(
        //            //        new RouteValueDictionary(new { controller = "Account", action = "Login" })
        //            //            );
        //            //    filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
        //            //}
        //        }


        //        //Generate an audit


        //        SqlParameter[] param = new SqlParameter[7];
        //        param[0] = new SqlParameter("@UserName", SqlDbType.NVarChar, 50);
        //        param[0].Value = UserName;

        //        param[1] = new SqlParameter("@IPADDRESS", SqlDbType.NVarChar, 50);
        //        param[1].Value = ip;

        //        param[2] = new SqlParameter("@AREAACCESSED", SqlDbType.NVarChar, 150);
        //        param[2].Value = "";

        //        param[3] = new SqlParameter("@CONTROLLERNAME", SqlDbType.NVarChar, 150);
        //        param[3].Value = controllerName;

        //        param[4] = new SqlParameter("@ACTIONAME", SqlDbType.NVarChar, 150);
        //        param[4].Value = actionName;

        //        param[5] = new SqlParameter("@browser", SqlDbType.NVarChar, 150);
        //        param[5].Value = browser;
        //        param[6] = new SqlParameter("@PARAMETERSFORM", SqlDbType.NVarChar, 5000);
        //        param[6].Value = JsonConvert.SerializeObject(Item);

        //        dataAccessCls.Open();
        //        DataTable dt = new DataTable();
        //        dt = dataAccessCls.SelectData("Portal_Logger_Insert", param);
        //        dataAccessCls.Close();

        //        //    Portal_Logger aLogger = new Portal_Logger()
        //        //{
        //        //    //Your Logger Identifier     
        //        //    //Logged On User Id
        //        //    USERNAME = UserName,
        //        //    //The IP Address of the Request
        //        //    IPADDRESS = ip,// Convert.ToString(ipHostInfo.AddressList.FirstOrDefault(address => address.AddressFamily == AddressFamily.InterNetwork)),
        //        //    //Get the Web Page name(from the URL that was accessed)
        //        //    //AreaAccessed = UserActivityUtility.GetWebPageName(request.RawUrl == "/" ? "/Home/UserLandingPage" : request.RawUrl),
        //        //    CONTROLLERNAME = controllerName,
        //        //    ACTIONAME = actionName,
        //        //    //Creates our Timestamp
        //        //     browser = browser,
        //        //    PARAMETERSFORM = JsonConvert.SerializeObject(Item)
        //        //};
        //        // Stores the Logger in the Database
        //        //entity.Portal_Logger.Add(aLogger);
        //        //entity.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    //Finishes executing the Logger as normal 
        //    base.OnActionExecuting(filterContext);
        //}

    }

}