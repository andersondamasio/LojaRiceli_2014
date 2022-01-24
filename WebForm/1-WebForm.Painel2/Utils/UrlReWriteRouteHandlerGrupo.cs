using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Loja.Utils
{
    public class UrlReWriteRouteHandlerGrupo : IRouteHandler
    {
        public string PageURL { get; private set; }

        public UrlReWriteRouteHandlerGrupo(string pageURL)
        {
            this.PageURL = pageURL;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return System.Web.Compilation.BuildManager.CreateInstanceFromVirtualPath(PageURL, typeof(System.Web.UI.Page)) as System.Web.UI.Page;
        }
    }
}