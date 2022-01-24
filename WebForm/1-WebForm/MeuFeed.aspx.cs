using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _2_Library.Dao.Site.PedidoFeedX;

namespace _1_WebForm
{
    public partial class MeuFeed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (_1_WebForm.App_Code.Utils.Recursos.VerificarAutenticacao(true))
            {
                SelectPedidoFeed();
            }


            //var xxx =  ((List<_2_Library.Dao.Site.ProdutoSkuX.ProdutoSkuFotoDto>)Eval("produtoSkuFotoDto")).Where(s=>s.  
        }

        private void SelectPedidoFeed()
        {
            List<PedidoFeedDto> pedidoFeedDto = new PedidoFeedTd().SelectPedidoFeed(null);
            RepeaterPedidoFeed.DataSource =  pedidoFeedDto;
            RepeaterPedidoFeed.DataBind();

        }

    }
}