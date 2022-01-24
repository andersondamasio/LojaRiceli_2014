using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Loja.Modelo.Clientex;
using Loja.Utils;

namespace Loja.Painel
{
    public partial class Cliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Login.ValidaLogin();
        }

        protected void EntityDataSourceClientes_Updating(object sender, EntityDataSourceChangingEventArgs e)
        {
            string cli_email = ((TextBox)ListViewClientes.EditItem.FindControl("cli_emailTextBox")).Text.Trim();

            if (new ClienteConsulta().SelecionarClienteCount(cli_email, ((_2_Library.Modelo.Cliente)e.Entity).cli_id) == 0)
            {
                ((_2_Library.Modelo.Cliente)e.Entity).cli_dataNascimento = Recursos.StringToDate(((DropDownList)ListViewClientes.EditItem.FindControl("cli_diaNascimentoDropDownList")).SelectedValue, ((DropDownList)ListViewClientes.EditItem.FindControl("cli_mesNascimentoDropDownList")).SelectedValue, ((DropDownList)ListViewClientes.EditItem.FindControl("cli_anoNascimentoDropDownList")).SelectedValue);
            }
            else {
                e.Cancel = true;
                Validacao.Alert(Page,"Já existe outro cliente usando este email");
            } 
        }

    }
}