<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="Loja.Painel.Usuario" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="Cabecalho.ascx" TagName="Cabecalho" TagPrefix="uc1" %>
<%@ Register Src="Rodape.ascx" TagName="Rodape" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <table width="1024px" align="center">
            <tr>
                <td>
                    <uc1:Cabecalho ID="Cabecalho1" runat="server" />

                    <asp:EntityDataSource ID="EntityDataSourceUsuario" runat="server" ConnectionString="name=LojaEntities"
                         DefaultContainerName="LojaEntities" EnableDelete="True" EnableFlattening="False" EnableInsert="True" 
                        EnableUpdate="True" EntitySetName="Usuario" OnDeleting="EntityDataSourceUsuario_Deleting" 
                        OnInserted="EntityDataSourceUsuario_Inserted" OnInserting="EntityDataSourceUsuario_Inserting"
                         OnUpdating="EntityDataSourceUsuario_Updating"
                        Where="it.[usu_excluido] is null && it.[loj_id] = @loj_id">
                        <WhereParameters>
                          <CustomControls:CustomParameterLoja Name="loj_id" DbType="Int32"   />
                        </WhereParameters>
                    </asp:EntityDataSource>
                   
                    <asp:ListView ID="ListViewUsuario" runat="server" DataKeyNames="usu_id,loj_id" DataSourceID="EntityDataSourceUsuario" InsertItemPosition="None" OnItemEditing="ListViewUsuario_ItemEditing">                     
                        <EditItemTemplate>
                              <tr style="background-color:blue">
                                <td>
                                    <asp:Label ID="usu_nomeLabel" runat="server" Text='<%# Eval("usu_nome") %>' />
                                </td>
                              
                                <td>
                                    <asp:Label ID="usu_dataHoraLabel" runat="server" Text='<%# Eval("usu_dataHora") %>' />
                                </td>
                                <td>
                                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Editar" />
                                     <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Excluir" OnClientClick="return confirm('Tem certeza que deseja excluir esta configuração ?');"/>
                                </td>
                            </tr>
                            <tr style="">
                                <td colspan="4">
                                  Nome:  <asp:TextBox ID="usu_nomeTextBox" runat="server" Text='<%# Bind("usu_nome") %>' />(máximo de 30 caracteres minúsculos)
                                     <asp:FilteredTextBoxExtender ID="usu_nomeTextBox_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="LowercaseLetters" TargetControlID="usu_nomeTextBox">
                    </asp:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="usu_nomeTextBox" SetFocusOnError="true" ValidationGroup="groupEdit" ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                               <br/>
                                     Senha:  <span ID="SpanAlterarSenha" runat="server" Visible="false">

                                <asp:TextBox ID="usu_senhaTextBox" runat="server" TextMode="Password" />
                                          Confirma senha: <asp:TextBox ID="usu_confirmaSenhaTextBox" runat="server" TextMode="Password" />
                                         <asp:RequiredFieldValidator ID="usu_senhaRequiredFieldValidator" runat="server" Display="Dynamic" ControlToValidate="usu_senhaTextBox" SetFocusOnError="true" ValidationGroup="groupEdit" ErrorMessage="Campo obrigatório" ></asp:RequiredFieldValidator>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="usu_confirmaSenhaTextBox" SetFocusOnError="true" ValidationGroup="groupEdit" ErrorMessage="Campo obrigatório" ></asp:RequiredFieldValidator>
                                      <asp:CompareValidator ID="compareSenha" runat="server" ValidationGroup="groupEdit" ControlToCompare="usu_senhaTextBox" ControlToValidate="usu_confirmaSenhaTextBox" ErrorMessage="Senha e confirmação estão diferentes" Display="Dynamic" />
                                     
                                     </span> 
                               <asp:Button ID="ButtonAterarSenha" runat="server" Text="Alterar Senha" OnClick="ButtonAterarSenha_Click" />
                              <br/>
                                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Atualizar" ValidationGroup="groupEdit" />
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
                               
                                <td colspan="4">
                               Nome:  <asp:TextBox ID="usu_nomeTextBox" runat="server" Text='<%# Bind("usu_nome") %>' />(máximo de 30 caracteres minúsculos)
                                     <asp:FilteredTextBoxExtender ID="usu_nomeTextBox_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="LowercaseLetters" TargetControlID="usu_nomeTextBox">
                    </asp:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="usu_nomeTextBox" SetFocusOnError="true" ValidationGroup="groupInsert" ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                               <br/>
                                  Senha:  <asp:TextBox ID="usu_senhaTextBox" runat="server" TextMode="Password" Text='<%# Bind("usu_senha") %>' />
                                        <asp:RequiredFieldValidator ID="usu_senhaRequiredFieldValidator" runat="server" Display="Dynamic" ControlToValidate="usu_senhaTextBox" SetFocusOnError="true" ValidationGroup="groupInsert" ErrorMessage="Campo obrigatório" ></asp:RequiredFieldValidator>
                                    <br>
                                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Salvar" ValidationGroup="groupInsert" />
                                     <asp:Button ID="CancelButton" runat="server" Text="Cancelar" OnClick="CancelButton_Click" />
                                </td>
                            </tr>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <tr style="">
                                  <td>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("usu_id") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="usu_nomeLabel" runat="server" Text='<%# Eval("usu_nome") %>' />
                                </td>
                               
                                <td>
                                    <asp:Label ID="usu_dataHoraLabel" runat="server" Text='<%# Eval("usu_dataHora") %>' />
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
                                            <tr runat="server" style="">
                                               <th  runat="server">Codigo</th>
                                                <th runat="server">Nome</th>
                                                <th runat="server">DataHora</th>
                                                  <th id="Th1" runat="server"></th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                              
                            </table>
                        </LayoutTemplate>
                    </asp:ListView>
                            <asp:DataPager ID="DataPagerUsuario" runat="server" PageSize="5" PagedControlID="ListViewUsuario">
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

                                    <%# (this.DataPagerUsuario.Visible = Container.TotalRowCount > this.DataPagerUsuario.PageSize).ToString().Replace("True",string.Empty) %>
                                </PagerTemplate>
                            </asp:TemplatePagerField>
                        </Fields>
                    </asp:DataPager>
                     <asp:Button ID="ButtonIncluirUsuario" runat="server" Text="Incluir Usuário" OnClick="ButtonIncluirUsuario_Click" />

                    <uc1:Rodape ID="Rodape1" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
