using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using _1_WebForm.App_Code.Utils;

namespace _1_WebForm
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




            /*  routes.Add("MinhaConta", new Route
                  (
                     "MinhaConta",
                     new CustomRouteHandler("~/MinhaConta.aspx")
                  ));*/

            routes.MapPageRoute("Index", "Index", "~/Index.aspx");
            routes.MapPageRoute("PaginaInicial", "", "~/Index.aspx");
            routes.MapPageRoute("ProdutoBusca", "ProdutoBusca", "~/ProdutoBusca.aspx");
            routes.MapPageRoute("Login", "Login", "~/Login.aspx");
            routes.MapPageRoute("LoginMinhaConta", "LoginMinhaConta", "~/LoginMinhaConta.aspx");
            routes.MapPageRoute("MeuCadastro", "MeuCadastro", "~/MeuCadastro.aspx");
            routes.MapPageRoute("MeusEnderecos", "MeusEnderecos", "~/MeusEnderecos.aspx");
            routes.MapPageRoute("MeusAmigos", "MeusAmigos", "~/MeusAmigos.aspx");
            routes.MapPageRoute("MeusPedidos", "MeusPedidos", "~/MeusPedidos.aspx");
            routes.MapPageRoute("MeusPontos", "MeusPontos", "~/MeusPontos.aspx");
            routes.MapPageRoute("MeuFeed", "MeuFeed", "~/MeuFeed.aspx");
            routes.MapPageRoute("RecuperarSenha", "RecuperarSenha", "~/RecuperarSenha.aspx");
            routes.MapPageRoute("Carrinho", "Carrinho", "~/Carrinho.aspx");
            routes.MapPageRoute("CadastroPagamento", "CadastroPagamento", "~/CadastroPagamento.aspx");
            routes.MapPageRoute("NaoConfigurado", "NaoConfigurado", "~/Redirecionamentos/NaoConfigurado.aspx");
            /*routes.MapPageRoute("LoginCadastro", "LoginCadastro", "~/Views/LoginCadastro.aspx");
        
            
           routes.MapPageRoute("MeuCadastro", "MeuCadastro", "~/Views/MeuCadastro.aspx");
           routes.MapPageRoute("CadastroFinalizar", "CadastroFinalizar", "~/Views/CadastroFinalizar.aspx");
           routes.MapPageRoute("PedidoConcluido", "PedidoConcluido", "~/Views/PedidoConcluido.aspx");
           routes.MapPageRoute("GerarBoleto", "Pagamento/GerarBoleto", "~/Views/Pagamento/GerarBoleto.aspx");
           routes.MapPageRoute("LojaNaoConfigurada", "Redirecionamento/LojaNaoConfigurada", "~/Views/Redirecionamento/LojaNaoConfigurada.aspx");
           */

            routes.Add(
               new System.Web.Routing.Route("{gru_nomeAmigavel}", new RouteValueDictionary(new
               {
                   proSku_id = (string)null
               }), new UrlReWriteRouteHandlerProduto("~/ProdutoDetalhe.aspx")));

            //filtro Busca de Cor e Tamanho
            routes.Add(new System.Web.Routing.Route("ProdutoBusca/cor-{proSku_cores}/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/ProdutoBusca.aspx")));

            //filtro Busca de Cor
            routes.Add(new System.Web.Routing.Route("ProdutoBusca/cor-{proSku_cores}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/ProdutoBusca.aspx")));

            //filtro Busca deTamanho
            routes.Add(new System.Web.Routing.Route("ProdutoBusca/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/ProdutoBusca.aspx")));


            //filtro Grupo de Cor e Tamanho
            routes.Add(new System.Web.Routing.Route("{gru_nomeAmigavel}/cor-{proSku_cores}/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/ProdutoGrupo.aspx")));
            routes.Add(new System.Web.Routing.Route("{pro_nome2}/{gru_nomeAmigavel}/cor-{proSku_cores}/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/ProdutoGrupo.aspx")));
            routes.Add(new System.Web.Routing.Route("{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/cor-{proSku_cores}/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/ProdutoGrupo.aspx")));
            routes.Add(new System.Web.Routing.Route("{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/cor-{proSku_cores}/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/ProdutoGrupo.aspx")));
            routes.Add(new System.Web.Routing.Route("{pro_nome5}/{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/cor-{proSku_cores}/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/ProdutoGrupo.aspx")));
            routes.Add(new System.Web.Routing.Route("{pro_nome6}/{pro_nome5}/{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/cor-{proSku_cores}/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/ProdutoGrupo.aspx")));
            routes.Add(new System.Web.Routing.Route("{pro_nome7}/{pro_nome6}/{pro_nome5}/{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/cor-{proSku_cores}/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/ProdutoGrupo.aspx")));


            //filtro Grupo de Cor
            routes.Add(new System.Web.Routing.Route("{gru_nomeAmigavel}/cor-{proSku_cores}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/ProdutoGrupo.aspx")));
            routes.Add(new System.Web.Routing.Route("{pro_nome2}/{gru_nomeAmigavel}/cor-{proSku_cores}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/ProdutoGrupo.aspx")));
            routes.Add(new System.Web.Routing.Route("{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/cor-{proSku_cores}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/ProdutoGrupo.aspx")));
            routes.Add(new System.Web.Routing.Route("{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/cor-{proSku_cores}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/ProdutoGrupo.aspx")));
            routes.Add(new System.Web.Routing.Route("{pro_nome5}/{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/cor-{proSku_cores}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/ProdutoGrupo.aspx")));
            routes.Add(new System.Web.Routing.Route("{pro_nome6}/{pro_nome5}/{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/cor-{proSku_cores}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/ProdutoGrupo.aspx")));
            routes.Add(new System.Web.Routing.Route("{pro_nome7}/{pro_nome6}/{pro_nome5}/{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/cor-{proSku_cores}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/ProdutoGrupo.aspx")));


            //filtro Grupo de Tamanho
            routes.Add(new System.Web.Routing.Route("{gru_nomeAmigavel}/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/ProdutoGrupo.aspx")));
            routes.Add(new System.Web.Routing.Route("{pro_nome2}/{gru_nomeAmigavel}/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/ProdutoGrupo.aspx")));
            routes.Add(new System.Web.Routing.Route("{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/ProdutoGrupo.aspx")));
            routes.Add(new System.Web.Routing.Route("{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/ProdutoGrupo.aspx")));
            routes.Add(new System.Web.Routing.Route("{pro_nome5}/{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/ProdutoGrupo.aspx")));
            routes.Add(new System.Web.Routing.Route("{pro_nome6}/{pro_nome5}/{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/ProdutoGrupo.aspx")));
            routes.Add(new System.Web.Routing.Route("{pro_nome7}/{pro_nome6}/{pro_nome5}/{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/ProdutoGrupo.aspx")));

            //filtro Grupo(Departamento)
            routes.Add(new System.Web.Routing.Route("{pro_nome2}/{gru_nomeAmigavel}", new UrlReWriteRouteHandlerProduto("~/ProdutoGrupo.aspx")));
            routes.Add(new System.Web.Routing.Route("{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}", new UrlReWriteRouteHandlerProduto("~/ProdutoGrupo.aspx")));
            routes.Add(new System.Web.Routing.Route("{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}", new UrlReWriteRouteHandlerProduto("~/ProdutoGrupo.aspx")));
            routes.Add(new System.Web.Routing.Route("{pro_nome5}/{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}", new UrlReWriteRouteHandlerProduto("~/ProdutoGrupo.aspx")));
            routes.Add(new System.Web.Routing.Route("{pro_nome6}/{pro_nome5}/{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}", new UrlReWriteRouteHandlerProduto("~/ProdutoGrupo.aspx")));
            routes.Add(new System.Web.Routing.Route("{pro_nome7}/{pro_nome6}/{pro_nome5}/{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}", new UrlReWriteRouteHandlerProduto("~/ProdutoGrupo.aspx")));

        }



        protected void Session_Start(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies.Get("rixP");
            if ((cookie != null)) // cookie always == null!?
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                if (!ticket.Expired)
                {
                    // do some stuff here
                }
            }
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

        /* void Application_OnPostAuthenticateRequest(object sender, EventArgs e)
         {
             // Get a reference to the current User

             System.Security.Principal.IPrincipal usr = HttpContext.Current.User;

             // If we are dealing with an authenticated forms authentication request

             if (usr.Identity.IsAuthenticated && usr.Identity.AuthenticationType == "Forms")
             {

                 FormsIdentity fIdent = usr.Identity as FormsIdentity;

                 // Create a CustomIdentity based on the FormsAuthenticationTicket  

                 CustomIdentity ci = new CustomIdentity(fIdent.Ticket);



                 // Create the CustomPrincipal



                 //CustomPrincipal p = new CustomPrincipal(ci.);

                 // Attach the CustomPrincipal to HttpContext.User and Thread.CurrentPrincipal

                // HttpContext.Current.User = p;

                // System.Threading.Thread.CurrentPrincipal = p;

             }
        
         }*/

     /*   private static bool inicializado = false;
        private Object thisLock = new Object();

        private void Inicializa()
        {
            if (inicializado) { return; }

            lock (thisLock)
            {
                if (inicializado) { return; }

                System.Web.UI.ScriptManager.ScriptResourceMapping.AddDefinition("jquery",
                    new System.Web.UI.ScriptResourceDefinition
                    {
                        Path = "~/scripts/jquery-1.9.1.min.js",
                        DebugPath = "~/scripts/jquery-1.9.1.js",
                        CdnPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.9.1.min.js",
                        CdnDebugPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.9.1.js"
                    });

                inicializado = true;
            }
        }
    */}
}