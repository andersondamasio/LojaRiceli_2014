using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Loja.Modelo;
using Loja.Modelo.Carrinhox;
using Loja.Modelo.Clientex;
using Loja.Modelo.Configuracaox;
using Loja.Modelo.Correiox;
using Loja.Modelo.Cupom;
using Loja.Modelo.FormaPagamentox;
using Loja.Modelo.Parcelamentox;
using Loja.Modelo.Pedidox;
using Loja.Servicos;
using Loja.Utils;
using _2_Library.Modelo;

namespace Loja.Views
{
    public partial class CadastroFinalizar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {         
            if (Request.IsAuthenticated && (Session["clienteLogin"] == null || Session["cli_id"] == null))
            {
                Loja.Modelo.Clientex.ClienteLogin clienteLogin = new Loja.Modelo.Clientex.ClienteConsulta().SelecionarClienteLogin(Convert.ToInt32(HttpContext.Current.User.Identity.Name));
                Session["clienteLogin"] = clienteLogin;
                Session["cli_id"] = clienteLogin.cli_id;
            }

            if (!IsPostBack || Session["carrinhoBean"] == null)
            {
                Session.Remove("carrinhoEntrega");
                PreencherElementos();
            }

            if (Request.IsAuthenticated)
            {
                PanelTipoCliente.Visible = false;
                if (!CheckBoxCadastrarEnderecoAdicional.Checked)
                    ListViewCadastroClienteAdicional.InsertItemPosition = InsertItemPosition.None;
            }
            else
                FormViewCadastroCliente.ChangeMode(FormViewMode.Insert);
        }
        private void PreencherElementos()
        {
            CarrinhoBean carrinhoBean = CalcularCarrinho();

            //produtos
            RepeaterCarrinhoFinalizarPedido.DataSource = carrinhoBean.car_produtos;
            RepeaterCarrinhoFinalizarPedido.DataBind();

            //parcelamento
            car_parcelamentoLiteral.Text = carrinhoBean.car_parcelamento.carPar_condicao;
            ped_forPagNumParcelaHiddenField.Value = carrinhoBean.car_parcelamento.carPar_quantidadeParcelas.ToString();
            ped_forPagValParcelaHiddenField.Value = carrinhoBean.car_parcelamento.carPar_valorParcela.ToString();
            parcPar_perJuroHiddenField.Value = carrinhoBean.car_parcelamento.carPar_percentualJuro.ToString();

            //opçoes de cartaoes para pagamento
            if (!RadioButtonBoleto.Checked)
            {
                ped_forPagCarNomeDropDownList.Items.Clear();
                ped_forPagCarNomeDropDownList.Items.Add(new ListItem(string.Empty, string.Empty));
                ped_forPagCarNomeDropDownList.DataSource = carrinhoBean.car_parcelamento.formasPagamento;
                ped_forPagCarNomeDropDownList.DataBind();
            }

            //opcoes de parcelamento cartao      
            ped_forPagNumParcelaDropDownList.DataSource = carrinhoBean.car_parcelamento.parcelas;
            ped_forPagNumParcelaDropDownList.DataBind();
            ped_forPagNumParcelaDropDownList.SelectedValue = carrinhoBean.car_parcelamento.carPar_quantidadeParcelas.ToString();

            //totais
            car_subTotalLiteral.Text = carrinhoBean.car_subTotal.ToString("c");
            if (carrinhoBean.car_parcelamento.carPar_percentualJuro == 0)
                car_totalLabel.Text = carrinhoBean.car_total.ToString("c");
            else
                car_totalLabel.Text = (carrinhoBean.car_parcelamento.carPar_valorParcela * carrinhoBean.car_parcelamento.carPar_quantidadeParcelas).ToString("c");

            ent_tipoLiteral.Text = carrinhoBean.car_entrega.car_meioEntrega;
            ent_valorLiteral.Text = "R$ " + carrinhoBean.car_entrega.car_valorEntrega;
            ent_prazoLiteral.Text = "Até " + carrinhoBean.car_entrega.car_prazoEntrega + " dias úteis.";
            car_totalEntregaLabel.Text = "R$ " + carrinhoBean.car_entrega.car_valorEntrega;

            Session["carrinhoBean"] = carrinhoBean;
        }

        private CarrinhoBean CalcularCarrinho()
        {
            CarrinhoBean carrinhoBean = new CarrinhoBean();
            
            if (PanelDesconto.Visible && !string.IsNullOrEmpty(car_descontoLiteral.Text.Trim()))
            {
                carrinhoBean.car_cupomTotal = Convert.ToDecimal(car_descontoLiteral.Text);
                car_descontoLiteral.Text = carrinhoBean.car_cupomTotal.ToString();
            }
            else
            {
                carrinhoBean.car_cupomTotal = 0;
                PanelDesconto.Visible = false;
            }

            carrinhoBean.car_totalDesconto = carrinhoBean.car_cupomTotal;
 
            carrinhoBean.car_entrega = CalcularEntrega();
            carrinhoBean.car_totalEntrega = Session["carrinhoEntrega"] == null ? 0 : Convert.ToDecimal(((CarrinhoEntrega)Session["carrinhoEntrega"]).car_valorEntrega);
            carrinhoBean.car_produtos = new CarrinhoConsulta().SelecionarItensCarrinho(carrinhoBean.car_totalDesconto, carrinhoBean.car_totalEntrega);
          

            if (carrinhoBean.car_produtos.Count == 0)
                Response.Redirect(Page.GetRouteUrl("PaginaInicial", null));


            carrinhoBean.car_subTotal = carrinhoBean.car_produtos.Sum(s => (decimal)s.proSku_precoVenda);

            carrinhoBean.car_total = (carrinhoBean.car_produtos.Sum(s => (decimal)s.proSku_precoVenda) - carrinhoBean.car_totalDesconto) + carrinhoBean.car_totalEntrega;

            if (carrinhoBean.car_total < 0)
                carrinhoBean.car_total = 0;

            //pega o parcelamento mais abrangente estre os skus para escolhar de qual usar 
            var carrinhoParcelamento = new ParcelamentoOperacao().SelecionarParcelamentoAbrangente(carrinhoBean.car_produtos);
            //fim----

            carrinhoBean.car_parcelamento = CalcularParcelamento(carrinhoParcelamento, carrinhoBean.car_total);

            return carrinhoBean;
        }

        private CarrinhoEntrega CalcularEntrega()
        {
            CarrinhoEntrega carrinhoEntrega = new CarrinhoEntrega();
            string cli_cep = null;

            if (Request.IsAuthenticated)
            {
                if (CheckBoxCadastrarEnderecoAdicional.Checked)
                {
                    if (ListViewCadastroClienteAdicional.SelectedIndex != -1)
                    {
                        Literal cli_cepLiteral = (Literal)ListViewCadastroClienteAdicional.Items[ListViewCadastroClienteAdicional.SelectedIndex].FindControl("cliEnd_cepLiteral");
                        cli_cep = cli_cepLiteral.Text;
                    }
                }
                else
                {
                    cli_cep = ((ClienteLogin)Session["clienteLogin"]).cli_cep;
                }
            }
            else
            {
                cli_cep = cli_cepHiddenField.Value;
            }

            if (!string.IsNullOrEmpty(cli_cep) && Validacao.ValidaInteiro(cli_cep))
            {
                /*Medidas medidas = new Medidas();
                medidas.totalAltura = car_produtos.Sum(s => (decimal)s.proSku_altura);
                medidas.totalLargura = car_produtos.Sum(s => (decimal)s.proSku_largura);
                medidas.totalComprimento = car_produtos.Sum(s => (decimal)s.proSku_comprimento);
                medidas.totalPeso = car_produtos.Sum(s => (decimal)s.proSku_peso);*/

                carrinhoEntrega = new CorreioConsulta().CalculaPrecoPrazo(Convert.ToInt32(cli_cep));
                PanelEntregaDesativada.Visible = false;
                PanelEntregaAtivada.Visible = true;
            }
            else
            {
                PanelEntregaDesativada.Visible = true;
                PanelEntregaAtivada.Visible = false;
            }

            return carrinhoEntrega;
        }
      
        private CarrinhoParcelamentoBean CalcularParcelamento(dynamic carrinhoParcelamento, decimal car_total)
        {
            CarrinhoParcelamentoBean carrinhoParcelamentoBean = new CarrinhoParcelamentoBean();


            if (RadioButtonBoleto.Checked)
            {
                carrinhoParcelamento.Parcelamento.parc_bloquear = true;
            };

            List<ParcelamentoParcelaBean> parcelas = new List<ParcelamentoParcelaBean>();

            IEnumerable<ParcelamentoParcelaBean> listParcelamentoParcela = carrinhoParcelamento.Parcelamento.Parcelamento_ParcelaBean;

            parcelas = listParcelamentoParcela.ToList();

            if (!RadioButtonBoleto.Checked)
                carrinhoParcelamentoBean.formasPagamento = new FormaPagamentoConsulta().SelecionarFormaPagamento((int)carrinhoParcelamento.Parcelamento.parc_id).Select(s => new FormaPagamentoBean { forPag_nome = s.forPag_nome, forPag_prazoPagamento = s.forPag_prazoPagamento }).ToList();

            int selectedValueNumeroParcela = 1;

            if (ped_forPagNumParcelaDropDownList.SelectedValue != string.Empty)
                selectedValueNumeroParcela = Convert.ToInt32(ped_forPagNumParcelaDropDownList.SelectedValue);

            if (PanelPagamentoCartao.Visible)
            {
                carrinhoParcelamentoBean.carPar_condicao = parcelas.Where(s => s.parcPar_quantidade == selectedValueNumeroParcela).FirstOrDefault().parcPar_condicao;
                carrinhoParcelamentoBean.carPar_quantidadeParcelas = parcelas.Where(s => s.parcPar_quantidade == selectedValueNumeroParcela).FirstOrDefault().parcPar_quantidade;
                carrinhoParcelamentoBean.carPar_valorParcela = parcelas.Where(s => s.parcPar_quantidade == selectedValueNumeroParcela).FirstOrDefault().parcPar_valor;
                carrinhoParcelamentoBean.carPar_percentualJuro = (parcelas.Where(s => s.parcPar_quantidade == selectedValueNumeroParcela).FirstOrDefault().parcPar_percentualJuro ?? 0);
            }
            else
            {
                carrinhoParcelamentoBean.carPar_condicao = parcelas.FirstOrDefault().parcPar_condicao;
                carrinhoParcelamentoBean.carPar_quantidadeParcelas = parcelas.FirstOrDefault().parcPar_quantidade;
                carrinhoParcelamentoBean.carPar_valorParcela = parcelas.FirstOrDefault().parcPar_valor;
                carrinhoParcelamentoBean.carPar_percentualJuro = (parcelas.FirstOrDefault().parcPar_percentualJuro ?? 0);
            }

            carrinhoParcelamentoBean.parcelas = parcelas;


            return carrinhoParcelamentoBean;
        }   

        protected void CheckBoxCadastrarEnderecoAdicional_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxCadastrarEnderecoAdicional.Checked)
            {
                ListViewCadastroClienteAdicional.Visible = true;
                ButtonNovoEnderecoEntrega.Visible = true;

                //ajusta bug do listView
                if (ListViewCadastroClienteAdicional.Items.Count > 0)
                    if (((Literal)ListViewCadastroClienteAdicional.Items[0].FindControl("cliEnd_cepLiteral")).Text == string.Empty)
                        ListViewCadastroClienteAdicional.DataBind();
            }
            else
            {
                ListViewCadastroClienteAdicional.SelectedIndex = -1;
                ListViewCadastroClienteAdicional.Visible = false;
                ButtonNovoEnderecoEntrega.Visible = false;
                PreencherElementos();
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

        protected void ped_forPagNumParcelaDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            PreencherElementos();
        }

        protected void EntityDataSourceFormViewCadastroClienteAdicional_Inserting(object sender, EntityDataSourceChangingEventArgs e)
        {
            ((ClienteEnderecoAdicional)e.Entity).cli_id = Convert.ToInt32(FormViewCadastroCliente.DataKey["cli_id"]);
            ((ClienteEnderecoAdicional)e.Entity).loj_id = Convert.ToInt32(FormViewCadastroCliente.DataKey["loj_id"]);
        }
     
        protected void EntityDataSourceFormViewCadastroClienteAdicional_Inserted(object sender, EntityDataSourceChangedEventArgs e)
        {
            ListViewCadastroClienteAdicional.InsertItemPosition = InsertItemPosition.None;
        }

        protected void ButtonNovoEnderecoEntrega_Click(object sender, EventArgs e)
        {
            ListViewCadastroClienteAdicional.InsertItemPosition = InsertItemPosition.FirstItem;
            ListViewCadastroClienteAdicional.DataBind();
        }

        protected void InsertCancelButton_Click(object sender, EventArgs e)
        {
            ListViewCadastroClienteAdicional.InsertItemPosition = InsertItemPosition.None;
            ListViewCadastroClienteAdicional.DataBind();
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
            {
                cli_cepTextBox.Attributes.Add("onkeyup", "CalculaPrecoPrazoLocalidade(this)");
                if (cli_cepTextBox.Text.Trim() == string.Empty)
                    Session.Remove("carrinhoEntrega");
            }
        }

        protected void ListViewCadastroClienteAdicional_ItemCreated(object sender, ListViewItemEventArgs e)
        {
           TextBox cliEnd_cepTextBox = ((TextBox)e.Item.FindControl("cliEnd_cepTextBox"));

           if (!Request.IsAuthenticated)
           {

               if (cliEnd_cepTextBox != null)
               {
                   cliEnd_cepTextBox.Attributes.Add("onkeyup", "CalculaPrecoPrazoLocalidade(this)");
                   if (cliEnd_cepTextBox.Text.Trim() == string.Empty && CheckBoxCadastrarEnderecoAdicional.Checked)
                       Session.Remove("carrinhoEntrega");
               }

               var insertButton = e.Item.FindControl("InsertButton");
               if (insertButton != null)
                   insertButton.Visible = false;

               var insertCancelButton = e.Item.FindControl("InsertCancelButton");
               if (insertCancelButton != null)
                   insertCancelButton.Visible = false;

               var validators = (from object required in e.Item.Controls
                                 where required is RequiredFieldValidator
                                 select required);
               foreach (RequiredFieldValidator requiredField in validators)
               {
                   requiredField.ValidationGroup = "validGroupFinalizarPedido";
               }
           }
           else {

               if (cliEnd_cepTextBox != null)
               {
                   cliEnd_cepTextBox.Attributes.Add("onkeyup", "SelecionarLocalidade(this)");
                   if (cliEnd_cepTextBox.Text.Trim() == string.Empty && CheckBoxCadastrarEnderecoAdicional.Checked)
                       Session.Remove("carrinhoEntrega");
               }          
           }
        }

        protected void ListViewCadastroClienteAdicional_SelectedIndexChanged(object sender, EventArgs e)
        {
            PreencherElementos();
        }

        protected void ButtonCalculaEntrega_Click(object sender, EventArgs e)
        {
            PreencherElementos();
        }

        protected void ButtonFinalizarPedido_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                CarrinhoBean carrinhoBean = (CarrinhoBean)Session["carrinhoBean"];

                if (carrinhoBean.car_entrega.car_meioEntrega != null)
                {
                    if (RadioButtonBoleto.Checked || RadioButtonCartao.Checked)
                    {
                        if (Request.IsAuthenticated)
                        {
                            if (CheckBoxCadastrarEnderecoAdicional.Checked && ListViewCadastroClienteAdicional.SelectedIndex == -1)
                            {
                                Validacao.Alert(Page, "Atenção, você escolheu a opção '" + CheckBoxCadastrarEnderecoAdicional.Text + "', mas ainda não salvou ou selecionou o endereço desejado. Selecione qual endereço no qual deseja efetuar a entrega.");
                            }
                            else
                                InserirPedido();
                        }
                        else
                            InserirClientePedido();
                    }
                    else
                    {
                        Validacao.Alert(Page, "Por favor escolha uma forma de pagamento");
                    }
                }
                else
                {
                    Validacao.Alert(Page, "Por favor cadastre um Cep válido para calcular o valor de sua entrega.");
                }
            }
            else
            {
                Validacao.Alert(Page, "Alguns dados ainda não foram preenchidos ou salvos, por favor verifique.");
            }
        }

        private void EnviarEmailPedidoRecebido(Pedido pedido)
        {
            ConfiguracaoBean configuracaoBean = new ConfiguracaoConsulta().SelecionarConfiguracao();
            string loj_email = Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio().loj_email;
            string con_emailPedidoRecebido = configuracaoBean.con_emailPedidoRecebido;
            new Smtp().SendEmailAsync(pedido.ped_cli_email, loj_email, "Pedido recebido", null, null, "Pedido recebido", TratarEmail(pedido.ped_id, con_emailPedidoRecebido));
        }

        private string TratarEmail(int pe_id, string mail_corpo)
        {
            Pedido pedido = new PedidoConsulta().SelecionarPedido(pe_id);

            string urlLoja = Request.Url.Authority + Page.GetRouteUrl("PaginaInicial", null);
            string ped_entrega = @"<p>" + pedido.ped_cliEnd_nome + " " + pedido.ped_cliEnd_sobrenome + @"
                <br> 
                " + pedido.ped_cliEnd_endereco + " " + pedido.ped_cliEnd_numero + " " + pedido.ped_cli_complemento + @" 
                <br>
                " + pedido.ped_cli_bairro + " - " + pedido.ped_cli_cidade + " - " + pedido.ped_cli_estado + @"
                <br> 
                CEP " + pedido.ped_cli_cep + @"
                </p>";

            string ped_produtos = string.Empty;
            foreach (PedidoProduto produtoPedido in pedido.PedidoProduto)
            {

                ped_produtos +=
                @"Produto:  " + produtoPedido.pedPro_pro_nome + " " + produtoPedido.pedPro_proSkuCor_nome + " " + produtoPedido.pedPro_proSkuTam_nome + " - " + produtoPedido.pedPro_proSku_id + @" -<br>
                 Qtd:     " + produtoPedido.pedPro_car_quantidade + @"<br>
                 Preço unitário:    " + produtoPedido.pedPro_proSku_precoVenda.ToString("c") + @"<br>
                 SubTotal: " + produtoPedido.pedPro_proSku_precoVendaTotal.ToString("c") + @"<br><br>";
            }

            mail_corpo = mail_corpo.Replace("[PEDIDO_NUMERO]", pedido.ped_id.ToString());
            mail_corpo = mail_corpo.Replace("[PEDIDO_DATAHORA]", pedido.ped_dataHora.ToString());
            mail_corpo = mail_corpo.Replace("[PEDIDO_CONDICAO]", pedido.ped_forPagPar_condicao.ToString());
            mail_corpo = mail_corpo.Replace("[PEDIDO_ENDENTREGA]", ped_entrega);
            mail_corpo = mail_corpo.Replace("[PEDIDO_PRODUTOS]", ped_produtos);
            mail_corpo = mail_corpo.Replace("[PEDIDO_PRAZOENTREGA]", pedido.ped_ent_prazo.ToString());
            mail_corpo = mail_corpo.Replace("[PEDIDO_SUBTOTAL]", pedido.ped_subTotal.ToString("c"));
            mail_corpo = mail_corpo.Replace("[PEDIDO_ENTREGATOTAL]", pedido.ped_ent_valor.ToString("c"));
            mail_corpo = mail_corpo.Replace("[PEDIDO_DESCONTOSTOTAL]", pedido.ped_descontos.ToString("c"));
            mail_corpo = mail_corpo.Replace("[PEDIDO_PEDIDOTOTAL]", pedido.ped_total.ToString("c"));
            mail_corpo = mail_corpo.Replace("[URL]", urlLoja);
            mail_corpo = mail_corpo.Replace("[LOJA_ENDERECO]", string.Empty);

            return mail_corpo;
        }

        private string FormaPagamentoSelecionado()
        {
            string formaPagamento = string.Empty;

            if (RadioButtonBoleto.Checked)
                formaPagamento = "Boleto";
            else
                if (RadioButtonCartao.Checked)
                    formaPagamento = "Cartão";

            return formaPagamento;
        }

        private void InserirClientePedido()
        {
            CarrinhoBean carrinhoBean = ((CarrinhoBean)Session["carrinhoBean"]);

            Cliente cliente = new Cliente();
            cliente.loj_id = Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio().loj_id;
            cliente.cli_senha = ((TextBox)FormViewCadastroCliente.FindControl("cli_senhaTextBox")).Text.Trim();

            cliente.cli_email = ((TextBox)FormViewCadastroCliente.FindControl("cli_emailTextBox")).Text.Trim();
            cliente.cli_cep = ((TextBox)FormViewCadastroCliente.FindControl("cli_cepTextBox")).Text.Replace("-", string.Empty).Trim();
            cliente.cli_endereco = ((TextBox)FormViewCadastroCliente.FindControl("cli_enderecoTextBox")).Text.Trim();
            cliente.cli_numero = ((TextBox)FormViewCadastroCliente.FindControl("cli_numeroTextBox")).Text.Trim();
            cliente.cli_complemento = ((TextBox)FormViewCadastroCliente.FindControl("cli_complementoTextBox")).Text.Trim();
            cliente.cli_referencia = ((TextBox)FormViewCadastroCliente.FindControl("cli_referenciaTextBox")).Text.Trim();
            cliente.cli_bairro = ((TextBox)FormViewCadastroCliente.FindControl("cli_bairroTextBox")).Text.Trim();
            cliente.cli_cidade = ((TextBox)FormViewCadastroCliente.FindControl("cli_cidadeTextBox")).Text.Trim();
            cliente.cli_estado = ((DropDownList)FormViewCadastroCliente.FindControl("cli_estadoDropDownList")).SelectedValue;
            cliente.cli_ddd1 = ((TextBox)FormViewCadastroCliente.FindControl("cli_ddd1TextBox")).Text.Trim();
            cliente.cli_fone1 = ((TextBox)FormViewCadastroCliente.FindControl("cli_fone1TextBox")).Text.Trim();
            cliente.cli_ddd2 = ((TextBox)FormViewCadastroCliente.FindControl("cli_ddd2TextBox")).Text.Trim();
            cliente.cli_fone2 = ((TextBox)FormViewCadastroCliente.FindControl("cli_fone2TextBox")).Text.Trim();
            cliente.cli_ddd3 = ((TextBox)FormViewCadastroCliente.FindControl("cli_ddd3TextBox")).Text.Trim();
            cliente.cli_fone3 = ((TextBox)FormViewCadastroCliente.FindControl("cli_fone3TextBox")).Text.Trim();
            cliente.cli_recebeInformativo = ((CheckBox)FormViewCadastroCliente.FindControl("cli_recebeInformativoCheckBox")).Checked;

            if (FormViewCadastroCliente.FindControl("PanelInformacaoClientePessoaFisica").Visible)
            {
                cliente.cli_cpfCnpj = ((TextBox)FormViewCadastroCliente.FindControl("cli_cpfTextBox")).Text.Trim();
                cliente.cli_nome = ((TextBox)FormViewCadastroCliente.FindControl("cli_nomeTextBox")).Text.Trim();
                cliente.cli_sobrenome = ((TextBox)FormViewCadastroCliente.FindControl("cli_sobrenomeTextBox")).Text.Trim();
                cliente.cli_dataNascimento = Recursos.StringToDate(((DropDownList)FormViewCadastroCliente.FindControl("cli_diaNascimentoDropDownList")).SelectedValue, ((DropDownList)FormViewCadastroCliente.FindControl("cli_mesNascimentoDropDownList")).SelectedValue, ((DropDownList)FormViewCadastroCliente.FindControl("cli_anoNascimentoDropDownList")).SelectedValue);
                cliente.cli_sexo = ((DropDownList)FormViewCadastroCliente.FindControl("cli_sexoDropDownList")).SelectedValue;
            }
            else
                if (FormViewCadastroCliente.FindControl("PanelInformacaoClientePessoaJuridica").Visible)
                {
                    cliente.cli_cpfCnpj = ((TextBox)FormViewCadastroCliente.FindControl("cli_cnpjTextBox")).Text.Trim();
                    cliente.cli_razaoSocial = ((TextBox)FormViewCadastroCliente.FindControl("cli_razaoSocialTextBox")).Text.Trim();
                    cliente.cli_inscricaoEstadual = ((TextBox)FormViewCadastroCliente.FindControl("cli_inscricaoEstadualTextBox")).Text.Trim();
                    cliente.cli_inscricaoEstadualIsento = ((CheckBox)FormViewCadastroCliente.FindControl("cli_inscricaoEstadualInsentoCheckBox")).Checked;
                }

            Pedido pedido = new Pedido();
            pedido.loj_id = cliente.loj_id;
            pedido.cli_id = cliente.cli_id;
            pedido.ped_enderecoIp = Recursos.SelecionarIp();
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

            if (CheckBoxCadastrarEnderecoAdicional.Checked && ListViewCadastroClienteAdicional.InsertItem.FindControl("cliEnd_nomeTextBox") != null && !string.IsNullOrEmpty(((TextBox)ListViewCadastroClienteAdicional.InsertItem.FindControl("cliEnd_nomeTextBox")).Text))
            {
                ClienteEnderecoAdicional clienteEnderecoAdicional = new ClienteEnderecoAdicional();
                clienteEnderecoAdicional.loj_id = cliente.loj_id;
                clienteEnderecoAdicional.cli_id = cliente.cli_id;
                clienteEnderecoAdicional.cliEnd_nome = ((TextBox)ListViewCadastroClienteAdicional.InsertItem.FindControl("cliEnd_nomeTextBox")).Text.Trim();
                clienteEnderecoAdicional.cliEnd_sobrenome = ((TextBox)ListViewCadastroClienteAdicional.InsertItem.FindControl("cliEnd_sobrenomeTextBox")).Text.Trim();
                clienteEnderecoAdicional.cliEnd_email = ((TextBox)ListViewCadastroClienteAdicional.InsertItem.FindControl("cliEnd_emailTextBox")).Text.Trim();
                clienteEnderecoAdicional.cliEnd_cep = ((TextBox)ListViewCadastroClienteAdicional.InsertItem.FindControl("cliEnd_cepTextBox")).Text.Replace("-", string.Empty).Trim();
                clienteEnderecoAdicional.cliEnd_endereco = ((TextBox)ListViewCadastroClienteAdicional.InsertItem.FindControl("cliEnd_enderecoTextBox")).Text.Trim();
                clienteEnderecoAdicional.cliEnd_numero = ((TextBox)ListViewCadastroClienteAdicional.InsertItem.FindControl("cliEnd_numeroTextBox")).Text.Trim();
                clienteEnderecoAdicional.cliEnd_complemento = ((TextBox)ListViewCadastroClienteAdicional.InsertItem.FindControl("cliEnd_complementoTextBox")).Text.Trim();
                clienteEnderecoAdicional.cliEnd_referencia = ((TextBox)ListViewCadastroClienteAdicional.InsertItem.FindControl("cliEnd_referenciaTextBox")).Text.Trim();
                clienteEnderecoAdicional.cliEnd_bairro = ((TextBox)ListViewCadastroClienteAdicional.InsertItem.FindControl("cliEnd_bairroTextBox")).Text.Trim();
                clienteEnderecoAdicional.cliEnd_cidade = ((TextBox)ListViewCadastroClienteAdicional.InsertItem.FindControl("cliEnd_cidadeTextBox")).Text.Trim();
                clienteEnderecoAdicional.cliEnd_estado = ((DropDownList)ListViewCadastroClienteAdicional.InsertItem.FindControl("cliEnd_estadoDropDownList")).SelectedValue;
                clienteEnderecoAdicional.cliEnd_ddd1 = ((TextBox)ListViewCadastroClienteAdicional.InsertItem.FindControl("cliEnd_ddd1TextBox")).Text.Trim();
                clienteEnderecoAdicional.cliEnd_fone1 = ((TextBox)ListViewCadastroClienteAdicional.InsertItem.FindControl("cliEnd_fone1TextBox")).Text.Trim();
                clienteEnderecoAdicional.cliEnd_ddd2 = ((TextBox)ListViewCadastroClienteAdicional.InsertItem.FindControl("cliEnd_ddd2TextBox")).Text.Trim();
                clienteEnderecoAdicional.cliEnd_fone2 = ((TextBox)ListViewCadastroClienteAdicional.InsertItem.FindControl("cliEnd_fone2TextBox")).Text.Trim();
                clienteEnderecoAdicional.cliEnd_ddd3 = ((TextBox)ListViewCadastroClienteAdicional.InsertItem.FindControl("cliEnd_ddd3TextBox")).Text.Trim();
                clienteEnderecoAdicional.cliEnd_fone3 = ((TextBox)ListViewCadastroClienteAdicional.InsertItem.FindControl("cliEnd_fone3TextBox")).Text.Trim();
                clienteEnderecoAdicional.cliEnd_sexo = ((DropDownList)ListViewCadastroClienteAdicional.InsertItem.FindControl("cliEnd_sexoDropDownList")).SelectedValue;
                clienteEnderecoAdicional.cliEnd_recebeInformativo = ((CheckBox)ListViewCadastroClienteAdicional.InsertItem.FindControl("cliEnd_recebeInformativoCheckBox")).Checked;

                cliente.ClienteEnderecoAdicional.Add(clienteEnderecoAdicional);

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

            pedido.ped_ent_meio = carrinhoBean.car_entrega.car_meioEntrega;
            pedido.ped_ent_localizacao = carrinhoBean.car_entrega.car_localizacao;
            pedido.ped_ent_prazo = carrinhoBean.car_entrega.car_prazoEntrega;
            pedido.ped_ent_valor = carrinhoBean.car_entrega.car_valorEntrega;
            pedido.cup_id = ped_cup_idHiddenField.Value != string.Empty ? Convert.ToInt32(ped_cup_idHiddenField.Value) : pedido.cup_id;
            //pedido.ped_cup_valor = carrinhoBean.car_cupomTotal != 0 ? carrinhoBean.car_cupomTotal : pedido.ped_cup_valor;
            pedido.ped_forPag_nome = FormaPagamentoSelecionado();
            pedido.ped_forPag_valorDesconto = 0;
            pedido.ped_forPag_percentualDesconto = 0;
            pedido.ped_forPagPar_condicao = carrinhoBean.car_parcelamento.carPar_condicao;
            pedido.ped_forPagPar_quantidade = carrinhoBean.car_parcelamento.carPar_quantidadeParcelas;//Convert.ToInt32(ped_forPagNumParcelaHiddenField.Value);
            pedido.ped_forPagPar_valor = carrinhoBean.car_parcelamento.carPar_valorParcela;//Convert.ToDecimal(ped_forPagValParcelaHiddenField.Value);
            pedido.ped_forPagPar_percentualJuro = carrinhoBean.car_parcelamento.carPar_percentualJuro;//Convert.ToDecimal(parcPar_perJuroHiddenField.Value);
            pedido.ped_forPag_prazoPagamento = DateTime.Now.AddDays(3);
            pedido.ped_forPag_situacao = "Pendente";

            if (PanelPagamentoCartao.Visible)
            {
                pedido.ped_forPagCar_nome = ped_forPagCarNomeDropDownList.SelectedValue;
                pedido.ped_forPagCar_numero = ped_forPagCarNumeroTextBox.Text.Trim();
                pedido.ped_forPagCar_mesValidade = ped_forPagCarMesVencimentoDropDownList.SelectedValue;
                pedido.ped_forPagCar_anoValidade = ped_forPagCarAnoVencimentoDropDownList.SelectedValue;
                pedido.ped_forPagCar_nomePortador = ped_forPagCarNomePortadorTextBox.Text.Trim();
                pedido.ped_forPagCar_codigoSeguranca = ped_forPagCarCodSegurancaTextBox.Text.Trim();
            }

            List<dynamic> carrinhoConfere = new CarrinhoConsulta().SelecionarItensCarrinhoConfere();


            if (carrinhoBean.car_produtos.Count != carrinhoConfere.Count)
            {
                Validacao.Alert(Page, "Caro cliente, houve uma alteração em seu carrinho, por favor verifique e tente novamente");
            }
            else
            {
                foreach (var car in carrinhoBean.car_produtos)
                {
                    PedidoProduto pedidoProduto = new PedidoProduto();
                    pedidoProduto.loj_id = cliente.loj_id;
                    pedidoProduto.ped_id = pedido.ped_id;
                    pedidoProduto.pedPro_car_quantidade = car.car_quantidade;
                    pedidoProduto.pedPro_gru_nome = null;
                    pedidoProduto.pedPro_pro_nome = car.pro_nome;
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
                    pedidoProduto.pedPro_dataHora = DateTime.Now;
                    pedido.PedidoProduto.Add(pedidoProduto);
                }

                pedido.ped_subTotal = carrinhoBean.car_subTotal;
                pedido.ped_descontos = (carrinhoBean.car_cupomTotal) + pedido.ped_forPag_valorDesconto + ((pedido.ped_forPag_percentualDesconto / 100) * carrinhoBean.car_subTotal);
                //pedido.ped_descontos = carrinhoBean.car_totalDesconto;// (pedido.ped_cup_valor ?? 0) + pedido.ped_forPag_valorDesconto + ((pedido.ped_forPag_percentualDesconto / 100) * carrinhoBean.car_subTotal);
                pedido.ped_total = (pedido.ped_subTotal - pedido.ped_descontos) + pedido.ped_ent_valor;
                pedido.ped_dataHora = DateTime.Now;
                cliente.Pedido.Add(pedido);

                Retorno retorno = new PedidoConsulta().InserirClientePedido(cliente);

                ConcluirPedido(pedido, retorno);
            }
        }

        private void InserirPedido()
        {
            CarrinhoBean carrinhoBean = ((CarrinhoBean)Session["carrinhoBean"]);

            int cli_id = Convert.ToInt32(HttpContext.Current.User.Identity.Name);

            Cliente cliente = new ClienteConsulta().SelecionarCliente(cli_id);

            Pedido pedido = new Pedido();
            pedido.loj_id = cliente.loj_id;
            pedido.cli_id = cliente.cli_id;
            pedido.ped_enderecoIp = Recursos.SelecionarIp();
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

            if (CheckBoxCadastrarEnderecoAdicional.Checked && ListViewCadastroClienteAdicional.SelectedValue != null)
            {
                var cliEnd_id = Convert.ToInt32(ListViewCadastroClienteAdicional.SelectedDataKey["cliEnd_id"]);

                ClienteEnderecoAdicional clienteEnderecoAdicional = new ClienteConsulta().SelecionarClienteEnderecoAdicional(cliEnd_id);

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

            pedido.ped_ent_meio = carrinhoBean.car_entrega.car_meioEntrega;
            pedido.ped_ent_localizacao = carrinhoBean.car_entrega.car_localizacao;
            pedido.ped_ent_prazo = carrinhoBean.car_entrega.car_prazoEntrega;
            pedido.ped_ent_valor = carrinhoBean.car_entrega.car_valorEntrega;
            pedido.cup_id = ped_cup_idHiddenField.Value != string.Empty ? Convert.ToInt32(ped_cup_idHiddenField.Value) : pedido.cup_id;
            //pedido.ped_cup_valor = carrinhoBean.car_cupomTotal != 0 ? carrinhoBean.car_cupomTotal : pedido.ped_cup_valor;
            pedido.ped_forPag_nome = FormaPagamentoSelecionado();
            pedido.ped_forPag_valorDesconto = 0;
            pedido.ped_forPag_percentualDesconto = 0;
            pedido.ped_forPagPar_condicao = carrinhoBean.car_parcelamento.carPar_condicao;
            pedido.ped_forPagPar_quantidade = carrinhoBean.car_parcelamento.carPar_quantidadeParcelas;//Convert.ToInt32(ped_forPagNumParcelaHiddenField.Value);
            pedido.ped_forPagPar_valor = carrinhoBean.car_parcelamento.carPar_valorParcela;//Convert.ToDecimal(ped_forPagValParcelaHiddenField.Value);
            pedido.ped_forPagPar_percentualJuro = carrinhoBean.car_parcelamento.carPar_percentualJuro;//Convert.ToDecimal(parcPar_perJuroHiddenField.Value);
            pedido.ped_forPag_prazoPagamento = DateTime.Now.AddDays(2);
            pedido.ped_forPag_situacao = "Pendente";

            if (PanelPagamentoCartao.Visible)
            {
                pedido.ped_forPag_prazoPagamento = DateTime.Now.AddDays(carrinhoBean.car_parcelamento.formasPagamento.Where(s => s.forPag_nome == ped_forPagCarNomeDropDownList.SelectedValue).FirstOrDefault().forPag_prazoPagamento);
                pedido.ped_forPagCar_nome = ped_forPagCarNomeDropDownList.SelectedValue;
                pedido.ped_forPagCar_numero = ped_forPagCarNumeroTextBox.Text.Trim();
                pedido.ped_forPagCar_mesValidade = ped_forPagCarMesVencimentoDropDownList.SelectedValue;
                pedido.ped_forPagCar_anoValidade = ped_forPagCarAnoVencimentoDropDownList.SelectedValue;
                pedido.ped_forPagCar_nomePortador = ped_forPagCarNomePortadorTextBox.Text.Trim();
                pedido.ped_forPagCar_codigoSeguranca = ped_forPagCarCodSegurancaTextBox.Text.Trim();
            }

            
            List<dynamic> carrinhoConfere = new CarrinhoConsulta().SelecionarItensCarrinhoConfere();


            if (carrinhoBean.car_produtos.Count != carrinhoConfere.Count)
            {
                Validacao.Alert(Page, "Caro cliente, houve uma alteração em seu carrinho, por favor verifique e tente novamente");
            }
            else
            {
                foreach (var car in carrinhoBean.car_produtos)
                {
                    PedidoProduto pedidoProduto = new PedidoProduto();
                    pedidoProduto.loj_id = cliente.loj_id;
                    pedidoProduto.ped_id = pedido.ped_id;
                    pedidoProduto.pedPro_car_quantidade = car.car_quantidade;
                    pedidoProduto.pedPro_gru_nome = null;
                    pedidoProduto.pedPro_pro_nome = car.pro_nome;
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
                    pedidoProduto.pedPro_dataHora = DateTime.Now;
                    pedido.PedidoProduto.Add(pedidoProduto);
                }

                pedido.ped_subTotal = carrinhoBean.car_subTotal;
                pedido.ped_descontos = (carrinhoBean.car_cupomTotal) + pedido.ped_forPag_valorDesconto + ((pedido.ped_forPag_percentualDesconto / 100) * carrinhoBean.car_subTotal);
                pedido.ped_total = (pedido.ped_subTotal - pedido.ped_descontos) + pedido.ped_ent_valor;
                pedido.ped_dataHora = DateTime.Now;

                Retorno retorno = new PedidoConsulta().InserirPedido(pedido);

                ConcluirPedido(pedido, retorno);
            }
        }

        private void ConcluirPedido(Pedido pedido, Retorno retorno)
        {
            if (retorno.menSis_id == 0)
            {
                if (!Request.IsAuthenticated)
                {
                    //--inicio--Usado para logar o cliente no sistema///
                    ClienteLogin clienteLogin = new ClienteLogin() { cli_id = pedido.cli_id, cli_cep = pedido.ped_cli_cep };
                    System.Web.Security.FormsAuthentication.SetAuthCookie(clienteLogin.cli_id.ToString(), false);
                    Session["clienteLogin"] = clienteLogin;
                    Session["cli_id"] = clienteLogin.cli_id;
                    //--fim-------------------------------------///
                }

                //--inicio--Usado temporariamente até concluir o pedido///
                Session["ped_id"] = pedido.ped_id;
                Session["ped_forPag_nome"] = pedido.ped_forPag_nome;
                //--fim-------------------------------------///

                EnviarEmailPedidoRecebido(pedido);
                Response.Redirect(Page.GetRouteUrl("PedidoConcluido", null));
            }
            else
            {
                Validacao.Alert(Page, retorno.menSis_mensagem);
            }
        }

        protected void RadioButtonBoleto_CheckedChanged(object sender, EventArgs e)
        {
            PanelPagamentoBoleto.Visible = true;
            PanelPagamentoCartao.Visible = false;
            PreencherElementos();
        }

        protected void RadioButtonCartao_CheckedChanged(object sender, EventArgs e)
        {
            PanelPagamentoBoleto.Visible = false;
            PanelPagamentoCartao.Visible = true;
            PreencherElementos();
        }

        protected void ButtonAplicarCupom_Click(object sender, EventArgs e)
        {
            string cup_chave = car_cupomTextBox.Text.Trim();
            int? cli_id = (int?)Session["cli_id"] ?? null;

            Retorno retorno = new CupomConsulta().SelecionarCupom(cup_chave, cli_id);
            Cupom cupom = (Cupom)retorno.objeto;

           if (cupom != null)
           {
               car_cupomMensagemLiteral.Text = "<br/>Cupom aplicado.";
               car_descontoLiteral.Text = cupom.cup_valor.ToString();
               ped_cup_idHiddenField.Value = cupom.cup_id.ToString();
               PanelDesconto.Visible = true;
               PreencherElementos();
           }
           else {
               car_cupomMensagemLiteral.Text = "<br>"+retorno.menSis_mensagem;
               ped_cup_idHiddenField.Value = string.Empty;
               PanelDesconto.Visible = false;
           }
        }
    }
}