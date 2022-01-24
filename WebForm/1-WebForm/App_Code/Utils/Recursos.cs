using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using _2_Library.Dao.Site.Clientex;
namespace _1_WebForm.App_Code.Utils
{
    public class Recursos
    {
        public static bool VerificarAutenticacao(bool redirecionarParaLogin){
            if (!HttpContext.Current.Request.IsAuthenticated)
            {
                if (redirecionarParaLogin)
                    System.Web.Security.FormsAuthentication.RedirectToLoginPage();
                return false;
            }
            return true;
        }


      /*  public static void Autenticar2(_2_Library.Dao.Clientex.ClienteDto clienteDto)
        {      
            FormsAuthentication.SetAuthCookie(HttpContext.Current.Server.HtmlEncode(clienteDto.cli_email), false);
 
            //string userData = clienteDto.cli_id + "|" + clienteDto.cli_nome.Replace("|", string.Empty);

            var serializer = new JavaScriptSerializer();
            var userData = serializer.Serialize(clienteDto);

            HttpCookie authCookie = new HttpCookie("rixP");
            authCookie = FormsAuthentication.GetAuthCookie(clienteDto.cli_email, false);
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, userData);
     
            authCookie.Value = FormsAuthentication.Encrypt(newTicket);
            HttpContext.Current.Response.Cookies.Add(authCookie);
        }*/

       


      /*  public CustomIdentity AutenticacaoDados()
        {
            CustomIdentity customIdentity = HttpContext.Current.User.Identity as CustomIdentity;

            if (customIdentity != null)
            {
                return customIdentity;
            }
           
            return customIdentity;
        }

        public void AutenticacaoSair()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Response.Redirect(GetRouteUrl("PaginaInicial", null));
        }*/

        public string GetRouteUrl(string routeName, object routeParameters)
        {
            var dict = new RouteValueDictionary(routeParameters);
            var data = RouteTable.Routes.GetVirtualPath(HttpContext.Current.Request.RequestContext, routeName, dict);
            if (data != null)
            {
                return data.VirtualPath;
            }
            return null;
        }

        public static string VerificaNavegador()
        {
            System.Web.HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
            string s = "Recursos do Navegador \n"
           + "tipo =" + browser.Type + "\n"
           + "Nome =" + browser.Browser + "\n"
           + "Version =" + browser.Version + "\n "
           + "Versão Principal =" + browser.MajorVersion + "\n"
           + "Minor Version =" + browser.MinorVersion + "\n"
           + "Plataforma =" + browser.Platform + "\n"
           + "é beta = " + browser.Beta + "\n"
           + "é Crawler =" + browser.Crawler + "\n"
           + "É AOL =" + browser.AOL + "\n"
           + "é Win16 =" + browser.Win16 + "\n"
           + "é Win32 =" + browser.Win32 + "\n"
           + "Suporta Frames =" + browser.Frames + "\n"
           + "suporta tabelas =" + browser.Tables + "\n"
           + "Suporta cookies =" + browser.Cookies + "\n"
           + "Suporta VBScript =" + browser.VBScript + "\n"
           + "Suporta JavaScript =" +
               browser.EcmaScriptVersion.ToString() + "\n"
           + "Suporta Java Applets =" + browser.JavaApplets + "\n"
           + "Suporta os controles ActiveX =" + browser.ActiveXControls
                 + "\n"
           + "Suporta JavaScript Version =" +
               browser["JavaScriptVersion"] + "\n";

            return s;
        }
    }
}