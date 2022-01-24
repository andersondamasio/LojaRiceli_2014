using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _1_WebForm.Painel2
{
    public partial class CadastroProduto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RadTreeViewGrupo_NodeClick(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
        {
            RadListViewProdutosGrupo.DataBind();
        }
    }
}