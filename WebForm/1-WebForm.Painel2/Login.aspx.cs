using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using _2_Library.Dao.Painel.UsuarioX;
using _2_Library.Utils;
using Loja.Modelo.Usuariox;

namespace Loja.Painel
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Recursos.DesabilitarDuploClick(ButtonEntrar, "Entrando...");

            if (Request.Url.Authority.StartsWith("localhost"))
            {
                typeof(FormsAuthentication).GetField("_RequireSSL", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
                    .SetValue(typeof(FormsAuthentication), false);
            }

            if (FormsAuthentication.RequireSSL && (HttpContext.Current.Request.Url.Port != 443))
                Response.Redirect(Request.Url.AbsoluteUri.Replace("http://", "https://"));

        }

        protected void ButtonEntrar_Click(object sender, EventArgs e)
        {
            string usu_nome = usu_nomeTextBox.Text.Trim();
            string usu_senha = usu_senhaTextBox.Text.Trim();

            UsuarioDto usuarioDto = new UsuarioTd().SelectUsuario(null, usu_nome, usu_senha);

            if (usuarioDto == null)
            {
                ScriptManager.RegisterStartupScript(
                Page,
                Page.GetType(),
                Guid.NewGuid().ToString(),
                "window.alert('Usuário ou senha inválido.');",
                true
              );
                usu_senhaTextBox.Focus();
            }
            else
            {
                Utils.Aut.Autenticar(usuarioDto);
                string redirUrl = FormsAuthentication.GetRedirectUrl(usuarioDto.usu_nome, false);

                if (string.IsNullOrEmpty(Request["ReturnUrl"]))
                    Response.Redirect(FormsAuthentication.DefaultUrl);
                else Response.Redirect(Request["ReturnUrl"]);
            }
        }
        

        public static void ValidaLogin() {

            if (HttpContext.Current.Session["usuario"] == null) {
                HttpContext.Current.Response.Redirect("Login.aspx?ReturnUrl="+HttpContext.Current.Request.Path);
            }
        }

    }
}