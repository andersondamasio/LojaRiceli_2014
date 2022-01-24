using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Loja.Modelo;
using Loja.Modelo.Clientex;
using Loja.Modelo.Configuracaox;
using Loja.Utils;

namespace Loja.Views
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Recursos.DesabilitarDuploClick(ButtonRecuperarSenha, "Enviando solicitação...", "groupRecuperarSenha");
            }
        }

        protected void ButtonEntrarFinalizar_Click(object sender, EventArgs e)
        {
            string cli_email = cli_emailTextBox.Text.Trim();
            string cli_senha = cli_senhaTextBox.Text.Trim();
            ClienteLogin(cli_email, cli_senha);
        }

        public Boolean ClienteLogin(string cli_email, string cli_senha)
        {
            ClienteLogin clienteLogin = new ClienteConsulta().SelecionarClienteLogin(cli_email, cli_senha);

            if (clienteLogin == null)
            {
                ScriptManager.RegisterStartupScript(
                this.Page,
                Page.GetType(),
                Guid.NewGuid().ToString(),
                "window.alert('Usuário ou senha inválido.');",
                true
              );
                if (cli_senhaTextBox != null)
                    cli_senhaTextBox.Focus();

                return false;
            }
            else
            {
                FormsAuthentication.SetAuthCookie(clienteLogin.cli_id.ToString(), false);
                Session["clienteLogin" + Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio().loj_id] = clienteLogin;
                Session["cli_id"] = clienteLogin.cli_id;
                new Loja.Modelo.Carrinhox.CarrinhoConsulta().AtualizarItensCarrinhoCliente(clienteLogin.cli_id);
                if (string.IsNullOrEmpty(HttpContext.Current.Request["ReturnUrl"]))
                    HttpContext.Current.Response.Redirect(Page.GetRouteUrl("MinhaConta", null));
                else HttpContext.Current.Response.Redirect(HttpContext.Current.Request["ReturnUrl"]);

                return true;
            }
        }

        protected void ButtonCadastrarFinalizar_Click(object sender, EventArgs e)
        {
            string cli_email = cli_emailNovoTextBox.Text.Trim();

            ClienteBean clienteBean = new ClienteConsulta().SelecionarCliente(cli_email);

            if (clienteBean == null)
            {
                Session["cli_emailNovo"] = cli_email;
                Session.Remove("clienteLogin");
                if (string.IsNullOrEmpty(Request["ReturnUrl"]))
                    Response.Redirect(FormsAuthentication.DefaultUrl);
                else Response.Redirect(Request["ReturnUrl"]);
            }
            else
            {
                Validacao.Alert(Page, "Já existe uma cadastro usando este email, tente recuperar sua senha caso tenha esquecido.");
            }
        }

        protected void ButtonRecuperarSenha_Click(object sender, EventArgs e)
        {
            RecuperarSenha();
        }

        private void RecuperarSenha()
        {
            string cli_email = TextBoxEmailRecuperarSenha.Text.Trim();
            ClienteRecuperarSenha clienteRecuperarSenha = new ClienteConsulta().SelecionarClienteRecuperarSenha(cli_email);

            if (clienteRecuperarSenha != null)
            {
                string cli_chave = Guid.NewGuid().ToString();
                string urlLoja = Request.Url.Authority + Page.GetRouteUrl("PaginaInicial", null);

                ConfiguracaoBean configuracaoBean = new ConfiguracaoConsulta().SelecionarConfiguracao();
                string con_emailRecuperarSenha = configuracaoBean.con_emailRecuperarSenha;
                con_emailRecuperarSenha = con_emailRecuperarSenha.Replace("[URLRECUPERARSENHA]", urlLoja + "RecuperarSenha?chave=" + cli_chave);
                con_emailRecuperarSenha = con_emailRecuperarSenha.Replace("[URLLOJA]", urlLoja);
                con_emailRecuperarSenha = con_emailRecuperarSenha.Replace("[NOMECLIENTE]", clienteRecuperarSenha.cli_nome);

                clienteRecuperarSenha.cli_chave = cli_chave;
                string loj_email = Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio().loj_email;

                System.Web.Caching.Cache cache = HttpContext.Current.Cache;
                cache.Insert(clienteRecuperarSenha.cli_chave, clienteRecuperarSenha, null, DateTime.Now.AddDays(2), new TimeSpan());

                new Smtp().EnviarMail(cli_email, loj_email, "Recuperar Senha", null, null, "Recuperar Senha", con_emailRecuperarSenha);

                Validacao.Alert(Page, "Foi enviado para você um email com os dados para alteração de sua senha.");

            }
            else
            {
                Validacao.Alert(Page, "Cliente não cadastrado.");
            }
        }
    }
}