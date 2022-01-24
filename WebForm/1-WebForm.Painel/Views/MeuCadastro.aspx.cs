using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _2_Library.Modelo;

namespace Loja.Views
{
    public partial class MeuCadastro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                PanelTipoCliente.Visible = false;
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    FormViewCadastroCliente.ChangeMode(FormViewMode.Insert);
                    TextBox cli_emailTextBox = (TextBox)FormViewCadastroCliente.FindControl("cli_emailTextBox");

                    if (string.IsNullOrEmpty(cli_emailTextBox.Text.Trim()))
                        cli_emailTextBox.Text = (string)Session["cli_emailNovo"] ?? string.Empty;

                    if (string.IsNullOrEmpty(cli_emailTextBox.Text.Trim()) && Session["cli_emailNovo"] == null)
                        Response.Redirect(Page.GetRouteUrl("LoginCadastro", null) + "?ReturnUrl=" + Page.GetRouteUrl("MeuCadastro", null));
                }
            }
        }

        protected void FormViewCadastroCliente_ItemInserting(object sender, FormViewInsertEventArgs e)
        {

            string cli_email = e.Values["cli_email"].ToString().Trim();

            Boolean existe = new Loja.Modelo.Clientex.ClienteConsulta().SelecionarClienteExiste(cli_email);

            if (existe)
            {
                Loja.Utils.Validacao.Alert(Page, "Já existe uma cadastro usando este email, tente recuperar sua senha caso tenha esquecido.");
                ((TextBox)FormViewCadastroCliente.FindControl("cli_emailTextBox")).Focus();
                e.Cancel = true;
            }
            else
            {
                if (RadioButtonClienteFisica.Checked)
                {
                    string dataNascimento = e.Values["cli_diaNascimento"] + "/" + e.Values["cli_mesNascimento"] + "/" + e.Values["cli_anoNascimento"];
                    e.Values.Remove("cli_diaNascimento");
                    e.Values.Remove("cli_mesNascimento");
                    e.Values.Remove("cli_anoNascimento");
                    e.Values.Add("cli_dataNascimento", dataNascimento);
                }
                else
                {
                    e.Values.Remove("cli_diaNascimento");
                    e.Values.Remove("cli_mesNascimento");
                    e.Values.Remove("cli_anoNascimento");
                }
                e.Values.Add("cli_dataHora", DateTime.Now);
                e.Values.Add("loj_id", Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio().loj_id);
            }
        }

        protected void RadioButtonClienteFisica_CheckedChanged(object sender, EventArgs e)
        {
            Panel panelInformacaoClientePessoaFisica = (Panel)FormViewCadastroCliente.FindControl("PanelInformacaoClientePessoaFisica");
            panelInformacaoClientePessoaFisica.Visible = true;
            Panel panelInformacaoClientePessoaJuridica = (Panel)FormViewCadastroCliente.FindControl("PanelInformacaoClientePessoaJuridica");
            panelInformacaoClientePessoaJuridica.Visible = false;
        }

        protected void RadioButtonClienteJuridica_CheckedChanged(object sender, EventArgs e)
        {
            Panel panelInformacaoClientePessoaFisica = (Panel)FormViewCadastroCliente.FindControl("PanelInformacaoClientePessoaFisica");
            panelInformacaoClientePessoaFisica.Visible = false;
            Panel panelInformacaoClientePessoaJuridica = (Panel)FormViewCadastroCliente.FindControl("PanelInformacaoClientePessoaJuridica");
            panelInformacaoClientePessoaJuridica.Visible = true;
        }

        protected void EntityDataSourceCadastroCliente_Inserted(object sender, EntityDataSourceChangedEventArgs e)
        {
            new LoginCadastro().ClienteLogin(((Cliente)e.Entity).cli_email, ((Cliente)e.Entity).cli_senha);
        }

        protected void FormViewCadastroCliente_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            TextBox cli_cnpjTextBox = ((TextBox)FormViewCadastroCliente.FindControl("cli_cnpjTextBox"));
            TextBox cli_cpfTextBox = ((TextBox)FormViewCadastroCliente.FindControl("cli_cpfTextBox"));
            DropDownList cli_diaNascimentoDropDownList = ((DropDownList)FormViewCadastroCliente.FindControl("cli_diaNascimentoDropDownList"));
            DropDownList cli_mesNascimentoDropDownList = ((DropDownList)FormViewCadastroCliente.FindControl("cli_mesNascimentoDropDownList"));
            DropDownList cli_anoNascimentoDropDownList = ((DropDownList)FormViewCadastroCliente.FindControl("cli_anoNascimentoDropDownList"));
            string dataNascimento = cli_diaNascimentoDropDownList.SelectedValue + "/" + cli_mesNascimentoDropDownList.SelectedValue + "/" + cli_anoNascimentoDropDownList.SelectedValue;

            if (cli_cnpjTextBox != null && cli_cnpjTextBox.Text != string.Empty && cli_cnpjTextBox.Text.Trim().Length == 14)
                e.NewValues["cli_cpfCnpj"] = cli_cnpjTextBox.Text.Trim();


            if (cli_cpfTextBox != null && cli_cpfTextBox.Text != string.Empty && cli_cpfTextBox.Text.Trim().Length == 11)
            {
                e.NewValues["cli_cpfCnpj"] = cli_cpfTextBox.Text.Trim();
                e.NewValues.Add("cli_dataNascimento", dataNascimento);
            }
        }

        protected void FormViewCadastroCliente_ItemCreated(object sender, EventArgs e)
        {
            TextBox cli_cepTextBox = ((TextBox)FormViewCadastroCliente.FindControl("cli_cepTextBox"));
            if (cli_cepTextBox != null)
                cli_cepTextBox.Attributes.Add("onkeyup", "SelecionarLocalidade(this)");
        }

    }
}