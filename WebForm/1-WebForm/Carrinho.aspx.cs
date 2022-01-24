using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _2_Library;
using _2_Library.Dao.Site.CarrinhoX;
using _2_Library.Dao.Site.Produto_GrupoX;

namespace _1_WebForm
{
    public partial class Carrinho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CalculaCarrinho();

            _2_Library.Utils.Recursos.DesabilitarSemValidarDuploClick(LinkButtonPagamento, "Processando...");
            _2_Library.Utils.Recursos.DesabilitarSemValidarDuploClick(LinkButtonPagamento2, "Processando...");
        }

        protected void DropDownListCarrinhoQuantidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList dropDownListCarrinhoQuantidade = ((DropDownList)sender);
            RepeaterItem repeaterItemCarrinho = (RepeaterItem)dropDownListCarrinhoQuantidade.Parent;
            HiddenField hiddenFieldProSku_id = (HiddenField)repeaterItemCarrinho.FindControl("HiddenFieldProSku_id");

            CarrinhoDto carrinhoDto = new CarrinhoDto();
            carrinhoDto.car_quantidade = Convert.ToInt32(dropDownListCarrinhoQuantidade.SelectedValue);
            carrinhoDto.proSku_id = Convert.ToInt32(hiddenFieldProSku_id.Value);

         _1_WebForm.App_Code.Utils.CustomPrincipal customPrincipal = _1_WebForm.App_Code.Utils.Aut.AutenticacaoDados();

         if (customPrincipal != null && customPrincipal.CliId != 0)
             carrinhoDto.cli_id = customPrincipal.CliId;

            new CarrinhoTd().UpdateCarrinho(null, carrinhoDto);
            CalculaCarrinho();
        }

        protected void LinkButtonRemover_Click(object sender, EventArgs e)
        {
            LinkButton linkButtonRemover = ((LinkButton)sender);

            CarrinhoDto carrinhoDto = new CarrinhoDto();
            carrinhoDto.proSku_id = Convert.ToInt32(linkButtonRemover.CommandArgument);

            new CarrinhoTd().RemoveCarrinho(null, carrinhoDto);



            CalculaCarrinho();
        }

        private void CalculaCarrinho()
        {
            string ent_cep = TextBoxCep.Text.Trim();

            int ent_cepDest = 0;
            if (!string.IsNullOrEmpty(ent_cep) && _2_Library.Utils.Validacao.ValidaInteiro(ent_cep))
                ent_cepDest = Convert.ToInt32(ent_cep);

            List<CarrinhoDto> carrinho = new CarrinhoTd().SelectCarrinho(null, null, ent_cepDest);
            if (carrinho.Count > 0)
            {
                PanelCarrinhoVazio.Visible = false;
                PanelCarrinhoCheio.Visible = true;
                CarrinhoTotaisDto carrinhoTotaisDto = new CarrinhoTd().SelectCarrinhoTotais(carrinho, null,null,null);
                //ParcelamentoParcelaDto parcelamentoParcelaDto = carrinhoTotaisDto.parcelamentoDto.parcelamentoParcelaDto.OrderByDescending(s => s.parcPar_quantidade).FirstOrDefault();

                //string condicao = "em até " + parcelamentoParcelaDto.parcPar_quantidade + " x de " + parcelamentoParcelaDto.parcPar_valor.ToString("c");

                LiteralSubTotal.Text = carrinhoTotaisDto.cart_subTotal.ToString("c");
                PanelEntregaGratis.Visible = (carrinhoTotaisDto.cart_entregaTotal.HasValue && carrinhoTotaisDto.cart_entregaTotal.Value == 0);
                LiteralTotal.Text = carrinhoTotaisDto.cart_total.ToString("c");
                LiteralCondicao.Text = carrinhoTotaisDto.cart_condicao;
                RepeaterCarrinho.DataSource = carrinho;
                RepeaterCarrinho.DataBind();
            }
            else
            {
                PanelCarrinhoVazio.Visible = true;
                PanelCarrinhoCheio.Visible = false;
            }
        }

    }
}