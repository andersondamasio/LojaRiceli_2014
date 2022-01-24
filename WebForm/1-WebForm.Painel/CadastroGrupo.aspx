<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadastroGrupo.aspx.cs"
    Inherits="Loja.Painel.CadastroGrupo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="Cabecalho.ascx" TagName="Cabecalho" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Cadastro de Grupos e Produtos</title>
</head>
<body>
    <form id="form1" runat="server">
         <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="Server"></asp:ToolkitScriptManager>
      <table width="1024px" align="center">
            <tr>
            <td>
        <uc1:Cabecalho ID="Cabecalho1" runat="server" />
        <!-- IFRAME SELECIONAR -->
        <asp:Panel ID="PanelDrop" runat="server" Style="color: Fuchsia; font-size: 30pt; position: absolute; top: 0px; z-index: 999999; background-color: Blue; height: auto; width: auto; cursor: move; margin: 0 auto;">
            <asp:Panel ID="PanelFrame" runat="server" Style="color: Fuchsia; font-size: 30pt; position: absolute; top: 10px; z-index: 1; background: #F5F5F5; display: none">
                <asp:Button ID="ButtonFechar" runat="server" Text="Fechar" OnClientClick="ResetFrame('iFrameGrupo');" />
                <iframe id="iFrameGrupo" src="" onload="calcHeight();" scrolling="no" height="1px"
                    frameborder="1">Carregando consulta...</iframe>
            </asp:Panel>
        </asp:Panel>
        <!---------------------------->
        <script>
            $(function () {
                $("#pro_paginaInicialDataDeTextBox").datepicker({ minDate: -1, maxDate: "+12M +10D" });
                $("#pro_paginaInicialDataAteTextBox").datepicker({ minDate: -1, maxDate: "+12M +10D" });
            });
        </script>
        <table border="2">
            <tr>
                <td valign="top" style="min-width:150px">
                    <asp:TreeView ID="TreeViewGrupo" runat="server" DataSourceID="XmlDataSourceGrupos"
                        Style="position: relative; z-index: 1; vertical-align: top; overflow: scroll; height: 541px;"
                        ImageSet="Arrows" MaxDataBindDepth="50" NodeIndent="20" ShowLines="True"
                        ExpandDepth="1" EnableClientScript="false" PopulateNodesFromClient="true" OnSelectedNodeChanged="TreeViewGrupo_SelectedNodeChanged">
                        <DataBindings>
                            <asp:TreeNodeBinding DataMember="MenuItem" TextField="gru_nome" ValueField="gru_id" 
                                ImageUrlField="gru_imagem" 
                                PopulateOnDemand="false" SelectAction="SelectExpand" FormatString="{0}"  />
                        </DataBindings>
                        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                        <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                            NodeSpacing="0px" VerticalPadding="0px" />
                        <ParentNodeStyle Font-Bold="False" />
                        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                    </asp:TreeView>
                    <asp:XmlDataSource ID="XmlDataSourceGrupos" runat="server" EnableCaching="False"
                        XPath="MenuItems/MenuItem" CacheDuration="10" DataFile="~/Cache/grupoPainel.xml"
                        TransformFile="~/Cache/TransformXSLTPainel.xsl" />
                </td>
                <td style="position: relative; width: 95%; z-index: 1; vertical-align: top; max-width: 300px">
                    <asp:Panel ID="PanelBotoes" runat="server" Enabled="False">
                        <table>
                            <tr>
                                <td colspan="2">Escolha a ação desejada:
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="ButtonAlterar" runat="server" Text="Alterar Grupo" OnClick="ButtonAlterar_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="ButtonIncluir" runat="server" Text="Incluir Grupo" OnClick="ButtonIncluir_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="ButtonExcluir" runat="server" OnClick="ButtonExcluir_Click" Text="Excluir Grupo"
                                        OnClientClick="return confirm('Tem certeza que deseja excluir?');" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="ButtonListarProduto" runat="server" Text="Listar Produto" OnClick="ButtonListarProduto_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="ButtonIncluirProduto" runat="server" Text="Incluir Produto" OnClick="ButtonIncluirProduto_Click" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="PanelCadastrarGrupo" runat="server" Visible="false">
                        <asp:HiddenField runat="server" ID="HiddenFieldTipoCadastro" />
                        Nome:<br />
                        <asp:TextBox ID="gru_nome1TextBox" runat="server" Width="356px" MaxLength="64" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                            ValidationGroup="groupCadastrarGrupo" ErrorMessage="Campo obrigatório" ControlToValidate="gru_nomeTextBox"></asp:RequiredFieldValidator>
                        <br />
                        Descrição:<br />
                        <asp:TextBox ID="gru_descricaoTextBox" runat="server" Height="79px" TextMode="MultiLine"
                            Width="356px" MaxLength="256" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                            ValidationGroup="groupCadastrarGrupo" ErrorMessage="Campo obrigatório" ControlToValidate="gru_descricaoTextBox"></asp:RequiredFieldValidator>
                        <br />
                        Prioridade de ordenação:
                    <asp:TextBox ID="gru_posicaoTextBox" runat="server" />
                        <asp:NumericUpDownExtender ID="gru_posicaoTextBox_NumericUpDownExtender" runat="server"
                            Enabled="True" Maximum="99" Minimum="0" RefValues="" ServiceDownMethod="" ServiceDownPath=""
                            ServiceUpMethod="" Tag="" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="gru_posicaoTextBox"
                            Width="60">
                        </asp:NumericUpDownExtender>
                        <br />
                        Bloquear:
                    <asp:CheckBox ID="gru_bloquearCheckBox" runat="server" />
                        <br />
                        <br />
                        <asp:Button ID="ButtonCancelarGrupo" runat="server" Text="Cancelar" OnClick="ButtonCancelar_Click" />
                        &nbsp;&nbsp;
                    <asp:Button ID="ButtonSalvarGrupo" runat="server" Text="Salvar" OnClick="ButtonSalvarGrupo_Click"
                        ValidationGroup="groupCadastrarGrupo" OnClientClick="return DesabilitarDuploClick(this,'Carregando...','groupCadastrarGrupo', true);" />
                    </asp:Panel>
                    <asp:Panel ID="PanelListarProduto" runat="server" Visible="false">
                        <asp:ListView ID="ListViewProduto" runat="server" DataSourceID="EntityDataSourceProduto"
                            OnItemCommand="ListViewProduto_ItemCommand" DataKeyNames="pro_id">
                            <EmptyDataTemplate>
                                <table id="Table1" runat="server" style="">
                                    <tr>
                                        <td>Nenhum produto foi encontrado.
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <tr style="text-align: left">
                                    <td>
                                        <asp:Label ID="pro_idLabel" runat="server" Text='<%# Eval("pro_id") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="pro_nomeLabel" runat="server" Text='<%# Eval("pro_nome") %>' />
                                    </td>
                                    <td>

                                      <%# Eval("ProSku.Count")%>

                                      
                                    sku(s)
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="Label1" runat="server" Checked='<%# Eval("pro_bloquear") %>' Enabled="false" />
                                    </td>
                                    <td>
                                        <asp:Button ID="ButtonAlterarProduto" runat="server" Text="Editar" CommandName="Select"
                                            CommandArgument='<%# Eval("pro_id") %>' OnClientClick="return DesabilitarDuploClick(this,'Carregando...','', false);" />
                                        <asp:Button ID="ButtonListarProdutoSku" runat="server" Text="Sku" CommandName="Select"
                                            CommandArgument='<%# Eval("pro_id") %>' OnClientClick="return DesabilitarDuploClick(this,'Carregando...','', false);" />
                                        <asp:Button ID="ButtonListarProdutoInfo" runat="server" Text="Informações" CommandName="Select"
                                            CommandArgument='<%# Eval("pro_id") %>' OnClientClick="return DesabilitarDuploClick(this,'Carregando...','', false);" />
                                      <asp:Button ID="ButtonDuplicarProduto" runat="server" Text="Duplicar"
                                            CommandArgument='<%# Eval("pro_id") %>' OnClientClick="return DesabilitarDuploClick(this,'Carregando...','', false);" /> 
                                        <asp:Button ID="ButtonExcluirProduto" runat="server" Text="Excluir" CommandName="Select"
                                            CommandArgument='<%# Eval("pro_id") %>' OnClientClick="return confirm('Tem certeza que deseja excluir?');return DesabilitarDuploClick(this,'Carregando...','', false);" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <SelectedItemTemplate>
                                <tr style="background-color: Aqua; text-align: left">
                                    <td>
                                        <asp:Label ID="pro_idLabel" runat="server" Text='<%# Eval("pro_id") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="pro_nomeLabel" runat="server" Text='<%# Eval("pro_nome") %>' />
                                    </td>
                                    <td>
                                          <%# Eval("ProSku.Count")%>
                                    sku(s)
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="Label1" runat="server" Checked='<%# Eval("pro_bloquear") %>' Enabled="false" />
                                    </td>
                                    <td>
                                        <asp:Button ID="ButtonAlterarProduto" runat="server" Text="Editar" CommandName="Select"
                                            CommandArgument='<%# Eval("pro_id") %>' OnClientClick="return DesabilitarDuploClick(this,'Carregando...','', false);" />
                                        <asp:Button ID="ButtonListarProdutoSku" runat="server" Text="Sku" CommandName="Select"
                                            CommandArgument='<%# Eval("pro_id") %>' OnClientClick="return DesabilitarDuploClick(this,'Carregando...','', false);" />
                                        <asp:Button ID="ButtonListarProdutoInfo" runat="server" Text="Informações" CommandName="Select"
                                            CommandArgument='<%# Eval("pro_id") %>' OnClientClick="return DesabilitarDuploClick(this,'Carregando...','', false);" />
                                         <asp:Button ID="ButtonDuplicarProduto" runat="server" Text="Duplicar"
                                            CommandArgument='<%# Eval("pro_id") %>' OnClientClick="return DesabilitarDuploClick(this,'Carregando...','', false);" /> 
                                        <asp:Button ID="ButtonExcluirProduto" runat="server" Text="Excluir" CommandName="Select"
                                            CommandArgument='<%# Eval("pro_id") %>' OnClientClick="return confirm('Tem certeza que deseja excluir?');return DesabilitarDuploClick(this,'Carregando...','', false);" />
                                       </td>
                                </tr>
                            </SelectedItemTemplate>
                            <LayoutTemplate>
                                <table id="Table2" runat="server" width="100%">
                                    <tr id="Tr1" runat="server">
                                        <td id="Td1" runat="server">
                                            <table id="itemPlaceholderContainer" runat="server" border="1" style="text-align: left"
                                                width="100%">
                                                <tr id="Tr2" runat="server" style="width: 10%">
                                                    <th id="Th1" runat="server">Código
                                                    </th>
                                                    <th id="Th2" runat="server">Nome
                                                    </th>
                                                    <th id="Th3" runat="server">Sku(s)
                                                    </th>
                                                    <th id="Th4" runat="server">Bloqueado
                                                    </th>
                                                    <th id="Th5" runat="server">Editar
                                                    </th>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server">
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="Tr3" runat="server">
                                        <td id="Td2" runat="server" style="">                                           
                                          
                                        </td>
                                    </tr>
                                </table>
                            </LayoutTemplate>
                        </asp:ListView>

                          <asp:DataPager ID="DataPager1" runat="server" PageSize="7" PagedControlID="ListViewProduto">
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

                                                            <%# (this.DataPager1.Visible = Container.TotalRowCount > this.DataPager1.PageSize).ToString().Replace("True",string.Empty) %>

                                                        </PagerTemplate>
                                                    </asp:TemplatePagerField>
                                                </Fields>
                                            </asp:DataPager>

                        <asp:EntityDataSource ID="EntityDataSourceProduto" runat="server" OrderBy="it.[pro_id]"
                            ConnectionString="name=LojaEntities" DefaultContainerName="LojaEntities" 
                            AutoGenerateWhereClause="true"
                            EnableFlattening="False"
                            CommandText="Select 
                            it.Produto.[pro_id],
                            it.Produto.[pro_nome],
                            it.Produto.[pro_bloquear],
                            (select proSku.proSku_id from [ProdutoSku] as proSku 
                            where proSku.pro_id = it.Produto.[pro_id]) as ProSku
                            from Produto_Grupo as it where it.Grupo.[gru_id] = @gru_id">
                            <CommandParameters>
                                <asp:ControlParameter ControlID="TreeViewGrupo" Name="gru_id" DbType="Int32" PropertyName="SelectedValue" />
                                <asp:SessionParameter SessionField="loja_id" Name="loj_id" DbType="Int32" />
                            </CommandParameters>
                        </asp:EntityDataSource>
                         
                    </asp:Panel> 
                    <asp:Panel ID="PanelCadastrarProduto" runat="server" Visible="False">
                        Nome:
                    <br />
                        <asp:TextBox ID="pro_nomeTextBox" runat="server" MaxLength="64" Width="356px" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                            ValidationGroup="groupCadastrarProduto" ErrorMessage="Campo obrigatório" ControlToValidate="pro_nomeTextBox"></asp:RequiredFieldValidator>
                        <br />
                        Grupo:
                    <br />
                        <asp:TextBox ID="gru_nomeTextBox" runat="server" Columns="20" Enabled="False" ValidationGroup="groupAdicionarProdutoGrupo" />
                        <asp:HiddenField ID="gru_idHiddenField" runat="server" />
                        <asp:ModalPopupExtender ID="gru_nomeTextBox_ModalPopupExtender" runat="server" DynamicServicePath=""
                            Enabled="True" BackgroundCssClass="modalBackground" PopupControlID="PanelFrame"
                            PopupDragHandleControlID="PanelDrop" TargetControlID="LinkButtonSelecionarGrupo"
                            CancelControlID="ButtonFechar" X="350" Y="50">
                        </asp:ModalPopupExtender>
                        &nbsp;

                    <asp:Button ID="ButtonAdicionarProdutoGrupo" runat="server" Text="Adicionar"
                        OnClick="ButtonAdicionarProdutoGrupo_Click" ValidationGroup="groupAdicionarProdutoGrupo" OnClientClick="return DesabilitarDuploClick(this,'Adicionando...','groupAdicionarProdutoGrupo', true);" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="Selecione um grupo para adicionar >" Display="Dynamic" ControlToValidate="gru_nomeTextBox" ValidationGroup="groupAdicionarProdutoGrupo"></asp:RequiredFieldValidator>
                        <asp:LinkButton ID="LinkButtonSelecionarGrupo" runat="server">Selecionar Grupo</asp:LinkButton><br />
                        <asp:Repeater ID="RepeaterProdutoGrupo" runat="server">
                            <ItemTemplate>
                                <%# Eval("gru_nome")%>
                                <asp:LinkButton ID="LinkButtonRemover" runat="server"
                                    CommandArgument='<%# Eval("gru_id")%>' OnClick="LinkButtonRemover_Click">Remover</asp:LinkButton>
                                - 
                            </ItemTemplate>
                        </asp:Repeater>

                        <br />
                        Marca:
                    <br />
                        <asp:TextBox ID="mar_nomeTextBox" runat="server" Columns="10" Enabled="False" />
                        <asp:HiddenField ID="mar_idHiddenField" runat="server" />
                        <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DynamicServicePath=""
                            Enabled="True" BackgroundCssClass="modalBackground" PopupControlID="PanelFrame"
                            PopupDragHandleControlID="PanelDrop" TargetControlID="LinkButtonSelecionarMarca"
                            CancelControlID="ButtonFechar" X="350" Y="50">
                        </asp:ModalPopupExtender>
                        <asp:LinkButton ID="LinkButtonSelecionarMarca" runat="server">Selecionar Marca</asp:LinkButton>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"
                            ValidationGroup="groupCadastrarProduto" ErrorMessage="Campo obrigatório" ControlToValidate="mar_nomeTextBox"></asp:RequiredFieldValidator>
                        <br />
                        Descrição:<br />
                        &nbsp;
                    <asp:TextBox ID="pro_descricaoTextBox" runat="server" Height="119px" TextMode="MultiLine"
                        Width="526px" />
                        <br />
                        Prioridade:
                    <asp:TextBox ID="pro_posicaoTextBox" runat="server" />
                        <asp:NumericUpDownExtender ID="NumericUpDownExtender1" runat="server" Enabled="True"
                            Maximum="99" Minimum="0" RefValues="" ServiceDownMethod="" ServiceDownPath=""
                            ServiceUpMethod="" Tag="" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="pro_posicaoTextBox"
                            Width="60">
                        </asp:NumericUpDownExtender>
                        <br />
                        Página inicial de:
                    <asp:TextBox ID="pro_paginaInicialDataDeTextBox" runat="server" Columns="10" MaxLength="10" />
                        -
                    <asp:TextBox ID="pro_paginaInicialHoraDeTextBox" runat="server" MaxLength="10" Columns="10" />
                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                            Enabled="True" MaskType="Time" TargetControlID="pro_paginaInicialHoraDeTextBox"
                            Mask="99:99:99">
                        </asp:MaskedEditExtender>
                        até:
                    <asp:TextBox ID="pro_paginaInicialDataAteTextBox" runat="server" Columns="10" MaxLength="10" />
                        -
                    <asp:TextBox ID="pro_paginaInicialHoraAteTextBox" runat="server" MaxLength="10" Columns="10" />
                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                            Enabled="True" MaskType="Time" TargetControlID="pro_paginaInicialHoraAteTextBox"
                            Mask="99:99:99">
                        </asp:MaskedEditExtender>
                        <br />
                        Bloquear:
                    <asp:CheckBox ID="pro_bloquearCheckBox" runat="server" />
                        <br />
                        <asp:HiddenField ID="pro_idHiddenField" runat="server" />
                        <asp:Button ID="ButtonCancelarProduto" runat="server" Text="Cancelar" OnClick="ButtonCancelarProduto_Click" />
                        <asp:Button ID="ButtonSalvarProduto" runat="server" Text="Salvar" OnClick="ButtonSalvarProduto_Click"
                            ValidationGroup="groupCadastrarProduto" OnClientClick="return DesabilitarDuploClick(this,'Carregando...','groupCadastrarProduto', true);" />
                    </asp:Panel>
                    <asp:Panel ID="PanelListarProdutoSku" runat="server" Visible="false">
                        <asp:Button ID="ButtonIncluirProdutoSku" runat="server" Text="Incluir Sku" OnClick="ButtonIncluirProdutoSku_Click" />
                        <asp:ListView ID="ListViewProdutoSku" runat="server" DataSourceID="EntityDataSourceProdutoSku"
                            DataKeyNames="proSku_id" OnItemCommand="ListViewProdutoSku_ItemCommand">
                            <EmptyDataTemplate>
                                <table runat="server" style="">
                                    <tr>
                                        <td>Nenhum dado foi retornado.
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <tr style="text-align: left">
                                    <td>
                                        <asp:Label ID="proSku_idLabel" runat="server" Text='<%# Eval("proSku_id") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("proSku_precoAnterior") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("proSku_precoVenda") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("proSku_precoCusto") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("proSkuCor_nome") %>' />
                                    </td>
                                    <td>
                                        <asp:Label runat="server" Text='<%# Eval("proSkuTam_nome") %>' />
                                    </td>
                                    <td>
                                        <img src="/1-WebForm/imagens/produtos/cores/<%# Eval("proSkuCor_imagem") %>" onerror="this.style.display = 'none'" />
                                    </td>
                                    <td>
                                        <%# (Boolean)Eval("proSku_disponivel") == false || (Boolean)Eval("proSku_bloquear") == true || (int)(Eval("proSku_quantidadeDisponivel") is DBNull ? 1 : Eval("proSku_quantidadeDisponivel")) == 0 ? "<img src=\"imagens/bloqueadoVenda.png\" />" : "<img src=\"imagens/desbloqueadoVenda.png\" />"%>
                                    </td>
                                     <td>
                                        <%# Eval("proSku_posicao") %>
                                    </td>
                                    <td>
                                        <asp:Button ID="ButtonAlterarProdutoSku" runat="server" Text="Editar" CommandName="Select" CommandArgument='<%# Eval("proSku_id") %>' OnClientClick="return DesabilitarDuploClick(this,'Carregando...','', false);" />
                                        <asp:Button ID="ButtonAlterarProdutoSkuFoto" runat="server" Text="Fotos" CommandName="Select" CommandArgument='<%# Eval("proSku_id") %>' OnClientClick="return DesabilitarDuploClick(this,'Carregando...','', false);" />
                                        <asp:Button ID="ButtonDuplicarProdutoSku" runat="server" Text="Duplicar" CommandArgument='<%# Eval("proSku_id") %>' OnClientClick="return DesabilitarDuploClick(this,'Carregando...','', false);" />
                                        <asp:Button ID="ButtonExcluirProdutoSku" runat="server" Text="Excluir" CommandArgument='<%# Eval("proSku_id") %>'
                                            OnClientClick="return confirm('Tem certeza que deseja excluir?');return DesabilitarDuploClick(this,'Carregando...','', false);" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <LayoutTemplate>
                                <table runat="server" width="100%">
                                    <tr runat="server">
                                        <td runat="server">
                                            <table id="itemPlaceholderContainer" runat="server" border="0" style="text-align: left"
                                                width="100%">
                                                <tr runat="server" style="">
                                                    <th runat="server">Código
                                                    </th>
                                                    <th runat="server">P.Anterior
                                                    </th>
                                                    <th runat="server">P.Venda
                                                    </th>
                                                    <th runat="server">P.Custo
                                                    </th>
                                                    <th runat="server">Cor
                                                    </th>
                                                    <th runat="server">Tamanho
                                                    </th>
                                                    <th runat="server"></th>                                                   
                                                     <th runat="server"></th>
                                                    <th runat="server">Pos.</th>
                                                    <th runat="server">Editar
                                                    </th>
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
                                        <asp:Label ID="proSku_idLabel" runat="server" Text='<%# Eval("proSku_id") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("proSku_precoAnterior") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("proSku_precoVenda") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("proSku_precoCusto") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("proSkuCor_nome") %>' />
                                    </td>
                                    <td>
                                        <asp:Label runat="server" Text='<%# Eval("proSkuTam_nome") %>' />
                                    </td>
                                    <td>
                                        <img src="/1-WebForm/imagens/produtos/cores/<%# Eval("proSkuCor_imagem") %>" onerror="this.style.display = 'none'" />
                                    </td>
                                    <td>
                                        <%# (Boolean)Eval("proSku_disponivel") == false || (Boolean)Eval("proSku_bloquear") == true || (int)(Eval("proSku_quantidadeDisponivel") is DBNull ? 1 : Eval("proSku_quantidadeDisponivel")) == 0 ? "<img src=\"imagens/bloqueadoVenda.png\" />" : "<img src=\"imagens/desbloqueadoVenda.png\" />"%>
                                    </td>
                                    <td>
                                        <%# Eval("proSku_posicao") %>
                                    </td>
                                    <td>
                                        <asp:Button ID="ButtonAlterarProdutoSku" runat="server" Text="Editar" CommandName="Select" CommandArgument='<%# Eval("proSku_id") %>' OnClientClick="return DesabilitarDuploClick(this,'Carregando...','', false);" />
                                        <asp:Button ID="ButtonAlterarProdutoSkuFoto" runat="server" Text="Fotos" CommandName="Select" CommandArgument='<%# Eval("proSku_id") %>' OnClientClick="return DesabilitarDuploClick(this,'Carregando...','', false);" />
                                        <asp:Button ID="ButtonDuplicarProdutoSku" runat="server" Text="Duplicar" CommandArgument='<%# Eval("proSku_id") %>' OnClientClick="return DesabilitarDuploClick(this,'Carregando...','', false);" />
                                        <asp:Button ID="ButtonExcluirProdutoSku" runat="server" Text="Excluir" CommandArgument='<%# Eval("proSku_id") %>'
                                            OnClientClick="return confirm('Tem certeza que deseja excluir?');return DesabilitarDuploClick(this,'Carregando...','', false);" />
                                    </td>
                            </SelectedItemTemplate>
                        </asp:ListView>
                          <asp:DataPager ID="DataPager2" runat="server" PageSize="20" PagedControlID="ListViewProdutoSku">
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

                                                              <%# (this.DataPager2.Visible = Container.TotalRowCount > this.DataPager2.PageSize).ToString().Replace("True",string.Empty) %>
                                                        </PagerTemplate>
                                                    </asp:TemplatePagerField>
                                                </Fields>
                                            </asp:DataPager>
                        <asp:EntityDataSource ID="EntityDataSourceProdutoSku" runat="server" ConnectionString="name=LojaEntities"
                            DefaultContainerName="LojaEntities" EnableFlattening="False" EntitySetName="ProdutoSku"
                            OrderBy="it.[proSku_posicao], it.[proSku_id]" Select="
                        it.[pro_id],
                        it.[proSku_id],
                        it.[proSku_nome],
                        it.[proSku_precoAnterior],
                        it.[proSku_precoVenda],
                        it.[proSku_precoCusto],
                        it.[proSku_disponivel],
                        it.[proSku_quantidadeDisponivel],
                        it.[proSku_bloquear],
                        DEREF(NAVIGATE(it, LojaModel.FK_ProdutoSku_ProdutoSkuTamanho)).proSkuTam_nome,
                        DEREF(NAVIGATE(it, LojaModel.FK_ProdutoSku_ProdutoSkuCor)).proSkuCor_nome,
                        DEREF(NAVIGATE(it, LojaModel.FK_ProdutoSku_ProdutoSkuCor)).proSkuCor_imagem,
                        it.[proSku_posicao]"
                            Where="it.[pro_id] = @pro_id">
                            <WhereParameters>
                                <asp:ControlParameter ControlID="ListViewProduto" DbType="Int32" Name="pro_id" PropertyName="SelectedValue" />
                            </WhereParameters>
                        </asp:EntityDataSource>
                        <br />
                    </asp:Panel>
                    <asp:Panel ID="PanelCadastrarProdutoSku" runat="server" Visible="false">
                        Nome:
                        <asp:TextBox ID="proSku_nomeTextBox" runat="server" MaxLength="134" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="proSku_nomeTextBox" ErrorMessage="Campo obrigatório" ValidationGroup="groupCadastrarProdutoSku"></asp:RequiredFieldValidator>
                        <br />
                        Preço anterior:
                    <asp:TextBox ID="proSku_precoAnteriorTextBox" runat="server" MaxLength="7" />
                        <asp:FilteredTextBoxExtender runat="server" Enabled="True" ValidChars="0123456789.,"
                            TargetControlID="proSku_precoAnteriorTextBox">
                        </asp:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="groupCadastrarProdutoSku"
                            ControlToValidate="proSku_precoAnteriorTextBox" ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                        <br />
                        Preço de venda:
                    <asp:TextBox ID="proSku_precoVendaTextBox" runat="server" MaxLength="7" />
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                            ValidChars="0123456789.," TargetControlID="proSku_precoVendaTextBox">
                        </asp:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="groupCadastrarProdutoSku"
                            ControlToValidate="proSku_precoVendaTextBox" ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                        <br />
                        Preço de custo:
                    <asp:TextBox ID="proSku_precoCustoTextBox" runat="server" MaxLength="7" />
                        <asp:FilteredTextBoxExtender runat="server" Enabled="True" ValidChars="0123456789.,"
                            TargetControlID="proSku_precoAnteriorTextBox">
                        </asp:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="groupCadastrarProdutoSku"
                            ControlToValidate="proSku_precoCustoTextBox" ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                        <br />
                        Código de Referência:
                    <asp:TextBox ID="proSku_idReferenciaTextBox" runat="server" MaxLength="50" />
                        <br />
                        Peso:
                    <asp:TextBox ID="proSku_pesoTextBox" runat="server" MaxLength="6" />
                        <asp:FilteredTextBoxExtender runat="server" Enabled="True" ValidChars="0123456789.,"
                            TargetControlID="proSku_pesoTextBox">
                        </asp:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="groupCadastrarProdutoSku"
                            ControlToValidate="proSku_pesoTextBox" ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                        <br />
                        Altura:
                    <asp:TextBox ID="proSku_alturaTextBox" runat="server" MaxLength="7" />
                        <asp:FilteredTextBoxExtender runat="server" Enabled="True" ValidChars="0123456789.,"
                            TargetControlID="proSku_alturaTextBox">
                        </asp:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="groupCadastrarProdutoSku"
                            ControlToValidate="proSku_alturaTextBox" ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                        <br />
                        Largura:
                    <asp:TextBox ID="proSku_larguraTextBox" runat="server" MaxLength="7" />
                        <asp:FilteredTextBoxExtender runat="server" Enabled="True" ValidChars="0123456789.,"
                            TargetControlID="proSku_larguraTextBox">
                        </asp:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="groupCadastrarProdutoSku"
                            ControlToValidate="proSku_larguraTextBox" ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                        <br />
                        Comprimento:
                    <asp:TextBox ID="proSku_comprimentoTextBox" runat="server" MaxLength="7" />
                        <asp:FilteredTextBoxExtender runat="server" Enabled="True" ValidChars="0123456789.,"
                            TargetControlID="proSku_comprimentoTextBox">
                        </asp:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="groupCadastrarProdutoSku"
                            ControlToValidate="proSku_comprimentoTextBox" ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                        <br />
                        Prazo adicional de entrega:
                    <asp:TextBox ID="proSku_prazoEntregaAdicionalTextBox" runat="server" MaxLength="2" />
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                            ValidChars="0123456789" TargetControlID="proSku_prazoEntregaAdicionalTextBox">
                        </asp:FilteredTextBoxExtender>
                        <br />
                        Quantidade máxima:
                    <asp:TextBox ID="proSku_quantidadeMaximaTextBox" runat="server" MaxLength="3" />
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                            ValidChars="0123456789" TargetControlID="proSku_quantidadeMaximaTextBox">
                        </asp:FilteredTextBoxExtender>
                        <br />
                        Quantidade disponível:
                    <asp:TextBox ID="proSku_quantidadeDisponivelTextBox" runat="server" MaxLength="3" />
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                            ValidChars="0123456789" TargetControlID="proSku_quantidadeMaximaTextBox">
                        </asp:FilteredTextBoxExtender>
                        <br />
                        Disponível:
                    <asp:CheckBox ID="proSku_disponivelCheckBox" runat="server" Text="sim" />
                        <br />
                        Bloquear:
                    <asp:CheckBox ID="proSku_bloquearCheckBox" runat="server" Text="sim" />
                        <br />
                        Destaque:
                    <asp:CheckBox ID="proSku_destaqueCheckBox" runat="server" Text="sim" />
                        <br />
                          Parcelamento:<asp:DropDownList ID="proSku_parcelamentoDropDownList" runat="server" DataTextField="parc_nome"
                            DataValueField="parc_id" AppendDataBoundItems="True">
                            <asp:ListItem Value=""></asp:ListItem>
                        </asp:DropDownList> <asp:LinkButton ID="LinkButtonAtualizarParcelamento" runat="server" OnClick="LinkButtonAtualizarParcelamento_Click">Atualizar</asp:LinkButton>&nbsp; <a href="CadastroParcelamento.aspx" target="_blank">Cadastrar</a>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ValidationGroup="groupCadastrarProdutoSku"
                            ControlToValidate="proSku_parcelamentoDropDownList" ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                        <br />
                        Cor:<asp:DropDownList ID="proSku_corDropDownList" runat="server" DataTextField="proSkuCor_nome"
                            DataValueField="proSkuCor_id" AppendDataBoundItems="True">
                            <asp:ListItem Value=""></asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        Tamanho:<asp:DropDownList ID="proSku_tamanhoDropDownList" runat="server" AppendDataBoundItems="True" DataTextField="proSkuTam_nome" DataValueField="proSkuTam_id">
                            <asp:ListItem Value=""></asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        Posição:
                        <asp:TextBox ID="proSku_posicaoTextBox" runat="server" MaxLength="3" />
                        <asp:NumericUpDownExtender ID="NumericUpDownExtender2" runat="server" Enabled="True" Maximum="99" Minimum="0" TargetControlID="proSku_posicaoTextBox" Width="60">
                        </asp:NumericUpDownExtender>
                        <br />

                        <br/>
                        <asp:Button ID="ButtonCancelarProdutoSku" runat="server" Text="Cancelar" OnClick="ButtonCancelarProdutoSku_Click" />
                        <asp:Button ID="ButtonSalvarProdutoSku" runat="server" Text="Salvar" OnClick="ButtonSalvarProdutoSku_Click"
                            ValidationGroup="groupCadastrarProdutoSku" OnClientClick="return DesabilitarDuploClick(this,'Carregando...','groupCadastrarProdutoSku', true);" />
                    </asp:Panel>

                    <asp:Panel ID="PanelListarProdutoSkuFoto" runat="server" Visible="false">
                        Escolha a foto:
                            <asp:FileUpload ID="proSku_fotoFileUpload" runat="server" />
                        &nbsp;
                            <asp:Button ID="ButtonEnviarProdutoSkuFoto" runat="server" Text="Enviar Foto" OnClick="ButtonEnviarProdutoSkuFoto_Click" ValidationGroup="groupEnviarProdutoSkuFoto" OnClientClick="return DesabilitarDuploClick(this,'Enviando...','groupEnviarProdutoSkuFoto', true);" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" Display="Dynamic" ValidationGroup="groupEnviarProdutoSkuFoto" ControlToValidate="proSku_fotoFileUpload" ErrorMessage="Escolha uma imagem"></asp:RequiredFieldValidator>

                        <br />

                        <asp:ListView ID="ListViewProdutoSkuFoto" runat="server" DataSourceID="EntityDataSourceProdutoSkuFoto"
                            DataKeyNames="proSkuFot_id" OnItemCommand="ListViewProdutoSkuFoto_ItemCommand">
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
                                        <img src="/1-WebForm/imagens/produtos/fotos/<%= Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio().loj_id + "/" + ListViewProduto.SelectedValue %>/<%# Eval("proSkuFot_nome") %>-m<%# Eval("proSkuFot_extensao") %>" style="max-height: 40px" />
                                    </td>
                                    <td>
                                        <asp:Label ID="proSkuFot_posicaoLabel" runat="server" Text='<%# Eval("proSkuFot_posicao") %>' />
                                    </td>
                                    <td>
                                        <asp:Button ID="ButtonAlterarProdutoSkuFoto" runat="server" Text="Editar" CommandName="Edit" CommandArgument='<%# Eval("proSkuFot_id") %>' OnClientClick="return DesabilitarDuploClick(this,'Carregando...','', false);" />
                                        <asp:Button ID="ButtonExcluirProdutoSkuFoto" runat="server" Text="Excluir" CommandArgument='<%# Eval("proSkuFot_id") %>' OnClientClick="return confirm('Tem certeza que deseja excluir?');return DesabilitarDuploClick(this,'Carregando...','', false);" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <tr style="">
                                    <td>
                                        <asp:TextBox ID="proSkuFot_nomeTextBox" runat="server" MaxLength="134" Enabled="false" Text='<%# Eval("proSkuFot_nome") %>' />
                                        <asp:FileUpload ID="proSkuFot_nomeFileUpload" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="proSkuFot_posicaoTextBox" runat="server" Columns="2" MaxLength="2" Text='<%# Eval("proSkuFot_posicao") %>' />
                                    </td>
                                    <td>
                                        <asp:Button ID="ButtonSalvarProdutoSkuFoto" runat="server" Text="Salvar" CommandName="" CommandArgument='<%# Eval("proSkuFot_id") %>' OnClientClick="return DesabilitarDuploClick(this,'Carregando...','', false);" />
                                        <asp:Button ID="ButtonCancelarProdutoSkuFoto" runat="server" Text="Cancelar" CommandName="Cancel" CommandArgument='<%# Eval("proSkuFot_id") %>' OnClientClick="return DesabilitarDuploClick(this,'Carregando...','', false);" />
                                    </td>
                                </tr>
                            </EditItemTemplate>
                            <LayoutTemplate>
                                <table runat="server">
                                    <tr runat="server">
                                        <td runat="server">
                                            <table id="itemPlaceholderContainer" runat="server" border="0">
                                                <tr runat="server" style="text-align: left">
                                                    <th runat="server">Foto</th>
                                                    <th runat="server">Posição</th>
                                                    <th runat="server">Editar</th>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server">
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr runat="server">
                                        <td runat="server" style="">
                                            <asp:DataPager ID="DataPager1" runat="server">
                                                <Fields>
                                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True" />
                                                </Fields>
                                            </asp:DataPager>
                                        </td>
                                    </tr>
                                </table>
                            </LayoutTemplate>

                        </asp:ListView>
                        <asp:EntityDataSource ID="EntityDataSourceProdutoSkuFoto" runat="server" ConnectionString="name=LojaEntities"
                            DefaultContainerName="LojaEntities" EnableFlattening="False"
                            EntitySetName="ProdutoSkuFoto" OrderBy="it.[proSkuFot_posicao],it.[proSkuFot_id]"
                            Select="it.[proSkuFot_id],it.[proSkuFot_posicao],it.[proSkuFot_nome],it.[proSkuFot_extensao]" Where="it.[proSku_id] = @proSku_id">
                            <WhereParameters>
                                <asp:ControlParameter ControlID="ListViewProdutoSku" Name="proSku_id" DbType="Int32" PropertyName="SelectedValue" />
                            </WhereParameters>
                        </asp:EntityDataSource>

                        <asp:Panel ID="PanelCadastrarProdutoSkuFoto" runat="server" Visible="false">
                            Imagem:<asp:TextBox ID="proSkuFot_nomeTextBox" runat="server" MaxLength="134" Enabled="false" />
                            <asp:FileUpload ID="proSkuFot_nomeFileUpload" runat="server" />
                            <br />
                            Posicao:
                                <asp:TextBox ID="proSkuFot_posicaoTextBox" runat="server" MaxLength="2" />
                            <br />
                            Titulo:
                                <asp:TextBox ID="proSkuFot_tituloTextBox" runat="server" MaxLength="134" Width="483px" />
                            <br />
                            <asp:Button ID="ButtonSalvarProdutoSkuFoto" runat="server" Text="Salvar" />
                            <asp:Button ID="ButtonCancelarProdutoSkuFoto" runat="server" Text="Cancelar" />
                        </asp:Panel>

                    </asp:Panel>


                    <asp:Panel ID="PanelListarProdutoInfo" runat="server" Visible="false">
                        <asp:Button ID="ButtonIncluirProdutoInfo" runat="server" Text="Incluir" OnClick="ButtonIncluirProdutoInfo_Click" />
                        &nbsp;
                    <asp:Button ID="ButtonAlterarProdutoInfo" runat="server" Text="Alterar" OnClick="ButtonAlterarProdutoInfo_Click"
                        EnableViewState="true" Enabled="false" />&nbsp;
                    <asp:Button ID="ButtonExcluirProdutoInfo" runat="server" Text="Excluir" OnClick="ButtonExcluirProdutoInfo_Click"
                        EnableViewState="true" Enabled="false" />
                        <asp:HiddenField ID="HiddenFieldProInfo_id" runat="server" />
                        <asp:Panel ID="PanelCadastrarProdutoInfo" runat="server" EnableViewState="false"
                            Visible="false">
                            Nome:<asp:TextBox ID="proInfo_nomeTextBox" runat="server" MaxLength="64" Width="220px"
                                ValidationGroup="groupCadastrarProdutoInfo"></asp:TextBox>
                            <asp:CheckBox ID="proInfo_bloquearCheckBox" runat="server" Text="Bloquear" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Nome é obrigatório"
                                ControlToValidate="proInfo_nomeTextBox" Display="Dynamic" ValidationGroup="groupCadastrarProdutoInfo"></asp:RequiredFieldValidator>
                            <asp:Button ID="ButtonSalvarProdutoInfo" runat="server" OnClick="ButtonSalvarProdutoInfo_Click"
                                Text="Salvar" ValidationGroup="groupCadastrarProdutoInfo" OnClientClick="return DesabilitarDuploClick(this,'Carregando...','groupCadastrarProdutoInfo', true);" />
                        </asp:Panel>
                        <fieldset>
                            <legend>Grupo de informações</legend>
                            <asp:ListView ID="ListViewProdutoInfo" runat="server" DataKeyNames="proInfo_id" DataSourceID="EntityDataSourceProdutoInfo"
                                OnItemCommand="ListViewProdutoInfo_ItemCommand">
                                <EmptyDataTemplate>
                                    <table style="">
                                        <tr>
                                            <td>Nenhum grupo de informações foi encontrado.
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <ItemTemplate>
                                    <td runat="server" style="">
                                        <asp:Button ID="ButtonInfo" runat="server" CommandArgument='<%# Eval("proInfo_id")%>'
                                            CommandName="Select" Text='<%# Eval("proInfo_nome") %>' Width="100%" />
                                    </td>
                                </ItemTemplate>
                                <SelectedItemTemplate>
                                    <td runat="server" style="">
                                        <asp:Button ID="ButtonInfo" runat="server" CommandArgument='<%# Eval("proInfo_id")%>'
                                            CommandName="Select" Style="background: blue" Text='<%# Eval("proInfo_nome") %>'
                                            Width="100%" />
                                    </td>
                                </SelectedItemTemplate>
                                <LayoutTemplate>
                                    <table runat="server" border="0" style="">
                                        <tr id="itemPlaceholderContainer" runat="server">
                                            <td id="itemPlaceholder" runat="server"></td>
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                            </asp:ListView>
                        </fieldset>
                        <asp:EntityDataSource ID="EntityDataSourceProdutoInfo" runat="server" ConnectionString="name=LojaEntities"
                            DefaultContainerName="LojaEntities" OrderBy="it.[proInfo_id]" EnableFlattening="False"
                            EntitySetName="ProdutoInfo" Where="it.[pro_id] = @pro_id" Select="it.[proInfo_id],it.[pro_id],it.[proInfo_nome]">
                            <WhereParameters>
                                <asp:ControlParameter ControlID="ListViewProduto" DbType="Int32" Name="pro_id" PropertyName="SelectedValue" />
                            </WhereParameters>
                        </asp:EntityDataSource>
                    </asp:Panel>
                    <asp:Panel ID="PanelListarProdutoInfoItem" runat="server" Visible="false">
                        <asp:Button ID="ButtonIncluirProdutoInfoItem" runat="server" Text="Incluir" OnClick="ButtonIncluirProdutoInfoItem_Click" />
                        &nbsp;
                    <asp:ListView ID="ListViewProdutoInfoItem" runat="server" DataSourceID="EntityDataSourceProdutoInfoItem"
                        DataKeyNames="proInfoItem_id" OnItemCommand="ListViewProdutoInfoItem_ItemCommand">
                        <EmptyDataTemplate>
                            <span>Nenhum dado foi retornado.</span>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Label ID="proInfoItem_descricaoLabel" runat="server" Text='<%# Eval("proInfoItem_descricao") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="proInfoItem_valorLabel" runat="server" Text='<%# Eval("proInfoItem_valor") %>' />
                                </td>
                                <td>
                                    <asp:Button ID="ButtonAlterarProdutoInfoItem" runat="server" Text="Alterar" CommandName="Select"
                                        CommandArgument='<%# Eval("proInfoItem_id") %>' />
                                    <asp:Button ID="ButtonExcluirProdutoInfoItem" runat="server" Text="Excluir" CommandName="Select"
                                        CommandArgument='<%# Eval("proInfoItem_id") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <SelectedItemTemplate>
                            <tr style="background-color: Blue">
                                <td>
                                    <asp:Label ID="proInfoItem_descricaoLabel" runat="server" Text='<%# Eval("proInfoItem_descricao") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="proInfoItem_valorLabel" runat="server" Text='<%# Eval("proInfoItem_valor") %>' />
                                </td>
                                <td>
                                    <asp:Button ID="ButtonAlterarProdutoInfoItem" runat="server" Text="Alterar" CommandName="Select"
                                        CommandArgument='<%# Eval("proInfoItem_id") %>' />
                                    <asp:Button ID="ButtonExcluirProdutoInfoItem" runat="server" Text="Excluir" CommandName="Select"
                                        CommandArgument='<%# Eval("proInfoItem_id") %>' />
                                </td>
                            </tr>
                        </SelectedItemTemplate>
                        <LayoutTemplate>
                            <table id="Table2" runat="server" width="100%">
                                <tr id="Tr1" runat="server">
                                    <td id="Td1" runat="server">
                                        <table id="itemPlaceholderContainer" runat="server" border="1" style="" width="100%">
                                            <tr id="Tr2" runat="server" style="width: 10%">
                                                <th id="Th1" runat="server">Descrição
                                                </th>
                                                <th id="Th2" runat="server">Valor
                                                </th>
                                                <th id="Th6" runat="server">Editar
                                                </th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="Tr3" runat="server">
                                    <td id="Td2" runat="server" style="">
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
                                                            (<asp:Label runat="server" ID="TotalItemsLabel" Text="<%# Container.TotalRowCount%>" />
                                                            registros) </b>
                                                    </PagerTemplate>
                                                </asp:TemplatePagerField>
                                            </Fields>
                                        </asp:DataPager>
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                    </asp:ListView>
                        <asp:EntityDataSource ID="EntityDataSourceProdutoInfoItem" runat="server" ConnectionString="name=LojaEntities"
                            DefaultContainerName="LojaEntities" EnableFlattening="False" EntitySetName="ProdutoInfoItem"
                            OrderBy="it.[proInfo_id]" Where="it.[proInfo_id] = @proInfo_id" Select="it.[proInfoItem_id],it.[proInfo_id], it.[proInfoItem_descricao], it.[proInfoItem_valor]"
                            EntityTypeFilter="">
                            <WhereParameters>
                                <asp:ControlParameter ControlID="ListViewProdutoInfo" DbType="Int32" Name="proInfo_id"
                                    PropertyName="SelectedValue" />
                            </WhereParameters>
                        </asp:EntityDataSource>
                    </asp:Panel>
                    <asp:Panel ID="PanelCadastrarProdutoInfoItem" runat="server" Visible="false">
                        <asp:HiddenField ID="proInfoItem_idHiddenField" runat="server" />
                        Descrição:
                    <asp:TextBox ID="proInfoItem_descricaoTextBox" runat="server" ValidationGroup="groupCadastrarProdutoInfoItem" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Campo obrigatório"
                            ControlToValidate="proInfoItem_descricaoTextBox" Display="Dynamic" ValidationGroup="groupCadastrarProdutoInfoItem"></asp:RequiredFieldValidator>
                        <br />
                        Valor:
                    <asp:TextBox ID="proInfoItem_valorTextBox" runat="server" />
                        <br />
                        Bloquear:
                    <asp:CheckBox ID="proInfoItem_bloquearCheckBox" runat="server" />
                        <br />
                        <asp:Button ID="ButtonCancelarProdutoInfoItem" runat="server" Text="Cancelar" OnClick="ButtonCancelarProdutoInfoItem_Click" />
                        &nbsp;
                    <asp:Button ID="ButtonSalvarProdutoInfoItem" runat="server" OnClick="ButtonSalvarProdutoInfoItem_Click"
                        Text="Salvar" ValidationGroup="groupCadastrarProdutoInfoItem" OnClientClick="return DesabilitarDuploClick(this,'Carregando...','groupCadastrarProdutoInfoItem', true);" />
                    </asp:Panel>
                </td>
            </tr>
        </table>
                </td>
 </tr>
                       </table>
    </form>
</body>
</html>
