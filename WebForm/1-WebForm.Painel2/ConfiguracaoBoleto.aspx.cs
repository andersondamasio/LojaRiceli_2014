using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loja.Painel
{
    public partial class ConfiguracaoBoleto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            }

        protected void ButtonIncluirBoleto_Click(object sender, EventArgs e)
        {
            ListViewBoleto.EditIndex = -1;
            ListViewBoleto.InsertItemPosition = InsertItemPosition.LastItem;
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            ListViewBoleto.InsertItemPosition = InsertItemPosition.None;
        }

        protected void EntityDataSourceBoleto_Inserting(object sender, EntityDataSourceChangingEventArgs e)
        {
            ((_2_Library.Modelo.ConfiguracaoBoleto)e.Entity).loj_id = new _2_Library.Dao.LojaX.LojaTd().SelectLoja(null).loj_id;
        }

        protected void EntityDataSourceBoleto_Inserted(object sender, EntityDataSourceChangedEventArgs e)
        {
            ListViewBoleto.InsertItemPosition = InsertItemPosition.None;

            if (((_2_Library.Modelo.ConfiguracaoBoleto)e.Entity).conBol_ativar)
                new Loja.Modelo.Configuracaox.ConfiguracaoConsulta().AtualizarConfiguracaoBoletoAtivar(((_2_Library.Modelo.ConfiguracaoBoleto)e.Entity).conBol_id);
        }

        protected void EntityDataSourceBoleto_Updated(object sender, EntityDataSourceChangedEventArgs e)
        {
            if (((_2_Library.Modelo.ConfiguracaoBoleto)e.Entity).conBol_ativar)
                new Loja.Modelo.Configuracaox.ConfiguracaoConsulta().AtualizarConfiguracaoBoletoAtivar(((_2_Library.Modelo.ConfiguracaoBoleto)e.Entity).conBol_id);
        }

        protected void EntityDataSourceBoleto_Deleting(object sender, EntityDataSourceChangingEventArgs e)
        {
            ListViewBoleto.EditIndex = -1;
            ListViewBoleto.InsertItemPosition = InsertItemPosition.None;
        }

        protected void ListViewBoleto_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            ListViewBoleto.InsertItemPosition = InsertItemPosition.None;
        }
    }
}