using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Loja.Utils;
using Loja.Modelo.Grupox;
using Loja.Modelo;
using Loja.Modelo.ProdutoSkux;
using System.Reflection;
using _2_Library.Modelo;

namespace Loja.Views
{
    public partial class ProdutoGrupo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (RouteData.Values["gru_nomeAmigavel"] != null)
            {
                if (!IsPostBack)
                {
                    string gru_nomeAmigavel = RouteData.Values["gru_nomeAmigavel"].ToString();

                    if (gru_nomeAmigavel.IndexOf('_') != -1)
                        gru_nomeAmigavel = gru_nomeAmigavel.Remove(gru_nomeAmigavel.IndexOf('_'));

                    Grupo grupo = new Consulta().SelecionarGrupo(gru_nomeAmigavel);

                    VerificarUrl(grupo, gru_nomeAmigavel);
                    
                    XmlDataSourceGrupos.Data = new GrupoConsulta().SelecionarProdutoVitrineGrupo(gru_nomeAmigavel);
                   //RepeaterCor.DataSource = new ProdutoSkuConsulta().SelecionarProdutoSkuCor(grupo, gru_nomeAmigavel);
                    RepeaterCor.DataBind();
                   // RepeaterTamanho.DataSource = new ProdutoSkuConsulta().SelecionarProdutoSkuTamanho(grupo, gru_nomeAmigavel);
                    RepeaterTamanho.DataBind();
                    RepeaterTamanhoLimpar.DataBind();
                    RepeaterCorLimpar.DataBind();

                    PanelFiltroLimpar.Visible     = (RepeaterTamanhoLimpar.Items.Count > 0 || RepeaterCorLimpar.Items.Count > 0);
                    RepeaterTamanhoLimpar.Visible = (RepeaterTamanhoLimpar.Items.Count > 0);
                     RepeaterCorLimpar.Visible     = (RepeaterCorLimpar.Items.Count > 0); 
                    RepeaterTamanho.Visible       = RepeaterTamanho.Items.Count > 0;
                    RepeaterCor.Visible           = RepeaterCor.Items.Count > 0;
                }
            }
            else
              Recursos.Redireciona404();
        }

        protected void LinkButtonOrderMenorPreco_Click(object sender, EventArgs e)
        {
            string url = Request.Url.AbsolutePath;

            if (Request.QueryString.Get("ordenar") != null)
            {
                PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                isreadonly.SetValue(this.Request.QueryString, false, null);
                Request.QueryString.Remove("ordenar");
                url = Request.Url.AbsolutePath +"?"+ Request.QueryString;
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

            Response.Redirect(url.Replace("??","?"));
        }

        private void VerificarUrl(Grupo grupo, string gru_nomeAmigavel)
        {
            if (grupo == null)
                Recursos.Redireciona404();
            else {
                Static.resultadoNomeAmigavel = string.Empty;
                string gruAmigavelEnviado = String.Join("/", Page.RouteData.Values.Where(s => s.Key != "proSku_cores" && s.Key != "proSku_tamanhos" && s.Value != null && s.Value != string.Empty).Select(s => s.Value));
              string gruAmigavelCorreto = String.Join("/", new Tratamento().ExtrairGrupo(grupo).Where(s => s != null && s != string.Empty));

              if (gruAmigavelEnviado != gruAmigavelCorreto)
              {
                  string url = HttpContext.Current.Request.Url.Scheme + Uri.SchemeDelimiter + HttpContext.Current.Request.Url.Host + Page.GetRouteUrl("PaginaInicial", null) + gruAmigavelCorreto;
                   
                  url = url + HttpContext.Current.Request.Url.Query;

                  Recursos.Redireciona301(url);
              }           
            }           
        }

        protected void ObjectDataSourceProduto_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["orderBy"] = Recursos.SelecionarCampoOrdenacao();
        }

        protected void MenuProdutoGrupo_MenuItemDataBound(object sender, MenuEventArgs e)
        {
            System.Xml.XmlNode node = (System.Xml.XmlNode)e.Item.DataItem;
            Static.resultadoNomeAmigavel = string.Empty;
            string pi = Page.GetRouteUrl("PaginaInicial", null);
            e.Item.NavigateUrl = pi.Remove(pi.LastIndexOf('/')) + Tratamento.ExtrairGrupo(node);
        }


        /*private string CriarMenu(string gru_nomeAmigavel)
        {
            LojaEntities lojaEntities = new LojaEntities();
            var grupo = (from gru in lojaEntities.Grupo
                         where (gru.gru_nomeAmigavel == gru_nomeAmigavel) &&
                         gru.gru_bloquear == false && gru.gru_subBloquear == false && !gru.gru_excluido.HasValue
                         select gru).FirstOrDefault();


            string menu = @"<ul>";
            var gruNivel2 = grupo.Grupo1.Where(s => s.gru_bloquear == false && s.gru_subBloquear == false && !s.gru_excluido.HasValue).OrderBy(s => s.gru_id).Select(s => new { cont = s.Produto_Grupo.Count(), s.gru_nome, s.gru_nomeAmigavel });
            if (gruNivel2.Count() > 0)
            {
               // menu += "<li><a href=\"" + grupo.gru_nomeAmigavel + "\"><b>" + grupo.gru_nome + "</b></a></li>";
                
                foreach (var gru1 in gruNivel2)
                {
                    if (gru1.cont > 0)
                    {
                        if (gru_nomeAmigavel == gru1.gru_nomeAmigavel)
                            menu += "<li><a href=\"" + gru1.gru_nomeAmigavel + "\"><b>" + gru1.gru_nome + "(" + gru1.cont + ")</b></a></li>";
                        else menu += "<li><a href=\"" + gru1.gru_nomeAmigavel + "\">" + gru1.gru_nome + "(" + gru1.cont + ")</a></li>";
                    }
                    else {
                        if (gru_nomeAmigavel == gru1.gru_nomeAmigavel)
                            menu += "<li><a href=\"" + gru1.gru_nomeAmigavel + "\"><b>" + gru1.gru_nome + "</b></a></li>";
                        else menu += "<li><a href=\"" + gru1.gru_nomeAmigavel + "\">" + gru1.gru_nome + "</a></li>";                   
                    }

                    if (gruNivel2.Count() > 5)
                        menu += "<div>Veja mais</div>";
                }
            }
            else
            {
                var gruNivel1 = grupo.Grupo2.Grupo1.Where(s => s.gru_bloquear == false && s.gru_subBloquear == false && !s.gru_excluido.HasValue).Select(s => new { cont = s.Produto_Grupo.Count(), s.gru_nome, s.gru_nomeAmigavel });

                foreach (var gru1 in gruNivel1)
                {
                    if (gru1.cont > 0)
                    {
                        if (gru_nomeAmigavel == gru1.gru_nomeAmigavel)
                            menu += "<li><a href=\"" + gru1.gru_nomeAmigavel + "\"><b>" + gru1.gru_nome + "(" + gru1.cont + ")</b></a></li>";
                        else menu += "<li><a href=\"" + gru1.gru_nomeAmigavel + "\">" + gru1.gru_nome + "(" + gru1.cont + ")</a></li>";
                    }
                    else
                    {
                        if (gru_nomeAmigavel == gru1.gru_nomeAmigavel)
                            menu += "<li><a href=\"" + gru1.gru_nomeAmigavel + "\"><b>" + gru1.gru_nome + "</b></a></li>";
                        else menu += "<li><a href=\"" + gru1.gru_nomeAmigavel + "\">" + gru1.gru_nome + "</a></li>";
                    }
                    if (gruNivel2.Count() > 5)
                        menu += "<div>Veja mais</div>";
                }
            }
            return menu += "</ul>";
        }*/

    }
}