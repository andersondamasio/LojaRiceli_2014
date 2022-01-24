<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfiguracaoLoja.aspx.cs" Inherits="Loja.Painel.ConfiguracaoLoja" %>

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
                    <asp:ListView ID="ListViewLoja" runat="server" DataKeyNames="loj_id" DataSourceID="EntityDataSourceLoja"
                         InsertItemPosition="None" OnItemEditing="ListViewLoja_ItemEditing"  OnItemUpdating="ListViewLoja_ItemUpdating">
                        <EditItemTemplate>
                            <tr style="background-color: blue">

                                <td>
                                    <asp:Label ID="loj_idLabel" runat="server" Text='<%# Eval("loj_id") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="loj_nomeLabel" runat="server" Text='<%# Eval("loj_nome") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="loj_dominioLabel" runat="server" Text='<%# Eval("loj_dominio") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="loj_emailLabel" runat="server" Text='<%# Eval("loj_email") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="loj_cepLabel" runat="server" Text='<%# Eval("loj_cep") %>' />
                                </td>
                              <td>
                                    <asp:CheckBox ID="loj_bloquearLabel" runat="server" Checked='<%# Eval("loj_bloquear") %>' Enabled="false" />
                                </td>
                                <td>
                                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Editar" />
                                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Excluir" OnClientClick="return confirm('Tem certeza que deseja excluir esta configuração ?');" />
                                </td>
                            </tr>
                            <tr style="">
                                <td colspan="8">Nome: 
                                    <asp:TextBox ID="loj_nomeTextBox" runat="server" MaxLength="50" Columns="25" Text='<%# Bind("loj_nome") %>' />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="loj_nomeTextBox" ValidationGroup="groupEdit" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                                    <br />
                                    Domínio: 
                                    <asp:TextBox ID="loj_dominioTextBox" runat="server" MaxLength="50" Columns="25" Text='<%# Bind("loj_dominio") %>' />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="loj_dominioTextBox" ValidationGroup="groupEdit" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                                    <br />
                                    Email: 
                                    <asp:TextBox ID="loj_emailTextBox" runat="server" MaxLength="32" Columns="25" Text='<%# Bind("loj_email") %>' />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="loj_emailTextBox" ValidationGroup="groupEdit" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                                    <br />
                                    Cep:
                                    <asp:TextBox ID="loj_cepTextBox" runat="server" MaxLength="8" Columns="7" Text='<%# Bind("loj_cep") %>' />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="loj_cepTextBox" ValidationGroup="groupEdit" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                                    <br />
                                    Bloquear:<asp:CheckBox ID="Label2" runat="server" Checked='<%# Bind("loj_bloquear") %>' />
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
                                <td colspan="8">Nome: 
                                    <asp:TextBox ID="loj_nomeTextBox" runat="server" MaxLength="50" Columns="25" Text='<%# Bind("loj_nome") %>' />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="loj_nomeTextBox" ValidationGroup="groupInsert" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                                    <br />
                                    Domínio: 
                                    <asp:TextBox ID="loj_dominioTextBox" runat="server" MaxLength="50" Columns="25" Text='<%# Bind("loj_dominio") %>' />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="loj_dominioTextBox" ValidationGroup="groupInsert" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                                    <br />
                                    Email: 
                                    <asp:TextBox ID="loj_emailTextBox" runat="server" MaxLength="32" Columns="25" Text='<%# Bind("loj_email") %>' />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="loj_emailTextBox" ValidationGroup="groupInsert" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                                    <br />
                                    Cep:
                                    <asp:TextBox ID="loj_cepTextBox" runat="server" MaxLength="8" Columns="7" Text='<%# Bind("loj_cep") %>' />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="loj_cepTextBox" ValidationGroup="groupInsert" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                                    <br />
                                    Bloquear:<asp:CheckBox ID="Label2" runat="server" Checked='<%# Bind("loj_bloquear") %>' />
                                     <br />
                                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Salvar" ValidationGroup="groupInsert" />
                                    <asp:Button ID="CancelButton" runat="server" Text="Cancelar" OnClick="CancelButton_Click" />
                                </td>
                            </tr>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <tr style="">

                                <td>
                                    <asp:Label ID="loj_idLabel" runat="server" Text='<%# Eval("loj_id") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="loj_nomeLabel" runat="server" Text='<%# Eval("loj_nome") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="loj_dominioLabel" runat="server" Text='<%# Eval("loj_dominio") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="loj_emailLabel" runat="server" Text='<%# Eval("loj_email") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="loj_cepLabel" runat="server" Text='<%# Eval("loj_cep") %>' />
                                </td>
                                <td>
                                    <asp:CheckBox ID="loj_bloquearLabel" runat="server" Checked='<%# Eval("loj_bloquear") %>' Enabled="false" />
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
                                        <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                                            <tr runat="server" style="">
                                                <th runat="server">Código</th>
                                                <th runat="server">Nome</th>
                                                <th runat="server">Domínio</th>
                                                <th runat="server">Email</th>
                                                <th runat="server">Cep</th>
                                                <th runat="server">Bloquear</th>
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
                    <asp:DataPager ID="DataPagerLoja" runat="server" PageSize="10" PagedControlID="ListViewLoja">
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

                                    <%# (this.DataPagerLoja.Visible = Container.TotalRowCount > this.DataPagerLoja.PageSize).ToString().Replace("True",string.Empty) %>
                                </PagerTemplate>
                            </asp:TemplatePagerField>
                        </Fields>
                    </asp:DataPager><br/>
                    <asp:Button ID="ButtonIncluirLoja" runat="server" Text="Incluir Loja" OnClick="ButtonIncluirLoja_Click" />

                     <asp:EntityDataSource ID="EntityDataSourceLoja" runat="server" ConnectionString="name=LojaEntities"
                        DefaultContainerName="LojaEntities" EnableDelete="True" EnableFlattening="False" EnableInsert="True" EnableUpdate="True"
                        EntitySetName="LojaCon" EntityTypeFilter="LojaCon" OnDeleting="EntityDataSourceLoja_Deleting" Include="Status"
                        OnInserted="EntityDataSourceLoja_Inserted" OnInserting="EntityDataSourceLoja_Inserting" OnDeleted="EntityDataSourceLoja_Deleted" >
                       
                    </asp:EntityDataSource>
                    <uc1:Rodape ID="Rodape1" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
