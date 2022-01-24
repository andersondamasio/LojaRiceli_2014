using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using _2_Library.Dao.Painel;
using _2_Library.Dao.Painel.ClienteX;
using _2_Library.Dao.Painel.UsuarioX;
using _2_Library.Utils;
using Loja.Modelo.Clientex;

namespace Loja.Painel
{
    public partial class Cliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void EntityDataSourceClientes_Updating(object sender, EntityDataSourceChangingEventArgs e)
        {
            string cli_email = ((TextBox)ListViewClientes.EditItem.FindControl("cli_emailTextBox")).Text.Trim();

            if (new ClienteConsulta().SelecionarClienteCount(cli_email, ((_2_Library.Modelo.Cliente)e.Entity).cli_id) == 0)
            {
                ((_2_Library.Modelo.Cliente)e.Entity).cli_dataNascimento = _2_Library.Utils.Recursos.StringToDate(((DropDownList)ListViewClientes.EditItem.FindControl("cli_diaNascimentoDropDownList")).SelectedValue, ((DropDownList)ListViewClientes.EditItem.FindControl("cli_mesNascimentoDropDownList")).SelectedValue, ((DropDownList)ListViewClientes.EditItem.FindControl("cli_anoNascimentoDropDownList")).SelectedValue);
            }
            else
            {
                e.Cancel = true;
                Validacao.Alert("Já existe outro cliente usando este email");
            }
        }

        protected void ButtonRealizarPedidoCliente_Click(object sender, EventArgs e)
        {
            ListViewItem listViewItem = ((Button)sender).NamingContainer as ListViewItem;

            string url = string.Empty;
            int cli_id = (int)ListViewClientes.DataKeys[listViewItem.DataItemIndex]["cli_id"];
            string cli_chave = Guid.NewGuid().ToString();

            UsuarioDto usuarioDto = Loja.Utils.Aut.AutenticacaoDadosUsuario();
            ClienteDto clienteDto = new ClienteTd().SelectCliente(null, cli_id);
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("cli_id", clienteDto.cli_id.ToString());
            nvc.Add("cli_chave", cli_chave);
            nvc.Add("usu_id", usuarioDto.usu_id.ToString());
            nvc.Add("usu_nome", usuarioDto.usu_nome.ToString());

            //reforça a segurança do login, se o session(chavePedidoComoCliente) também não existir o login não é realizado
            Session.Add("chavePedidoComoCliente", cli_chave + clienteDto.cli_id);

            if (usuarioDto.loj_dominio != "localhost")
                url = "http://" + usuarioDto.loj_dominio + "/Login?ReturnUrl=/";
            else
                url = "http://" + usuarioDto.loj_dominio + "/1-webform/Login?ReturnUrl=/1-webform/";

            Recursos.RedirectAndPOST(url, nvc, true);
        }
    }
}