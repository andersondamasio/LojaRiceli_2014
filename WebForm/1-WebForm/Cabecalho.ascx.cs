using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using _1_WebForm.App_Code.Utils;
using _2_Library.Dao.Site.GrupoX;
using _2_Library.Dao.LojaX;
using _2_Library.Modelo;
using _2_Library.Utils;

namespace _1_WebForm
{
    public partial class Cabecalho : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {          
                XmlDataSourceGrupos.Data = new GrupoTd().SelectGrupoCabecalho(null);   
                LojaDto lojaDto = new LojaTd().SelectLoja(null);
                LiteralCssLoja.Text = "<link href=\"" + Page.GetRouteUrl("PaginaInicial", null) + "css/" + lojaDto.loj_id + @"/estilos.css"" rel=\""stylesheet\"" type=\""text/css\"" />";
                AdicionaScriptHeader();
            }
        }


        protected void ImageButtonBusca_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string pro_nomeChave = TextBoxBusca.Text.Trim();
            string urlBusca = string.Format("{0}?q={1}", Page.GetRouteUrl("ProdutoBusca", null), pro_nomeChave);

            Response.Redirect(urlBusca);
        }

        private void AdicionaScriptHeader()
        {
            string script = "var paginaInicial = \"" + Page.GetRouteUrl("PaginaInicial", null) + "\";" +
                            "var paginaAtual = \"" + HttpContext.Current.Request.Url.AbsolutePath + "\";";
            Validacao.SetScriptHeader(script);  
        }


    }
}