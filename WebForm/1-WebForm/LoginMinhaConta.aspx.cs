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
using _2_Library.Dao.Site.ClienteSocialX;
using _2_Library.Dao.Site.SocialPerfilX;


namespace _1_WebForm
{
    public partial class LoginMinhaConta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InicializaLogin();
        }

        protected void ButtonEntrar_Click(object sender, EventArgs e)
        {
            string cli_email = TextBoxCli_email.Text.Trim();
            string cli_senha = TextBoxCli_senha.Text.Trim();

            ClienteDto clienteDto = new ClienteTd().SelectClienteLogin(null, cli_email, cli_senha);

            if (clienteDto == null)
            {
                _2_Library.Utils.Validacao.Alert("Usuário ou senha inválido.");

                TextBoxCli_senha.Focus();
            }
            else
            {
                //associa a conta ao facebook
                int sp_id = AssociarConta(clienteDto.cli_id);
                if (sp_id != 0)
                    clienteDto.sp_id = sp_id;

                Aut.Autenticar(new CliAut { CliId = clienteDto.cli_id, CliEmail = clienteDto.cli_email, CliNome = clienteDto.cli_nome,CliSpId = clienteDto.sp_id });
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
                Response.Redirect(Page.GetRouteUrl("MeuCadastro", null));
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

        private void InicializaLogin()
        {
            string conectarRede = Request.QueryString["conectarRede"];
            SocialPerfilDto socialPerfilDto = null;

            if (Session["socialPerfilDto"] != null)
            {
                socialPerfilDto = (SocialPerfilDto)Session["socialPerfilDto"];

                if (!string.IsNullOrEmpty(conectarRede))
                {
                    if (conectarRede == "1")
                    {
                        PanelTextoContaAssociar.Visible = true;
                        PanelTextoContaLogin.Visible = false;
                        TextBoxCli_email.Text = socialPerfilDto.sp_email;

                    }
                    else
                        if (conectarRede == "0")
                        {
                            TextBoxCli_emailCadastro.Text = socialPerfilDto.sp_email;
                        }
                }
            }

            _2_Library.Utils.Recursos.DesabilitarDuploClick(ButtonEntrar, "Processando...", ButtonEntrar.ValidationGroup);
            _2_Library.Utils.Recursos.DesabilitarDuploClick(ButtonCadastrar, "Processando...", ButtonCadastrar.ValidationGroup);
            _2_Library.Utils.Recursos.DesabilitarDuploClick(ButtonRecuperarSenha, "Processando...", ButtonRecuperarSenha.ValidationGroup);

        }

        private int AssociarConta(int? cli_id) {

            string conectarRede = Request.QueryString["conectarRede"];
            SocialPerfilDto socialPerfilDto = null;

            if (Session["socialPerfilDto"] != null)
            {
                socialPerfilDto = (SocialPerfilDto)Session["socialPerfilDto"];

                if (!string.IsNullOrEmpty(conectarRede))
                {
                    if (conectarRede == "1")
                    {
                        if (!socialPerfilDto.cli_id.HasValue)
                        {
                            socialPerfilDto.cli_id = cli_id;
                            int sp_id = new SocialPerfilTd().UpdateSocialPerfil(null, socialPerfilDto);
                            Validacao.Alert("Conta associada com sucesso.");
                            return sp_id;
                        }
                        else {

                            Validacao.Alert("Conta já associada.");
                        }
                    }
                    else
                        if (conectarRede == "0")
                        {

                        }
                }
            }
            return 0;
        }
    }
}