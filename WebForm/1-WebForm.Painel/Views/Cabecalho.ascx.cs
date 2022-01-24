using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loja.Views
{
    public partial class Cabecalho : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated && (Session["clienteLogin"] == null || Session["cli_id"] == null))
            {
               Loja.Modelo.Clientex.ClienteLogin clienteLogin = new Loja.Modelo.Clientex.ClienteConsulta().SelecionarClienteLogin(Convert.ToInt32(HttpContext.Current.User.Identity.Name));
               Session["clienteLogin" + Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio().loj_id] = clienteLogin;
               Session["cli_id"] = clienteLogin.cli_id;
            }
        }
    }
}