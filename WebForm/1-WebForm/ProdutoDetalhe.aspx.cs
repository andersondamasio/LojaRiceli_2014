using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _2_Library.Dao.Site.CarrinhoX;
using _2_Library.Dao.Site.ProdutoSkuAvisoX;
using _2_Library.Dao.Site.ProdutoSkuX;
using _2_Library.Utils;

namespace _1_WebForm
{
    public partial class ProdutoDetalhe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AtualizaObjetos();
        }

        private void AtualizaObjetos()
        {
            if (!IsPostBack)
            {
                var route_proSku_id = RouteData.Values["proSku_id"];

                if (route_proSku_id != null && route_proSku_id is int)
                {
                    Int32 proSku_id = Convert.ToInt32(route_proSku_id);
                    ProdutoSkuDto produtoSkuDto = new ProdutoSkuTd().SelectByProdutoSkuProdutoDetalhe(null, proSku_id,null);

                    ValidarUrlProdutoSku(produtoSkuDto);

                    RepeaterProdutoSkuGrupoDetalhe.DataSource = new List<ProdutoSkuDto>() { produtoSkuDto };
                    RepeaterProdutoSkuGrupoDetalhe.DataBind();

                }
                else
                {
                    Recursos.Redireciona404();
                }
            }
        }

        protected void ButtonProdutoSkuComprar_Click(object sender, EventArgs e)
        {
            CarrinhoDto carrinhoDto = new CarrinhoDto();
            carrinhoDto.car_quantidade = 1;
            carrinhoDto.proSku_id = Convert.ToInt32(HiddenFieldProSku_id.Text);

            _1_WebForm.App_Code.Utils.CustomPrincipal customPrincipal = _1_WebForm.App_Code.Utils.Aut.AutenticacaoDados();

            if (customPrincipal != null && customPrincipal.CliId != 0)
                carrinhoDto.cli_id = customPrincipal.CliId;

            new CarrinhoTd().InsertCarrinho(null,carrinhoDto);
            Session["SessionID"] = HttpContext.Current.Session.SessionID;
            Response.Redirect(Page.GetRouteUrl("Carrinho", null));
        }
            
        protected void ButtonCadastrarAviso_Click(object sender, EventArgs e)
        {
            ProdutoSkuAvisoDto produtoSkuAvisoDto = new ProdutoSkuAvisoDto();
            produtoSkuAvisoDto.proSkuAvi_email = TextBoxProSkuAvi_email.Text.Trim();
            produtoSkuAvisoDto.proSkuAvi_nome = TextBoxProSkuAvi_nome.Text.Trim();
            produtoSkuAvisoDto.proSku_id = Convert.ToInt32(HiddenFieldProSku_id.Text);
            produtoSkuAvisoDto.cli_id = null;

            new ProdutoSkuAvisoTd().InsertProdutoSkuAviso(null, produtoSkuAvisoDto);
        }

        public void ValidarUrlProdutoSku(ProdutoSkuDto produtoSkuDto)
        {
            if (produtoSkuDto == null)
                Recursos.Redireciona404();

            string nomeAmigavelCorreto = string.Empty;
            string nomeAmigavelAtual = string.Empty;

            nomeAmigavelAtual = HttpContext.Current.Request.Url.AbsolutePath;
            nomeAmigavelCorreto = (Page.GetRouteUrl("PaginaInicial", null) + Tratamento.GerarNomeAmigavel(produtoSkuDto.proSku_nome + "-" + produtoSkuDto.proSkuCor_nome + "-" + produtoSkuDto.proSkuTam_nome) + "-" + produtoSkuDto.proSku_id).ToLower();


            if (!nomeAmigavelAtual.Equals(nomeAmigavelCorreto)) {

                Recursos.Redireciona301(nomeAmigavelCorreto);
            }
        }


      /*  protected override void Render(HtmlTextWriter output)
        {
            using (HtmlTextWriter htmlwriter = new HtmlTextWriter(new System.IO.StringWriter()))
            {
                base.Render(htmlwriter);
                string html = htmlwriter.InnerWriter.ToString();
                html = Regex.Replace(html, @"(?<=[^])\t{2,}|(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,11}(?=[<])|(?=[\n])\s{2,}", "");
                html = Regex.Replace(html, @"[ \f\r\t\v]?([\n\xFE\xFF/{}[\];,<>*%&|^!~?:=])[\f\r\t\v]?", "$1");
                html = html.Replace(";\n", ";");
                output.Write(html);
            }
        }*/

    }
}