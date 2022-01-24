using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _2_Library.Utils;
using Loja.Modelo.Statusx;

namespace Loja.Painel
{
    public partial class ConfiguracaoPedidoStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             }
        protected void ButtonIncluirPedidoStatus_Click(object sender, EventArgs e)
        {
            ListViewPedidoStatus.EditIndex = -1;
            ListViewPedidoStatus.InsertItemPosition = InsertItemPosition.LastItem;
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            ListViewPedidoStatus.InsertItemPosition = InsertItemPosition.None;
        }

        protected void EntityDataSourcePedidoStatus_Inserting(object sender, EntityDataSourceChangingEventArgs e)
        {
            ((_2_Library.Modelo.Status)e.Entity).loj_id = new _2_Library.Dao.LojaX.LojaTd().SelectLoja(null).loj_id;
            ((_2_Library.Modelo.Status)e.Entity).stat_dataHora = DateTime.Now;       
        }

        protected void EntityDataSourcePedidoStatus_Inserted(object sender, EntityDataSourceChangedEventArgs e)
        {
            ListViewPedidoStatus.InsertItemPosition = InsertItemPosition.None;

            if (((_2_Library.Modelo.Status)e.Entity).stat_ativar)
                new StatusConsulta().AtualizarConfiguracaoStatuAtivar(((_2_Library.Modelo.Status)e.Entity).stat_id);
        }

        protected void EntityDataSourcePedidoStatus_Deleting(object sender, EntityDataSourceChangingEventArgs e)
        {
            ListViewPedidoStatus.EditIndex = -1;
            ListViewPedidoStatus.InsertItemPosition = InsertItemPosition.None;
            if (new Loja.Modelo.Statusx.StatusConsulta().SelecionarStatusExistePedido(((_2_Library.Modelo.Status)e.Entity).stat_id))
            {
                Validacao.Alert( "Não é possível excluir um status quando ele já está vinculado a algum pedido.");
                e.Cancel = true;
            }
        }

        protected void ListViewPedidoStatus_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            ListViewPedidoStatus.InsertItemPosition = InsertItemPosition.None;
        }

        protected void EntityDataSourcePedidoStatus_Updated(object sender, EntityDataSourceChangedEventArgs e)
        {
            if (((_2_Library.Modelo.Status)e.Entity).stat_ativar)
                new StatusConsulta().AtualizarConfiguracaoStatuAtivar(((_2_Library.Modelo.Status)e.Entity).stat_id);
        }

        protected void ListViewPedidoStatus_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            if (((RadioButton)ListViewPedidoStatus.Items[e.ItemIndex].FindControl("stat_ativarRadioButton")).Checked)
            {
                Validacao.Alert( "Não é possível excluir um Status Padrão.");
                e.Cancel = true;
            }
        }
    }
}