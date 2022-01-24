using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;

namespace _1_WebForm.App_Code.Utils
{
    public class UrlReWriteRouteHandlerProduto : IRouteHandler //, IHttpHandler
    {
        public string PageURL { get; private set; }
        public static object RouteData = new object();

        public UrlReWriteRouteHandlerProduto(string pageURL)
        {
            this.PageURL = pageURL;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            string pageProdutoGrupo = "~/ProdutoGrupo.aspx";
            string pageProdutoDetalhe = "~/ProdutoDetalhe.aspx";

            var values = requestContext.RouteData.Values;
            int proSku_id = 0;

            if (values["proSku_id"] == null)
            {
                string nomeProduto = values["gru_nomeAmigavel"] as string;
                string proSku_idValida = nomeProduto.Substring(nomeProduto.LastIndexOf('-') + 1);
                Int32 tempnum = 0;
                bool hasNum = Int32.TryParse(proSku_idValida, out tempnum);
                if (hasNum)
                {
                    proSku_id = Convert.ToInt32(proSku_idValida);
                    requestContext.RouteData.Values["proSku_id"] = proSku_id;
                    PageURL = pageProdutoDetalhe;
                }
                else
                {
                    PageURL = pageProdutoGrupo;
                }
            }


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