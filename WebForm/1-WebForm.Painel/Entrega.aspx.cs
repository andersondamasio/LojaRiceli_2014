using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Loja.Modelo.Entregax;
using Loja.Utils;

namespace Loja.Painel
{
    public partial class Entrega : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Login.ValidaLogin();
        }
        protected void ButtonIncluirEntrega_Click(object sender, EventArgs e)
        {
            ListViewEntrega.EditIndex = -1;
            ListViewEntrega.InsertItemPosition = InsertItemPosition.LastItem;
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            ListViewEntrega.InsertItemPosition = InsertItemPosition.None;
        }

        protected void EntityDataSourceEntrega_Inserting(object sender, EntityDataSourceChangingEventArgs e)
        {
            if (!new EntregaConsulta().SelecionarEntregaExiste(((_2_Library.Modelo.Entrega)e.Entity).ent_cepInicial, ((_2_Library.Modelo.Entrega)e.Entity).ent_cepFinal, 0))
            {
                TextBox ent_horaInicialTextBox = (TextBox)ListViewEntrega.InsertItem.FindControl("ent_horaInicialTextBox");
                TextBox ent_horaFinalTextBox = (TextBox)ListViewEntrega.InsertItem.FindControl("ent_horaFinalTextBox");

                TimeSpan time;
                if (((_2_Library.Modelo.Entrega)e.Entity).ent_dataHoraInicial != null && ent_horaInicialTextBox.Text.Trim() != string.Empty)
                {
                    if (TimeSpan.TryParse(ent_horaInicialTextBox.Text, out time))
                        ((_2_Library.Modelo.Entrega)e.Entity).ent_dataHoraInicial = ((_2_Library.Modelo.Entrega)e.Entity).ent_dataHoraInicial + time;
                }

                if (((_2_Library.Modelo.Entrega)e.Entity).ent_dataHoraFinal != null && ent_horaFinalTextBox.Text.Trim() != string.Empty)
                {
                    if (TimeSpan.TryParse(ent_horaFinalTextBox.Text, out time))
                        ((_2_Library.Modelo.Entrega)e.Entity).ent_dataHoraFinal = ((_2_Library.Modelo.Entrega)e.Entity).ent_dataHoraFinal + time;
                }
                ((_2_Library.Modelo.Entrega)e.Entity).loj_id = Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio().loj_id;
                ((_2_Library.Modelo.Entrega)e.Entity).ent_dataHora = DateTime.Now;
            }
            else
            {
                e.Cancel = true;
                Validacao.Alert(Page, "Faixa de Cep já está cadastrada.");
            }
        }

        protected void EntityDataSourceEntrega_Inserted(object sender, EntityDataSourceChangedEventArgs e)
        {
            ListViewEntrega.InsertItemPosition = InsertItemPosition.None;
        }

        protected void EntityDataSourceEntrega_Deleting(object sender, EntityDataSourceChangingEventArgs e)
        {
            ListViewEntrega.EditIndex = -1;
            ListViewEntrega.InsertItemPosition = InsertItemPosition.None;
        }

        protected void ListViewEntrega_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            ListViewEntrega.InsertItemPosition = InsertItemPosition.None;
        }

        protected void EntityDataSourceEntrega_Updating(object sender, EntityDataSourceChangingEventArgs e)
        {
            if (!new EntregaConsulta().SelecionarEntregaExiste(((_2_Library.Modelo.Entrega)e.Entity).ent_cepInicial, ((_2_Library.Modelo.Entrega)e.Entity).ent_cepFinal, ((_2_Library.Modelo.Entrega)e.Entity).ent_id))
            {
                TextBox ent_horaInicialTextBox = (TextBox)ListViewEntrega.EditItem.FindControl("ent_horaInicialTextBox");
                TextBox ent_horaFinalTextBox = (TextBox)ListViewEntrega.EditItem.FindControl("ent_horaFinalTextBox");

                TimeSpan time;
                if (((_2_Library.Modelo.Entrega)e.Entity).ent_dataHoraInicial != null && ent_horaInicialTextBox.Text.Trim() != string.Empty)
                {
                    if (TimeSpan.TryParse(ent_horaInicialTextBox.Text, out time))
                    {
                        string novoDateTime = ((_2_Library.Modelo.Entrega)e.Entity).ent_dataHoraInicial.Value.ToShortDateString();
                        ((_2_Library.Modelo.Entrega)e.Entity).ent_dataHoraInicial = Convert.ToDateTime(novoDateTime) + time;
                    }
                }

                if (((_2_Library.Modelo.Entrega)e.Entity).ent_dataHoraFinal != null && ent_horaFinalTextBox.Text.Trim() != string.Empty)
                {
                    if (TimeSpan.TryParse(ent_horaFinalTextBox.Text, out time))
                    {
                        string novoDateTime = ((_2_Library.Modelo.Entrega)e.Entity).ent_dataHoraFinal.Value.ToShortDateString();
                        ((_2_Library.Modelo.Entrega)e.Entity).ent_dataHoraFinal = Convert.ToDateTime(novoDateTime) + time;
                    }
                }
            }
            else {
                e.Cancel = true;
                Validacao.Alert(Page, "Faixa de Cep já está cadastrada.");
            }
        }

    }
}