using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;

namespace Loja.Utils
{
    public class UrlReWriteRouteHandlerProdutoCorTamanho : IRouteHandler //, IHttpHandler
    {
        public string PageURL { get; private set; }
        public static object RouteData = new object();

        public UrlReWriteRouteHandlerProdutoCorTamanho(string pageURL)
        {
            this.PageURL = pageURL;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
          /*  var values = requestContext.RouteData.Values;
            if (values["proSku_cores"] != null)
            {
                requestContext.RouteData.Values.Add("gru_nomeAmigavel", requestContext.RouteData.Values["pro_nome"]);


            }*/


            return System.Web.Compilation.BuildManager.CreateInstanceFromVirtualPath(PageURL, typeof(System.Web.UI.Page)) as System.Web.UI.Page;
        }


       /* public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Server.Transfer(PageURL, true);
        }*/

    }
}