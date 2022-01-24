using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _2_Library.Utils;
using Loja.Modelo.Usuariox;

namespace Loja.Painel
{
    public partial class Usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          }
        protected void ButtonIncluirUsuario_Click(object sender, EventArgs e)
        {
            ListViewUsuario.EditIndex = -1;
            ListViewUsuario.InsertItemPosition = InsertItemPosition.LastItem;
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            ListViewUsuario.InsertItemPosition = InsertItemPosition.None;
        }

        protected void EntityDataSourceUsuario_Inserting(object sender, EntityDataSourceChangingEventArgs e)
        {
            string usu_nome = ((TextBox)ListViewUsuario.InsertItem.FindControl("usu_nomeTextBox")).Text.Trim();
            
            if (!new UsuarioConsulta().SelecionarUsuarioExiste(usu_nome, 0))
            {
                ((_2_Library.Modelo.Usuario)e.Entity).loj_id = new _2_Library.Dao.LojaX.LojaTd().SelectLoja(null).loj_id;
                ((_2_Library.Modelo.Usuario)e.Entity).usu_dataHora = DateTime.Now;
            }
            else
            {
                e.Cancel = true;
               Validacao.Alert( "Nome de usuário já cadastrado.");
            }
        }

        protected void EntityDataSourceUsuario_Inserted(object sender, EntityDataSourceChangedEventArgs e)
        {
            ListViewUsuario.InsertItemPosition = InsertItemPosition.None;
        }

        protected void EntityDataSourceUsuario_Deleting(object sender, EntityDataSourceChangingEventArgs e)
        {
            ListViewUsuario.EditIndex = -1;
            ListViewUsuario.InsertItemPosition = InsertItemPosition.None;
        }

        protected void ListViewUsuario_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            ListViewUsuario.InsertItemPosition = InsertItemPosition.None;
        }

        protected void EntityDataSourceUsuario_Updating(object sender, EntityDataSourceChangingEventArgs e)
        {
            string usu_nome = ((TextBox)ListViewUsuario.EditItem.FindControl("usu_nomeTextBox")).Text.Trim();
            TextBox usu_senhaTextBox = ((TextBox)ListViewUsuario.EditItem.FindControl("usu_senhaTextBox"));

            if (usu_senhaTextBox.Visible && usu_senhaTextBox.Text.Trim() != string.Empty)
                ((_2_Library.Modelo.Usuario)e.Entity).usu_senha = usu_senhaTextBox.Text.Trim();
           
            if (new UsuarioConsulta().SelecionarUsuarioExiste(usu_nome, ((_2_Library.Modelo.Usuario)e.Entity).usu_id))
            {
                e.Cancel = true;
                Validacao.Alert( "Nome de usuário já cadastrado.");
            }
        }

        protected void ButtonAterarSenha_Click(object sender, EventArgs e)
        {
            if (((Button)sender).Text == "Alterar Senha")
            {
                ListViewUsuario.EditItem.FindControl("SpanAlterarSenha").Visible = true;
                ((Button)sender).Text = "Cancelar";
            }
            else
            {
                ListViewUsuario.EditItem.FindControl("SpanAlterarSenha").Visible = false;
                ((Button)sender).Text = "Alterar Senha";
            }
        }
    }
}