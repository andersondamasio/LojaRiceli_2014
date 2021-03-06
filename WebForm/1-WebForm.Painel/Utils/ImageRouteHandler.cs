using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Loja.Utils
{
    public class ImageRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            string grupo = requestContext.RouteData.Values["grupo"] as string;
            string aquivo = requestContext.RouteData.Values["aquivo"] as string;

            if (string.IsNullOrEmpty(aquivo))
            {
                requestContext.HttpContext.Response.Clear();
                requestContext.HttpContext.Response.StatusCode = 404;
                requestContext.HttpContext.Response.End();
            }
            else
            {
                requestContext.HttpContext.Response.Clear();
                requestContext.HttpContext.Response.ContentType = GetContentType(requestContext.HttpContext.Request.Url.ToString());

                // find physical path to image here.  
                string filepath = requestContext.HttpContext.Server.MapPath("~/imagens/produtos/fotos/" + aquivo);

                requestContext.HttpContext.Response.WriteFile(filepath);
                requestContext.HttpContext.Response.End();
            }
            return null;
        }

        private static string GetContentType(String path)
        {
            switch (Path.GetExtension(path))
            {
                case ".bmp": return "Image/bmp";
                case ".gif": return "Image/gif";
                case ".jpg": return "Image/jpeg";
                case ".png": return "Image/png";
                default: break;
            }
            return "";
        }
    }
}