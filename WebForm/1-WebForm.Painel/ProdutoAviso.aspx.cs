using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loja.Painel
{
    public partial class ProdutoAviso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Login.ValidaLogin();
        }
    }
}