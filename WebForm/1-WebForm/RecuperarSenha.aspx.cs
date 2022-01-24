using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _1_WebForm.App_Code.Utils;
using _2_Library.Dao.Site.Clientex;

namespace _1_WebForm
{
    public partial class RecuperarSenha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClienteDto clienteDto = SelecionarClienteDto();
                if (clienteDto == null)
                {
                    _2_Library.Utils.Validacao.Alert("Seu link expirou, por favor tente recuperar sua senha novamente.");
                    _2_Library.Utils.Validacao.Redirecionar(Page.GetRouteUrl("login", null) + "?ReturnUrl=" + Page.GetRouteUrl("PaginaInicial", null) + "MeuCadastro");
                }
            }
        }

        protected void ButtonAlterar_Click(object sender, EventArgs e)
        {
            ClienteDto clienteDto = SelecionarClienteDto();
            if (clienteDto != null)
            {
                string cli_senha = cli_senhaTextBox.Text.Trim();

                if (!string.IsNullOrEmpty(cli_senha))
                {
                    if (cli_senha.Length > 4 && cli_senha.Length < 32)
                    {
                        new ClienteTd().UpdateClienteSenha(null, clienteDto.cli_email, cli_senha);
                        _2_Library.Utils.Validacao.Alert("Senha atualizada com sucesso.");
                        _2_Library.Utils.Validacao.Redirecionar( Page.GetRouteUrl("login", null) + "?ReturnUrl=" + Page.GetRouteUrl("PaginaInicial", null) + "MeuCadastro");
                    }
                    else
                        _2_Library.Utils.Validacao.Alert("A senha deve ter mais de 4 caracteres.");
                }
                else
                    _2_Library.Utils.Validacao.Alert("Preencha com a senha");
            }
            else
            {
                _2_Library.Utils.Validacao.Alert("Seu link expirou, por favor tente recuperar sua senha novamente.");
                _2_Library.Utils.Validacao.Redirecionar( Page.GetRouteUrl("login", null) + "?ReturnUrl=" + Page.GetRouteUrl("PaginaInicial", null) + "MinhaConta");
            }
        }

        private ClienteDto SelecionarClienteDto()
        {
            System.Web.Caching.Cache cache = HttpContext.Current.Cache;
            string cli_chave = Request.QueryString["chave"];

            if (string.IsNullOrEmpty(cli_chave))
                return null;

            return (ClienteDto)cache.Get(cli_chave);
        }
    }
}