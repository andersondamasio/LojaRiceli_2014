using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _1_WebForm.UserControls
{
    public partial class SocialLinksCompartilhar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AtribuirLink();
        }

        private void AtribuirLink()
        {
           string linkCompartilhar = Request.Url.AbsoluteUri;

            _1_WebForm.App_Code.Utils.CustomPrincipal customPrincipal = _1_WebForm.App_Code.Utils.Aut.AutenticacaoDados();
            if (customPrincipal != null)
            {
                linkCompartilhar += "?af=" + customPrincipal.CliId;
            }

            LiteralLinkCompatilharFb.Text = linkCompartilhar;
            LiteralLinkCompatilharGp.Text = linkCompartilhar;
            LiteralLinkCompatilharTw.Text = linkCompartilhar;
            LiteralLinkCompatilharTw2.Text = linkCompartilhar;
        }
    }
}