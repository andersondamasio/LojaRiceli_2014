<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelecionarMarca.aspx.cs"
    Inherits="Loja.Painel.selecao.SelecionarMarca" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../js/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="../js/MaxLength.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("[id*=mar_descricaoTextBox]").MaxLength(
            {
                MaxLength: 350,
                CharacterCountControl: $('#counter')
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
   <asp:Panel runat="server" ID="PanelCadastrarMarca" Visible="false">
    <table>
        <tr>
            <td>
                Nome:
            </td>
            <td>
                Descrição:
            </td>
            <td>
                Bloquear:
            </td>
            <td>
            </td>
        </tr>
        <tr style="vertical-align: top">
            <td>
                <asp:TextBox ID="mar_nomeTextBox" runat="server" Columns="30" MaxLength="64"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="mar_nomeTextBox"
                    Display="Dynamic" ErrorMessage="Campo obrigatório" SetFocusOnError="true" ValidationGroup="groupCadastrarMarca"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox ID="mar_descricaoTextBox" runat="server" TextMode="MultiLine" MaxLength="350"
                    Height="57px" Width="289px"></asp:TextBox></asp:TextBox>
            </td>
            <td>
                <asp:CheckBox ID="mar_bloquearCheckBox" runat="server" Text="sim" />
            </td>
            <td>
                <asp:Button ID="ButtonCancelarMarca" runat="server" Text="Cancelar" 
                    onclick="ButtonCancelarMarca_Click" />
                &nbsp;<asp:Button ID="ButtonSalvarMarca" runat="server" 
                    onclick="ButtonSalvarMarca_Click" Text="salvar" 
                    ValidationGroup="groupCadastrarMarca" />
            </td>
        </tr>
    </table>
  </asp:Panel>
        <label>
            Nome:
        </label>
        <asp:TextBox ID="TextBoxChaveFiltro" runat="server" MaxLength="32"></asp:TextBox>
        <asp:Button ID="ButtonFiltrar" runat="server" Text="Filtrar" />
        &nbsp;
        <asp:Button ID="ButtonIncluirMarca" runat="server" Text="Incluir Marca" 
            onclick="ButtonIncluirMarca_Click" />
        <hr />
        <asp:ListView ID="ListViewMarcas" runat="server" DataSourceID="EntityDataSourceMarca"
            OnItemCommand="ListViewMarca_ItemCommand" DataKeyNames="mar_id">
            <EmptyDataTemplate>
                <table runat="server" style="">
                    <tr>
                        <td>
                            Nenhum dado foi retornado.
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <ItemTemplate>
                <tr style="">
                    <td>
                        <asp:Label ID="mar_idLabel" runat="server" Text='<%# Eval("mar_id") %>' />
                    </td>
                    <td>
                        <asp:Label ID="mar_nomeLabel" runat="server" Text='<%# Eval("mar_nome") %>' />
                    </td>
                    <td>
                        <asp:CheckBox ID="mar_bloquearCheckBox" runat="server" Checked='<%# Eval("mar_bloquear") %>'
                            Enabled="false" />
                    </td>
                    <td>
                        <%# Eval("Produto.Count")%>
                    </td>
                    <td>
                        <asp:Button ID="ButtonAlterarMarca" runat="server" Text="Alterar" CommandName="Select"
                            CommandArgument='<%# Eval("mar_id") %>' OnClientClick="this.value = 'Carregando...';this.disabled = true;this.disabled = true;__doPostBack(this.name,'');" />
                    </td>
                    <td>
                        <asp:LinkButton ID="LinkButtonSelect" runat="server" Text="Selecionar" CommandName="Select" />
                    </td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table runat="server">
                    <tr runat="server">
                        <td runat="server">
                            <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                                <tr runat="server" style="">
                                    <th runat="server">
                                        <asp:LinkButton runat="server" ID="LinkButton1" Text="Código" CommandName="Sort"
                                            CommandArgument="mar_id" />
                                    </th>
                                    <th runat="server">
                                        <asp:LinkButton runat="server" ID="LinkButton2" Text="Nome" CommandName="Sort" CommandArgument="mar_nome" />
                                    </th>
                                    <th runat="server">
                                        <asp:LinkButton runat="server" ID="LinkButton4" Text="Bloquear" CommandName="Sort"
                                            CommandArgument="mar_bloquear" />
                                    </th>
                                    <th runat="server">
                                        N° produtos
                                    </th>
                                    <th runat="server">
                                        Editar
                                    </th>
                                    <th id="Th1" runat="server">
                                    </th>
                                </tr>
                                <tr runat="server" id="itemPlaceholder">
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td runat="server" style="">
                            <asp:DataPager ID="DataPager1" runat="server" PageSize="7">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False"
                                        ShowPreviousPageButton="False" FirstPageText="Primeiro" LastPageText="Último"
                                        NextPageText="Próximo" PreviousPageText="Anterior" />
                                    <asp:NumericPagerField />
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False"
                                        ShowPreviousPageButton="False" FirstPageText="Primeiro" LastPageText="Último"
                                        NextPageText="Próximo" PreviousPageText="Anterior" />
                                    <asp:TemplatePagerField>
                                        <PagerTemplate>
                                            <b>registros de
                                                <asp:Label runat="server" ID="CurrentPageLabel" Text="<%# Container.StartRowIndex %>" />
                                                a
                                                <asp:Label runat="server" ID="TotalPagesLabel" Text="<%# Container.StartRowIndex+Container.PageSize %>" />
                                                (<asp:Label runat="server" ID="TotalItemsLabel" Text="<%# Container.TotalRowCount%>" />registros)
                                            </b>
                                        </PagerTemplate>
                                    </asp:TemplatePagerField>
                                </Fields>
                            </asp:DataPager>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
            <SelectedItemTemplate>
                <tr style="background-color: Blue">
                    <td>
                        <asp:Label ID="mar_idLabel" runat="server" Text='<%# Eval("mar_id") %>' />
                    </td>
                    <td>
                        <asp:Label ID="mar_nomeLabel" runat="server" Text='<%# Eval("mar_nome") %>' />
                    </td>
                    <td>
                        <asp:CheckBox ID="mar_bloquearCheckBox" runat="server" Checked='<%# Eval("mar_bloquear") %>'
                            Enabled="false" />
                    </td>
                    <td>
                        <%# Eval("Produto.Count")%>
                    </td>
                    <td>
                        <asp:Button ID="ButtonAlterarMarca" runat="server" Text="Alterar" CommandName="Select"
                            CommandArgument='<%# Eval("mar_id") %>' OnClientClick="this.value = 'Carregando...';this.disabled = true;this.disabled = true;__doPostBack(this.name,'');" />
                    </td>
                    <td>
                        <asp:LinkButton ID="LinkButtonSelect" runat="server" Text="Selecionar" CommandName="Select" />
                    </td>
                </tr>
            </SelectedItemTemplate>
        </asp:ListView>
        <asp:EntityDataSource ID="EntityDataSourceMarca" runat="server" ConnectionString="name=LojaEntities"
            DefaultContainerName="LojaEntities" EnableFlattening="False" EntitySetName="Marca"
            OrderBy="it.[mar_id]" Select="it.[mar_id], it.[mar_nome], it.[mar_descricao], it.[mar_bloquear], it.[Produto]"
            Where="(it.[loj_id] = @loj_id) and (@Nome is null or it.[mar_nome]  Like '%' + @Nome +   '%')  ">
            <WhereParameters>
                <asp:ControlParameter ControlID="TextBoxChaveFiltro" Name="Nome" PropertyName="Text"
                    DbType="String" />
                <asp:SessionParameter SessionField="loj_id" Name="loj_id" DbType="Int32" />
            </WhereParameters>
        </asp:EntityDataSource> <br/>
    <asp:HiddenField ID="HiddenFieldTipoCadastro" runat="server" />
    </form>
</body>
</html>
