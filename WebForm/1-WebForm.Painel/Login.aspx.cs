using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Loja.Modelo.Usuariox;

namespace Loja.Painel
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Loja.Utils.Recursos.DesabilitarDuploClick(ButtonEntrar, "Entrando...");
        }

        protected void ButtonEntrar_Click(object sender, EventArgs e)
        {
            string usu_nome = usu_nomeTextBox.Text.Trim();
            string usu_senha = usu_senhaTextBox.Text.Trim();
            SelecionarUsuario(usu_nome, usu_senha);
        }

        public void SelecionarUsuario(string usu_nome, string usu_senha)
        {
            _2_Library.Modelo.Usuario usuario = new UsuarioConsulta().SelecionarUsuario(usu_nome, usu_senha);

            if (usuario == null)
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
                Session["usuario"] = usuario;
                Session["loj_id"] = usuario.loj_id;

                System.Web.Caching.Cache cache = HttpContext.Current.Cache;
                _2_Library.Modelo.LojaCon lojaCon = Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio();
                    if (lojaCon.loj_id != usuario.loj_id)
                        cache.Remove("Loja" + Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio().loj_dominio);

                if (string.IsNullOrEmpty(HttpContext.Current.Request["ReturnUrl"]))
                    HttpContext.Current.Response.Redirect("Index.aspx");
                else HttpContext.Current.Response.Redirect(HttpContext.Current.Request["ReturnUrl"]);
            }
        }

        public static void ValidaLogin() {

            if (HttpContext.Current.Session["usuario"] == null) {
                HttpContext.Current.Response.Redirect("Login.aspx?ReturnUrl="+HttpContext.Current.Request.Path);
            }
        }

    }
}