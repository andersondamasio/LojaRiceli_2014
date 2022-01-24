using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _2_Library.Utils;
using Loja.Modelo.Cupom;

namespace Loja.Painel
{
    public partial class Cupom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void ButtonIncluirCupom_Click(object sender, EventArgs e)
        {
            ListViewCupom.EditIndex = -1;
            ListViewCupom.InsertItemPosition = InsertItemPosition.LastItem;
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            ListViewCupom.InsertItemPosition = InsertItemPosition.None;
        }

        protected void EntityDataSourceCupom_Inserting(object sender, EntityDataSourceChangingEventArgs e)
        {
            if (((_2_Library.Modelo.Cupom)e.Entity).cli_id.HasValue)
            {
                Boolean clienteExiste = new Loja.Modelo.Clientex.ClienteConsulta().SelecionarClienteExiste(((_2_Library.Modelo.Cupom)e.Entity).cli_id.Value);
                e.Cancel = !clienteExiste;
            }
            if (!e.Cancel)
            {
                ((_2_Library.Modelo.Cupom)e.Entity).loj_id = new _2_Library.Dao.LojaX.LojaTd().SelectLoja(null).loj_id;
                ((_2_Library.Modelo.Cupom)e.Entity).cup_dataHora = DateTime.Now;
            }
            else {
                Validacao.Alert( "Cliente não encontrado.");
            }
        }

        protected void EntityDataSourceCupom_Inserted(object sender, EntityDataSourceChangedEventArgs e)
        {
            ListViewCupom.InsertItemPosition = InsertItemPosition.None;
        }

        protected void EntityDataSourceCupom_Deleting(object sender, EntityDataSourceChangingEventArgs e)
        {
            ListViewCupom.EditIndex = -1;
            ListViewCupom.InsertItemPosition = InsertItemPosition.None;

            new CupomConsulta().ExcluirCupom(((_2_Library.Modelo.Cupom)e.Entity).cup_id);
            e.Cancel = true;
        }

        protected void ListViewCupom_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            ListViewCupom.InsertItemPosition = InsertItemPosition.None;
        }

        protected void EntityDataSourceCupons_Updating(object sender, EntityDataSourceChangingEventArgs e)
        {
            if (((_2_Library.Modelo.Cupom)e.Entity).cli_id.HasValue)
            {
                Boolean clienteExiste = new Loja.Modelo.Clientex.ClienteConsulta().SelecionarClienteExiste(((_2_Library.Modelo.Cupom)e.Entity).cli_id.Value);
                e.Cancel = !clienteExiste;
            }
            if(e.Cancel) {
                Validacao.Alert( "Cliente não encontrado.");
            }
            ((_2_Library.Modelo.Cupom)e.Entity).cup_dataHoraUltimaAtualizacao = DateTime.Now;
        }
    }
}