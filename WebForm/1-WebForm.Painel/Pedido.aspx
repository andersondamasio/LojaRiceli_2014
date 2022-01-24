<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pedido.aspx.cs" Inherits="Loja.Painel.Pedido" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="Cabecalho.ascx" TagName="Cabecalho" TagPrefix="uc1" %>
<%@ Register Src="Rodape.ascx" TagName="Rodape" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
      <table width="1024px" align="center">
          <tr>
              <td>
          <uc1:Cabecalho ID="Cabecalho1" runat="server" />
            <asp:ListView ID="ListViewPedidos" runat="server" Style="width:100%" DataSourceID="EntityDataSourcePedidos" DataKeyNames="ped_id" OnSelectedIndexChanging="ListViewPedidos_SelectedIndexChanging" OnPagePropertiesChanging="ListViewPedidos_PagePropertiesChanging">
                <EmptyDataTemplate>
                    <table runat="server" style="" width="100%">
                        <tr>
                            <td>Nenhum dado foi retornado.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr style="border: 2px solid black">
                        <td>
                            <asp:Label ID="ped_idLabel" runat="server" Text='<%# Eval("ped_id") %>' />
                        </td>
                            <td>
                            <asp:Label ID="ped_dataHoraLabel" runat="server" Text='<%# Eval("ped_dataHora") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ped_cli_emailLabel" runat="server" Text='<%# Eval("ped_cli_email") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ped_cli_cpfCnpjLabel" runat="server" Text='<%# Eval("ped_cli_cpfCnpj") %>' />
                        </td>
                        <td>
                            <%# Eval("ped_cli_nome") %> <%# Eval("ped_cli_sobrenome") %>
                        </td>
                        <td>
                            <asp:Label ID="ped_cli_cidadeLabel" runat="server" Text='<%# Eval("ped_cli_cidade") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ped_cli_estadoLabel" runat="server" Text='<%# Eval("ped_cli_estado") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ped_forPag_nomeLabel" runat="server" Text='<%# Eval("ped_forPag_nome") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ped_totalLabel" runat="server" Text='<%# Eval("ped_total","{0:N}") %>' />
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("stat_nome") %>' />
                        </td>
                    
                        <td>
                            <asp:LinkButton ID="LinkButtonSelecionar" runat="server" Text="Selecionar" CommandName="Select">Selecionar</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <SelectedItemTemplate>
                    <tr style="border: 2px solid black; background-color: blue">
                        <td>
                            <asp:Label ID="ped_idLabel" runat="server" Text='<%# Eval("ped_id") %>' />
                        </td>
                          <td>
                            <asp:Label ID="ped_dataHoraLabel" runat="server" Text='<%# Eval("ped_dataHora") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ped_cli_emailLabel" runat="server" Text='<%# Eval("ped_cli_email") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ped_cli_cpfCnpjLabel" runat="server" Text='<%# Eval("ped_cli_cpfCnpj") %>' />
                        </td>
                        <td>
                            <%# Eval("ped_cli_nome") %> <%# Eval("ped_cli_sobrenome") %>
                        </td>
                        <td>
                            <asp:Label ID="ped_cli_cidadeLabel" runat="server" Text='<%# Eval("ped_cli_cidade") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ped_cli_estadoLabel" runat="server" Text='<%# Eval("ped_cli_estado") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ped_forPag_nomeLabel" runat="server" Text='<%# Eval("ped_forPag_nome") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ped_totalLabel" runat="server" Text='<%# Eval("ped_total","{0:N}") %>' />
                        </td>
                         <td>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("stat_nome") %>' />
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButtonSelecionar" runat="server" Text="Selecionar" CommandName="Select">Selecionar</asp:LinkButton>
                        </td>
                    </tr>
                </SelectedItemTemplate>
                <LayoutTemplate>
                    <table runat="server" width="100%">
                        <tr runat="server">
                            <td runat="server">
                                <table id="itemPlaceholderContainer" runat="server" border="1" style="" width="100%">
                                    <tr runat="server" style="">
                                        
                                        <th runat="server"><asp:LinkButton runat="server" ID="LinkButton3" Text="Cód." CommandName="Sort" CommandArgument="ped_id" /></th>
                                        <th runat="server"><asp:LinkButton runat="server" ID="LinkButton1" Text="DataHora" CommandName="Sort" CommandArgument="PED_dataHora" /></th>
                                        <th runat="server"><asp:LinkButton runat="server" ID="LinkButton2" Text="Email" CommandName="Sort" CommandArgument="ped_cli_email" /></th>
                                        <th runat="server"><asp:LinkButton runat="server" ID="LinkButton4" Text="CpfCnpj" CommandName="Sort" CommandArgument="ped_cli_cpfCnpj" /></th>
                                        <th runat="server"><asp:LinkButton runat="server" ID="LinkButton5" Text="Nome" CommandName="Sort" CommandArgument="ped_cli_nome" /></th>
                                        <th runat="server"><asp:LinkButton runat="server" ID="LinkButton6" Text="Cidade" CommandName="Sort" CommandArgument="ped_cli_cidade" /></th>
                                        <th runat="server"><asp:LinkButton runat="server" ID="LinkButton7" Text="Uf" CommandName="Sort" CommandArgument="ped_cli_estado" /></th>
                                        <th runat="server"><asp:LinkButton runat="server" ID="LinkButton8" Text="FormaPag." CommandName="Sort" CommandArgument="ped_forPag_nome" /></th>
                                        <th runat="server"><asp:LinkButton runat="server" ID="LinkButton9" Text="Total" CommandName="Sort" CommandArgument="ped_total" /></th>
                                        <th runat="server"><asp:LinkButton runat="server" ID="LinkButton10" Text="Status" CommandName="Sort" CommandArgument="stat_nome" /></th>
                                        <th></th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
            </asp:ListView>
            <asp:DataPager ID="DataPagerPedidos" runat="server" PageSize="5" PagedControlID="ListViewPedidos">
                                                <Fields>
                                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False"
                                                        ShowPreviousPageButton="true" FirstPageText="Primeiro" LastPageText="Último"
                                                        NextPageText="Próximo" PreviousPageText="Anterior" />
                                                    <asp:NumericPagerField Visible="false" />
                                                    <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="true"
                                                        ShowPreviousPageButton="false" FirstPageText="Primeiro" LastPageText="Último"
                                                        NextPageText="Próximo" PreviousPageText="Anterior" />
                                                    <asp:TemplatePagerField>
                                                        <PagerTemplate> 
                                                            <b>registros de
                                                            <asp:Label runat="server" ID="CurrentPageLabel" Text="<%# Container.StartRowIndex %>" />
                                                                a
                                                            <asp:Label runat="server" ID="TotalPagesLabel" Text="<%# Container.StartRowIndex+Container.PageSize %>" />
                                                                (<asp:Label runat="server" ID="TotalItemsLabel" Text="<%# Container.TotalRowCount%>" />
                                                                registros) </b>

                                                            <%# (this.DataPagerPedidos.Visible = Container.TotalRowCount >= this.DataPagerPedidos.PageSize).ToString().Replace("True",string.Empty) %>

                                                        </PagerTemplate>
                                                    </asp:TemplatePagerField>
                                                </Fields>
                                            </asp:DataPager>
            <asp:EntityDataSource ID="EntityDataSourcePedidos" runat="server" ConnectionString="name=LojaEntities"
                DefaultContainerName="LojaEntities" EnableFlattening="False" EntitySetName="Pedido"
                Select="it.[ped_id], it.[ped_cli_nome],it.[ped_cli_sobreNome], it.[ped_cli_cpfCnpj], it.[ped_cli_email],
             it.[ped_cli_cidade], it.[ped_cli_estado], it.[ped_forPag_nome], it.[ped_forPagPar_quantidade], it.[ped_forPagPar_valor],
             it.[ped_subTotal], it.[ped_descontos], it.[ped_total], it.[ped_dataHora], DEREF(NAVIGATE(it, LojaModel.FK_Pedido_Status)).stat_nome"
                OrderBy="it.[ped_id] DESC"  Where="it.[loj_id] = @loj_id">
                <WhereParameters>
                    <asp:SessionParameter Type="Int32"  Name="loj_id" SessionField="loj_id" />
                </WhereParameters>
            </asp:EntityDataSource>


            <asp:FormView ID="FormViewFormPedidos" runat="server" Style="width:100%" DataKeyNames="ped_id,loj_id" DataSourceID="EntityDataSourceFormPedidos">
                <HeaderTemplate>
                    <div>
                        <b>Dados do Pedido</b>
                    </div>
                </HeaderTemplate>
                <ItemTemplate>
                    Código:
                <asp:Label ID="ped_idLabel" runat="server" Text='<%# Eval("ped_id") %>' />
                    <br />
                    Data e Hora:
                     <asp:Label ID="ped_dataHoraLabel" runat="server" Text='<%# Bind("ped_dataHora") %>' />
                     <br/>
                    Url de Origem:
                <asp:Label ID="ped_urlOrigemLabel" runat="server" Text='<%# Bind("ped_urlOrigem") %>' />
                    <br/>
                    Endereco IP:
                <asp:Label ID="ped_enderecoIpLabel" runat="server" Text='<%# Bind("ped_enderecoIp") %>' />
                    <br />

                    <table border="2" width="100%">
                        <tr valign="top">
                          
                            <td>
                                  <div><b>Cobrança</b></div>
                                CpfCnpj:
                <asp:Label ID="ped_cli_cpfCnpjLabel" runat="server" Text='<%# Bind("ped_cli_cpfCnpj") %>' />
                                <br />
                                Nome:
                                <%# Eval("ped_cli_nome") %> <%# Eval("ped_cli_sobrenome") %>
                                <br />
                                Email:
                <asp:Label ID="ped_cli_emailLabel" runat="server" Text='<%# Bind("ped_cli_email") %>' />
                                <br />
                                Cep:
                <asp:Label ID="ped_cli_cepLabel" runat="server" Text='<%# Bind("ped_cli_cep") %>' />
                                <br />
                                Endereço:
                <asp:Label ID="ped_cli_enderecoLabel" runat="server" Text='<%# Bind("ped_cli_endereco") %>' />
                                <br />
                                Número:
                <asp:Label ID="ped_cli_numeroLabel" runat="server" Text='<%# Bind("ped_cli_numero") %>' />
                                <br />
                                Complemento:
                <asp:Label ID="ped_cli_complementoLabel" runat="server" Text='<%# Bind("ped_cli_complemento") %>' />
                                <br />
                                Referência:
                <asp:Label ID="ped_cli_referenciaLabel" runat="server" Text='<%# Bind("ped_cli_referencia") %>' />
                                <br />
                                Bairro:
                <asp:Label ID="ped_cli_bairroLabel" runat="server" Text='<%# Bind("ped_cli_bairro") %>' />
                                <br />
                                Cidade:
                <asp:Label ID="ped_cli_cidadeLabel" runat="server" Text='<%# Bind("ped_cli_cidade") %>' />
                                <br />
                                Estado:
                <asp:Label ID="ped_cli_estadoLabel" runat="server" Text='<%# Bind("ped_cli_estado") %>' />
                                <br />
                                Fone1: <%# Eval("ped_cli_ddd1") %> <%# Eval("ped_cli_fone1") %>
                                <br />
                                Fone2: <%# Eval("ped_cli_ddd2") %> <%# Eval("ped_cli_fone2") %>
                                <br />
                                Fone3: <%# Eval("ped_cli_ddd3") %> <%# Eval("ped_cli_fone3") %>
                                <br />
                        
                        <asp:Panel ID="PanelInformacaoClientePessoaFisica" runat="server" Visible='<%# Eval("ped_cli_cpfCnpj").ToString().Length == 11 %>'>
                                Data Nascimento:
                <asp:Label ID="ped_cli_dataNascimentoLabel" runat="server" Text='<%# Bind("ped_cli_dataNascimento","{0:dd/MM/yyyy}") %>' />
                                <br />
                                Sexo:
                <asp:Label ID="ped_cli_sexoLabel" runat="server" Text='<%# Bind("ped_cli_sexo") %>' />
                                <br />
                                </asp:Panel>

                               <asp:Panel ID="PanelInformacaoClientePessoaJuridica" runat="server" Visible='<%# Eval("ped_cli_cpfCnpj").ToString().Length == 14 %>'>
                                Razao Social:
                <asp:Label ID="ped_cli_razaoSocialLabel" runat="server" Text='<%# Bind("ped_cli_razaoSocial") %>' />
                                <br />
                                Inscrição Estadual:
                <asp:Label ID="ped_cli_inscricaoEstadualLabel" runat="server" Text='<%# Bind("ped_cli_inscricaoEstadual") %>' />
                                <br />
                                Inscrição Estadual Isento:
                <asp:CheckBox ID="ped_cli_inscricaoEstadualIsentoCheckBox" runat="server" Checked='<%# Eval("ped_cli_inscricaoEstadualIsento") ?? false %>' Enabled="false" />
                                <br />
                                      </asp:Panel>
                            </td>
                            <td>
                                  <div><b>Entrega</b></div>
                                Nome:
                <%# Eval("ped_cliEnd_nome") %> <%# Eval("ped_cliEnd_sobrenome") %>
                                <br />
                                Email:
                <asp:Label ID="ped_cliEnd_emailLabel" runat="server" Text='<%# Bind("ped_cliEnd_email") %>' />
                                <br />
                                Cep:
                <asp:Label ID="ped_cliEnd_cepLabel" runat="server" Text='<%# Bind("ped_cliEnd_cep") %>' />
                                <br />
                                Endereço:
                <asp:Label ID="ped_cliEnd_enderecoLabel" runat="server" Text='<%# Bind("ped_cliEnd_endereco") %>' />
                                <br />
                                Número:
                <asp:Label ID="ped_cliEnd_numeroLabel" runat="server" Text='<%# Bind("ped_cliEnd_numero") %>' />
                                <br />
                                Complemento:
                <asp:Label ID="ped_cliEnd_complementoLabel" runat="server" Text='<%# Bind("ped_cliEnd_complemento") %>' />
                                <br />
                                Referência:
                <asp:Label ID="ped_cliEnd_referenciaLabel" runat="server" Text='<%# Bind("ped_cliEnd_referencia") %>' />
                                <br />
                                Bairro:
                <asp:Label ID="ped_cliEnd_bairroLabel" runat="server" Text='<%# Bind("ped_cliEnd_bairro") %>' />
                                <br />
                                Cidade:
                <asp:Label ID="ped_cliEnd_cidadeLabel" runat="server" Text='<%# Bind("ped_cliEnd_cidade") %>' />
                                <br />
                                Estado:
                <asp:Label ID="ped_cliEnd_estadoLabel" runat="server" Text='<%# Bind("ped_cliEnd_estado") %>' />
                                <br />
                                Fone1: <%# Eval("ped_cliEnd_ddd1") %> <%# Eval("ped_cliEnd_fone1") %>
                                <br />
                                Fone2: <%# Eval("ped_cliEnd_ddd2") %> <%# Eval("ped_cliEnd_fone2") %>
                                <br />
                                Fone3: <%# Eval("ped_cliEnd_ddd3") %> <%# Eval("ped_cliEnd_fone3") %>
                             </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                <div><b>Valores de entrega</b></div>
                                Tipo:
                <asp:Label ID="ped_ent_tipoLabel" runat="server" Text='<%# Bind("ped_ent_tipo") %>' />
                                <br />
                                Meio:
                <asp:Label ID="ped_ent_meioLabel" runat="server" Text='<%# Bind("ped_ent_meio") %>' />
                                <br />
                                Localização:
                <asp:Label ID="ped_ent_localizacaoLabel" runat="server" Text='<%# Bind("ped_ent_localizacao") %>' />
                                <br />
                                Prazo de entrega:
                <asp:Label ID="ped_ent_prazoLabel" runat="server" Text='<%# Bind("ped_ent_prazo") %>' />
                                <br />
                                Valor:
                <asp:Label ID="ped_ent_valorLabel" runat="server" Text='<%# Bind("ped_ent_valor","{0:N}") %>' />
                                <br />
                            </td>
                            <td>
                                <div><b>Cupom de desconto</b></div>

                                Código:
                <asp:Label ID="ped_cup_idLabel" runat="server" Text='<%# Bind("cup_id") %>' /><br/>
                                Chave:
                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Cupom.cup_chave") %>' />
                                <br />
                                Valor:
                <asp:Label ID="ped_cup_valorLabel" runat="server" Text='<%# Bind("Cupom.cup_valor","{0:N}") %>' />
                                <br />

                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                <div><b>Forma de Pagamento</b></div>

                                Nome:
                <asp:Label ID="ped_forPag_nomeLabel" runat="server" Font-Bold="true" Text='<%# Bind("ped_forPag_nome") %>' />
                                <span style="visibility:<%# (string)Eval("ped_forPag_nome") == "Boleto" ? string.Empty : "hidden" %>">
                             <a href="?ped_id=<%# Eval("ped_id") %>&cli_id=<%# Eval("cli_id") %>" target="_blank">Gerar Boleto</a>
                                 </span> <br />
                                Prazo de Pagamento:
                <asp:Label ID="ped_forPag_prazoPagamentoLabel" runat="server" Text='<%# Bind("ped_forPag_prazoPagamento","{0:dd/MM/yyyy}") %>' />
                                <br />
                                Valor de Descontos:
                <asp:Label ID="ped_forPag_valorDescontoLabel" runat="server" Text='<%# Bind("ped_forPag_valorDesconto","{0:N}") %>' />
                                <br />
                                Percentual de descontos:
                <asp:Label ID="ped_forPag_percentualDescontoLabel" runat="server" Text='<%# Bind("ped_forPag_percentualDesconto") %>' />
                                <br />
                                Situação:
                <asp:Label ID="ped_forPag_situacaoLabel" runat="server" Text='<%# Bind("ped_forPag_situacao") %>' />
                                <br />
                            </td>
                            <td>
                                <div><b>Forma de Parcelamento</b></div>
                                Condição:
                <asp:Label ID="ped_forPagPar_condicaoLabel" runat="server" Text='<%# Bind("ped_forPagPar_condicao") %>' />
                               
                                <br />
                                Quant. de Parcelas
                <asp:Label ID="ped_forPagPar_quantidadeLabel" runat="server" Text='<%# Bind("ped_forPagPar_quantidade") %>' />
                                <br />
                                Valor das Parcelas:
                <asp:Label ID="ped_forPagPar_valorLabel" runat="server" Text='<%# Bind("ped_forPagPar_valor","{0:N}") %>' />
                                <br />
                                Percentual de Juro:
                <asp:Label ID="ped_forPagPar_percentualJuroLabel" runat="server" Text='<%# Bind("ped_forPagPar_percentualJuro") %>' />
                                <br />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td style="visibility:<%# (string)Eval("ped_forPag_nome") == "Cartão" ? string.Empty : "hidden" %>">
                                <div><b>Dados do Cartao</b></div>
                    Nome do Portador:
                <asp:Label ID="ped_forPagCar_nomePortadorLabel" runat="server" Text='<%# Bind("ped_forPagCar_nomePortador") %>' />
                    <br />
                    Nome do Cartão:
                <asp:Label ID="ped_forPagCar_nomeLabel" runat="server" Text='<%# Bind("ped_forPagCar_nome") %>' />
                    <br />
                    Número:
                <asp:Label ID="ped_forPagCar_numeroLabel" runat="server" Text='<%# Bind("ped_forPagCar_numero") %>' />
                    <br />
                    Mes de validade:
                <asp:Label ID="ped_forPagCar_mesValidadeLabel" runat="server" Text='<%# Bind("ped_forPagCar_mesValidade") %>' />
                    <br />
                    Ano de Validade:
                <asp:Label ID="ped_forPagCar_anoValidadeLabel" runat="server" Text='<%# Bind("ped_forPagCar_anoValidade") %>' />
                    <br />
                    Código de Segurança:
                <asp:Label ID="ped_forPagCar_codigoSegurancaLabel" runat="server" Text='<%# Bind("ped_forPagCar_codigoSeguranca") %>' />
                    <br />
                                </td>
                            <td>
                                <div><b>Totais</b></div>
                    SubTotal:
                <asp:Label ID="ped_subTotalLabel" runat="server" Text='<%# Bind("ped_subTotal","{0:N}") %>' />
                    <br />
                    Descontos:
                <asp:Label ID="ped_descontosLabel" runat="server" Text='<%# Bind("ped_descontos","{0:N}") %>' />
                    <br />
                 Entrega:
                 <%# Eval("ped_ent_valor") %>
                    <br />
                    Total:
                <asp:Label ID="ped_totalLabel" runat="server" Text='<%# Bind("ped_total","{0:N}") %>' />
                    </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table border="1" width="100%">
                                    <tr>
                                        <td colspan="7"><b>Produtos</b></td>
                                    </tr>
                                    <tr>
                                        <td>Sku</td>
                                        <td>Nome</td>
                                        <td>Cor</td>
                                        <td>Tamanho</td>
                                        <td>Quant.</td>
                                        <td>PrecoVenda</td>
                                        <td>Total</td>
                                    </tr>
                                    <tr>
                               <asp:Repeater ID="RepeaterPedidoProduto" runat="server" DataSource='<%# Eval("PedidoProduto") %>'>
                                   <ItemTemplate>
                                     <tr>
                                                <td><%# Eval("pedPro_proSku_id") %></td>
                                                <td><%# Eval("pedPro_pro_nome") %></td>
                                                <td><%# Eval("pedPro_proSkuCor_nome") %></td>
                                                <td><%# Eval("pedPro_proSkuTam_nome") %></td>
                                                <td><%# Eval("pedPro_car_quantidade") %></td>
                                                <td><%# Eval("pedPro_proSku_precoVenda","{0:N}") %></td>
                                                <td><%# Eval("pedPro_proSku_precoVendaTotal","{0:N}") %></td>
                                            </tr>
                                   </ItemTemplate>
                               </asp:Repeater>
                                </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:FormView> 
            <asp:EntityDataSource ID="EntityDataSourceFormPedidos" runat="server" ConnectionString="name=LojaEntities" DefaultContainerName="LojaEntities"
                 EnableFlattening="False" EntitySetName="Pedido" Include="PedidoProduto,Cupom" Where="it.[ped_id] = @ped_id and it.[loj_id] = @loj_id">
                <WhereParameters>
                    <asp:SessionParameter Type="Int32"  Name="loj_id" SessionField="loj_id" />
                    <asp:ControlParameter ControlID="ListViewPedidos" Name="ped_id" Type="Int32" PropertyName="SelectedDataKey.Values[ped_id]" />
                </WhereParameters>
            </asp:EntityDataSource>

           
            <asp:ListView ID="ListViewPedidoStatus"  runat="server" Style="width:100%" DataKeyNames="loj_id,ped_id,pedStat_id" 
                DataSourceID="EntityDataSourcePedidoStatus" InsertItemPosition="FirstItem" Visible="false">
                <EditItemTemplate>
                     <tr style="vertical-align:top">          
                        <td>
                             <%# Eval("Status.stat_nome") %>
                        </td>
                        <td>
                           <%# Eval("pedStat_descricao") %>
                        </td>
                        <td>
                            <asp:TextBox ID="pedStat_observacaoTextBox" runat="server" TextMode="MultiLine" Text='<%# Bind("pedStat_observacao") %>' />
                        </td>
                        <td>
                            
                        </td>
                          <td>
                            <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Atualizar" />
                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancelar" />
                        </td>
                    </tr>
                </EditItemTemplate>
                <EmptyDataTemplate>
                    <table runat="server" style="" width="100%">
                        <tr>
                            <td>Nenhum dado foi retornado.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <InsertItemTemplate>
                    <tr style="vertical-align:top">
                        <td>
                            <asp:DropDownList ID="pedStatus_idDropDownList" runat="server" 
                                DataSourceID="EntityDataSourceStatus" 
                                DataTextField="stat_nome" DataValueField="stat_id"
                                SelectedValue='<%# Bind("stat_id") %>' AppendDataBoundItems="true">
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="pedStatus_idDropDownList" Display="Dynamic" ErrorMessage="Escolha o novo Status" ValidationGroup="groupStatusInsert"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="stat_descricaoTextBox" runat="server" TextMode="MultiLine" Text='<%# Bind("pedStat_descricao") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="stat_observacaoTextBox" runat="server" TextMode="MultiLine" Text='<%# Bind("pedStat_observacao") %>' />
                        </td>
                        <td> 
                            <asp:CheckBox ID="CheckBoxEnviarEmail" runat="server" Checked="true" Text="Enviar email ao cliente?"/>
                        </td>
                          <td>
                            <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Salvar" ValidationGroup="groupStatusInsert" OnClientClick=" if(Page_ClientValidate('groupStatusInsert')) return confirm('Tem certeza que deseja incluir este novo status?');" />
                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Limpar" />
                        </td>
                    </tr>
                </InsertItemTemplate>
                <ItemTemplate>
                    <tr style="">
                        <td>
                            <%# Eval("Status.stat_nome") %>
                        </td>
                        <td>
                            <asp:Label ID="pedStat_descricaoLabel" runat="server" Text='<%# Eval("pedStat_descricao") %>' />
                        </td>
                        <td>
                            <asp:Label ID="pedStat_observacaoLabel" runat="server" Text='<%# Eval("pedStat_observacao") %>' />
                        </td>
                        <td>
                            <asp:Label ID="pedStat_dataHoraLabel" runat="server" Text='<%# Eval("pedStat_dataHora") %>' />
                        </td>
                        <td>
                            <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Editar" />
                        </td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table runat="server" width="100%">
                        <tr runat="server">
                            <td runat="server">
                                <table id="itemPlaceholderContainer" runat="server" border="0" style="" width="100%">
                                    <tr runat="server" style="">
                                        <th runat="server">Nome</th>
                                        <th runat="server">Descrição</th>
                                        <th runat="server">Observação</th>
                                        <th runat="server"></th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>  
            </asp:ListView>
            <asp:DataPager ID="DataPagerPedidoStatus" runat="server" PageSize="5" PagedControlID="ListViewPedidoStatus">
                                                <Fields>
                                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False"
                                                        ShowPreviousPageButton="true" FirstPageText="Primeiro" LastPageText="Último"
                                                        NextPageText="Próximo" PreviousPageText="Anterior" />
                                                    <asp:NumericPagerField Visible="false" />
                                                    <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="true"
                                                        ShowPreviousPageButton="false" FirstPageText="Primeiro" LastPageText="Último"
                                                        NextPageText="Próximo" PreviousPageText="Anterior" />
                                                    <asp:TemplatePagerField>
                                                        <PagerTemplate> 
                                                            <b>registros de
                                                            <asp:Label runat="server" ID="CurrentPageLabel" Text="<%# Container.StartRowIndex %>" />
                                                                a
                                                            <asp:Label runat="server" ID="TotalPagesLabel" Text="<%# Container.StartRowIndex+Container.PageSize %>" />
                                                                (<asp:Label runat="server" ID="TotalItemsLabel" Text="<%# Container.TotalRowCount%>" />
                                                                registros) </b>

                                                            <%# (this.DataPagerPedidoStatus.Visible = Container.TotalRowCount >= this.DataPagerPedidoStatus.PageSize).ToString().Replace("True",string.Empty) %>

                                                        </PagerTemplate>
                                                    </asp:TemplatePagerField>
                                                </Fields>
                                            </asp:DataPager>
            <asp:EntityDataSource ID="EntityDataSourcePedidoStatus" runat="server" ConnectionString="name=LojaEntities" Include="Status"
                 DefaultContainerName="LojaEntities" EnableFlattening="False" EntitySetName="PedidoStatus" 
                EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeFilter="PedidoStatus"
                 Where="it.[ped_id] = @ped_id and it.[loj_id] = @loj_id" OnInserting="EntityDataSourcePedidoStatus_Inserting" OrderBy="it.[pedStat_id] DESC" OnInserted="EntityDataSourcePedidoStatus_Inserted">
                <WhereParameters>
                    <asp:ControlParameter ControlID="ListViewPedidos" Name="ped_id" PropertyName="SelectedValue" Type="Int32" />
                    <asp:SessionParameter Type="Int32"  Name="loj_id" SessionField="loj_id" />
                </WhereParameters>
            </asp:EntityDataSource>
            <asp:EntityDataSource ID="EntityDataSourceStatus" runat="server" ConnectionString="name=LojaEntities"
                 DefaultContainerName="LojaEntities" EnableFlattening="False" EntitySetName="Status"
                 Select="it.[stat_id], it.[stat_nome]" Where="it.[loj_id] = @loj_id">
                 <WhereParameters>
                    <asp:SessionParameter Type="Int32"  Name="loj_id" SessionField="loj_id" />
                </WhereParameters>
        </asp:EntityDataSource>
       
        <uc1:Rodape ID="Rodape1" runat="server" />
         </td>
          </tr>
      </table>
    </form>
</body>
</html>
