using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using System.IO;
using Loja.Utils;
using System.Web.Http;
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

           /* routes.MapPageRoute("PaginaInicial", "", "~/Views/Index.aspx");
            routes.MapPageRoute("Carrinho", "Carrinho", "~/Views/Carrinho.aspx");
            routes.MapPageRoute("Login", "Login", "~/Views/Login.aspx");
            routes.MapPageRoute("LoginCadastro", "LoginCadastro", "~/Views/LoginCadastro.aspx");
            routes.MapPageRoute("RecuperarSenha", "RecuperarSenha", "~/Views/RecuperarSenha.aspx");
            routes.MapPageRoute("MinhaConta", "MinhaConta", "~/Views/MinhaConta.aspx");
            routes.MapPageRoute("MeuCadastro", "MeuCadastro", "~/Views/MeuCadastro.aspx");
            routes.MapPageRoute("CadastroFinalizar", "CadastroFinalizar", "~/Views/CadastroFinalizar.aspx");
            routes.MapPageRoute("PedidoConcluido", "PedidoConcluido", "~/Views/PedidoConcluido.aspx");
            routes.MapPageRoute("GerarBoleto", "Pagamento/GerarBoleto", "~/Views/Pagamento/GerarBoleto.aspx");
            routes.MapPageRoute("LojaNaoConfigurada", "Redirecionamento/LojaNaoConfigurada", "~/Views/Redirecionamento/LojaNaoConfigurada.aspx");
             
            routes.Add(
                new System.Web.Routing.Route("{gru_nomeAmigavel}", new RouteValueDictionary(new
                {
                    proSku_id = (string)null
                }), new UrlReWriteRouteHandlerProduto("~/Views/ProdutoDetalhe.aspx")));
            */

             //filtro Cor e Tamanho
           /*  routes.Add(new System.Web.Routing.Route("{gru_nomeAmigavel}/cor-{proSku_cores}/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/Views/ProdutoGrupo.aspx")));
             routes.Add(new System.Web.Routing.Route("{pro_nome2}/{gru_nomeAmigavel}/cor-{proSku_cores}/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/Views/ProdutoGrupo.aspx")));
             routes.Add(new System.Web.Routing.Route("{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/cor-{proSku_cores}/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/Views/ProdutoGrupo.aspx")));
             routes.Add(new System.Web.Routing.Route("{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/cor-{proSku_cores}/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/Views/ProdutoGrupo.aspx")));
             routes.Add(new System.Web.Routing.Route("{pro_nome5}/{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/cor-{proSku_cores}/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/Views/ProdutoGrupo.aspx")));
             routes.Add(new System.Web.Routing.Route("{pro_nome6}/{pro_nome5}/{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/cor-{proSku_cores}/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/Views/ProdutoGrupo.aspx")));
             routes.Add(new System.Web.Routing.Route("{pro_nome7}/{pro_nome6}/{pro_nome5}/{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/cor-{proSku_cores}/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/Views/ProdutoGrupo.aspx")));
            */

             //filtro Cor
            /* routes.Add(new System.Web.Routing.Route("{gru_nomeAmigavel}/cor-{proSku_cores}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/Views/ProdutoGrupo.aspx")));
             routes.Add(new System.Web.Routing.Route("{pro_nome2}/{gru_nomeAmigavel}/cor-{proSku_cores}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/Views/ProdutoGrupo.aspx")));
             routes.Add(new System.Web.Routing.Route("{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/cor-{proSku_cores}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/Views/ProdutoGrupo.aspx")));
             routes.Add(new System.Web.Routing.Route("{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/cor-{proSku_cores}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/Views/ProdutoGrupo.aspx")));
             routes.Add(new System.Web.Routing.Route("{pro_nome5}/{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/cor-{proSku_cores}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/Views/ProdutoGrupo.aspx")));
             routes.Add(new System.Web.Routing.Route("{pro_nome6}/{pro_nome5}/{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/cor-{proSku_cores}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/Views/ProdutoGrupo.aspx")));
             routes.Add(new System.Web.Routing.Route("{pro_nome7}/{pro_nome6}/{pro_nome5}/{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/cor-{proSku_cores}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/Views/ProdutoGrupo.aspx")));
            */

             //filtro Tamanho
             /*routes.Add(new System.Web.Routing.Route("{gru_nomeAmigavel}/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/Views/ProdutoGrupo.aspx")));
             routes.Add(new System.Web.Routing.Route("{pro_nome2}/{gru_nomeAmigavel}/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/Views/ProdutoGrupo.aspx")));
             routes.Add(new System.Web.Routing.Route("{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/Views/ProdutoGrupo.aspx")));
             routes.Add(new System.Web.Routing.Route("{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/Views/ProdutoGrupo.aspx")));
             routes.Add(new System.Web.Routing.Route("{pro_nome5}/{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/Views/ProdutoGrupo.aspx")));
             routes.Add(new System.Web.Routing.Route("{pro_nome6}/{pro_nome5}/{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/Views/ProdutoGrupo.aspx")));
             routes.Add(new System.Web.Routing.Route("{pro_nome7}/{pro_nome6}/{pro_nome5}/{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}/tamanho-{proSku_tamanhos}", new UrlReWriteRouteHandlerProdutoCorTamanho("~/Views/ProdutoGrupo.aspx")));
            */
             //filtro Grupo(Departamento)
      
           /*  routes.Add(new System.Web.Routing.Route("{pro_nome2}/{gru_nomeAmigavel}", new UrlReWriteRouteHandlerProduto("~/Views/ProdutoGrupo.aspx")));
             routes.Add(new System.Web.Routing.Route("{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}", new UrlReWriteRouteHandlerProduto("~/Views/ProdutoGrupo.aspx")));
             routes.Add(new System.Web.Routing.Route("{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}", new UrlReWriteRouteHandlerProduto("~/Views/ProdutoGrupo.aspx")));
             routes.Add(new System.Web.Routing.Route("{pro_nome5}/{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}", new UrlReWriteRouteHandlerProduto("~/Views/ProdutoGrupo.aspx")));
             routes.Add(new System.Web.Routing.Route("{pro_nome6}/{pro_nome5}/{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}", new UrlReWriteRouteHandlerProduto("~/Views/ProdutoGrupo.aspx")));
             routes.Add(new System.Web.Routing.Route("{pro_nome7}/{pro_nome6}/{pro_nome5}/{pro_nome4}/{pro_nome3}/{pro_nome2}/{gru_nomeAmigavel}", new UrlReWriteRouteHandlerProduto("~/Views/ProdutoGrupo.aspx")));
       */
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