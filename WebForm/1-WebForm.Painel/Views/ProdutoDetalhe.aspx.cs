using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Loja.Modelo;
using Loja.Modelo.Carrinhox;
using Loja.Modelo.FormaPagamentox;
using Loja.Modelo.Parcelamentox;
using Loja.Modelo.ProdutoSkux;
using Loja.Modelo.Produtox;
using Loja.Utils;

namespace Loja.Views
{
    public partial class ProdutoDetalhe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int proSku_id = Convert.ToInt32(RouteData.Values["proSku_id"]);

            if (!IsPostBack)
            {
                SelecionarProdutoSku();
            }
        }

        private void SelecionarProdutoSku()
        {
            if (RouteData.Values["proSku_id"] != null && RouteData.Values["proSku_id"] is int)
            {
                Int32 proSku_id = Convert.ToInt32(RouteData.Values["proSku_id"]);

                dynamic produtoSku = new ProdutoSkuConsulta().SelecionarProdutoSku(proSku_id);
                if (produtoSku != null)
                {
                    VerificarUrl(produtoSku);
                    FormViewProdutoDetalhe.DataSource = new List<dynamic>() { produtoSku };
                    FormViewProdutoDetalhe.DataBind();
                }
                else
                {
                   Recursos.Redireciona404();
                }
            }
        }

        private void VerificarUrl(dynamic produtoSku)
        {
            if (RouteData.Values.Count == 2)
            {
                long tempnum = 0;
                if (long.TryParse(RouteData.Values["proSku_id"] + string.Empty, out tempnum))
                {
                    var nomeProdutoAmigavel = RouteData.Values["gru_nomeAmigavel"];
                    Int32 proSku_id = Convert.ToInt32(RouteData.Values["proSku_id"]);

                    if (proSku_id != 0)
                    {
                        string pro_nome = Utils.Tratamento.GerarNomeAmigavel(produtoSku.pro_nome + "-" + (produtoSku.proSkuCor_nome != null ? produtoSku.proSkuCor_nome + "-" : string.Empty) + (produtoSku.proSkuTam_nome != null ? produtoSku.proSkuTam_nome : string.Empty));

                        if (!nomeProdutoAmigavel.Equals(pro_nome + "-" + proSku_id))
                        {
                            string url = Page.GetRouteUrl("PaginaInicial", null) + pro_nome + "-" + proSku_id;

                            url = url + HttpContext.Current.Request.Url.Query;

                            Recursos.Redireciona301(url);
                        }
                    }
                }
                else RouteData.Values["pro_id"] = 0;
            }
        }

        protected void ButtonProdutoSkuComprar_Click(object sender, EventArgs e)
        {
            _2_Library.Modelo.Carrinho carrinho = new _2_Library.Modelo.Carrinho();
            carrinho.car_quantidade = 1;
            carrinho.proSku_id = Convert.ToInt32(((TextBox)FormViewProdutoDetalhe.FindControl("HiddenFieldProdutoSkuComprar")).Text);

            if (Session["cli_id"] != null)
                carrinho.cli_id = Convert.ToInt32(Session["cli_id"]);
            
           Retorno retorno = new CarrinhoConsulta().InserirItemCarrinho(carrinho);

           if (retorno.menSis_id == 0)
               Response.Redirect(Page.GetRouteUrl("Carrinho", null));
           else {
               Validacao.Alert(Page, retorno.menSis_mensagem);
           }
        }



        /*  public void SelecionarFormaPagamento()
          {
              var formaPagamento = new FormaPagamentoDao().SelecionarFormaPagamento();
              parcelamentoDropDownList.DataSource = formaPagamento;
              parcelamentoDropDownList.DataBind();
          }

          public void SelecionarParcelamento(Int32 forPag_id)
          {
              var selecionarParcelamentoFormaPagamento = new ParcelamentoDao().SelecionarParcelamento_FormaPagamento().Where(s => s.forPag_id == forPag_id);

              RepeaterParcelamentoFormaPagamento.DataSource = selecionarParcelamentoFormaPagamento.Select(s => new { s.forPag_id, s.Parcelamento, s.FormaPagamento, s.Parcelamento.Parcelamento_Parcela });
              RepeaterParcelamentoFormaPagamento.DataBind();
          }*/

    }
}