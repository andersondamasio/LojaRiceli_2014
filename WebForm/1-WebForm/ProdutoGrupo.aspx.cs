using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _2_Library.Dao.Site.GrupoX;
using _2_Library.Dao.Site.ProdutoSkuX;
using _2_Library.Modelo;
using _2_Library.Utils;

namespace _1_WebForm
{
    public partial class ProdutoGrupo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LiteralMenu.Text = MontaMenuGrupo();
            AtualizaObjetos();
        }

        private void AtualizaObjetos()
        {
            if (!IsPostBack)
            {
                string gru_nomeAmigavel = string.Join("/", RouteData.Values.Where(s => s.Key.StartsWith("pro_nome") || s.Key.StartsWith("gru_nomeAmigavel")).Select((pair) => string.Format("{0}", pair.Value)));

                List<GrupoDto> grupoCaminho = new GrupoTd().SelectGrupoCaminho(null, gru_nomeAmigavel);
                ValidarUrlProdutoGrupo(grupoCaminho);

                RepeaterGrupoCaminho.DataSource = grupoCaminho;
                RepeaterGrupoCaminho.DataBind();

                RepeaterProdutoSkuCorLimpar.DataBind();
                RepeaterProdutoSkuTamanhoLimpar.DataBind();

                PanelFiltroLimpar.Visible = (RepeaterProdutoSkuCorLimpar.Items.Count > 0 || RepeaterProdutoSkuTamanhoLimpar.Items.Count > 0);
                PanelProdutoSkuCorLimparFiltros.Visible = (RepeaterProdutoSkuCorLimpar.Items.Count > 0);
                PanelProdutoSkuTamanhoLimparFiltros.Visible = (RepeaterProdutoSkuTamanhoLimpar.Items.Count > 0);
                RepeaterProdutoSkuCorLimpar.Visible = (RepeaterProdutoSkuCorLimpar.Items.Count > 0);
                RepeaterProdutoSkuTamanhoLimpar.Visible = (RepeaterProdutoSkuTamanhoLimpar.Items.Count > 0);

                RepeaterProdutoSkuCor.DataSource = new ProdutoSkuTd().SelectProdutoSkuCor(null, gru_nomeAmigavel);
                RepeaterProdutoSkuTamanho.DataSource = new ProdutoSkuTd().SelectProdutoSkuTamanho(null, gru_nomeAmigavel);
                RepeaterProdutoSkuCor.DataBind();
                RepeaterProdutoSkuTamanho.DataBind();
                PanelLabelCor.Visible = (RepeaterProdutoSkuCor.Items.Count > 0);
                PanelLabelTamanho.Visible = (RepeaterProdutoSkuTamanho.Items.Count > 0);

            }
        }

        private string MontaMenuGrupo()
        {

            string gru_nomeAmigavel = Tratamento.GetUrlAmigavelAtual();

            IEnumerable<Grupo> grupo = new GrupoTd().SelectGrupoInicial(null);

            int? gru_pai = grupo.Where(s2 => s2.gru_nomeAmigavel == gru_nomeAmigavel).Select(s=>s.gru_pai).FirstOrDefault();

            if (!gru_pai.HasValue)
                Recursos.Redireciona404();

            IEnumerable<Grupo> gruposPai = grupo.Where(s => s.gru_id == gru_pai);

            if (!gruposPai.FirstOrDefault().gru_pai.HasValue)
            {
                gruposPai = gruposPai.FirstOrDefault().Grupo1.AsQueryable();
            }
            string menu = @"<ul>";


            foreach (var gruPais in gruposPai.Where(s2=>s2.gru_bloquear == false && s2.gru_subBloquear == false).OrderBy(s => s.gru_posicao))
            {
                menu += "<li>" +
                            "<li><a href=\"" + Page.GetRouteUrl("PaginaInicial", null) + gruPais.gru_nomeAmigavel + "\"><h1>" + gruPais.gru_nome + "</h1></a></li>" +
                        "</li>";

                foreach (var gruPais2 in gruPais.Grupo1.Where(s2 => s2.gru_bloquear == false && s2.gru_subBloquear == false).OrderBy(s => s.gru_posicao))
                {
                    string estilo = gruPais2.gru_nomeAmigavel == gru_nomeAmigavel ? "style=\"font-weight:bold\"" : string.Empty;
                    menu += "<li>" +
                             "<ul>" +
                                "<li><a href=\"" + Page.GetRouteUrl("PaginaInicial", null) + gruPais2.gru_nomeAmigavel + "\"" + estilo + ">" + gruPais2.gru_nome + "</a></li>" +
                             "</ul>" +
                           "</li>";
                }
            }
            return menu;
            return "";
        }

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

        private void ValidarUrlProdutoGrupo(List<GrupoDto> grupoCaminho)
        {
            if (grupoCaminho == null)
                Recursos.Redireciona404();

            string urlCaminhoCorTamanho = Tratamento.GetUrlRouteCorTamanho();

            string nomeAmigavelAtual = HttpContext.Current.Request.Url.AbsolutePath;
            string nomeAmigavelCorreto = (Page.GetRouteUrl("PaginaInicial", null) + grupoCaminho.LastOrDefault().gru_nomeAmigavel + urlCaminhoCorTamanho).ToLower();

            if (!nomeAmigavelAtual.Equals(nomeAmigavelCorreto))
            {
               Recursos.Redireciona301(nomeAmigavelCorreto);
            } 
        }

        protected void ObjectDataSourceProduto_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["orderBy"] = Recursos.SelecionarCampoOrdenacao(LinkButtonOrderMenorPreco, LinkButtonOrderMaiorPreco, LinkButtonOrderMaiorDesconto);
        }
    }
}