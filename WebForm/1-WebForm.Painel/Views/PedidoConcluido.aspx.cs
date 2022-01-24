using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Loja.Modelo.Configuracaox;
using Loja.Modelo.Pedidox;
using Loja.Utils;

namespace Loja.Views
{
    public partial class PedidoConcluido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                Response.Redirect(Page.GetRouteUrl("Carrinho", null));
            }
            else
                if (Session["ped_forPag_nome"].ToString() == "Boleto")
                {
                    PanelPagamentoBoleto.Visible = true;
                }

            new Loja.Modelo.Carrinhox.CarrinhoConsulta().ExcluirItensCarrinho();
        }

        protected void EntityDataSourcePedidoConcluido_Selected(object sender, EntityDataSourceSelectedEventArgs e)
        {
           /* Session.Remove("ped_id");
            Session.Remove("ped_forPag_nome");*/
        }

    }
}