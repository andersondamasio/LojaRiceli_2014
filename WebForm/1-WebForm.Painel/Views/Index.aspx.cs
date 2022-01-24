using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Loja.Utils;
using Loja.Modelo;
using System.Xml.Serialization;
using System.IO;
using Loja.Modelo.Grupox;
using System.Reflection;

namespace Loja.Views
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           XmlDataSourceGrupos.DataFile = string.Empty;
           XmlDataSourceGrupos.Data = new GrupoConsulta().SelecionarProdutoVitrineInicial();
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
    }
}