<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Entrega.aspx.cs" Inherits="Loja.Painel.Entrega" %>

<%@ Register Src="Cabecalho.ascx" TagName="Cabecalho" TagPrefix="uc1" %>
<%@ Register Src="Rodape.ascx" TagName="Rodape" TagPrefix="uc1" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager runat="server" ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" />
        <table width="1024px" align="center">
            <tr>
                <td>
                    <uc1:Cabecalho ID="Cabecalho1" runat="server" />
                    <script>
                        $(function () {
                            $("#ent_dataInicialTextBox").datepicker({ minDate: -1, maxDate: "+12M +10D" });
                            $("#ent_dataFinalTextBox").datepicker({ minDate: -1, maxDate: "+12M +10D" });
                            $("#ListViewEntrega_ent_dataInicialTextBox").datepicker({ minDate: -1, maxDate: "+12M +10D" });
                            $("#ListViewEntrega_ent_dataFinalTextBox").datepicker({ minDate: -1, maxDate: "+12M +10D" });
                        });
                    </script>
                    <asp:ListView ID="ListViewEntrega" runat="server" DataSourceID="EntityDataSourceEntrega" DataKeyNames="ent_id,loj_id" InsertItemPosition="None" OnItemEditing="ListViewEntrega_ItemEditing">
                        <EditItemTemplate>
                            <tr style="background-color: blue">
                                <td>
                                    <asp:Label ID="ent_idLabel" runat="server" Text='<%# Eval("ent_id") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="ent_cepInicialLabel" runat="server" Text='<%# Eval("ent_cepInicial") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="ent_cepFinalLabel" runat="server" Text='<%# Eval("ent_cepFinal") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="ent_dataHoraInicialLabel" runat="server" Text='<%# Eval("ent_dataHoraInicial") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="ent_dataHoraFinalLabel" runat="server" Text='<%# Eval("ent_dataHoraFinal") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="ent_localizacaoLabel" runat="server" Text='<%# Eval("ent_cidade") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ent_estado") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="ent_prazoLabel" runat="server" Text='<%# Eval("ent_prazo") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="ent_valorLabel" runat="server" Text='<%# Eval("ent_valor") %>' />
                                </td>
                                <td>
                                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Editar" />
                                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Excluir" OnClientClick="return confirm('Tem certeza que deseja excluir esta configuração ?');" />
                                </td>
                            </tr>
                            <tr style="">

                                <td colspan="7">Cep de: 
                                    <asp:TextBox ID="ent_cepInicialTextBox" runat="server" MaxLength="8" Columns="8" Text='<%# Bind("ent_cepInicial") %>' />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="ent_cepInicialTextBox" SetFocusOnError="true" ValidationGroup="groupEdit" ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                                    <asp:FilteredTextBoxExtender ID="cep_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="ent_cepInicialTextBox">
                                    </asp:FilteredTextBoxExtender>
                                    até: 
                                    <asp:TextBox ID="ent_cepFinalTextBox" runat="server" MaxLength="8" Columns="8" Text='<%# Bind("ent_cepFinal") %>' />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="ent_cepFinalTextBox" SetFocusOnError="true" ValidationGroup="groupEdit" ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="ent_cepFinalTextBox">
                                    </asp:FilteredTextBoxExtender>
                                    <br />
                                    DataHora de: 
                                    <asp:TextBox ID="ent_dataInicialTextBox" runat="server" MaxLength="10" Columns="8" ClientIDMode="Static" Text='<%# Bind("ent_dataHoraInicial","{0:dd/MM/yyyy}") %>' />
                                    -
                         <asp:TextBox ID="ent_horaInicialTextBox" runat="server" MaxLength="10" Columns="8" Text='<%# Eval("ent_dataHoraInicial","{0:T}") %>' />
                                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                        Enabled="True" MaskType="Time" TargetControlID="ent_horaInicialTextBox"
                                        Mask="99:99:99">
                                    </asp:MaskedEditExtender>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                        Enabled="True" MaskType="Date" TargetControlID="ent_dataInicialTextBox"
                                        Mask="99/99/9999">
                                    </asp:MaskedEditExtender>
                                    <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender3"
                                        ControlToValidate="ent_dataInicialTextBox" ValidationGroup="groupEdit" InvalidValueMessage="Data inválida">
                                    </asp:MaskedEditValidator>
                                    até: 
                                    <asp:TextBox ID="ent_dataFinalTextBox" runat="server" MaxLength="10" Columns="8" ClientIDMode="Static" Text='<%# Bind("ent_dataHoraFinal","{0:dd/MM/yyyy}") %>' />
                                    - 
                        <asp:TextBox ID="ent_horaFinalTextBox" runat="server" MaxLength="10" Columns="8" Text='<%# Eval("ent_dataHoraFinal","{0:T}") %>' />
                                    <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                        Enabled="True" MaskType="Time" TargetControlID="ent_horaFinalTextBox"
                                        Mask="99:99:99">
                                    </asp:MaskedEditExtender>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureAMPMPlaceholder=""
                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                        Enabled="True" MaskType="Date" TargetControlID="ent_dataFinalTextBox"
                                        Mask="99/99/9999">
                                    </asp:MaskedEditExtender>

                                    <asp:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender4"
                                        ControlToValidate="ent_dataFinalTextBox" ValidationGroup="groupEdit" InvalidValueMessage="Data inválida">
                                    </asp:MaskedEditValidator>
                                    <br />
                                    Cidade:
                                    <asp:TextBox ID="ent_cidadadeTextBox" runat="server" MaxLength="128" Text='<%# Bind("ent_cidade") %>' />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="ent_cidadadeTextBox" SetFocusOnError="true" ValidationGroup="groupEdit" ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                                    <br />
                                    Estado: 
                                    <asp:DropDownList ID="ent_estadoDropDownList" runat="server" SelectedValue='<%# Bind("ent_estado") %>'>
                                        <asp:ListItem Value="" Text="Selecione seu estado" />
                                        <asp:ListItem Value="AC">Acre</asp:ListItem>
                                        <asp:ListItem Value="AL">Alagoas</asp:ListItem>
                                        <asp:ListItem Value="AM">Amazonas</asp:ListItem>
                                        <asp:ListItem Value="AP">Amap&#225;</asp:ListItem>
                                        <asp:ListItem Value="BA">Bahia</asp:ListItem>
                                        <asp:ListItem Value="CE">Cear&#225;</asp:ListItem>
                                        <asp:ListItem Value="DF">Distrito Federal</asp:ListItem>
                                        <asp:ListItem Value="ES">Esp&#237;rito Santo</asp:ListItem>
                                        <asp:ListItem Value="GO">Goi&#225;s</asp:ListItem>
                                        <asp:ListItem Value="MA">Maranh&#227;o</asp:ListItem>
                                        <asp:ListItem Value="MG">Minas Gerais</asp:ListItem>
                                        <asp:ListItem Value="MS">Mato Grosso do Sul</asp:ListItem>
                                        <asp:ListItem Value="MT">Mato Grosso</asp:ListItem>
                                        <asp:ListItem Value="PA">Par&#225;</asp:ListItem>
                                        <asp:ListItem Value="PB">Para&#237;ba</asp:ListItem>
                                        <asp:ListItem Value="PE">Pernambuco</asp:ListItem>
                                        <asp:ListItem Value="PI">Piau&#237;</asp:ListItem>
                                        <asp:ListItem Value="PR">Paran&#225;</asp:ListItem>
                                        <asp:ListItem Value="RJ">Rio de Janeiro</asp:ListItem>
                                        <asp:ListItem Value="RN">Rio Grande do Norte</asp:ListItem>
                                        <asp:ListItem Value="RO">Rond&#244;nia</asp:ListItem>
                                        <asp:ListItem Value="RR">Roraima</asp:ListItem>
                                        <asp:ListItem Value="RS">Rio Grande do Sul</asp:ListItem>
                                        <asp:ListItem Value="SC">Santa Catarina</asp:ListItem>
                                        <asp:ListItem Value="SE">Sergipe</asp:ListItem>
                                        <asp:ListItem Value="SP">S&#227;o Paulo</asp:ListItem>
                                        <asp:ListItem Value="TO">Tocantins</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ControlToValidate="ent_estadoDropDownList" SetFocusOnError="true" ValidationGroup="groupEdit" ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                                    <br />
                                    Prazo: 
                                    <asp:TextBox ID="ent_prazoTextBox" runat="server" MaxLength="3" Text='<%# Bind("ent_prazo") %>' />
                                    <br />
                                    Valor: 
                                    <asp:TextBox ID="ent_valorTextBox" runat="server" MaxLength="7" Text='<%# Bind("ent_valor") %>' />
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
                                <td colspan="7">Cep de: 
                                    <asp:TextBox ID="ent_cepInicialTextBox" runat="server" MaxLength="8" Columns="8" Text='<%# Bind("ent_cepInicial") %>' />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="ent_cepInicialTextBox" SetFocusOnError="true" ValidationGroup="groupInsert" ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="ent_cepInicialTextBox">
                                    </asp:FilteredTextBoxExtender>
                                    até: 
                                    <asp:TextBox ID="ent_cepFinalTextBox" runat="server" MaxLength="8" Columns="8" Text='<%# Bind("ent_cepFinal") %>' />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="ent_cepFinalTextBox" SetFocusOnError="true" ValidationGroup="groupInsert" ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="ent_cepFinalTextBox">
                                    </asp:FilteredTextBoxExtender>
                                    <br />
                                    DataHora de: 
                                    <asp:TextBox ID="ent_dataInicialTextBox" runat="server" MaxLength="10" Columns="8" ClientIDMode="Static" Text='<%# Bind("ent_dataHoraInicial","{0:dd/MM/yyyy}") %>' />
                                    -
                         <asp:TextBox ID="ent_horaInicialTextBox" runat="server" MaxLength="10" Columns="8" />
                                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                        Enabled="True" MaskType="Time" TargetControlID="ent_horaInicialTextBox"
                                        Mask="99:99:99">
                                    </asp:MaskedEditExtender>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                        Enabled="True" MaskType="Date" TargetControlID="ent_dataInicialTextBox"
                                        Mask="99/99/9999">
                                    </asp:MaskedEditExtender>
                                    <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender3"
                                        ControlToValidate="ent_dataInicialTextBox" ValidationGroup="groupInsert" InvalidValueMessage="Data inválida">
                                    </asp:MaskedEditValidator>
                                    até: 
                                    <asp:TextBox ID="ent_dataFinalTextBox" runat="server" MaxLength="10" Columns="8" ClientIDMode="Static" Text='<%# Bind("ent_dataHoraFinal","{0:dd/MM/yyyy}") %>' />
                                    - 
                        <asp:TextBox ID="ent_horaFinalTextBox" runat="server" MaxLength="10" Columns="8" />
                                    <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                        Enabled="True" MaskType="Time" TargetControlID="ent_horaFinalTextBox"
                                        Mask="99:99:99">
                                    </asp:MaskedEditExtender>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureAMPMPlaceholder=""
                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                        Enabled="True" MaskType="Date" TargetControlID="ent_dataFinalTextBox"
                                        Mask="99/99/9999">
                                    </asp:MaskedEditExtender>
                                    <asp:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender4"
                                        ControlToValidate="ent_dataFinalTextBox" ValidationGroup="groupInsert" InvalidValueMessage="Data inválida">
                                    </asp:MaskedEditValidator>
                                    <br />
                                    Cidade:
                                    <asp:TextBox ID="ent_cidadadeTextBox" runat="server" MaxLength="128" Text='<%# Bind("ent_cidade") %>' />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="ent_cidadadeTextBox" SetFocusOnError="true" ValidationGroup="groupInsert" ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                                    <br />
                                    Estado: 
                                    <asp:DropDownList ID="ent_estadoDropDownList" runat="server" SelectedValue='<%# Bind("ent_estado") %>'>
                                        <asp:ListItem Value="" Text="Selecione seu estado" />
                                        <asp:ListItem Value="AC">Acre</asp:ListItem>
                                        <asp:ListItem Value="AL">Alagoas</asp:ListItem>
                                        <asp:ListItem Value="AM">Amazonas</asp:ListItem>
                                        <asp:ListItem Value="AP">Amap&#225;</asp:ListItem>
                                        <asp:ListItem Value="BA">Bahia</asp:ListItem>
                                        <asp:ListItem Value="CE">Cear&#225;</asp:ListItem>
                                        <asp:ListItem Value="DF">Distrito Federal</asp:ListItem>
                                        <asp:ListItem Value="ES">Esp&#237;rito Santo</asp:ListItem>
                                        <asp:ListItem Value="GO">Goi&#225;s</asp:ListItem>
                                        <asp:ListItem Value="MA">Maranh&#227;o</asp:ListItem>
                                        <asp:ListItem Value="MG">Minas Gerais</asp:ListItem>
                                        <asp:ListItem Value="MS">Mato Grosso do Sul</asp:ListItem>
                                        <asp:ListItem Value="MT">Mato Grosso</asp:ListItem>
                                        <asp:ListItem Value="PA">Par&#225;</asp:ListItem>
                                        <asp:ListItem Value="PB">Para&#237;ba</asp:ListItem>
                                        <asp:ListItem Value="PE">Pernambuco</asp:ListItem>
                                        <asp:ListItem Value="PI">Piau&#237;</asp:ListItem>
                                        <asp:ListItem Value="PR">Paran&#225;</asp:ListItem>
                                        <asp:ListItem Value="RJ">Rio de Janeiro</asp:ListItem>
                                        <asp:ListItem Value="RN">Rio Grande do Norte</asp:ListItem>
                                        <asp:ListItem Value="RO">Rond&#244;nia</asp:ListItem>
                                        <asp:ListItem Value="RR">Roraima</asp:ListItem>
                                        <asp:ListItem Value="RS">Rio Grande do Sul</asp:ListItem>
                                        <asp:ListItem Value="SC">Santa Catarina</asp:ListItem>
                                        <asp:ListItem Value="SE">Sergipe</asp:ListItem>
                                        <asp:ListItem Value="SP">S&#227;o Paulo</asp:ListItem>
                                        <asp:ListItem Value="TO">Tocantins</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ControlToValidate="ent_estadoDropDownList" SetFocusOnError="true" ValidationGroup="groupInsert" ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                                    <br />
                                    Prazo: 
                                    <asp:TextBox ID="ent_prazoTextBox" runat="server" MaxLength="3" Text='<%# Bind("ent_prazo") %>' />
                                    <br />
                                    Valor: 
                                    <asp:TextBox ID="ent_valorTextBox" runat="server" MaxLength="7" Text='<%# Bind("ent_valor") %>' />
                                    <br />
                                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Salvar" ValidationGroup="groupInsert" />
                                    <asp:Button ID="CancelButton" runat="server" Text="Cancelar" OnClick="CancelButton_Click" />
                                </td>
                            </tr>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Label ID="ent_idLabel" runat="server" Text='<%# Eval("ent_id") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="ent_cepInicialLabel" runat="server" Text='<%# Eval("ent_cepInicial") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="ent_cepFinalLabel" runat="server" Text='<%# Eval("ent_cepFinal") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="ent_dataHoraInicialLabel" runat="server" Text='<%# Eval("ent_dataHoraInicial") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="ent_dataHoraFinalLabel" runat="server" Text='<%# Eval("ent_dataHoraFinal") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="ent_localizacaoLabel" runat="server" Text='<%# Eval("ent_cidade") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("ent_estado") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="ent_prazoLabel" runat="server" Text='<%# Eval("ent_prazo") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="ent_valorLabel" runat="server" Text='<%# Eval("ent_valor") %>' />
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
                                                <th runat="server">CepInicial</th>
                                                <th runat="server">CepFinal</th>
                                                <th runat="server">DataHoraInicial</th>
                                                <th runat="server">DataHoraFinal</th>
                                                <th runat="server">Cidade</th>
                                                <th runat="server">Estado</th>
                                                <th runat="server">Prazo</th>
                                                <th runat="server">Valor</th>
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
                    <asp:DataPager ID="DataPagerEntrega" runat="server" PageSize="5" PagedControlID="ListViewEntrega">
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

                                    <%# (this.DataPagerEntrega.Visible = Container.TotalRowCount > this.DataPagerEntrega.PageSize).ToString().Replace("True",string.Empty) %>
                                </PagerTemplate>
                            </asp:TemplatePagerField>
                        </Fields>
                    </asp:DataPager>
                    <br />
                    <asp:Button ID="ButtonIncluirEntrega" runat="server" Text="Incluir Entrega" OnClick="ButtonIncluirEntrega_Click" />


                    <asp:EntityDataSource ID="EntityDataSourceEntrega" runat="server" ConnectionString="name=LojaEntities" DefaultContainerName="LojaEntities" EnableDelete="True" EnableFlattening="False"
                        EnableInsert="True" EnableUpdate="True" EntitySetName="Entrega" EntityTypeFilter="Entrega"
                        OnDeleting="EntityDataSourceEntrega_Deleting" OnInserted="EntityDataSourceEntrega_Inserted"
                        OnInserting="EntityDataSourceEntrega_Inserting" OnUpdating="EntityDataSourceEntrega_Updating"
                        Where="it.[loj_id] = @loj_id">
                        <WhereParameters>
                            <asp:SessionParameter Type="Int32" Name="loj_id" SessionField="loj_id" />
                        </WhereParameters>
                    </asp:EntityDataSource>

                    <uc1:Rodape ID="Rodape1" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
