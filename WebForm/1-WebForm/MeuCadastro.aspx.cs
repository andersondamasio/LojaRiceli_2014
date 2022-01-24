using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using _1_WebForm.App_Code.Utils;
using _2_Library.Dao.Site.Clientex;

namespace _1_WebForm
{
    public partial class MeuCadastro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AlimentaDados();

            if (!IsPostBack)
                if (Request.QueryString["cad"] == "ok")
                    _2_Library.Utils.Validacao.Alert("Parabéns, seu cadastro foi efetuado com Sucesso.");
        }

        private void AlimentaDados() {

            if (Session["cli_email"] == null || _1_WebForm.App_Code.Utils.Recursos.VerificarAutenticacao(false))
            {
                if (_1_WebForm.App_Code.Utils.Recursos.VerificarAutenticacao(true))
                {
                    if (!IsPostBack)
                        SelectCliente();

                    abaMeusPedidos.Visible = true;
                    abaMeusAmigos.Visible = true;
                    abaMeusEnderecos.Visible = true;
                    abaMeusPontos.Visible = true;
                    cli_cpfTextBox.Enabled = false;
                    PanelSenha.Visible = false;
                    ButtonAlterarSenha.Visible = true;
                    ButtonCancelarAlterarSenha.Visible = false;
                    LinkButtonVoltarAlterarEmail.Visible = false;

                    if (HiddenFieldTc.Value == "f")
                        RadioButtonClienteFisica_CheckedChanged(null, null);
                    else RadioButtonClienteJuridica_CheckedChanged(null, null);

                }
            }
            else
            {
                cli_emailTextBox.Text = Session["cli_email"].ToString();
                cli_cpfTextBox.Enabled = true;
                PanelTipoCliente.Visible = true;
                PanelSenha.Visible = true;
                ButtonAlterarSenha.Visible = false;
                ButtonCancelarAlterarSenha.Visible = false;
                LinkButtonVoltarAlterarEmail.Visible = true;
                fieldsetAlterarSenha.Attributes.Add("style", "border:0");
            }
        
        }

        private ClienteDto GetClienteDto() {

            ClienteDto clienteDto = new ClienteDto();

            clienteDto.cli_cpfCnpj = (cli_cnpjTextBox.Text.Trim().Length > 0) ? cli_cnpjTextBox.Text.Trim() : cli_cpfTextBox.Text.Trim();

            clienteDto.cli_email = cli_emailTextBox.Text.Trim();
            clienteDto.cli_senha = cli_senhaTextBox.Text.Trim();
            clienteDto.cli_nome = cli_nomeTextBox.Text.Trim();
            clienteDto.cli_sobrenome = cli_sobrenomeTextBox.Text.Trim();
            clienteDto.cli_cep = cli_cepTextBox.Text.Trim();
            clienteDto.cli_endereco = cli_enderecoTextBox.Text.Trim();
            clienteDto.cli_numero = cli_numeroTextBox.Text.Trim();
            clienteDto.cli_complemento = cli_complementoTextBox.Text.Trim();
            clienteDto.cli_bairro = cli_bairroTextBox.Text.Trim();
            clienteDto.cli_cidade = cli_cidadeTextBox.Text.Trim();
            clienteDto.cli_estado = cli_estadoDropDownList.SelectedValue;
            clienteDto.cli_referencia = cli_referenciaTextBox.Text.Trim();
            clienteDto.cli_ddd1 = cli_ddd1TextBox.Text.Trim();
            clienteDto.cli_fone1 = cli_fone1TextBox.Text.Trim();
            clienteDto.cli_ddd2 = cli_ddd2TextBox.Text.Trim();
            clienteDto.cli_fone2 = cli_fone2TextBox.Text.Trim();
            clienteDto.cli_ddd3 = cli_ddd3TextBox.Text.Trim();
            clienteDto.cli_fone3 = cli_fone3TextBox.Text.Trim();
            clienteDto.cli_recebeInformativo = cli_recebeInformativoCheckBox.Checked;
            clienteDto.cli_bloquear = false;
            clienteDto.cli_inscricaoEstadualIsento = false;

            if (clienteDto.cli_cpfCnpj.Length == 11)
            {
                clienteDto.cli_dataNascimento = _2_Library.Utils.Recursos.StringToDate(cli_diaNascimentoDropDownList.SelectedValue, cli_mesNascimentoDropDownList.SelectedValue, cli_anoNascimentoDropDownList.SelectedValue);
                clienteDto.cli_sexo = cli_sexoDropDownList.SelectedValue;
            }
            else
                if (clienteDto.cli_cpfCnpj.Length == 14)
                {
                    clienteDto.cli_razaoSocial = cli_razaoSocialTextBox.Text.Trim();
                    clienteDto.cli_inscricaoEstadual = cli_inscricaoEstadualTextBox.Text.Trim();
                    clienteDto.cli_inscricaoEstadualIsento = cli_inscricaoEstadualIsentoCheckBox.Checked;
                }
                else {
                    return null;
                }
            return clienteDto;
        }

        private void InsertCliente()
        {
            ClienteDto clienteDto = GetClienteDto();
            clienteDto = new ClienteTd().InsertCliente(null, clienteDto);

            if (clienteDto == null)
            {
                _2_Library.Utils.Validacao.Alert("Já existe um cliente cadastrado com este email");
            }
            else
            {
                Session.Remove("cli_email");

                Aut.Autenticar(new CliAut { CliId = clienteDto.cli_id, CliEmail = clienteDto.cli_email, CliNome = clienteDto.cli_nome });
            }
        }

        private void SelectCliente()
        {
            ClienteDto clienteDto = new ClienteTd().SelectCliente(null, HttpContext.Current.User.Identity.Name);
            cli_emailTextBox.Text = clienteDto.cli_email;

            cli_nomeTextBox.Text = clienteDto.cli_nome;
            cli_sobrenomeTextBox.Text = clienteDto.cli_sobrenome;

            cli_cepTextBox.Text = clienteDto.cli_cep;
            cli_enderecoTextBox.Text = clienteDto.cli_endereco;
            cli_numeroTextBox.Text = clienteDto.cli_numero;
            cli_complementoTextBox.Text = clienteDto.cli_complemento;
            cli_bairroTextBox.Text = clienteDto.cli_bairro;
            cli_cidadeTextBox.Text = clienteDto.cli_cidade;
            cli_estadoDropDownList.SelectedValue = clienteDto.cli_estado;
            cli_referenciaTextBox.Text = clienteDto.cli_referencia;
            cli_ddd1TextBox.Text = clienteDto.cli_ddd1;
            cli_fone1TextBox.Text = clienteDto.cli_fone1;
            cli_ddd2TextBox.Text = clienteDto.cli_ddd2;
            cli_fone2TextBox.Text = clienteDto.cli_fone2;
            cli_ddd3TextBox.Text = clienteDto.cli_ddd3;
            cli_fone3TextBox.Text = clienteDto.cli_fone3;
            cli_recebeInformativoCheckBox.Checked = clienteDto.cli_recebeInformativo;
            clienteDto.cli_bloquear = false;
            clienteDto.cli_inscricaoEstadualIsento = false;

            if (clienteDto.cli_cpfCnpj.Length == 14)
            {
                cli_razaoSocialTextBox.Text = clienteDto.cli_razaoSocial;
                cli_inscricaoEstadualTextBox.Text = clienteDto.cli_inscricaoEstadual;
                cli_cnpjTextBox.Text = clienteDto.cli_cpfCnpj;
                cli_inscricaoEstadualIsentoCheckBox.Checked = clienteDto.cli_inscricaoEstadualIsento;
                HiddenFieldTc.Value = "j";
            }
            else
                if (clienteDto.cli_cpfCnpj.Length == 11)
                {
                    cli_cpfTextBox.Text = clienteDto.cli_cpfCnpj;
                    cli_diaNascimentoDropDownList.SelectedValue = clienteDto.cli_dataNascimento.Value.Day.ToString();
                    cli_mesNascimentoDropDownList.SelectedValue = clienteDto.cli_dataNascimento.Value.Month.ToString();
                    cli_anoNascimentoDropDownList.SelectedValue = clienteDto.cli_dataNascimento.Value.Year.ToString();
                    cli_sexoDropDownList.SelectedValue = clienteDto.cli_sexo;
                    HiddenFieldTc.Value = "f";
                }
        }

        private void UpdateCliente() {

            new ClienteTd().UpdateCliente(null, GetClienteDto());

        }

        protected void ButtonCadastroClienteSalvar_Click(object sender, EventArgs e)
        {
            if (abaMeusPedidos.Visible)
            {
                UpdateCliente();
                _2_Library.Utils.Validacao.Alert("Atualizado com sucesso.");
            }
            else
            {
                InsertCliente();
                Response.Redirect(Page.GetRouteUrl("MeuCadastro", new { cad = "ok" }));
            }
        }

        protected void RadioButtonClienteFisica_CheckedChanged(object sender, EventArgs e)
        {
            PanelDadosPessoaFisica1.Visible = true;
            PanelDadosPessoaFisica2.Visible = true;
            PanelDadosPessoaJuridica1.Visible = false;
            PanelDadosPessoaJuridica2.Visible = false;
        }

        protected void RadioButtonClienteJuridica_CheckedChanged(object sender, EventArgs e)
        {
            PanelDadosPessoaFisica1.Visible = false;
            PanelDadosPessoaFisica2.Visible = false;
            PanelDadosPessoaJuridica1.Visible = true;
            PanelDadosPessoaJuridica2.Visible = true;
        }

        protected void ButtonAlterarSenha_Click(object sender, EventArgs e)
        {
            PanelSenha.Visible = true;
            ButtonCancelarAlterarSenha.Visible = true;
            ((Button)sender).OnClientClick = "alert('Clique em Salvar Alterações');return false;";
            ((Button)sender).EnableViewState = false;
        }

        protected void ButtonCancelarAlterarSenha_Click(object sender, EventArgs e)
        {
            PanelSenha.Visible = false;
            ButtonCancelarAlterarSenha.Visible = false;
        }

        protected void LinkButtonAlterarEmail_Click(object sender, EventArgs e)
        {
            _1_WebForm.App_Code.Utils.Recursos.VerificarAutenticacao(true);
        }

    }
}