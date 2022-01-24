using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _2_Library.Dao.Site.PedidoX;

namespace _1_WebForm
{
    public partial class PedidoConcluido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //para testes
           // Session["PedidoConcluidoPedId"] = "1194";
            
            SelectPedido();
            RemoverSessao();
        }

        private void SelectPedido() {

            int ped_id = Convert.ToInt32(Session["PedidoConcluidoPedId"]);

            FormViewPedidoConcluido.DataSource = new List<PedidoDto>(){ new PedidoTd().SelectPedido(null, ped_id)};
            FormViewPedidoConcluido.DataBind();        
        }

        private void RemoverSessao() {
            new _2_Library.Dao.Site.CarrinhoX.CarrinhoTd().RemoveCarrinho(null);
        }
    }
}