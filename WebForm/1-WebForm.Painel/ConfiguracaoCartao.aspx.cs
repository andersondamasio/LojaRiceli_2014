using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loja.Painel
{
    public partial class ConfiguracaoCartao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Login.ValidaLogin();
        }

        protected void ButtonIncluirCartao_Click(object sender, EventArgs e)
        {
            ListViewCartao.EditIndex = -1;
            ListViewCartao.InsertItemPosition = InsertItemPosition.LastItem;
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            ListViewCartao.InsertItemPosition = InsertItemPosition.None;
        }

        protected void EntityDataSourceCartao_Inserting(object sender, EntityDataSourceChangingEventArgs e)
        {
            ((_2_Library.Modelo.FormaPagamento)e.Entity).loj_id = Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio().loj_id;
            ((_2_Library.Modelo.FormaPagamento)e.Entity).forPag_dataHora = DateTime.Now;
        }

        protected void EntityDataSourceCartao_Inserted(object sender, EntityDataSourceChangedEventArgs e)
        {
            ListViewCartao.InsertItemPosition = InsertItemPosition.None;
        }

        protected void EntityDataSourceCartao_Deleting(object sender, EntityDataSourceChangingEventArgs e)
        {
            ListViewCartao.EditIndex = -1;
            ListViewCartao.InsertItemPosition = InsertItemPosition.None;
        }

        protected void ListViewCartao_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            ListViewCartao.InsertItemPosition = InsertItemPosition.None;
        }
    }
}