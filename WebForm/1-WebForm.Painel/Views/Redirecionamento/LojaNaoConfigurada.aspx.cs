using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loja.Views.Redirecionamento
{
    public partial class LojaNaoConfigurada : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["status"]))
                Loja.Utils.Validacao.Alert(Page, "Loja não Disponível");
            else
                Loja.Utils.Validacao.Alert(Page, "Loja não Configurada");
        }
    }
}