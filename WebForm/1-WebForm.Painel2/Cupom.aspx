<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cupom.aspx.cs" Inherits="Loja.Painel.Cupom" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="Cabecalho.ascx" TagName="Cabecalho" TagPrefix="uc1" %>
<%@ Register Src="Rodape.ascx" TagName="Rodape" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> </title>

</head>
<body>
    <form id="form1" runat="server">    
        <table width="1024px" align="center">
            <tr>
                <td>
                    <uc1:Cabecalho ID="Cabecalho1" runat="server" />
                                <script>
                                    $(function () {
                                        $("#ListViewCupom_cup_validadeTextBox").datepicker({ minDate: -1, maxDate: "+12M +10D" });
                                        $("#cup_validadeTextBox").datepicker({ minDate: -1, maxDate: "+12M +10D" });
                });
         </script>   
                    <asp:ListView ID="ListViewCupom" runat="server" DataKeyNames="cup_id,loj_id" DataSourceID="EntityDataSourceCupons" InsertItemPosition="None" OnItemEditing="ListViewCupom_ItemEditing">
                        <EditItemTemplate>
                             <tr style="background-color:blue">
                                <td>
                                    <asp:Label ID="cup_chaveLabel" runat="server" Text='<%# Eval("cup_chave") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="cli_idLabel" runat="server" Text='<%# Eval("cli_id") %>' />
                                </td>

                                <td>
                                    <asp:Label ID="cup_validadeLabel" runat="server" Text='<%# Eval("cup_validade","{0:dd/MM/yyyy}") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="cup_quantidadeLabel" runat="server" Text='<%# Eval("cup_quantidade") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="cup_pedidoMinimoLabel" runat="server" Text='<%# Eval("cup_pedidoMinimo","{0:N}") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="cup_valorLabel" runat="server" Text='<%# Eval("cup_valor","{0:N}") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="cup_dataHoraLabel" runat="server" Text='<%# Eval("cup_dataHora") %>' />
                                </td>
                                <td>
                                  <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Excluir" OnClientClick="return confirm('Tem certeza que deseja excluir esta configuração ?');"/>
                                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Editar" />
                                </td>
                            </tr>
                            <tr style="text-align: left">
                                <td colspan="7">
                                   Chave: <asp:TextBox ID="cup_chaveTextBox" runat="server" MaxLength="30" Columns="12" Text='<%# Eval("cup_chave") %>' Enabled="false"/>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="cup_chaveTextBox" SetFocusOnError="true" ValidationGroup="groupEdit" ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                                    <br />
                                   Código do Cliente: <asp:TextBox ID="cli_idTextBox" runat="server" MaxLength="7" Columns="7" Text='<%# Bind("cli_id") %>' />
                                    <br />
                                   Validade: <asp:TextBox ID="cup_validadeTextBox" runat="server" MaxLength="10" Columns="10" Text='<%# Bind("cup_validade","{0:dd/MM/yyyy}") %>' ClientIDMode="Static" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="cup_validadeTextBox" SetFocusOnError="true" ValidationGroup="groupEdit" ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" id="rexNumber" controltovalidate="cup_validadeTextBox" validationexpression="^([0]?[1-9]|[1|2][0-9]|[3][0|1])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$" SetFocusOnError="true" Display="Dynamic" ValidationGroup="groupEdit" errormessage="Data inválida" />
                                    <br />
                                   Quantidade: <asp:TextBox ID="cup_quantidadeTextBox" runat="server" MaxLength="3" Columns="3" Text='<%# Bind("cup_quantidade") %>' />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="cup_quantidadeTextBox" SetFocusOnError="true" ValidationGroup="groupEdit" ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                                    <br />
                                    Valor Pedido Mínimo<asp:TextBox ID="cup_pedidoMinimoTextBox" runat="server" MaxLength="7" Columns="7" Text='<%# Bind("cup_pedidoMinimo","{0:N}") %>' />
                                    <br />
                                    Valor do Cupom:<asp:TextBox ID="cup_valorTextBox" runat="server" MaxLength="7" Columns="7" Text='<%# Eval("cup_valor","{0:N}") %>' Enabled="false"/>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ControlToValidate="cup_valorTextBox" SetFocusOnError="true" ValidationGroup="groupEdit" ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                                    <br />
                                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Salvar"  ValidationGroup="groupEdit"/>
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
                            <tr style="text-align: left">
                                  <td colspan="7">
                                    Chave: <asp:TextBox ID="cup_chaveTextBox" runat="server" MaxLength="30" Columns="12" Text='<%# Bind("cup_chave") %>' />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="cup_chaveTextBox" SetFocusOnError="true" ValidationGroup="groupInsert" ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                                    <br />
                                   Código do Cliente: <asp:TextBox ID="cli_idTextBox" runat="server" MaxLength="7" Columns="7" Text='<%# Bind("cli_id") %>' />(opcional)
                                    <br />
                                   Validade: <asp:TextBox ID="cup_validadeTextBox" runat="server" MaxLength="10" Columns="10" Text='<%# Bind("cup_validade","{0:dd/MM/yyyy}") %>' />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="cup_validadeTextBox" SetFocusOnError="true" ValidationGroup="groupInsert" ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" id="rexNumber" controltovalidate="cup_validadeTextBox" validationexpression="^([0]?[1-9]|[1|2][0-9]|[3][0|1])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$" SetFocusOnError="true" Display="Dynamic" ValidationGroup="groupInsert" errormessage="Data inválida" />
                                    <br />
                                   Quantidade: <asp:TextBox ID="cup_quantidadeTextBox" runat="server" MaxLength="3" Columns="3" Text='<%# Bind("cup_quantidade") %>' ClientIDMode="Static" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="cup_quantidadeTextBox" SetFocusOnError="true" ValidationGroup="groupInsert" ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                                    <br />
                                    Valor Pedido Mínimo<asp:TextBox ID="cup_pedidoMinimoTextBox" runat="server" MaxLength="7" Columns="7" Text='<%# Bind("cup_pedidoMinimo","{0:N}") %>' />
                                    <br />
                                    Valor do Cupom:<asp:TextBox ID="cup_valorTextBox" runat="server" MaxLength="7" Columns="7" Text='<%# Bind("cup_valor","{0:N}") %>' />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ControlToValidate="cup_valorTextBox" SetFocusOnError="true" ValidationGroup="groupInsert" ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                                    <br />
                                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Salvar" ValidationGroup="groupInsert" />
                                     <asp:Button ID="CancelButton" runat="server" Text="Cancelar" OnClick="CancelButton_Click" />
                                </td>
                            </tr>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Label ID="cup_chaveLabel" runat="server" Text='<%# Eval("cup_chave") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="cli_idLabel" runat="server" Text='<%# Eval("cli_id") %>' />
                                </td>

                                <td>
                                    <asp:Label ID="cup_validadeLabel" runat="server" Text='<%# Eval("cup_validade","{0:dd/MM/yyyy}") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="cup_quantidadeLabel" runat="server" Text='<%# Eval("cup_quantidade") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="cup_pedidoMinimoLabel" runat="server" Text='<%# Eval("cup_pedidoMinimo","{0:N}") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="cup_valorLabel" runat="server" Text='<%# Eval("cup_valor","{0:N}") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="cup_dataHoraLabel" runat="server" Text='<%# Eval("cup_dataHora") %>' />
                                </td>
                                <td>
                                  
                                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Editar" />
                                     <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Excluir" OnClientClick="return confirm('Tem certeza que deseja excluir esta configuração ?');"/>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <table runat="server">
                                <tr runat="server">
                                    <td runat="server">
                                        <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                                            <tr runat="server" style="text-align: left">

                                                <th runat="server">Chave</th>
                                                <th runat="server">Cod.Cliente</th>
                                                <th runat="server">Validade</th>
                                                <th runat="server">Quantidade</th>
                                                <th runat="server">Pedido Minimo</th>
                                                <th runat="server">Valor</th>
                                                <th runat="server">Data e Hora</th>
                                                <th runat="server"></th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                               
                            </table>
                        </LayoutTemplate>
                        <SelectedItemTemplate>
                            <tr style="">
                                
                                <td>
                                    <asp:Label ID="cup_idLabel" runat="server" Text='<%# Eval("cup_id") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="cup_validadeLabel" runat="server" Text='<%# Eval("cup_validade") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="cup_quantidadeLabel" runat="server" Text='<%# Eval("cup_quantidade") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="cup_pedidoMinimoLabel" runat="server" Text='<%# Eval("cup_pedidoMinimo") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="cup_valorLabel" runat="server" Text='<%# Eval("cup_valor") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="cup_dataHoraLabel" runat="server" Text='<%# Eval("cup_dataHora") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="cli_idLabel" runat="server" Text='<%# Eval("cli_id") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="loj_idLabel" runat="server" Text='<%# Eval("loj_id") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="ClienteLabel" runat="server" Text='<%# Eval("Cliente") %>' />
                                </td>
                                <td>
                                     
                                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Editar" />
                                     <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Excluir" OnClientClick="return confirm('Tem certeza que deseja excluir esta configuração ?');"/>
                                </td>
                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>
                    <asp:DataPager ID="DataPagerCupom" runat="server" PageSize="5" PagedControlID="ListViewCupom">
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

                                    <%# (this.DataPagerCupom.Visible = Container.TotalRowCount >= this.DataPagerCupom.PageSize).ToString().Replace("True",string.Empty) %>
                                </PagerTemplate>
                            </asp:TemplatePagerField>
                        </Fields>
                    </asp:DataPager>
                     <asp:Button ID="ButtonIncluirCupom" runat="server" Text="Incluir Cupom" OnClick="ButtonIncluirCupom_Click" />
                    <asp:EntityDataSource ID="EntityDataSourceCupons" runat="server" ConnectionString="name=LojaEntities"
                        DefaultContainerName="LojaEntities" EnableDelete="True" EnableFlattening="False" EnableInsert="True"
                        EnableUpdate="True" EntitySetName="Cupom" EntityTypeFilter="Cupom" OnInserting="EntityDataSourceCupom_Inserting"
                        OnDeleting="EntityDataSourceCupom_Deleting" OnInserted="EntityDataSourceCupom_Inserted" OnUpdating="EntityDataSourceCupons_Updating"
                        Where="it.[cup_excluido] is null && it.[loj_id] = @loj_id">
                        <WhereParameters>
                           <CustomControls:CustomParameterLoja Name="loj_id" DbType="Int32"   />
                        </WhereParameters>
                    </asp:EntityDataSource>

                    <uc1:Rodape ID="Rodape1" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
