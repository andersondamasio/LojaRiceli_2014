<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadastroParcelamento.aspx.cs"
    Inherits="Loja.Painel.CadastroParcelamento" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="Cabecalho.ascx" TagName="Cabecalho" TagPrefix="uc1" %>



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
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <uc1:Cabecalho ID="Cabecalho1" runat="server" />
                    Cadastro de parcelamento<br />

                    <asp:ListView ID="ListViewParcelamento" runat="server" DataSourceID="EntityDataSourceParcelamento" DataKeyNames="parc_id,loj_id" OnItemCommand="ListViewParcelamento_ItemCommand" OnItemDeleting="ListViewParcelamento_ItemDeleting">
                        <EmptyDataTemplate>
                            <table runat="server" style="">
                                <tr>
                                    <td>Nenhum dado foi retornado.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Label ID="parc_nomeLabel" runat="server" Text='<%# Eval("parc_nome") %>' />
                                </td>
                                <td>
                                    <asp:CheckBox ID="parc_ativarCheckBox" runat="server" Checked='<%# Eval("parc_ativarJuro") %>' Enabled="false" />
                                </td>

                                <td>
                                    <asp:CheckBox ID="parc_bloquearCheckBox" runat="server" Checked='<%# Eval("parc_bloquear") %>' Enabled="false" />
                                </td>

                                <td>
                                    <asp:Button ID="ButtonAlterarParcelamento" runat="server" Text="Editar" CommandName="Select"
                                        CommandArgument='<%# Eval("parc_id") %>' OnClientClick="return DesabilitarDuploClick(this,'Carregando...','', false);" />
                                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Excluir" OnClientClick="return confirm('Tem certeza que deseja excluir esta configuração ?');" Visible="false" />
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
                                                <th runat="server">Ativar juros</th>
                                                <th runat="server">Bloquear</th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <SelectedItemTemplate>
                            <tr style="background-color: Aqua; text-align: left">
                                <td>
                                    <asp:Label ID="parc_nomeLabel" runat="server" Text='<%# Eval("parc_nome") %>' />
                                </td>
                                <td>
                                    <asp:CheckBox ID="parc_ativarCheckBox" runat="server" Checked='<%# Eval("parc_ativarJuro") %>' Enabled="false" />
                                </td>

                                <td>
                                    <asp:CheckBox ID="parc_bloquearCheckBox" runat="server" Checked='<%# Eval("parc_bloquear") %>' Enabled="false" />
                                </td>

                                <td>
                                    <asp:Button ID="ButtonAlterarParcelamento" runat="server" Text="Editar" CommandName="Select"
                                        CommandArgument='<%# Eval("parc_id") %>' OnClientClick="return DesabilitarDuploClick(this,'Carregando...','', false);" />
                                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Excluir" OnClientClick="return confirm('Tem certeza que deseja excluir esta configuração ?');" Visible="false" />
                                </td>
                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>

                    <asp:DataPager ID="DataPagerParcelamento" runat="server" PageSize="5" PagedControlID="ListViewParcelamento">
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

                                    <%# (this.DataPagerParcelamento.Visible = Container.TotalRowCount > this.DataPagerParcelamento.PageSize).ToString().Replace("True",string.Empty) %>
                                </PagerTemplate>
                            </asp:TemplatePagerField>
                        </Fields>
                    </asp:DataPager>

                    <asp:EntityDataSource ID="EntityDataSourceParcelamento" runat="server" ConnectionString="name=LojaEntities"
                        DefaultContainerName="LojaEntities" EnableFlattening="False"
                        EntitySetName="Parcelamento" EnableDelete="true" OrderBy="it.[parc_id]"
                        Where="it.[loj_id] = @loj_id">
                        <WhereParameters>
                         <CustomControls:CustomParameterLoja Name="loj_id" DbType="Int32"   />
                        </WhereParameters>
                    </asp:EntityDataSource>

                    <asp:Button ID="ButtonIncluirParcelamento" runat="server" OnClick="ButtonIncluirParcelamento_Click" Text="Incluir Parcelamento" />

                    <asp:Panel ID="PanelCadastrarParcelamento" runat="server" Visible="False">
                        <div>
                            Nome:<asp:TextBox ID="parc_nomeTextBox" runat="server"></asp:TextBox>
                        </div>
                        <table border="2">

                            <tr>
                                <td>Bloquear:<asp:CheckBox ID="parc_bloquearCheckBox" runat="server" Text="sim" />

                                </td>
                                <td colspan="2">Com juros:<asp:CheckBox ID="parc_ativarJuroCheckBox" runat="server" Text="sim" AutoPostBack="True" OnCheckedChanged="parc_ativarJuroCheckBox_CheckedChanged" />
                                    &nbsp;&nbsp;&nbsp; Valor Mínima de Parcela:<asp:TextBox ID="parc_valorMinimoTextBox" runat="server" MaxLength="7"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="parc_valorMinimoTextBox_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="parc_valorMinimoTextBox" ValidChars="0123456789.,">
                                    </asp:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top;">Cartões:<br />
                                    <asp:CheckBoxList ID="CheckBoxListFormaPagamento" runat="server"
                                        DataValueField="forPag_id" DataTextField="forPag_nome">
                                    </asp:CheckBoxList>
                                </td>
                                <td style="vertical-align: top; width: 350px;">Parcelas:<br />
                                    <asp:Repeater ID="RepeaterParcela" runat="server" DataSource='<%# Enumerable.Range(1, 12) %>'>
                                        <HeaderTemplate>
                                            <div style="float: left; width: 50%;">
                                                <ul style="list-style: none">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <li>
                                                <asp:HiddenField ID="parcPar_quantidadeHiddenField" runat="server" Value='<%# Container.DataItem %>' />
                                                <asp:CheckBox ID="par_bloquearCheckBox" runat="server" />
                                                <asp:Label ID="parcPar_quantidadeLabel" runat="server" Text='<%# Container.DataItem %>' />x 
                                                <asp:TextBox ID="par_percJurosTextBox" runat="server" MaxLength="7" Columns="5" Enabled='' />
                                                <%# (Container.ItemIndex != 0 && Container.ItemIndex % 7 == 0) ? "</ul></div><div style=\"float: right; width: 50%;\"><ul style=\"list-style:none\">" : string.Empty %>
                                            </li>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </ul>
                          </div>
                                        </FooterTemplate>
                                    </asp:Repeater>

                                </td>
                                <td style="vertical-align: bottom">
                                    <asp:Button ID="ButtonSalvarParcelamento" runat="server" Height="21px" OnClick="ButtonSalvarParcelamento_Click" Text="Salvar Parcelamento" Width="121px" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:HiddenField runat="server" ID="HiddenFieldTipoCadastro" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
