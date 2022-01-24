using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _1_WebForm.App_Code.Utils;
using _2_Library.Dao.Site.PedidoX;

namespace _1_WebForm
{
    public partial class MeusPedidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (_1_WebForm.App_Code.Utils.Recursos.VerificarAutenticacao(true))
                SelectPedido();
        }

        private void SelectPedido()
        {
            CustomPrincipal customPrincipal = Aut.AutenticacaoDados();
            int cli_id = customPrincipal.CliId;

            List<PedidoDto> pedidosDto = new PedidoTd().SelectPedido(null, cli_id, null);
            RepeaterPedidos.DataSource = pedidosDto;
            RepeaterPedidos.DataBind();
            PanelPedidosMensagem.Visible = RepeaterPedidos.Items.Count == 0;

            DesativaLinkButtonEfetuarPagamento(pedidosDto);
            
        }

        protected void LinkButtonEfetuarPagamento_Click(object sender, EventArgs e)
        {
            int ped_id = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            try
            {
                CustomPrincipal customPrincipal = Aut.AutenticacaoDados();
                Session["PedidoConcluidoPedId"] = ped_id;

                string urlPagSeguro = new _2_Library.Gateways.PagSeguro.Transacao().ProcessarPedido(ped_id);

                if (urlPagSeguro.StartsWith("http"))
                {
                    Response.Redirect(urlPagSeguro);
                }
                else
                {
                    _2_Library.Utils.Validacao.Alert(urlPagSeguro);
                    new PedidoTd().UpdatePedido(new PedidoDto() { ped_id = ped_id, ped_mensagemErro = urlPagSeguro });
                }
            }
            catch (Exception ex)
            {
                new PedidoTd().UpdatePedido(new PedidoDto() { ped_id = ped_id, ped_mensagemErro = ex.Message });
                _2_Library.Utils.Validacao.Alert("Houve um problema ao processar seu Pedido, por favor tente novamente." + ex.Message);
            }
        }

        /// <summary>
        ///desativa o link de efetuar o pagamento cujo o mesmo já efetuou ou já expirou
        /// </summary>
        /// <param name="pedidosDto"></param>
        private void DesativaLinkButtonEfetuarPagamento(List<PedidoDto> pedidosDto)
        {
            for (int i = 0; i < pedidosDto.Count; i++)
            {
                LinkButton linkButtonEfetuarPagamento = (LinkButton)(from System.Web.UI.Control control in RepeaterPedidos.Controls[i].FindControl("RepeaterPedidoStatus").Controls[0].Controls
                                                                     where control.ID == "LinkButtonEfetuarPagamento"
                                                                     select control).FirstOrDefault();

                PedidoDto pedidoDto = pedidosDto.Where(s => s.ped_id == Convert.ToInt32(linkButtonEfetuarPagamento.CommandArgument)).FirstOrDefault();
                linkButtonEfetuarPagamento.Visible = (pedidoDto.ped_forPag_prazoPagamento >= DateTime.Now.Date) && pedidoDto.ped_forPag_situacao != "Pago";
            }      
        }
    }
}