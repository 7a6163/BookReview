using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BookReview
{
    // 注意: 如需啟用 IIS6 或 IIS7 傳統模式的說明，
    // 請造訪 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        public void Application_AcquireRequestState()
        {
            var langCookie = HttpContext.Current.Request.Cookies["lang"];

            //Sets default culture to english invariant
            string langName = "en";

            if (langCookie == null)
            {
                //Try to get values from Accept lang HTTP header
                if (HttpContext.Current.Request.UserLanguages != null && HttpContext.Current.Request.UserLanguages.Length != 0)
                {
                    //Gets accepted list 
                    langName = HttpContext.Current.Request.UserLanguages[0].Substring(0, 2);
                }

                //Checking first if there is no value in session 
                //and set default language 
                //this can happen for first user's request
                langCookie = new HttpCookie("lang", langName)
                {
                    HttpOnly = true
                };

                HttpContext.Current.Response.AppendCookie(langCookie);
            }

            var ci = new CultureInfo(langCookie.Value);

            //Finally setting culture for each request
            Thread.CurrentThread.CurrentUICulture = ci;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
        }
    }
}