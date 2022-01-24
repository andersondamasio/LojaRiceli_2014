<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfiguracaoPedidoStatus.aspx.cs" Inherits="Loja.Painel.ConfiguracaoPedidoStatus" %>

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

                    <asp:ListView ID="ListViewPedidoStatus" runat="server" DataKeyNames="stat_id,loj_id" DataSourceID="EntityDataSourcePedidoStatus" InsertItemPosition="None" OnItemEditing="ListViewPedidoStatus_ItemEditing" OnItemDeleting="ListViewPedidoStatus_ItemDeleting">
                        <EditItemTemplate>
                            <tr style="background-color: blue">
                                <td>
                                    <asp:Label ID="stat_idLabel" runat="server" Text='<%# Eval("stat_id") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="stat_nomeLabel" runat="server" Text='<%# Eval("stat_nome") %>' />
                                </td>
                               
                                <td>
                                    <asp:Literal ID="Literal1" Text='<%# Eval("stat_emailCorpo") != null ? Eval("stat_emailCorpo").ToString().Substring(0,10)+"..." : string.Empty %>' Mode="Encode" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="stat_emailCopiaOcultaLabel" runat="server" Text='<%# Eval("stat_emailCopiaOculta") != null ? Eval("stat_emailCopiaOculta").ToString().Substring(0,20)+"..." : string.Empty %>' />
                                </td>
                                 <td>
                                    <asp:RadioButton ID="Label1" runat="server" Checked='<%# Eval("stat_ativar") %>' Enabled="false" />
                                </td>
                                 <td>
                                    <asp:CheckBox ID="Label2" runat="server" Checked='<%# Eval("stat_bloquear") %>' Enabled="false"/>
                                </td>
                                 <td>
                                    <asp:Label ID="stat_dataHoraLabel" runat="server" Text='<%# Eval("stat_dataHora") %>' />
                                </td>
                                <td>
                                  
                                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Editar" />
                                      <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Excluir" />
                                </td>
                            </tr>
                            <tr style="">

                                <td colspan="7">
                                                                      Nome(Status): <asp:TextBox ID="stat_nomeTextBox" runat="server" Text='<%# Bind("stat_nome") %>' MaxLength="50" Columns="30"/>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="stat_nomeTextBox" ErrorMessage="Campo obrigatório" SetFocusOnError="true" ValidationGroup="groupEdit" Display="Dynamic"></asp:RequiredFieldValidator><br />
                                     Cópia oculta:<asp:TextBox ID="stat_emailCopiaOcultaTextBox" runat="server" Text='<%# Bind("stat_emailCopiaOculta") %>' MaxLength="400" Columns="30"/>(emails separados por ",")
                                     <br />
                                     Ativar como padrão: <asp:RadioButton ID="RadioButton1" runat="server" Checked='<%# Bind("stat_ativar") %>' />
                                    <br>
                                     Bloquear: <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("stat_bloquear") %>'/>
                                    <br />
                                    Corpo do email:<asp:TextBox ID="stat_emailCorpoTextBox" runat="server" TextMode="MultiLine" Rows="40" Width="100%" Text='<%# Bind("stat_emailCorpo") %>' BorderStyle="Solid" />
                                    <br />
                                    
                                      <asp:Button ID="UpdateButton" runat="server" CommandName="Update" ValidationGroup="groupEdit" Text="Salvar" />
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

                                <td colspan="7">
                                     Nome(Status): <asp:TextBox ID="stat_nomeTextBox" runat="server" Text='<%# Bind("stat_nome") %>' MaxLength="50" Columns="30"/>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="stat_nomeTextBox" ErrorMessage="Campo obrigatório" SetFocusOnError="true" ValidationGroup="groupEdit" Display="Dynamic"></asp:RequiredFieldValidator><br />
                                     Cópia oculta:<asp:TextBox ID="stat_emailCopiaOcultaTextBox" runat="server" Text='<%# Bind("stat_emailCopiaOculta") %>' MaxLength="400" Columns="30"/>(emails separados por ",")
                                     <br />
                                     Ativar como padrão: <asp:RadioButton ID="RadioButton1" runat="server" Checked='<%# Bind("stat_ativar") %>' Text="Sim" />
                                    <br>
                                     Bloquear: <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("stat_bloquear") %>'/>
                                    <br />
                                    Corpo do email:<asp:TextBox ID="stat_emailCorpoTextBox" runat="server" TextMode="MultiLine" Rows="40" Width="100%" Text='<%# Bind("stat_emailCorpo") %>' BorderStyle="Solid" />
                                    <br />
                                    
                                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Salvar" ValidationGroup="groupInsert" />
                                    <asp:Button ID="CancelButton" runat="server" Text="Cancelar" OnClick="CancelButton_Click" />
                                </td>
                            </tr>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Label ID="stat_idLabel" runat="server" Text='<%# Eval("stat_id") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="stat_nomeLabel" runat="server" Text='<%# Eval("stat_nome") %>' />
                                </td>
                               
                                <td>
                                    <asp:Literal ID="Literal1" Text='<%# Eval("stat_emailCorpo") != null ? Eval("stat_emailCorpo").ToString().Substring(0,10)+"..." : string.Empty %>' Mode="Encode" runat="server" />
                                </td>
                                <td>
                                   <asp:Label ID="stat_emailCopiaOcultaLabel" runat="server" Text='<%# Eval("stat_emailCopiaOculta") != null ? Eval("stat_emailCopiaOculta").ToString().Substring(0,20)+"..." : string.Empty %>' />
                                </td>
                                 <td>
                                    <asp:RadioButton ID="stat_ativarRadioButton" runat="server" Checked='<%# Eval("stat_ativar") %>'  Enabled="false"/>
                                </td>
                                 <td>
                                    <asp:CheckBox ID="Label2" runat="server" Checked='<%# Eval("stat_bloquear") %>' Enabled="false"/>
                                </td>
                                 <td>
                                    <asp:Label ID="stat_dataHoraLabel" runat="server" Text='<%# Eval("stat_dataHora") %>' />
                                </td>
                                <td>
                                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Editar" />
                                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Excluir" OnClientClick="return confirm('Tem certeza que deseja excluir esta configuração ?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <table runat="server">
                                <tr runat="server">
                                    <td runat="server">
                                        <table id="itemPlaceholderContainer" runat="server" border="2" style="min-width:600px">
                                            <tr runat="server" style="">
                                                <th runat="server">Código</th>
                                                <th runat="server">Nome(status)</th>
                                                <th runat="server">EmailCorpo</th>
                                                <th runat="server">EmailCópiaOculta</th>
                                                <th id="Th2" runat="server">Padrão</th>
                                                <th id="Th3" runat="server">Bloquear</th>
                                                <th id="Th1" runat="server">Data e Hora</th>
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

                                    <%# (this.DataPagerPedidoStatus.Visible = Container.TotalRowCount > this.DataPagerPedidoStatus.PageSize).ToString().Replace("True",string.Empty) %>
                                </PagerTemplate>
                            </asp:TemplatePagerField>
                        </Fields>
                    </asp:DataPager>
                    <br />
                    <asp:Button ID="ButtonIncluirEntrega" runat="server" Text="Incluir Status" OnClick="ButtonIncluirPedidoStatus_Click" />

                    <asp:EntityDataSource ID="EntityDataSourcePedidoStatus" runat="server" ConnectionString="name=LojaEntities"
                         DefaultContainerName="LojaEntities" EnableDelete="True" EnableFlattening="False" EnableInsert="True" 
                        EnableUpdate="True" EntitySetName="Status" EntityTypeFilter="Status"
                         OnDeleting="EntityDataSourcePedidoStatus_Deleting" OnInserted="EntityDataSourcePedidoStatus_Inserted"
                         OnInserting="EntityDataSourcePedidoStatus_Inserting"
                        Where="it.[loj_id] = @loj_id" OnUpdated="EntityDataSourcePedidoStatus_Updated">
                <WhereParameters>
                    <asp:SessionParameter Type="Int32"  Name="loj_id" SessionField="loj_id" />
                </WhereParameters>
                    </asp:EntityDataSource>


                    <asp:Panel runat="server" ID="PanelTags" GroupingText="Tags disponíveis para o Corpo do email:">
                        [PEDIDO_NUMERO] 
[PEDIDO_STATUS]
[PEDIDO_DATAHORA] 
[PEDIDO_CONDICAO]
[PEDIDO_ENDENTREGA]
[PEDIDO_PRODUTOS]
[PEDIDO_PRAZOENTREGA]

[PEDIDO_SUBTOTAL] 
[PEDIDO_ENTREGATOTAL]  
[PEDIDO_DESCONTOSTOTAL]  
[PEDIDO_PEDIDOTOTAL] 
[URL]
[LOJA_ENDERECO]
                        <br/><br/>

                        Exemplo: Seu Pedido de número [PEDIDO_NUMERO] foi [PEDIDO_STATUS].


                    </asp:Panel>


                    <uc1:Rodape ID="Rodape1" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
