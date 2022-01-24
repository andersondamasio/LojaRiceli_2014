using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _2_Library.Modelo;
using _2_Library.Utils;
using Loja.Modelo;

namespace Loja.Painel
{
    public partial class ConfiguracaoLoja : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

            int loj_id = new _2_Library.Dao.LojaX.LojaTd().SelectLoja(null).loj_id;

            InicializaLoja(((_2_Library.Modelo.LojaCon)e.Entity));
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

        protected void ListViewLoja_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            if (e.NewValues["loj_dominio"] != null)
            {
                if ((e.NewValues["loj_dominio"].ToString() != e.OldValues["loj_dominio"].ToString()) && (new _2_Library.Dao.LojaX.LojaTd().SelectLoja(null).loj_dominio == e.OldValues["loj_dominio"].ToString()))
                {
                    Validacao.Alert( "Não é possível alterar seu próprio domínio.");
                    e.Cancel = true;
                }
            }
        }

        private void InicializaLoja(_2_Library.Modelo.LojaCon lojaCon)
        {
            string pastaFoto = Loja.Modelo.Static.caminhoDiretorioFoto;
            string pastaCss = Loja.Modelo.Static.caminhoDiretorioFoto;
            String caminhoImagemSemFoto = pastaFoto + @"\v.jpg";
            String caminhoDiretorioFotoLoja = pastaFoto + @"\" + lojaCon.loj_id + @"\sem-foto\";
            String caminhoCss = pastaCss + @"\estilos.css";
            String caminhoCssLoja = pastaCss + @"\" + lojaCon.loj_id + @"\";

                if (!Directory.Exists(caminhoDiretorioFotoLoja))
                {
                    Directory.CreateDirectory(caminhoDiretorioFotoLoja);
                    File.Copy(caminhoImagemSemFoto, caminhoDiretorioFotoLoja + "sem-foto.jpg");
                }

                if (!Directory.Exists(caminhoCssLoja))
                {
                    Directory.CreateDirectory(caminhoCssLoja);
                    File.Copy(caminhoCss, caminhoCssLoja + "estilos.css");
                }

                IISManager.AddHostSite("riceli.com.br", lojaCon.loj_dominio);
        }

        protected void EntityDataSourceLoja_Deleted(object sender, EntityDataSourceChangedEventArgs e)
        {
            LojaCon lojaCon =  (LojaCon)e.Entity;
            IISManager.RemoveHostSite("riceli.com.br", lojaCon.loj_dominio);
        }
    }
}