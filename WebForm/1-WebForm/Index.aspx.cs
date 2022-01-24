using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _2_Library.Dao.Site.GrupoX;
using _2_Library.Modelo;

namespace _1_WebForm
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LiteralMenu.Text = MontaMenuGrupoInicial();
        }

        private string MontaMenuGrupoInicial()
        {
            IEnumerable<Grupo> grupo = new GrupoTd().SelectGrupoInicial(null);
            IEnumerable<Grupo> gruposPai = grupo.Where(s => s.gru_pai == grupo.Where(s2 => !s2.gru_pai.HasValue).FirstOrDefault().gru_id);
           

            string menu = @"<ul>";

            foreach (var gruPais in gruposPai.Where(s2 => s2.gru_bloquear == false && s2.gru_subBloquear == false).OrderBy(s => s.gru_posicao))
            {
                menu += "<li>" +
                            "<li><a href=\"" + Page.GetRouteUrl("PaginaInicial", null) + gruPais.gru_nomeAmigavel + "\"><h1>" + gruPais.gru_nome + "</h1></a></li>" +
                        "</li>";

                foreach (var gruPais2 in gruPais.Grupo1.Where(s2 => s2.gru_bloquear == false && s2.gru_subBloquear == false).OrderBy(s => s.gru_posicao))
                {
                    menu += "<li>" +
                             "<ul>" +
                                "<li><a href=\""+Page.GetRouteUrl("PaginaInicial", null)+gruPais2.gru_nomeAmigavel + "\">" + gruPais2.gru_nome + "</a></li>" +
                             "</ul>" +
                           "</li>";
                }
            }
            return menu;
        }

        /*private string MontaMenuTodosOsGrupos(IQueryable<Grupo> gruposPai)
        {
            string menu = @"<ul>";
            foreach (var gruPais in gruposPai)
            {
                menu += @"<li>" + gruPais.gru_nome + "</li>";
            }
            menu += "<ul>";
            return menu;
        }*/
    }
}