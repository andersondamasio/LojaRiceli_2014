using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Loja.Modelo.Clientex;
using Loja.Utils;

namespace Loja.Views
{
    public partial class RecuperarSenha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClienteRecuperarSenha clienteRecuperarSenha = SelecionarClienteRecuperarSenha();
                if (clienteRecuperarSenha == null)
                {
                    Validacao.Alert(Page, "Seu link expirou, por favor tente recuperar sua senha novamente.");
                    Validacao.Redirecionar(Page, Page.GetRouteUrl("login", null) + "?ReturnUrl=" + Page.GetRouteUrl("PaginaInicial", null) + "MinhaConta");
                }
            }
        }

        protected void ButtonAlterar_Click(object sender, EventArgs e)
        {
            ClienteRecuperarSenha clienteRecuperarSenha = SelecionarClienteRecuperarSenha();

            if (clienteRecuperarSenha != null)
            {
                string cli_senha = TextBoxSenha.Text.Trim();


                if (!string.IsNullOrEmpty(cli_senha))
                {
                    if (cli_senha.Length > 4 && cli_senha.Length < 32)
                    {
                        new ClienteConsulta().AtualizarSenha(clienteRecuperarSenha.cli_email, cli_senha);
                        new Login().ClienteLogin(clienteRecuperarSenha.cli_email, cli_senha);
                    }
                    else
                        Validacao.Alert(Page, "A senha deve ter mais de 4 caracteres.");
                }
                else
                    Validacao.Alert(Page, "Preencha com a senha");
            }
            else {
                Validacao.Alert(Page, "Seu link expirou, por favor tente recuperar sua senha novamente.");
                Validacao.Redirecionar(Page, Page.GetRouteUrl("login", null) + "?ReturnUrl=" + Page.GetRouteUrl("PaginaInicial", null) + "MinhaConta");
            }
        }


        private ClienteRecuperarSenha SelecionarClienteRecuperarSenha()
        {

            System.Web.Caching.Cache cache = HttpContext.Current.Cache;
            string cli_chave = Request.QueryString["chave"];
            return (ClienteRecuperarSenha)cache.Get(cli_chave);
        
        }
    }
}