using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Loja.Modelo;
using Loja.Modelo.Configuracaox;
using Loja.Utils;
using _2_Library.Modelo;

namespace Loja.Painel
{
    public partial class Pedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Login.ValidaLogin();
        }

        protected void ListViewPedidos_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            ListViewPedidoStatus.Visible = true;
        }

        protected void EntityDataSourcePedidoStatus_Inserting(object sender, EntityDataSourceChangingEventArgs e)
        {           
                ((PedidoStatus)e.Entity).ped_id = Convert.ToInt32(ListViewPedidos.SelectedValue);
                ((PedidoStatus)e.Entity).loj_id = Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio().loj_id;
                ((PedidoStatus)e.Entity).pedStat_dataHora = DateTime.Now;
        }

        protected void EntityDataSourcePedidoStatus_Inserted(object sender, EntityDataSourceChangedEventArgs e)
        {           
            PedidoStatus pedidoStatus = (PedidoStatus)e.Entity;
            new Loja.Modelo.Painel.Pedidox.PedidoConsulta().AtualizarPedidoStatus(pedidoStatus.ped_id, pedidoStatus.stat_id);
            CheckBox checkBoxEnviarEmail = (CheckBox)ListViewPedidoStatus.InsertItem.FindControl("CheckBoxEnviarEmail");
            if (checkBoxEnviarEmail.Checked)
            {
                EnviarEmailPedidoRecebido(pedidoStatus.ped_id);
            }
            
            ListViewPedidos.DataBind();
        }

        private void EnviarEmailPedidoRecebido(int pe_id)
        {
            _2_Library.Modelo.Pedido pedido = new Loja.Modelo.Pedidox.PedidoConsulta().SelecionarPedido(pe_id);
            string loj_email = Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio().loj_email;

            if (pedido.Status.stat_emailCorpo != null)
                new Smtp().SendEmailAsync(pedido.ped_cli_email, loj_email, "Pedido " + pedido.ped_id + " " + pedido.Status.stat_nome, (pedido.Status.stat_emailCopiaOculta != null ? pedido.Status.stat_emailCopiaOculta.Split(',') : null), null, "Pedido " + pedido.Status.stat_nome, TratarEmail(pedido, pedido.Status.stat_emailCorpo));
        }

        private string TratarEmail(_2_Library.Modelo.Pedido pedido, string mail_corpo)
        {
            string urlLoja = Request.Url.Authority+"/"; //+ Page.GetRouteUrl("PaginaInicial", null);
            string ped_entrega = @"<p>" + pedido.ped_cliEnd_nome + " " + pedido.ped_cliEnd_sobrenome + @"
                <br> 
                " + pedido.ped_cliEnd_endereco + " " + pedido.ped_cliEnd_numero + " " + pedido.ped_cli_complemento + @" 
                <br>
                " + pedido.ped_cli_bairro + " - " + pedido.ped_cli_cidade + " - " + pedido.ped_cli_estado + @"
                <br> 
                CEP " + pedido.ped_cli_cep + @"
                </p>";

            string ped_produtos = string.Empty;
            foreach (PedidoProduto produtoPedido in pedido.PedidoProduto)
            {

                ped_produtos +=
                @"Produto:  " + produtoPedido.pedPro_pro_nome + " " + produtoPedido.pedPro_proSkuCor_nome + " " + produtoPedido.pedPro_proSkuTam_nome + " - " + produtoPedido.pedPro_proSku_id + @" -<br>
                 Qtd:     " + produtoPedido.pedPro_car_quantidade + @"<br>
                 Preço unitário:    " + produtoPedido.pedPro_proSku_precoVenda.ToString("c") + @"<br>
                 SubTotal: " + produtoPedido.pedPro_proSku_precoVendaTotal.ToString("c") + @"<br><br>";
            }

            mail_corpo = mail_corpo.Replace("[PEDIDO_NUMERO]", pedido.ped_id.ToString());
            mail_corpo = mail_corpo.Replace("[PEDIDO_DATAHORA]", pedido.ped_dataHora.ToString());
            mail_corpo = mail_corpo.Replace("[PEDIDO_CONDICAO]", pedido.ped_forPagPar_condicao.ToString());
            mail_corpo = mail_corpo.Replace("[PEDIDO_ENDENTREGA]", ped_entrega);
            mail_corpo = mail_corpo.Replace("[PEDIDO_PRODUTOS]", ped_produtos);
            mail_corpo = mail_corpo.Replace("[PEDIDO_PRAZOENTREGA]", pedido.ped_ent_prazo.ToString());
            mail_corpo = mail_corpo.Replace("[PEDIDO_SUBTOTAL]", pedido.ped_subTotal.ToString("c"));
            mail_corpo = mail_corpo.Replace("[PEDIDO_ENTREGATOTAL]", pedido.ped_ent_valor.ToString("c"));
            mail_corpo = mail_corpo.Replace("[PEDIDO_DESCONTOSTOTAL]", pedido.ped_descontos.ToString("c"));
            mail_corpo = mail_corpo.Replace("[PEDIDO_PEDIDOTOTAL]", pedido.ped_total.ToString("c"));
            mail_corpo = mail_corpo.Replace("[URL]", urlLoja);
            mail_corpo = mail_corpo.Replace("[LOJA_ENDERECO]", string.Empty);
            mail_corpo = mail_corpo.Replace("[PEDIDO_STATUS]", pedido.PedidoStatus.OrderByDescending(s => s.pedStat_id).FirstOrDefault().Status.stat_nome);

            return mail_corpo;
        }

        protected void ListViewPedidos_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            ListViewPedidos.SelectedIndex = -1;
            ListViewPedidoStatus.Visible = false;
        }
    }
}