using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HarrisZhang
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
#if !DEBUG
            var serverName = Request.ServerVariables["SERVER_NAME"];
            if (!"www.harriszhang.com".Equals(serverName, StringComparison.OrdinalIgnoreCase))
            {
                Response.RedirectPermanent("https://www.harriszhang.com" + Request.RawUrl);
                Response.End();
            }
#endif
        }
    }
}
