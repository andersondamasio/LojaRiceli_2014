<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfiguracaoCartao.aspx.cs" Inherits="Loja.Painel.ConfiguracaoCartao" %>

<%@ Register Src="Cabecalho.ascx" TagName="Cabecalho" TagPrefix="uc1" %>
<%@ Register Src="Rodape.ascx" TagName="Rodape" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
        <table width="1024px" align="center">
            <tr>
                <td>
                    <uc1:Cabecalho ID="Cabecalho1" runat="server" />

                    <asp:ListView ID="ListViewCartao" runat="server" DataKeyNames="forPag_id,loj_id" DataSourceID="EntityDataSourceCartao" OnItemEditing="ListViewCartao_ItemEditing">
                        <EditItemTemplate>
                            <tr style="background-color: blue">
                                <td>
                                    <asp:Label ID="forPag_idLabel" runat="server" Text='<%# Eval("forPag_id") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="forPag_nomeLabel" runat="server" Text='<%# Eval("forPag_nome") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="forPag_tipoLabel" runat="server" Text='<%# Eval("forPag_tipo") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="forPag_prazoPagamentoLabel" runat="server" Text='<%# Eval("forPag_prazoPagamento") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="forPag_valorDescontoLabel" runat="server" Text='<%# Eval("forPag_valorDesconto","{0:N}") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="forPag_percentualDescontoLabel" runat="server" Text='<%# Eval("forPag_percentualDesconto") %>' />
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Eval("forPag_bloquear") %>' Enabled="false" />
                                </td>
                                <td>
                                    <asp:Label ID="forPag_dataHoraLabel" runat="server" Text='<%# Eval("forPag_dataHora") %>' />
                                </td>
                                <td>
                                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Excluir" OnClientClick="return confirm('Tem certeza que deseja excluir esta configuração ?');" />

                                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Editar" />
                                </td>
                            </tr>
                            <tr style="">
                                <td colspan="7">Nome:  
                                    <asp:TextBox ID="forPag_nomeTextBox" runat="server" MaxLength="30" Text='<%# Bind("forPag_nome") %>' />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="groupEdit" ControlToValidate="forPag_nomeTextBox" ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                                    <br />
                                    Prazo Pagamento: 
                                    <asp:TextBox ID="forPag_prazoPagamentoTextBox" runat="server" MaxLength="3" Text='<%# Bind("forPag_prazoPagamento") %>' />
                                    <br />
                                    Valor Desconto:  
                                    <asp:TextBox ID="forPag_valorDescontoTextBox" runat="server" MaxLength="7" Text='<%# Bind("forPag_valorDesconto","{0:N}") %>' />
                                    <br />
                                    Percentual Desconto: 
                                    <asp:TextBox ID="forPag_percentualDescontoTextBox" runat="server" MaxLength="7" Text='<%# Bind("forPag_percentualDesconto") %>' />
                                    <br />
                                    Bloquear: 
                                    <asp:CheckBox ID="forPag_bloquearCheckBox" runat="server" Checked='<%# Bind("forPag_bloquear") %>' />
                                    <br />
                                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Salvar" ValidationGroup="groupEdit" />
                                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancelar" />
                                </td>
                            </tr>
                        </EditItemTemplate>
                        <EmptyDataTemplate>
                            <table runat="server" style="">
                                <tr>
                                    <td>Nenhum dado foi retornado.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <InsertItemTemplate>
                            <tr style="">
                                <td colspan="7">Nome:  
                                    <asp:TextBox ID="forPag_nomeTextBox" runat="server" MaxLength="30" Text='<%# Bind("forPag_nome") %>' />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="groupInsert" ControlToValidate="forPag_nomeTextBox" ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                                    <br />
                                    Prazo Pagamento: 
                                    <asp:TextBox ID="forPag_prazoPagamentoTextBox" runat="server" MaxLength="3" Text='<%# Bind("forPag_prazoPagamento") %>' />

                                    <br />
                                    Valor Desconto:  
                                    <asp:TextBox ID="forPag_valorDescontoTextBox" runat="server" MaxLength="7" Text='<%# Bind("forPag_valorDesconto","{0:N}") %>' />
                                    <br />
                                    Percentual Desconto: 
                                    <asp:TextBox ID="forPag_percentualDescontoTextBox" runat="server" MaxLength="7" Text='<%# Bind("forPag_percentualDesconto") %>' />
                                    <br />
                                    Bloquear: 
                                    <asp:CheckBox ID="forPag_bloquearCheckBox" runat="server" Checked='<%# Bind("forPag_bloquear") %>' />
                                    <br />
                                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Salvar" ValidationGroup="groupInsert" />
                                    <asp:Button ID="CancelButton" runat="server" Text="Cancelar" OnClick="CancelButton_Click" />
                                </td>
                            </tr>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Label ID="forPag_idLabel" runat="server" Text='<%# Eval("forPag_id") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="forPag_nomeLabel" runat="server" Text='<%# Eval("forPag_nome") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="forPag_tipoLabel" runat="server" Text='<%# Eval("forPag_tipo") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="forPag_prazoPagamentoLabel" runat="server" Text='<%# Eval("forPag_prazoPagamento") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="forPag_valorDescontoLabel" runat="server" Text='<%# Eval("forPag_valorDesconto","{0:N}") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="forPag_percentualDescontoLabel" runat="server" Text='<%# Eval("forPag_percentualDesconto") %>' />
                                </td>
                                <td>
                                    <asp:CheckBox ID="forPag_bloquearCheckBox" runat="server" Checked='<%# Eval("forPag_bloquear") %>' Enabled="false" />
                                </td>
                                <td>
                                    <asp:Label ID="forPag_dataHoraLabel" runat="server" Text='<%# Eval("forPag_dataHora") %>' />
                                </td>
                                <td>
                                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Excluir" OnClientClick="return confirm('Tem certeza que deseja excluir esta configuração ?');" />
                                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Editar" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <table runat="server">
                                <tr runat="server">
                                    <td runat="server">
                                        <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                                            <tr runat="server" style="">
                                                <th runat="server">Nome</th>
                                                <th runat="server">Tipo</th>
                                                <th runat="server">Prazo Pagamento</th>
                                                <th runat="server">Valor Desconto</th>
                                                <th runat="server">Percentual Desconto</th>
                                                <th runat="server">Bloquear</th>
                                                <th runat="server">DataHora</th>
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
                    <br />

                    <asp:EntityDataSource ID="EntityDataSourceCartao" runat="server" ConnectionString="name=LojaEntities"
                        DefaultContainerName="LojaEntities" EnableDelete="True" EnableFlattening="False" EnableInsert="True" EnableUpdate="True"
                        EntitySetName="FormaPagamento" EntityTypeFilter="FormaPagamento" OnDeleting="EntityDataSourceCartao_Deleting"
                        OnInserted="EntityDataSourceCartao_Inserted" OnInserting="EntityDataSourceCartao_Inserting"
                        Where="it.[loj_id] = @loj_id">
                        <WhereParameters>
                            <CustomControls:CustomParameterLoja Name="loj_id" DbType="Int32"   />
                        </WhereParameters>
                    </asp:EntityDataSource>
                    <asp:DataPager ID="DataPagerCartao" runat="server" PageSize="5" PagedControlID="ListViewCartao">
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

                                    <%# (this.DataPagerCartao.Visible = Container.TotalRowCount >= this.DataPagerCartao.PageSize).ToString().Replace("True",string.Empty) %>
                                </PagerTemplate>
                            </asp:TemplatePagerField>
                        </Fields>
                    </asp:DataPager>
                    <asp:Button ID="ButtonIncluirCartao" runat="server" Text="Incluir Cartão" OnClick="ButtonIncluirCartao_Click" />

                    <uc1:Rodape ID="Rodape1" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
