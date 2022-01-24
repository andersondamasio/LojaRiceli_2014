using System;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using _1_WebForm.App_Code.Utils;
using _2_Library.Dao.Site.ProdutoSkuX;
using _2_Library.Modelo;
using _2_Library.Utils;

namespace _1_WebForm
{
    public partial class ProdutoBusca : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //LiteralMenu.Text = MontaMenuGrupo();
            AtualizaObjetos();

        }

        private void AtualizaObjetos()
        {
            string pro_nomeChave = Request.QueryString["q"];
            
            Validacao.SetScript("document.getElementById('Cabecalho_TextBoxBusca').value = '"+pro_nomeChave+"';");
            
            pro_nomeChave = pro_nomeChave != null ? pro_nomeChave.Trim() : null;

            if (!IsPostBack && !string.IsNullOrEmpty(pro_nomeChave))
            {
                RepeaterProdutoSkuCorLimpar.DataBind();
                RepeaterProdutoSkuTamanhoLimpar.DataBind();

                PanelFiltroLimpar.Visible = (RepeaterProdutoSkuCorLimpar.Items.Count > 0 || RepeaterProdutoSkuTamanhoLimpar.Items.Count > 0);
                PanelProdutoSkuCorLimparFiltros.Visible = (RepeaterProdutoSkuCorLimpar.Items.Count > 0);
                PanelProdutoSkuTamanhoLimparFiltros.Visible = (RepeaterProdutoSkuTamanhoLimpar.Items.Count > 0);
                RepeaterProdutoSkuCorLimpar.Visible = (RepeaterProdutoSkuCorLimpar.Items.Count > 0);
                RepeaterProdutoSkuTamanhoLimpar.Visible = (RepeaterProdutoSkuTamanhoLimpar.Items.Count > 0);

                RepeaterProdutoSkuCor.DataSource = new ProdutoSkuTd().SelectProdutoSkuBuscaCor(null, pro_nomeChave);
                RepeaterProdutoSkuTamanho.DataSource = new ProdutoSkuTd().SelectProdutoSkuBuscaTamanho(null, pro_nomeChave);
                RepeaterProdutoSkuCor.DataBind();
                RepeaterProdutoSkuTamanho.DataBind();
                PanelLabelCor.Visible = (RepeaterProdutoSkuCor.Items.Count > 0);
                PanelLabelTamanho.Visible = (RepeaterProdutoSkuTamanho.Items.Count > 0);
            }

          

        }
        
    /*    private string MontaMenuGrupo()
        {
            string gru_nomeAmigavel = RouteData.Values["gru_nomeAmigavel"].ToString();

            IQueryable<Grupo> grupo = new _2_Library.Dao.Site.GrupoX.GrupoTd().SelectGrupoInicial(null);
            int? gru_pai = grupo.Where(s2 => s2.gru_nomeAmigavel == gru_nomeAmigavel).FirstOrDefault().gru_pai;
           
            IQueryable<Grupo> gruposPai = grupo.Where(s => s.gru_id == gru_pai);

            if (!gruposPai.FirstOrDefault().gru_pai.HasValue)
            {
                gruposPai = gruposPai.FirstOrDefault().Grupo1.AsQueryable();
            }
            string menu = @"<ul>";


            foreach (var gruPais in gruposPai.OrderBy(s => s.gru_posicao).OrderBy(s2 => s2.gru_nome))
            {
                menu += "<li>" +
                            "<li><a href=\"" + Page.GetRouteUrl("PaginaInicial", null) + gruPais.gru_nomeAmigavel + "\"><h1>" + gruPais.gru_nome + "</h1></a></li>" +
                        "</li>";

                foreach (var gruPais2 in gruPais.Grupo1.OrderBy(s => s.gru_posicao).OrderBy(s2=>s2.gru_nome))
                {
                    string estilo = gruPais2.gru_nomeAmigavel == gru_nomeAmigavel ? "style=\"font-weight:bold\"" : string.Empty;
                    menu += "<li>" +
                             "<ul>" +
                                "<li><a href=\"" + Page.GetRouteUrl("PaginaInicial", null) + gruPais2.Grupo2.gru_nomeAmigavel + "/" + gruPais2.gru_nomeAmigavel + "\"" + estilo + ">" + gruPais2.gru_nome + "</a></li>" +
                             "</ul>" +
                           "</li>";
                }
            }
            return menu;
        }
        */
        protected void LinkButtonOrderMenorPreco_Click(object sender, EventArgs e)
        {
            string url = Request.Url.AbsolutePath;

            if (Request.QueryString.Get("ordenar") != null)
            {
                PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                isreadonly.SetValue(this.Request.QueryString, false, null);
                Request.QueryString.Remove("ordenar");
                url = Request.Url.AbsolutePath + "?" + Request.QueryString;
            }
            url = Request.Url.AbsolutePath + "?" + Request.QueryString;
            if (Request.QueryString.Count > 0)
                url = (url + "&ordenar=menorpreco");
            else url = (url + "?ordenar=menorpreco");
            Response.Redirect(url.Replace("??", "?"));
        }

        protected void LinkButtonOrderMaiorPreco_Click(object sender, EventArgs e)
        {
            string url = Request.Url.AbsolutePath;

            if (Request.QueryString.Get("ordenar") != null)
            {
                PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                isreadonly.SetValue(this.Request.QueryString, false, null);
                Request.QueryString.Remove("ordenar");
                url = Request.Url.AbsolutePath + "?" + Request.QueryString;
            }

            url = Request.Url.AbsolutePath + "?" + Request.QueryString;
            if (Request.QueryString.Count > 0)
                url = (url + "&ordenar=maiorpreco");
            else url = (url + "?ordenar=maiorpreco");

            Response.Redirect(url.Replace("??", "?"));
        }

        protected void LinkButtonOrderMaiorDesconto_Click(object sender, EventArgs e)
        {
            string url = Request.Url.AbsolutePath;

            if (Request.QueryString.Get("ordenar") != null)
            {
                PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                isreadonly.SetValue(this.Request.QueryString, false, null);
                Request.QueryString.Remove("ordenar");
                url = Request.Url.AbsolutePath + "?" + Request.QueryString;
            }

            url = Request.Url.AbsolutePath + "?" + Request.QueryString;
            if (Request.QueryString.Count > 0)
                url = (url + "&ordenar=maiordesconto");
            else url = (url + "?ordenar=maiordesconto");

            Response.Redirect(url.Replace("??", "?"));
        }

        protected void ObjectDataSourceProduto_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["orderBy"] = _2_Library.Utils.Recursos.SelecionarCampoOrdenacao(LinkButtonOrderMenorPreco, LinkButtonOrderMaiorPreco, LinkButtonOrderMaiorDesconto);
        }
    }
}