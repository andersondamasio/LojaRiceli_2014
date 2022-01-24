using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Loja.Utils
{
    public class UrlReWriteRouteHandler : IRouteHandler
    {
        public string PageURL { get; private set; }

        public UrlReWriteRouteHandler(string pageURL)
        {
            this.PageURL = pageURL;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
                string getCurrURL = requestContext.HttpContext.Request.Url.AbsolutePath;
                string segments1 = requestContext.HttpContext.Request.Url.Segments[1];
                var values = requestContext.RouteData.Values;
                if (values["gru_nomeAmigavel"] != null)
                {
                    //PageURL = GetCurrURL;
                    //requestContext.RouteData.Values["filtro"] = "P";
                }
                else
                {
                    int proSku_id = 0;
                    if (values["proSku_id"] != null)
                    {
                        HttpContext.Current.Response.Clear();
                        HttpContext.Current.Response.Status = "301 Moved Permanently";
                        HttpContext.Current.Response.AddHeader("Location", getCurrURL.Replace(values["proSku_id"] as string, string.Empty));
                        HttpContext.Current.Response.End();
                    }
                    else
                        if (values["proSku_id"] == null)
                        {
                            string nomeProduto = values["nomeProduto"] as string;
                            string proSku_idValida = nomeProduto.Substring(nomeProduto.LastIndexOf('-') + 1);
                            Int32 tempnum = 0;
                            bool hasNum = Int32.TryParse(proSku_idValida, out tempnum);
                            if (hasNum)
                            {
                                proSku_id = Convert.ToInt32(proSku_idValida);
                                requestContext.RouteData.Values["proSku_id"] = proSku_id;
                            }
                            else
                            {
                               /* getCurrURL = getCurrURL.TrimEnd('/');
                                getCurrURL += "/marc.a";
                                HttpContext.Current.Response.Redirect(getCurrURL, false);
                                */
                                    HttpContext.Current.Response.StatusCode = (int)System.Net.HttpStatusCode.NotFound;
                                    HttpContext.Current.Response.End();
                                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.ApplicationPath, false);
                                    
                                /*  HttpContext.Current.Response.Clear();
                                  HttpContext.Current.Response.Status = "301 Moved Permanently";
                                  HttpContext.Current.Response.AddHeader("Location", HttpContext.Current.Request.ApplicationPath);
                                  HttpContext.Current.Response.End();*/
                            }
                        }
                        else
                        {
                            PageURL = getCurrURL;
                        }
                }
            return System.Web.Compilation.BuildManager.CreateInstanceFromVirtualPath(PageURL, typeof(System.Web.UI.Page)) as System.Web.UI.Page;
        }
    }
}