using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _1_WebForm.UserControls
{
    public partial class MensagemBoasVindas : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            _1_WebForm.App_Code.Utils.CustomPrincipal customPrincipal = _1_WebForm.App_Code.Utils.Aut.AutenticacaoDados();
            string mensagem = null;

            if (customPrincipal != null)
            {
                mensagem = customPrincipal.CliNome;
                if (customPrincipal.CliSpId.HasValue)
                    LinkButtonLoginFacebook.Visible = false;
            }
            else
            {
                mensagem = "Visitante";
            }

            if (Session["usu_nome"] != null)
                mensagem = "Usuário:" + Session["usu_nome"] + " Comprando como cliente:" + mensagem;

            LabelMensagemBoasVindas.Text = mensagem;

            if (Session["socialPerfilDto"] != null && !HttpContext.Current.Request.IsAuthenticated)
            {
                LinkButtonLoginFacebook.Visible = false;
                LinkButtonSair.Visible = true;
                LabelMensagemBoasVindas.Text = ((_2_Library.Dao.Site.ClienteSocialX.SocialPerfilDto)Session["socialPerfilDto"]).sp_nome;
            }

            if (HttpContext.Current.Request.IsAuthenticated)
                LinkButtonSair.Visible = true;
        }

        protected void LinkButtonSair_Click(object sender, EventArgs e)
        {
            Session.Clear();
            _1_WebForm.App_Code.Utils.Aut.Logout();
        }
    }
}