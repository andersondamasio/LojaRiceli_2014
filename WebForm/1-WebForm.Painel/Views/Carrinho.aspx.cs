using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Loja.Modelo.Carrinhox;
using Loja.Modelo.Parcelamentox;
using Loja.Servicos;
using Loja.Utils;

namespace Loja.Views
{
    public partial class Carrinho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SelecionarItensCarrinho();
        }

        protected void LinkButtonProdutoSkuRemover_Click(object sender, EventArgs e)
        {
            LinkButton linkButtonProdutoSkuRemover = (LinkButton)sender;
            new CarrinhoConsulta().ExcluirItemCarrinho(Convert.ToInt32(linkButtonProdutoSkuRemover.CommandArgument));
            SelecionarItensCarrinho();
        }

        protected void DropDownListCarrinhoQuantidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList dropDownListCarrinhoQuantidade = ((DropDownList)sender);
            RepeaterItem repeaterItemCarrinho = (RepeaterItem)dropDownListCarrinhoQuantidade.Parent;
            HiddenField hiddenFieldProSku_id = (HiddenField)repeaterItemCarrinho.FindControl("HiddenFieldProSku_id");

            new CarrinhoConsulta().AtualizarItemCarrinho(Convert.ToInt32(hiddenFieldProSku_id.Value), Convert.ToInt32(dropDownListCarrinhoQuantidade.SelectedValue));
            SelecionarItensCarrinho();
        }

        protected void SelecionarItensCarrinho()
        {
            var carrinho = new CarrinhoConsulta().SelecionarItensCarrinho(0,0);

            RepeaterCarrinho.DataSource = carrinho;
            RepeaterCarrinho.DataBind();
            CalcularCarrinho(carrinho);
        }

        private void CalcularCarrinho(List<dynamic> carrinho)
        {
            if (RepeaterCarrinho.Items.Count > 0)
            {
                /*Literal car_subTotalLiteral = (Literal)(from System.Web.UI.WebControls.RepeaterItem repeaterItem in RepeaterCarrinho.Controls
                                                        where repeaterItem.ItemType == ListItemType.Footer
                                                        select repeaterItem).FirstOrDefault().FindControl("car_subTotalLiteral");

                Literal car_totalLiteral = (Literal)(from System.Web.UI.WebControls.RepeaterItem repeaterItem in RepeaterCarrinho.Controls
                                                     where repeaterItem.ItemType == ListItemType.Footer
                                                     select repeaterItem).FirstOrDefault().FindControl("car_totalLiteral");

                Literal car_parcelamentoLiteral = (Literal)(from System.Web.UI.WebControls.RepeaterItem repeaterItem in RepeaterCarrinho.Controls
                                                            where repeaterItem.ItemType == ListItemType.Footer
                                                            select repeaterItem).FirstOrDefault().FindControl("car_parcelamentoLiteral");
                */
                decimal proSku_precoVenda = carrinho.Sum(s => (decimal)s.proSku_precoVenda);

                car_subTotalLiteral.Text = proSku_precoVenda.ToString("c");
                car_totalLiteral.Text = proSku_precoVenda.ToString("c");

                //pega o parcelamento mais abrangente estre os skus para escolhar de qual usar 
                 var carrinhoProdutoSku  = new ParcelamentoOperacao().SelecionarParcelamentoAbrangente(carrinho);
                
                if (carrinhoProdutoSku.Parcelamento.parc_bloquear)
                    car_parcelamentoLiteral.Text = string.Empty;
                else
                {
                    car_parcelamentoLiteral.Text = carrinhoProdutoSku.Parcelamento.parc_quantidade + " x de " + Recursos.ValorComFormatacao(proSku_precoVenda / carrinhoProdutoSku.Parcelamento.parc_quantidade, 2);
                }
            }
            PanelCarrinhoCheio.Visible = RepeaterCarrinho.Items.Count > 0;
            PanelCarrinhoVazio.Visible = RepeaterCarrinho.Items.Count == 0;
        }

        protected void ButtonFinalizarPedido_Click(object sender, EventArgs e)
        {

            if (Request.IsAuthenticated)
            {
                Response.Redirect(Page.GetRouteUrl("CadastroFinalizar", null));
            }
            else {
                Response.Redirect(Page.GetRouteUrl("Login", null)+"?ReturnUrl="+Page.GetRouteUrl("CadastroFinalizar",null));
            }
        }
    }
}