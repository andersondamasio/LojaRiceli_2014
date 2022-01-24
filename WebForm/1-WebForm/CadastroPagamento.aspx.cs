using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using _1_WebForm.App_Code.Utils;
using _2_Library;
using _2_Library.Dao.Site.CarrinhoX;
using _2_Library.Dao.Site.ClienteEnderecoAdicionalX;
using _2_Library.Dao.Site.Clientex;
using _2_Library.Dao.LojaX;
using _2_Library.Dao.Site.PedidoX;
using _2_Library.Dao.Site.StatusX;
using _2_Library.Gateways.PagSeguro;
using _2_Library.Modelo;


namespace _1_WebForm
{
    public partial class CadastroPagamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cli_email"] == null)
            {
                if (_1_WebForm.App_Code.Utils.Recursos.VerificarAutenticacao(true))
                {
                    if (!IsPostBack)
                    {
                        TrataObjetos(true, false, false, false);
                        SelectCliente();
                    }
                }
                else return;
            }
            else
            {
                TrataObjetos(false, false, false, true);
            }

            _2_Library.Utils.Recursos.DesabilitarDuploClick(ButtonFinalizarPedido, "Processando...", ButtonFinalizarPedido.ValidationGroup);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="autenticado">Autenticado ou não</param>
        /// <param name="editandoCadastro">prepara form para editar registro</param>
        /// <param name="inserindoAdicionalCadastro">prepara form para inserir cadastro adicional</param>
        /// <param name="inserindoCadastro">prepara form para inserir um cadastro</param>
        private void TrataObjetos(bool autenticado, bool editandoCadastro, bool inserindoAdicionalCadastro, bool inserindoCadastro)
        {
            if (autenticado)
            {
                PanelTipoCliente.Visible = false;
                PanelEnderecoCobrancaResumo.Visible = true;
                PanelEnderecoCobrancaEditar.Visible = false;
                PanelEnderecoAdicionalEntrega.Visible = false;
                LinkButtonVoltarAlterarEmail.Visible = false;
                if (editandoCadastro)
                {
                    PanelEnderecoCobrancaResumo.Visible = false;
                    PanelEnderecoCobrancaEditar.Visible = true;
                    PanelSenha.Visible = false;
                    cli_cpfTextBox.Enabled = false;
                    cli_emailTextBox.Enabled = false;
                }

                if (inserindoAdicionalCadastro)
                {
                    PanelEnderecoAdicionalEntrega.Visible = true;
                }
            }
            else
            {
                if (inserindoCadastro)
                {
                    PanelEnderecoCobrancaEditar.Visible = true;
                    PanelEnderecoAdicionalEntregaPasso2.Visible = false;
                    LinkButtonAdicionarEnderecoAdicionalEntrega.Visible = false;
                    LinkButtonAtualizarCadastro.Visible = false;
                    LinkButtonCancelarCadastro.Visible = false;
                    PanelTipoCliente.Visible = true;
                    PanelEnderecoCobrancaResumo.Visible = false;

                    TrataFormInsertCadastro();
                }
            }

        }

        private void TrataFormInsertCadastro()
        {

            if (Session["cli_email"] == null)
                Response.Redirect(Page.GetRouteUrl("CadastroPagamento", null));
            else
            {
                cli_emailTextBox.Text = Session["cli_email"].ToString();
                cli_emailTextBox.Enabled = false;
            }
        }

        private void SelectCliente()
        {
            ClienteDto clienteDto = new ClienteTd().SelectCliente(null, HttpContext.Current.User.Identity.Name);

            if (clienteDto.cli_cpfCnpj.Length == 14)
                HiddenFieldTc.Value = "j";
            else HiddenFieldTc.Value = "f";

            if (PanelEnderecoCobrancaResumo.Visible)
            {
                cli_nomeLiteral.Text = clienteDto.cli_nome;
                cli_sobrenomeLiteral.Text = clienteDto.cli_sobrenome;
                cli_enderecoLiteral.Text = clienteDto.cli_endereco;
                cli_numeroLiteral.Text = clienteDto.cli_numero;
                cli_bairroLiteral.Text = clienteDto.cli_bairro;
                cli_cidadeLiteral.Text = clienteDto.cli_cidade;
                cli_estadoLiteral.Text = clienteDto.cli_estado;
                cli_cepLiteral.Text = clienteDto.cli_cep;
                cli_ddd1Literal.Text = clienteDto.cli_ddd1;
                cli_fone1Literal.Text = clienteDto.cli_fone1;
            }
            else
            {
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

                }
                else
                    if (clienteDto.cli_cpfCnpj.Length == 11)
                    {
                        cli_cpfTextBox.Text = clienteDto.cli_cpfCnpj;
                        cli_diaNascimentoDropDownList.SelectedValue = clienteDto.cli_dataNascimento.Value.Day.ToString();
                        cli_mesNascimentoDropDownList.SelectedValue = clienteDto.cli_dataNascimento.Value.Month.ToString();
                        cli_anoNascimentoDropDownList.SelectedValue = clienteDto.cli_dataNascimento.Value.Year.ToString();
                        cli_sexoDropDownList.SelectedValue = clienteDto.cli_sexo;
                    }
            }
        }

        private void SelectClienteAdicional()
        {

            ListViewEnderecoAdicionalEntrega.DataBind();
        }

        protected void LinkButtonEditarCadastro_Click(object sender, EventArgs e)
        {
            TrataObjetos(true, true, false, false);

            if (HiddenFieldTc.Value == "f")
                RadioButtonClienteFisica_CheckedChanged(null, null);
            else RadioButtonClienteJuridica_CheckedChanged(null, null);

            SelectCliente();
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

        protected void LinkButtonAtualizarCadastro_Click(object sender, EventArgs e)
        {      
                UpdateCliente();
                TrataObjetos(true, false, false, false);
                SelectCliente();
        }

        protected void LinkButtonCancelarCadastro_Click(object sender, EventArgs e)
        {
            TrataObjetos(true, false, false, false);
        }

        protected void LinkButtonAlterarEmail_Click(object sender, EventArgs e)
        {
            Session.Remove("cli_email");
            Response.Redirect(Page.GetRouteUrl("CadastroPagamento", null));
        }

        private void UpdateCliente()
        {
            ClienteDto clienteDto = GetClienteDto();
            new ClienteTd().UpdateCliente(null, GetClienteDto());
        }

        protected void CheckBoxEnderecoAdicionalEntrega_CheckedChanged(object sender, EventArgs e)
        {
            TrataObjetos(true, false, !((CheckBox)sender).Checked, false);
            if (!((CheckBox)sender).Checked)
                SelectClienteAdicional();
        }

        protected void LinkButtonAdicionarEnderecoAdicionalEntrega_Click(object sender, EventArgs e)
        {
            ListViewEnderecoAdicionalEntrega.InsertItemPosition = InsertItemPosition.FirstItem;
            ListViewEnderecoAdicionalEntrega.DataBind();
        }

        protected void LinkButtonInsertCancel_Click(object sender, EventArgs e)
        {
            ListViewEnderecoAdicionalEntrega.InsertItemPosition = InsertItemPosition.None;
            ListViewEnderecoAdicionalEntrega.DataBind();
        }

        private void ListaOpcoesEntrega()
        {
            string cepEntrega;

            if (CheckBoxEnderecoAdicionalEntrega.Checked || ListViewEnderecoAdicionalEntrega.SelectedIndex == -1)
                cepEntrega = cli_cepLiteral.Text;
            else
            {
                TextBox cliEnd_cepVarTextBox = (TextBox)ListViewEnderecoAdicionalEntrega.Items[ListViewEnderecoAdicionalEntrega.SelectedIndex].FindControl("cliEnd_cepTextBox");
                Literal cliEnd_cepVarLiteral = (Literal)ListViewEnderecoAdicionalEntrega.Items[ListViewEnderecoAdicionalEntrega.SelectedIndex].FindControl("cliEnd_cepLiteral");
                cepEntrega = cliEnd_cepVarLiteral != null ? cliEnd_cepVarLiteral.Text : cliEnd_cepVarTextBox.Text;
            }

            if (HiddenFieldCepEntrega.Value != cepEntrega)
            {
                HiddenFieldCepEntrega.Value = cepEntrega;
            }
            _2_Library.Utils.Validacao.SetScript("SelectCarrinhoTotais();");
        }

        public void ListViewEnderecoAdicionalEntrega_InsertItem()
        {
            ClienteEnderecoAdicionalDto clienteEnderecoAdicionalDto = new ClienteEnderecoAdicionalDto();
            TryUpdateModel(clienteEnderecoAdicionalDto);

            _1_WebForm.App_Code.Utils.CustomPrincipal customPrincipal = _1_WebForm.App_Code.Utils.Aut.AutenticacaoDados();

            if (customPrincipal != null && customPrincipal.CliId != 0)
            {
                clienteEnderecoAdicionalDto.cli_id = customPrincipal.CliId;
            }

            new ClienteEnderecoAdicionalTd().InsertClienteEnderecoAdicional(clienteEnderecoAdicionalDto);
            ListViewEnderecoAdicionalEntrega.InsertItemPosition = InsertItemPosition.None;
        }

        // O nome do parâmetro id deve corresponder ao valor DataKeyNames definido no controle
        public void ListViewEnderecoAdicionalEntrega_UpdateItem(int cliEnd_id)
        {
            ClienteEnderecoAdicionalDto clienteEnderecoAdicionalDto = new ClienteEnderecoAdicionalDto();
            TryUpdateModel(clienteEnderecoAdicionalDto);
            new ClienteEnderecoAdicionalTd().UpdateClienteEnderecoAdicional(clienteEnderecoAdicionalDto);
        }

        // O nome do parâmetro id deve corresponder ao valor DataKeyNames definido no controle
        public void ListViewEnderecoAdicionalEntrega_DeleteItem(int cliEnd_id)
        {
            ClienteEnderecoAdicionalDto clienteEnderecoAdicionalDto = new ClienteEnderecoAdicionalDto();
            clienteEnderecoAdicionalDto.cliEnd_id = cliEnd_id;
            new ClienteEnderecoAdicionalTd().RemoveClienteEnderecoAdicional(clienteEnderecoAdicionalDto);
        }

        // O tipo de retorno pode ser alterado para IEnumerable, no entanto, para dar suporte à paginação de
        // e classificação, os seguintes parâmetros devem ser adicionados:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable ListViewEnderecoAdicionalEntrega_GetData()
        {
             _1_WebForm.App_Code.Utils.CustomPrincipal customPrincipal = _1_WebForm.App_Code.Utils.Aut.AutenticacaoDados();

            if (customPrincipal != null && customPrincipal.CliId != 0)
            {
                ClienteEnderecoAdicionalTd clienteEnderecoAdicionalTd = new ClienteEnderecoAdicionalTd();
                List<ClienteEnderecoAdicionalDto> clienteEndAdDto = clienteEnderecoAdicionalTd.SelectAllClienteEnderecoAdicional(null,  customPrincipal.CliId);
                return clienteEndAdDto.AsQueryable();
            }
            else
                return null;
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            ListaOpcoesEntrega();
        }

        protected void ButtonFinalizarPedido_Click(object sender, EventArgs e)
        {

            if (Session["cli_email"] == null && (PanelEnderecoCobrancaEditar.Visible || ListViewEnderecoAdicionalEntrega.EditIndex > -1 || ListViewEnderecoAdicionalEntrega.InsertItemPosition == InsertItemPosition.FirstItem))
            {
                _2_Library.Utils.Validacao.Alert("Por favor Salve ou Cancele sua edição antes de continuar");
            }
            else
            {
                int cli_id = 0;
                CustomPrincipal customPrincipal = Aut.AutenticacaoDados();

                if (customPrincipal != null && customPrincipal.CliId != 0)
                {
                    cli_id = customPrincipal.CliId;
                }

                if (Session["cli_email"] == null)
                {
                    InserirPedido(cli_id);
                }
                else
                {
                    InserirPedidoCliente();
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////
        private ClienteDto GetClienteDto()
        {
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
                else
                {
                    return null;
                }
            return clienteDto;
        }

        private Cliente GetCliente()
        {
            Cliente clienteDto = new Cliente();
          
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
            clienteDto.cli_dataHora = DateTime.Now;
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
                else
                {
                    return null;
                }
            return clienteDto;
        }

        private Pedido GetPedido(ClienteDto cliente) {

            Pedido pedido = new Pedido();
            pedido.loj_id = cliente.loj_id;
            pedido.cli_id = cliente.cli_id;
            pedido.ped_enderecoIp = _2_Library.Utils.Recursos.SelecionarIp();
            pedido.ped_cli_cpfCnpj = cliente.cli_cpfCnpj;
            pedido.ped_cli_nome = cliente.cli_nome;
            pedido.ped_cli_sobrenome = cliente.cli_sobrenome;
            pedido.ped_cli_email = cliente.cli_email;
            pedido.ped_cli_cep = cliente.cli_cep;
            pedido.ped_cli_endereco = cliente.cli_endereco;
            pedido.ped_cli_numero = cliente.cli_numero;
            pedido.ped_cli_complemento = cliente.cli_complemento;
            pedido.ped_cli_referencia = cliente.cli_referencia;
            pedido.ped_cli_bairro = cliente.cli_bairro;
            pedido.ped_cli_cidade = cliente.cli_cidade;
            pedido.ped_cli_estado = cliente.cli_estado;
            pedido.ped_cli_ddd1 = cliente.cli_ddd1;
            pedido.ped_cli_fone1 = cliente.cli_fone1;
            pedido.ped_cli_ddd2 = cliente.cli_ddd2;
            pedido.ped_cli_fone2 = cliente.cli_fone2;
            pedido.ped_cli_ddd3 = cliente.cli_ddd3;
            pedido.ped_cli_fone3 = cliente.cli_fone3;
            pedido.ped_cli_rg = cliente.cli_rg;
            pedido.ped_cli_dataNascimento = cliente.cli_dataNascimento;
            pedido.ped_cli_sexo = cliente.cli_sexo;
            pedido.ped_cli_razaoSocial = cliente.cli_razaoSocial;
            pedido.ped_cli_inscricaoEstadual = cliente.cli_inscricaoEstadual;
            pedido.ped_cli_inscricaoEstadualIsento = cliente.cli_inscricaoEstadualIsento;
            pedido.ped_cli_recebeInformativo = cliente.cli_recebeInformativo;


            if (!CheckBoxEnderecoAdicionalEntrega.Checked && ListViewEnderecoAdicionalEntrega.SelectedValue != null)
            {
                int cliEnd_id = Convert.ToInt32(ListViewEnderecoAdicionalEntrega.SelectedDataKey["cliEnd_id"]);

                ClienteEnderecoAdicionalDto clienteEnderecoAdicional = new ClienteEnderecoAdicionalTd().SelectClienteEnderecoAdicional(null, cliEnd_id);

                pedido.ped_cliEnd_nome = clienteEnderecoAdicional.cliEnd_nome;
                pedido.ped_cliEnd_sobrenome = clienteEnderecoAdicional.cliEnd_sobrenome;
                pedido.ped_cliEnd_email = clienteEnderecoAdicional.cliEnd_email;
                pedido.ped_cliEnd_cep = clienteEnderecoAdicional.cliEnd_cep;
                pedido.ped_cliEnd_endereco = clienteEnderecoAdicional.cliEnd_endereco;
                pedido.ped_cliEnd_numero = clienteEnderecoAdicional.cliEnd_numero;
                pedido.ped_cliEnd_complemento = clienteEnderecoAdicional.cliEnd_complemento;
                pedido.ped_cliEnd_referencia = clienteEnderecoAdicional.cliEnd_referencia;
                pedido.ped_cliEnd_bairro = clienteEnderecoAdicional.cliEnd_bairro;
                pedido.ped_cliEnd_cidade = clienteEnderecoAdicional.cliEnd_cidade;
                pedido.ped_cliEnd_estado = clienteEnderecoAdicional.cliEnd_estado;
                pedido.ped_cliEnd_ddd1 = clienteEnderecoAdicional.cliEnd_ddd1;
                pedido.ped_cliEnd_fone1 = clienteEnderecoAdicional.cliEnd_fone1;
                pedido.ped_cliEnd_ddd2 = clienteEnderecoAdicional.cliEnd_ddd2;
                pedido.ped_cliEnd_fone2 = clienteEnderecoAdicional.cliEnd_fone2;
                pedido.ped_cliEnd_ddd3 = clienteEnderecoAdicional.cliEnd_ddd3;
                pedido.ped_cliEnd_fone3 = clienteEnderecoAdicional.cliEnd_fone3;
            }
            else
            {
                pedido.ped_cliEnd_nome = cliente.cli_nome;
                pedido.ped_cliEnd_sobrenome = cliente.cli_sobrenome;
                pedido.ped_cliEnd_email = cliente.cli_email;
                pedido.ped_cliEnd_cep = cliente.cli_cep;
                pedido.ped_cliEnd_endereco = cliente.cli_endereco;
                pedido.ped_cliEnd_numero = cliente.cli_numero;
                pedido.ped_cliEnd_complemento = cliente.cli_complemento;
                pedido.ped_cliEnd_referencia = cliente.cli_referencia;
                pedido.ped_cliEnd_bairro = cliente.cli_bairro;
                pedido.ped_cliEnd_cidade = cliente.cli_cidade;
                pedido.ped_cliEnd_estado = cliente.cli_estado;
                pedido.ped_cliEnd_ddd1 = cliente.cli_ddd1;
                pedido.ped_cliEnd_fone1 = cliente.cli_fone1;
                pedido.ped_cliEnd_ddd2 = cliente.cli_ddd2;
                pedido.ped_cliEnd_fone2 = cliente.cli_fone2;
                pedido.ped_cliEnd_ddd3 = cliente.cli_ddd3;
                pedido.ped_cliEnd_fone3 = cliente.cli_fone3;
            }

            CarrinhoTotaisDto carrinhoTotaisDto = (CarrinhoTotaisDto)System.Web.HttpContext.Current.Session["carrinhoTotaisDto"];

            if (carrinhoTotaisDto != null)
            {
                pedido.ped_ent_meio = "correios";
                pedido.ped_ent_localizacao = carrinhoTotaisDto.correioDto.co_cidade + "-" + carrinhoTotaisDto.correioDto.co_estado;
                pedido.ped_ent_prazo = carrinhoTotaisDto.cart_entregaPrazoTotal.Value;
                pedido.ped_ent_valor = carrinhoTotaisDto.cart_entregaTotal.Value; ;

                if (carrinhoTotaisDto.cupomDto != null)
                {
                    pedido.cup_id = carrinhoTotaisDto.cupomDto.cup_id;
                    carrinhoTotaisDto.cart_cupomTotal = carrinhoTotaisDto.cupomDto.cup_valor;
                }

                pedido.ped_forPag_nome = RadioButtonPagamentoPagSeguro.Checked ? "Aguardando Pagamento" : string.Empty;
                pedido.ped_forPag_valorDesconto = 0;
                pedido.ped_forPag_percentualDesconto = 0;
                pedido.ped_forPagPar_condicao = RadioButtonPagamentoPagSeguro.Checked ? "Aguardando Pagamento" : string.Empty;//carrinhoTotaisDto.cart_condicao;
                pedido.ped_forPagPar_quantidade = 0;//Convert.ToInt32(ped_forPagNumParcelaHiddenField.Value);
                pedido.ped_forPagPar_valor = 0;//Convert.ToDecimal(ped_forPagValParcelaHiddenField.Value);
                pedido.ped_forPagPar_percentualJuro = 0;//Convert.ToDecimal(parcPar_perJuroHiddenField.Value);
                pedido.ped_forPag_prazoPagamento = DateTime.Now.AddDays(2);
                pedido.ped_forPag_situacao = "Pendente";
                pedido.ped_forPag_gateway = RadioButtonPagamentoPagSeguro.Checked ? "pagseguro" : null;

                Status status = new StatusTd().SelectFirstStatus(null);
                PedidoStatus pedidoStatus = new PedidoStatus();
                pedidoStatus.loj_id = pedido.loj_id;
                pedidoStatus.ped_id = pedido.ped_id;
                pedidoStatus.stat_id = status.stat_id;
                pedidoStatus.pedStat_dataHora = DateTime.Now;
                pedido.PedidoStatus.Add(pedidoStatus);
                pedido.stat_id = status.stat_id;

                foreach (var car in carrinhoTotaisDto.carrinhoDto)
                {
                    PedidoProduto pedidoProduto = new PedidoProduto();
                    pedidoProduto.ped_id = pedido.ped_id;
                    pedidoProduto.pedPro_car_quantidade = car.car_quantidade;
                    pedidoProduto.pedPro_gru_nome = null;
                    pedidoProduto.pedPro_pro_nome = car.proSku_nome;
                    pedidoProduto.pedPro_proSku_id = car.proSku_id;
                    pedidoProduto.pedPro_proSku_altura = car.proSku_altura;
                    pedidoProduto.pedPro_proSku_largura = car.proSku_largura;
                    pedidoProduto.pedPro_proSku_comprimento = car.proSku_comprimento;
                    pedidoProduto.pedPro_proSku_peso = car.proSku_peso;
                    pedidoProduto.pedPro_proSkuCor_nome = car.proSkuCor_nome;
                    pedidoProduto.pedPro_proSkuTam_nome = car.proSkuTam_nome;
                    pedidoProduto.pedPro_proSku_prazoEntregaAdicional = car.proSku_prazoEntregaAdicional;
                    pedidoProduto.pedPro_proSku_precoCusto = car.proSku_precoCusto;
                    pedidoProduto.pedPro_proSku_precoAnterior = car.proSku_precoAnterior;
                    pedidoProduto.pedPro_proSku_precoVenda = car.proSku_precoVenda;
                    pedidoProduto.pedPro_proSku_precoVendaTotal = car.proSku_precoVenda * car.car_quantidade;
                    pedidoProduto.loj_id = pedido.loj_id;
                    //referencia direta produtoSku//
                    pedidoProduto.proSku_id = car.proSku_id;
                    pedidoProduto.proSku_loj_id = cliente.loj_id;
                    pedidoProduto.proSku_disponivel = true;
                    //
                    pedidoProduto.pedPro_dataHora = DateTime.Now;
                    pedido.PedidoProduto.Add(pedidoProduto);
                }

                if (Session["usu_id"] != null)
                    pedido.usu_id = Convert.ToInt32(Session["usu_id"]);

                pedido.ped_subTotal = carrinhoTotaisDto.cart_subTotal;
                pedido.ped_descontos = (carrinhoTotaisDto.cart_cupomTotal) + pedido.ped_forPag_valorDesconto + ((pedido.ped_forPag_percentualDesconto / 100) * carrinhoTotaisDto.cart_subTotal);
                decimal ped_total = (pedido.ped_subTotal - pedido.ped_descontos);
                pedido.ped_total = (ped_total < 0 ? 0 : ped_total) + pedido.ped_ent_valor;
            }
            else
            {
                Response.Redirect(Page.GetRouteUrl("CadastroPagamento", null));
            }
            return pedido;
        }

        private void InserirPedido(int cli_id)
        {
            ClienteDto cliente = new ClienteTd().SelectCliente(null, cli_id);
            if (cliente != null)
            {
                Pedido pedido = GetPedido(cliente);
                pedido = new PedidoTd().InsertPedido(null, pedido);
                ConcluirPedido(pedido);
            }
            else {
                Aut.LogoutLogin();
            }
        }

        private Pedido GetPedido(Cliente cliente)
        {
            Pedido pedido = new Pedido();
            pedido.ped_enderecoIp = _2_Library.Utils.Recursos.SelecionarIp();
            pedido.ped_cli_cpfCnpj = cliente.cli_cpfCnpj;
            pedido.ped_cli_nome = cliente.cli_nome;
            pedido.ped_cli_sobrenome = cliente.cli_sobrenome;
            pedido.ped_cli_email = cliente.cli_email;
            pedido.ped_cli_cep = cliente.cli_cep;
            pedido.ped_cli_endereco = cliente.cli_endereco;
            pedido.ped_cli_numero = cliente.cli_numero;
            pedido.ped_cli_complemento = cliente.cli_complemento;
            pedido.ped_cli_referencia = cliente.cli_referencia;
            pedido.ped_cli_bairro = cliente.cli_bairro;
            pedido.ped_cli_cidade = cliente.cli_cidade;
            pedido.ped_cli_estado = cliente.cli_estado;
            pedido.ped_cli_ddd1 = cliente.cli_ddd1;
            pedido.ped_cli_fone1 = cliente.cli_fone1;
            pedido.ped_cli_ddd2 = cliente.cli_ddd2;
            pedido.ped_cli_fone2 = cliente.cli_fone2;
            pedido.ped_cli_ddd3 = cliente.cli_ddd3;
            pedido.ped_cli_fone3 = cliente.cli_fone3;
            pedido.ped_cli_rg = cliente.cli_rg;
            pedido.ped_cli_dataNascimento = cliente.cli_dataNascimento;
            pedido.ped_cli_sexo = cliente.cli_sexo;
            pedido.ped_cli_razaoSocial = cliente.cli_razaoSocial;
            pedido.ped_cli_inscricaoEstadual = cliente.cli_inscricaoEstadual;
            pedido.ped_cli_inscricaoEstadualIsento = cliente.cli_inscricaoEstadualIsento;
            pedido.ped_cli_recebeInformativo = cliente.cli_recebeInformativo;

            pedido.ped_cliEnd_nome = cliente.cli_nome;
            pedido.ped_cliEnd_sobrenome = cliente.cli_sobrenome;
            pedido.ped_cliEnd_email = cliente.cli_email;
            pedido.ped_cliEnd_cep = cliente.cli_cep;
            pedido.ped_cliEnd_endereco = cliente.cli_endereco;
            pedido.ped_cliEnd_numero = cliente.cli_numero;
            pedido.ped_cliEnd_complemento = cliente.cli_complemento;
            pedido.ped_cliEnd_referencia = cliente.cli_referencia;
            pedido.ped_cliEnd_bairro = cliente.cli_bairro;
            pedido.ped_cliEnd_cidade = cliente.cli_cidade;
            pedido.ped_cliEnd_estado = cliente.cli_estado;
            pedido.ped_cliEnd_ddd1 = cliente.cli_ddd1;
            pedido.ped_cliEnd_fone1 = cliente.cli_fone1;
            pedido.ped_cliEnd_ddd2 = cliente.cli_ddd2;
            pedido.ped_cliEnd_fone2 = cliente.cli_fone2;
            pedido.ped_cliEnd_ddd3 = cliente.cli_ddd3;
            pedido.ped_cliEnd_fone3 = cliente.cli_fone3;

            CarrinhoTotaisDto carrinhoTotaisDto = (CarrinhoTotaisDto)System.Web.HttpContext.Current.Session["carrinhoTotaisDto"];

            if (carrinhoTotaisDto != null)
            {

                pedido.ped_ent_meio = "correios";
                pedido.ped_ent_localizacao = carrinhoTotaisDto.correioDto.co_cidade + "-" + carrinhoTotaisDto.correioDto.co_estado;
                pedido.ped_ent_prazo = carrinhoTotaisDto.cart_entregaPrazoTotal.Value;
                pedido.ped_ent_valor = carrinhoTotaisDto.cart_entregaTotal.Value; ;

                if (carrinhoTotaisDto.cupomDto != null)
                {
                    pedido.cup_id = carrinhoTotaisDto.cupomDto.cup_id;
                    carrinhoTotaisDto.cart_cupomTotal = carrinhoTotaisDto.cupomDto.cup_valor;
                }

                pedido.ped_forPag_nome = "Aguardando";
                pedido.ped_forPag_valorDesconto = 0;
                pedido.ped_forPag_percentualDesconto = 0;
                pedido.ped_forPagPar_condicao = string.Empty;//carrinhoTotaisDto.cart_condicao;
                pedido.ped_forPagPar_quantidade = 0;//Convert.ToInt32(ped_forPagNumParcelaHiddenField.Value);
                pedido.ped_forPagPar_valor = 0;//Convert.ToDecimal(ped_forPagValParcelaHiddenField.Value);
                pedido.ped_forPagPar_percentualJuro = 0;//Convert.ToDecimal(parcPar_perJuroHiddenField.Value);
                pedido.ped_forPag_prazoPagamento = DateTime.Now.AddDays(2);
                pedido.ped_forPag_situacao = "Pendente";
                pedido.ped_forPag_gateway = RadioButtonPagamentoPagSeguro.Checked ? "pagseguro" : null;

                Status status = new StatusTd().SelectFirstStatus(null);
                PedidoStatus pedidoStatus = new PedidoStatus();
                pedidoStatus.loj_id = cliente.loj_id;
                pedidoStatus.ped_id = pedido.ped_id;
                pedidoStatus.stat_id = status.stat_id;
                pedidoStatus.pedStat_dataHora = DateTime.Now;
                pedido.PedidoStatus.Add(pedidoStatus);
                pedido.stat_id = status.stat_id;

                foreach (var car in carrinhoTotaisDto.carrinhoDto)
                {
                    PedidoProduto pedidoProduto = new PedidoProduto();
                    pedidoProduto.ped_id = pedido.ped_id;
                    pedidoProduto.pedPro_car_quantidade = car.car_quantidade;
                    pedidoProduto.pedPro_gru_nome = null;
                    pedidoProduto.pedPro_pro_nome = car.proSku_nome;
                    pedidoProduto.pedPro_proSku_id = car.proSku_id;
                    pedidoProduto.pedPro_proSku_altura = car.proSku_altura;
                    pedidoProduto.pedPro_proSku_largura = car.proSku_largura;
                    pedidoProduto.pedPro_proSku_comprimento = car.proSku_comprimento;
                    pedidoProduto.pedPro_proSku_peso = car.proSku_peso;
                    pedidoProduto.pedPro_proSkuCor_nome = car.proSkuCor_nome;
                    pedidoProduto.pedPro_proSkuTam_nome = car.proSkuTam_nome;
                    pedidoProduto.pedPro_proSku_prazoEntregaAdicional = car.proSku_prazoEntregaAdicional;
                    pedidoProduto.pedPro_proSku_precoCusto = car.proSku_precoCusto;
                    pedidoProduto.pedPro_proSku_precoAnterior = car.proSku_precoAnterior;
                    pedidoProduto.pedPro_proSku_precoVenda = car.proSku_precoVenda;
                    pedidoProduto.pedPro_proSku_precoVendaTotal = car.proSku_precoVenda * car.car_quantidade;
                    pedidoProduto.loj_id = cliente.loj_id;
                    //referencia direta produtoSku//
                    pedidoProduto.proSku_id = car.proSku_id;
                    pedidoProduto.proSku_loj_id = cliente.loj_id;
                    pedidoProduto.proSku_disponivel = true;
                    //
                    pedidoProduto.pedPro_dataHora = DateTime.Now;
                    pedido.PedidoProduto.Add(pedidoProduto);
                }

                pedido.ped_subTotal = carrinhoTotaisDto.cart_subTotal;
                pedido.ped_descontos = (carrinhoTotaisDto.cart_cupomTotal) + pedido.ped_forPag_valorDesconto + ((pedido.ped_forPag_percentualDesconto / 100) * carrinhoTotaisDto.cart_subTotal);
                decimal ped_total = (pedido.ped_subTotal - pedido.ped_descontos);
                pedido.ped_total = (ped_total < 0 ? 0 : ped_total) + pedido.ped_ent_valor;

            }
            else
            {
                Response.Redirect(Page.GetRouteUrl("CadastroPagamento", null));
            }
            return pedido;
        }

        private void InserirPedidoCliente()
        {
            int loj_id = new LojaTd().SelectLoja(null).loj_id;

            Cliente cliente = new Cliente();
            cliente = GetCliente();
            cliente.loj_id = loj_id;

            Pedido pedido = GetPedido(cliente);
            pedido.loj_id = loj_id;
            pedido.Cliente = cliente;

           pedido = new PedidoTd().InsertPedido(null, pedido);

           Session.Remove("cli_email");

           Aut.Autenticar(new CliAut { CliId = pedido.cli_id, CliEmail = pedido.ped_cli_email, CliNome = pedido.ped_cli_nome });
        
          ConcluirPedido(pedido);
        }

        private void ConcluirPedido(Pedido pedido)
        {
            try
            {
                Session["PedidoConcluidoPedId"] = pedido.ped_id;

                string urlPagSeguro = new Transacao().ProcessarPedido(pedido);

                if (urlPagSeguro.StartsWith("http"))
                {
                    Response.Redirect(urlPagSeguro);
                }
                else
                {
                    new PedidoTd().UpdatePedido(new PedidoDto() { ped_id = pedido.ped_id, ped_mensagemErro = urlPagSeguro });
                    _2_Library.Utils.Validacao.Alert(urlPagSeguro);
                    _2_Library.Utils.Validacao.Redirecionar(Page.GetRouteUrl("CadastroPagamento", null));
                }
            }
            catch (Exception ex) {
                new PedidoTd().UpdatePedido(new PedidoDto() {ped_id = pedido.ped_id, ped_mensagemErro = ex.Message });
                _2_Library.Utils.Validacao.Alert("Houve um problema ao processar seu Pedido, por favor tente novamente."+ex.Message);
            }

        }
    }
}
