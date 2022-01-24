using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using _1_WebForm.App_Code.Utils;
using _2_Library.Dao.Site.CarrinhoX;
using _2_Library.Dao.Site.Clientex;
using _2_Library.Dao.Site.ConfiguracaoX;
using _2_Library.Dao.LojaX;
using _2_Library.Utils;


namespace _1_WebForm
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Url.Authority.StartsWith("localhost"))
            {
                typeof(FormsAuthentication).GetField("_RequireSSL", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
                    .SetValue(typeof(FormsAuthentication), false);
            }

            if (FormsAuthentication.RequireSSL && (HttpContext.Current.Request.Url.Port != 443))
                Response.Redirect(Request.Url.AbsoluteUri.Replace("http://", "https://"));

            if (!LoginComoCliente())
                TrataLogin();

        }

        protected void ButtonEntrar_Click(object sender, EventArgs e)
        {
            string cli_email = TextBoxCli_email.Text.Trim();
            string cli_senha = TextBoxCli_senha.Text.Trim();

            ClienteDto clienteDto = new ClienteTd().SelectClienteLogin(null, cli_email, cli_senha);

            if (clienteDto == null)
            {
                Validacao.Alert("Usuário ou senha inválido.");
                TextBoxCli_senha.Focus();
            }
            else
            {
                Session.Clear();
                Aut.Autenticar(new CliAut { CliId = clienteDto.cli_id, CliEmail = clienteDto.cli_email, CliNome = clienteDto.cli_nome, CliSpId = clienteDto.sp_id });
                string redirUrl = FormsAuthentication.GetRedirectUrl(clienteDto.cli_email, false);

                new CarrinhoTd().UpdateCarrinhoCli(null, null, clienteDto.cli_id);

                if (string.IsNullOrEmpty(Request["ReturnUrl"]))
                    Response.Redirect(FormsAuthentication.DefaultUrl);
                else Response.Redirect(Request["ReturnUrl"]);
            }
        }

        protected void ButtonCadastrar_Click(object sender, EventArgs e)
        {
            string cli_email = TextBoxCli_emailCadastro.Text.Trim();

            if (new ClienteTd().SelectClienteExiste(null, cli_email))
            {
                Session.Remove("cli_email");
                _2_Library.Utils.Validacao.Alert("Este email já está sendo usado, tente novamente.");
            }
            else
            {
                Session["cli_email"] = cli_email;
                Response.Redirect(Page.GetRouteUrl("CadastroPagamento", null));
            }
        }

        protected void ButtonRecuperarSenha_Click(object sender, EventArgs e)
        {
            RecuperarSenha();
        }

        private void RecuperarSenha()
        {
            string cli_email = TextBoxEmailRecuperarSenha.Text.Trim();
            ClienteDto clienteDto = new ClienteTd().SelectCliente(null, cli_email);

            if (clienteDto != null)
            {
                string cli_chave = Guid.NewGuid().ToString();
                string urlLoja = Request.Url.Authority + Page.GetRouteUrl("PaginaInicial", null);

                ConfiguracaoDto configuracaoDto = new ConfiguracaoTd().SelectConfiguracao(null);
                string con_emailRecuperarSenha = configuracaoDto.con_emailRecuperarSenha;
                con_emailRecuperarSenha = con_emailRecuperarSenha.Replace("[URLRECUPERARSENHA]", urlLoja + "RecuperarSenha?chave=" + cli_chave);
                con_emailRecuperarSenha = con_emailRecuperarSenha.Replace("[URLLOJA]", urlLoja);
                con_emailRecuperarSenha = con_emailRecuperarSenha.Replace("[NOMECLIENTE]", clienteDto.cli_nome);

                clienteDto.cli_chave = cli_chave;
                string loj_email = new LojaTd().SelectLoja(null).loj_email;

                System.Web.Caching.Cache cache = HttpContext.Current.Cache;
                cache.Insert(cli_chave, clienteDto, null, DateTime.Now.AddDays(2), new TimeSpan());

                new Smtp().EnviarMail(cli_email, loj_email, "Recuperar Senha", null, null, "Recuperar Senha", con_emailRecuperarSenha);

                _2_Library.Utils.Validacao.Alert("Foi enviado para você um email com os dados para alteração de sua senha.");

            }
            else
            {
                _2_Library.Utils.Validacao.Alert("Cliente não cadastrado.");
            }
        }

        private void TrataLogin()
        {
            string returnUrl = Request.QueryString["ReturnUrl"];

            if (!string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = returnUrl.ToLower();
                if (returnUrl == Page.GetRouteUrl("MeuCadastro", null) ||
                    returnUrl == Page.GetRouteUrl("MeusPedidos", null))
                    Response.Redirect(string.Format("LoginMinhaConta?ReturnUrl={0}", returnUrl));
            }

            _2_Library.Utils.Recursos.DesabilitarDuploClick(ButtonEntrar, "Processando...", ButtonEntrar.ValidationGroup);
            _2_Library.Utils.Recursos.DesabilitarDuploClick(ButtonCadastrar, "Processando...", ButtonCadastrar.ValidationGroup);
            _2_Library.Utils.Recursos.DesabilitarDuploClick(ButtonRecuperarSenha, "Processando...", ButtonRecuperarSenha.ValidationGroup);
        }

        private bool LoginComoCliente()
        {

            var cli_id = Request.Params["cli_id"];
            var cli_chave = Request.Params["cli_chave"];
            var usu_id = Request.Params["usu_id"];
            var usu_nome = Request.Params["usu_nome"];
            var chavePedidoComoCliente = Session["chavePedidoComoCliente"];

            if (cli_chave != null)
            {
                if (cli_id != null && cli_chave != null && usu_id != null && usu_nome != null && chavePedidoComoCliente != null)
                {
                    Validacao.Alert("Sessão inválida");
                }
                else
                {
                    ClienteDto clienteDto = new ClienteTd().SelectCliente(null, Convert.ToInt32(cli_id));

                    if (clienteDto != null)
                    {
                        Session.RemoveAll();
                        Session["usu_id"] = usu_id;
                        Session["usu_nome"] = usu_nome;

                        Aut.Autenticar(new CliAut { CliId = clienteDto.cli_id, CliEmail = clienteDto.cli_email, CliNome = clienteDto.cli_nome });
                        string redirUrl = FormsAuthentication.GetRedirectUrl(clienteDto.cli_email, false);

                        new _2_Library.Dao.Site.CarrinhoX.CarrinhoTd().UpdateCarrinhoCli(null, null, clienteDto.cli_id);

                        if (string.IsNullOrEmpty(Request["ReturnUrl"]))
                            Response.Redirect(FormsAuthentication.DefaultUrl);
                        else Response.Redirect(Request["ReturnUrl"]);
                    }
                    else
                    {
                        Validacao.Alert("Sessão inválida(não encontrado)");

                    }
                }
                return true;
            }
            else return false;
        }
    }
}