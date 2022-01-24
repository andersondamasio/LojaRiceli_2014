using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using System.IO;
using Loja.Utils;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Configuration;

namespace Loja
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }

        void RegisterRoutes(RouteCollection routes)
        {
            routes.RouteExistingFiles = false;
            routes.LowercaseUrls = true;
            routes.Ignore("{*allswf}", new { allswf = @".*\.swf(/.*)?" });
            routes.Ignore("{*allpng}", new { allpng = @".*\.png(/.*)?" });
            routes.Ignore("{*allgif}", new { allgif = @".*\.gif(/.*)?" });
            routes.Ignore("{*allaxd}", new { allaxd = @".*\.axd(/.*)?" });
            routes.Ignore("{*allaspx}", new { allaspx = @".*\.aspx(/.*)?" });
            routes.Ignore("{*allcss}", new { allcss = @".*\.css(/.*)?" });
            routes.Ignore("{*alljpg}", new { alljpg = @".*\.jpg(/.*)?" });
            routes.Ignore("{*alljs}", new { alljs = @".*\.js(/.*)?" });
            routes.Ignore("{*allashx}", new { allashx = @".*\.ashx(/.*)?" });
            routes.Ignore("imagens/{*pathInfo}");
            routes.MapPageRoute("PaginaInicial", "", "~/Index.aspx");
            routes.MapPageRoute("NaoConfigurado", "NaoConfigurado.aspx", "~/Redirecionamentos/NaoConfigurado.aspx");
           
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            
        }

    } 
}