using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Loja.Modelo;
using _2_Library.Modelo;

namespace Loja.Painel.selecao
{
    public partial class SelecionarMarca : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ListViewMarca_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

            if (e.CommandName != "Sort")
            {
                if (e.CommandSource is Button)
                {
                    Int32 mar_id = Convert.ToInt32(((Button)e.CommandSource).CommandArgument);
                    HiddenFieldTipoCadastro.Value = "1";
                    Marca marca = new Consulta().SelecionaMarca(mar_id);
                    mar_nomeTextBox.Text = marca.mar_nome;
                    mar_descricaoTextBox.Text = marca.mar_descricao;
                    mar_bloquearCheckBox.Checked = marca.mar_bloquear;
                    PanelCadastrarMarca.Visible = true;
                }
                else
                {
                    if (e.CommandSource is LinkButton)
                    {
                        var valorText = ((Label)e.Item.FindControl("mar_nomeLabel")).Text;
                        var valorHidden = ((Label)e.Item.FindControl("mar_idLabel")).Text;
                        string Script =
                        string.Format(
                        "parent.SetaValorRegistro('{0}','{1}','{2}','{3}');",
                        this.Request.QueryString["controleDestinoText"],
                        this.Request.QueryString["controleDestinoHidden"],
                        valorText,
                        valorHidden
                        );
                        this.ClientScript.RegisterStartupScript(typeof(SelecionarMarca), "fechar", "<script>" + Script + "</script>");
                    }
                }
            }
        }

        protected void ButtonIncluirMarca_Click(object sender, EventArgs e)
        {
            HiddenFieldTipoCadastro.Value = "0";
            PanelCadastrarMarca.Visible = true;
        }

        protected void ButtonSalvarMarca_Click(object sender, EventArgs e)
        {
            Marca marca = new Marca();
            marca.mar_nome = mar_nomeTextBox.Text.Trim();
            marca.mar_descricao = mar_descricaoTextBox.Text.Trim();
            marca.mar_bloquear = mar_bloquearCheckBox.Checked;
            if (HiddenFieldTipoCadastro.Value == "0")
            {
                new Consulta().InserirMarca(marca);
            }
            else
            {
                 marca.mar_id = Convert.ToInt32(ListViewMarcas.SelectedValue);
                 new Consulta().AtualizarMarca(marca);
            }
            PanelCadastrarMarca.Visible = false;
            ListViewMarcas.DataBind();
        }

        protected void ButtonCancelarMarca_Click(object sender, EventArgs e)
        {
            PanelCadastrarMarca.Visible = false;
        }
    }
}