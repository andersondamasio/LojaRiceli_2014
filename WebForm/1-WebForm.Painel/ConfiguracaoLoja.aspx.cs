using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loja.Painel
{
    public partial class ConfiguracaoLoja : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Login.ValidaLogin();
        }

        protected void ButtonIncluirLoja_Click(object sender, EventArgs e)
        {
            ListViewLoja.EditIndex = -1;
            ListViewLoja.InsertItemPosition = InsertItemPosition.LastItem;
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            ListViewLoja.InsertItemPosition = InsertItemPosition.None;
        }

        protected void EntityDataSourceLoja_Inserting(object sender, EntityDataSourceChangingEventArgs e)
        {
            ((_2_Library.Modelo.LojaCon)e.Entity).loj_dataHora = DateTime.Now;
        }

        protected void EntityDataSourceLoja_Inserted(object sender, EntityDataSourceChangedEventArgs e)
        {
            ListViewLoja.InsertItemPosition = InsertItemPosition.None;

            int loj_id = Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio().loj_id;

            string sessionClienteLogin = "clienteLogin" + loj_id;
            string sessionCliId = "cli_id" + loj_id;

            if (Session[sessionClienteLogin] != null)
                Session.Remove(sessionClienteLogin);

            if (Session[sessionCliId] != null)
                Session.Remove(sessionCliId);

            InicializaLoja(((_2_Library.Modelo.LojaCon)e.Entity).loj_id);
        }

        protected void EntityDataSourceLoja_Deleting(object sender, EntityDataSourceChangingEventArgs e)
        {
            ListViewLoja.EditIndex = -1;
            ListViewLoja.InsertItemPosition = InsertItemPosition.None;
        }

        protected void ListViewLoja_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            ListViewLoja.InsertItemPosition = InsertItemPosition.None;
        }

        protected void EntityDataSourceLoja_Updated(object sender, EntityDataSourceChangedEventArgs e)
        {
            string sessionClienteLogin = "clienteLogin" + Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio().loj_id;
            string sessionCliId = "cli_id" + Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio().loj_id;

            if (Session[sessionClienteLogin] != null)
                Session.Remove(sessionClienteLogin);

            if (Session[sessionCliId] != null)
                Session.Remove(sessionCliId);  

        }

        protected void ListViewLoja_ItemUpdated(object sender, ListViewUpdatedEventArgs e)
        {
            /*  string loj_dominio = HttpContext.Current.Request.Url.Host;
              System.Web.Caching.Cache cache = HttpContext.Current.Cache;

              if (e.NewValues["loj_dominio"] != null)
              if ((e.NewValues["loj_dominio"].ToString() != e.OldValues["loj_dominio"].ToString()) &&  (loj_dominio == e.OldValues["loj_dominio"].ToString()))
                  cache.Remove("Loja" + loj_dominio);
             */
        }

        protected void EntityDataSourceLoja_Updating(object sender, EntityDataSourceChangingEventArgs e)
        {
        }

        protected void ListViewLoja_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            if (e.NewValues["loj_dominio"] != null)
            {
                if ((e.NewValues["loj_dominio"].ToString() != e.OldValues["loj_dominio"].ToString()) && (Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio().loj_dominio == e.OldValues["loj_dominio"].ToString()))
                {
                    Loja.Utils.Validacao.Alert(Page, "Não é possível alterar seu próprio domínio.");
                    e.Cancel = true;
                }

                string loj_dominio = HttpContext.Current.Request.Url.Host;
                System.Web.Caching.Cache cache = HttpContext.Current.Cache;

                if ((Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio().loj_dominio == e.OldValues["loj_dominio"].ToString()))
                    cache.Remove("Loja" + loj_dominio);
            }
        }

        private void InicializaLoja(int loj_id)
        {
            String caminhoDiretorioFoto = Request.PhysicalApplicationPath + @"imagens\produtos\fotos\" + loj_id + "\\";
            String caminhoImagemSemFoto = Request.PhysicalApplicationPath + @"imagens\produtos\fotos\v.jpg";

            if (File.Exists(caminhoDiretorioFoto + "v.jpg"))
            {
                File.Delete(caminhoDiretorioFoto + "v.jpg");
                Directory.Delete(caminhoDiretorioFoto);
            }
            else
                if (!Directory.Exists(caminhoDiretorioFoto))
                {
                    Directory.CreateDirectory(caminhoDiretorioFoto);
                    File.Copy(caminhoImagemSemFoto, caminhoDiretorioFoto + "v.jpg");
                }
        }

        protected void EntityDataSourceLoja_Deleted(object sender, EntityDataSourceChangedEventArgs e)
        {
            if (e.Entity != null)
                InicializaLoja(((_2_Library.Modelo.LojaCon)e.Entity).loj_id);
        }
    }
}