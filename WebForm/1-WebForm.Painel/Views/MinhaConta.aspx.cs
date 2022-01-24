using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loja.Views
{
    public partial class MinhaConta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                
                Response.Redirect(Page.GetRouteUrl("LoginCadastro", null) + "?ReturnUrl=" + Page.GetRouteUrl("MinhaConta", null));
            }
        }
    }
}