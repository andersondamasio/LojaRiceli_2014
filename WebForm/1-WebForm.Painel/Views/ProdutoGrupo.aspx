<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProdutoGrupo.aspx.cs" Inherits="Loja.Views.ProdutoGrupo" %>

<%@ Register Src="Cabecalho.ascx" TagName="Cabecalho" TagPrefix="uc1" %>
<%@ Register Src="Rodape.ascx" TagName="Rodape" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:Cabecalho ID="Cabecalho1" runat="server" />
        <table border="2" width="100%">
            <tr>
                <td style="width: 200px; vertical-align: top" rowspan="2">
                    <asp:Menu ID="MenuProdutoGrupo" runat="server" DataSourceID="XmlDataSourceGrupos"
                        NodeIndent="20" ShowLines="True" ExpandDepth="2" StaticDisplayLevels="2" StaticSubMenuIndent="10px" MaximumDynamicDisplayLevels="5" BackColor="#E3EAEB" DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#666666" OnMenuItemDataBound="MenuProdutoGrupo_MenuItemDataBound">
                        <DataBindings>
                            <asp:MenuItemBinding 
                                DataMember="MenuItem"
                                TextField="gru_nome" 
                                ValueField="gru_id"
                                Text="" 
                                Value="gru_nome" 
                                NavigateUrlField="gru_nomeAmigavel" 
                                NavigateUrl="gru_nome" 
                                SelectableField="gru_nome"
                           />
                        </DataBindings>
                        <DynamicHoverStyle BackColor="#666666" ForeColor="White" />
                        <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                        <DynamicMenuStyle BackColor="#E3EAEB" />
                        <DynamicSelectedStyle BackColor="#1C5E55" />
                        <StaticHoverStyle BackColor="#666666" ForeColor="White" />
                        <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                        <StaticSelectedStyle BackColor="#1C5E55" />
                    </asp:Menu>

                    <asp:XmlDataSource ID="XmlDataSourceGrupos" runat="server" EnableCaching="False"
                        XPath="MenuItems/MenuItem" TransformFile="~/Cache/TransformXSLTGrupo.xsl" />
                    <asp:Literal ID="LiteralMenu" runat="server"></asp:Literal>
                    <br />
                    <asp:Panel runat="server" ID="PanelFiltroLimpar">
                         <div>Filtrando por:</div> 
                           <asp:Repeater ID="RepeaterTamanhoLimpar" runat="server" DataSource='<%# Loja.Utils.Tratamento.FiltroTamanhoLimparList((string)RouteData.Values["proSku_tamanhos"]) %>' EnableViewState="false">
                            <HeaderTemplate>Tamanho:</HeaderTemplate>
                            <ItemTemplate>
                                <div>
                                    <%# Container.DataItem.ToString()  %> - <a href="<%# Page.GetRouteUrl("PaginaInicial", null)+Loja.Utils.Tratamento.FiltroTamanhoLimparRemover((string)RouteData.Values["proSku_tamanhos"],(string)RouteData.Values["proSku_cores"],Container.DataItem.ToString()) %>">...........Limpar</a>
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                <br />
                            </FooterTemplate>
                        </asp:Repeater>
                        
                        <asp:Repeater ID="RepeaterCorLimpar" runat="server" DataSource='<%# Loja.Utils.Tratamento.FiltroCorLimparList((string)RouteData.Values["proSku_cores"]) %>' EnableViewState="false">
                            <HeaderTemplate>Cor:</HeaderTemplate>
                            <ItemTemplate>
                                <div>
                                    <%# Container.DataItem.ToString()  %> - <a href="<%# Page.GetRouteUrl("PaginaInicial", null)+Loja.Utils.Tratamento.FiltroCorLimparRemover((string)RouteData.Values["proSku_cores"],(string)RouteData.Values["proSku_tamanhos"],Container.DataItem.ToString()) %>">...........Limpar</a>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <hr />
                    </asp:Panel>
                  
                    <asp:Repeater ID="RepeaterTamanho" runat="server" EnableViewState="false">
                        <HeaderTemplate>
                            <div>Tamanhos</div>
                        </HeaderTemplate>
                           <ItemTemplate>
                            <asp:Panel  runat="server" ID="PanelTamanho" Style="float: left"  Visible='<%# Loja.Utils.Tratamento.FiltroTamanhoConsultada((string)RouteData.Values["proSku_tamanhos"],(string)Eval("proSkuTam_nome")) %>'>
                                  <%# Eval("proSkuTam_nome") %>-
                            </asp:Panel>
                            <asp:Panel runat="server" ID="PanelTamanho2" Style="float: left" Visible='<%# !Loja.Utils.Tratamento.FiltroTamanhoConsultada((string)RouteData.Values["proSku_tamanhos"],(string)Eval("proSkuTam_nome")) %>' >
                                <a href="<%# Page.GetRouteUrl("PaginaInicial", null)+Loja.Utils.Tratamento.FiltroTamanho((string)RouteData.Values["proSku_tamanhos"],(string)RouteData.Values["proSku_cores"],(string)Eval("proSkuTam_nome")) %>">
                                    <%# Eval("proSkuTam_nome") %>
                                </a>-
                            </asp:Panel>
                        </ItemTemplate>
                        <FooterTemplate> 
                            <br />
                        </FooterTemplate>
                    </asp:Repeater>

                    
                    <asp:Repeater ID="RepeaterCor" runat="server" EnableViewState="false" Visible='<%# RepeaterCor.Items.Count > 0%>'>
                        <HeaderTemplate>
                            <div>Cores</div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Panel  runat="server" ID="PanelCor" Style="float: left"  Visible='<%# Loja.Utils.Tratamento.FiltroCorConsultada((string)RouteData.Values["proSku_cores"],(string)Eval("proSkuCor_nome")) %>'>
                                <img src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/produtos/cores/<%# Eval("proSkuCor_imagem") %>" onerror="this.style.display = 'none'" />-
                            </asp:Panel>
                            <asp:Panel runat="server" ID="PanelCor2" Style="float: left" Visible='<%# !Loja.Utils.Tratamento.FiltroCorConsultada((string)RouteData.Values["proSku_cores"],(string)Eval("proSkuCor_nome")) %>' >
                                    <a href="<%# Page.GetRouteUrl("PaginaInicial", null)+Loja.Utils.Tratamento.FiltroCor((string)RouteData.Values["proSku_cores"],(string)RouteData.Values["proSku_tamanhos"],(string)Eval("proSkuCor_nome")) %>">
                                    <img src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/produtos/cores/<%# Eval("proSkuCor_imagem") %>" onerror="this.style.display = 'nones'" />
                                </a>-
                            </asp:Panel>
                          
                        </ItemTemplate>
                    </asp:Repeater>
                    <br />
                </td>
                <td style="width: 100%; vertical-align: top">Ordenar por:<asp:LinkButton ID="LinkButtonOrderMenorPreco" runat="server" OnClick="LinkButtonOrderMenorPreco_Click">Menor Preço</asp:LinkButton>
                    &nbsp;
                                <asp:LinkButton ID="LinkButtonOrderMaiorPreco" runat="server" OnClick="LinkButtonOrderMaiorPreco_Click">Maior Preço</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 100%; vertical-align: top">
                    <asp:ListView ID="ListViewProdutosProdutoSku" runat="server" DataSourceID="ObjectDataSourceProduto"
                        GroupItemCount="3">
                        <EmptyDataTemplate>
                            <div>
                                Nenhum produto encontrado.
                            </div>
                        </EmptyDataTemplate>
                        <LayoutTemplate>
                            <div style="width: 100%;">
                                <asp:PlaceHolder runat="server" ID="groupPlaceHolder" />
                            </div>
                        </LayoutTemplate>
                        <GroupTemplate>
                            <div style="clear: both;">
                                <asp:PlaceHolder runat="server" ID="itemPlaceHolder" />
                            </div>
                        </GroupTemplate>
                        <ItemTemplate>
                            <div class="productItem">

                                <div id="divProdutoFoto">
                                    <input type="hidden" id='hiddenNomeAmigavel<%# Eval("pro_id") %>' name="hiddenNomeAmigavel<%# Eval("pro_id") %>" value="<%= Request.ApplicationPath.TrimEnd('/') %>/<%# Loja.Utils.Tratamento.GerarNomeAmigavel((string)Eval("pro_nome"))%>" />
                                    <a id='linkProduto1<%# Eval("pro_id") %>' href="<%= Request.ApplicationPath.TrimEnd('/') %>/<%# Eval("pro_nomeAmigavel") %>">
                                        <img width="160" height="232" id='foto<%# Eval("pro_id") %>' src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/produtos/fotos/<%=Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio().loj_id %>/<%# Eval("pro_id")%>/<%# Eval("ProdutoSkuFoto.proSkuFot_nome") %>-v<%# Eval("ProdutoSkuFoto.proSkuFot_extensao") %>" border="0" alt="<%# Eval("pro_nome") %>" onerror="semFotoProdutoVitrine(this,'<%=Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio().loj_id %>');" />
                                    </a>
                                </div>

                                <div id="divProdutoCor">
                                    <asp:Repeater ID="RepeaterProdutoSkuCor" runat="server" DataSource='<%# Eval("ProdutoSkuBean")%>'>
                                        <ItemTemplate>
                                            <span runat="server" id="PanelProdutoSkuCores" visible='<%# Eval("proSkuCor_nome")+"" == string.Empty ? false : true%>'>
                                                <a id="linkProdutoSku1<%# Eval("proSku_id") %>" href="" onload="alert('ssss');">
                                                    <img src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/produtos/cores/<%# Eval("proSkuCor_imagem")%>" title="<%# Eval("proSkuCor_nome")%>" alt="<%# Eval("proSkuCor_nome")%>"
                                                        style="cursor: pointer;" onmouseover="trocaImagemProdutoVitrine('<%# Eval("pro_id") %>','<%# Eval("proSku_id") %>','<%# Loja.Utils.Tratamento.GerarNomeAmigavel((string)Eval("proSkuCor_nome"))%>-<%# Loja.Utils.Tratamento.GerarNomeAmigavel((string)Eval("proSkuTam_nome"))%>','<%=Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio().loj_id %>','<%# Eval("ProdutoSkuFotoBean.proSkuFot_nome") %>','<%# Eval("ProdutoSkuFotoBean.proSkuFot_extensao") %>')" />
                                                </a>
                                            </span>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <a id='linkProduto2<%# Eval("pro_id") %>' href="<%= Page.GetRouteUrl("PaginaInicial", null) %><%# Eval("pro_nomeAmigavel") %>">

                                    <div id="divProdutoNome">
                                        <%# Eval("mar_nome")%><br />
                                        <b><%# Eval("pro_nome")%></b>

                                    </div>

                                    <div id="divProdutoPreco">
                                        <asp:Repeater ID="RepeaterProdutoSkuPreco" runat="server" DataSource='<%# Eval("ProdutoSkuBean")%>'>
                                            <ItemTemplate>
                                                <span runat="server" id="PanelProdutoSkuDisponivel" visible='<%# (Boolean)Eval("proSku_disponivel") %>'>
                                                    <span id='precoSku<%# Eval("proSku_id") %>' style="display: none">
                                                        <%# ((Decimal)Eval("proSku_precoAnterior") > (Decimal)Eval("proSku_precoVenda")) ? "De: R$ " + Eval("proSku_precoAnterior","{0:N}") : string.Empty%>
                                                        <%# ((Decimal)Eval("proSku_precoAnterior") > (Decimal)Eval("proSku_precoVenda")) ? "Por: R$ " + Eval("proSku_precoVenda","{0:N}") : "R$ "+Eval("proSku_precoVenda","{0:N}") %>

                                                        <asp:Panel runat="server" ID="PanelParcelamentoSku" Visible='<%#  !(bool)Eval("ParcelamentoBean.parc_bloquear") %>'>

                                                            <%# Eval("ParcelamentoBean.parc_quantidade") %> x R$
                                                 <%# Eval("ParcelamentoBean.parc_valor","{0:N}") %>
                                                            <%# (Boolean)Eval("ParcelamentoBean.parc_ativarJuro") ? string.Empty : "sem juros" %>
                                                        </asp:Panel>

                                                    </span>
                                                </span>
                                                <span runat="server" id="PanelProdutoSkuIndisponivel" visible='<%# !(Boolean)Eval("proSku_disponivel") %>'>
                                                    <span id='precoSku<%# Eval("proSku_id") %>' style="display: none">indisponível
                                                    </span>
                                                </span>

                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>

                                    <div id="divProdutoMostraPreco">
                                        <span runat="server" id="PanelProdutoSkuDisponivel" visible='<%# Eval("ProdutoSku.proSku_disponivel") %>'>
                                            <div id='preco<%# Eval("pro_id") %>'>
                                                <%# ((Decimal)Eval("ProdutoSku.proSku_precoAnterior") > (Decimal)Eval("ProdutoSku.proSku_precoVenda")) ? "De: R$ " + Eval("ProdutoSku.proSku_precoAnterior","{0:N}") : string.Empty%>
                                                <%# ((Decimal)Eval("ProdutoSku.proSku_precoAnterior") > (Decimal)Eval("ProdutoSku.proSku_precoVenda")) ? "Por: R$ " + Eval("ProdutoSku.proSku_precoVenda","{0:N}") : "R$ "+Eval("ProdutoSku.proSku_precoVenda","{0:N}") %>
                                            </div>
                                        </span>
                                        <span runat="server" id="PanelProdutoSkuIndisponivel" visible='<%# !(Boolean)Eval("ProdutoSku.proSku_disponivel") %>'>
                                            <div id='preco<%# Eval("pro_id") %>'>
                                                indisponível
                                            </div>
                                        </span>
                                    </div>

                                    <div id='divProdutoParcelamento<%# Eval("pro_id") %>'>
                                        <span runat="server" id="PanelProdutoParcelamentoDisponivel" visible='<%# (Boolean)Eval("ProdutoSku.proSku_disponivel") && !(Boolean)Eval("ProdutoSku.ParcelamentoBean.parc_bloquear") %>'>
                                            <%# Eval("ProdutoSku.ParcelamentoBean.parc_quantidade") %> x R$
                                                 <%# Eval("ProdutoSku.ParcelamentoBean.parc_valor","{0:N}") %>
                                            <%# (Boolean)Eval("ProdutoSku.ParcelamentoBean.parc_ativarJuro") ? string.Empty : "sem juros" %>
                                        </span>
                                    </div>
                                </a>
                            </div>

                        </ItemTemplate>
                        <ItemSeparatorTemplate>
                            <div class="itemSeparator">
                            </div>
                        </ItemSeparatorTemplate>
                        <GroupSeparatorTemplate>
                            <div class="groupSeparator">
                            </div>
                        </GroupSeparatorTemplate>
                        <EmptyDataTemplate>
                        </EmptyDataTemplate>
                    </asp:ListView>
                    <div style="clear: both">
                        <asp:DataPager ID="DataPager1" runat="server" PageSize="6" QueryStringField="p" PagedControlID="ListViewProdutosProdutoSku">
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

                                        <%# (this.DataPager1.Visible = Container.TotalRowCount >= this.DataPager1.PageSize).ToString().Replace("True",string.Empty) %>
                                    </PagerTemplate>
                                </asp:TemplatePagerField>
                            </Fields>
                        </asp:DataPager>
                    </div>
                    <asp:ObjectDataSource ID="ObjectDataSourceProduto" runat="server"
                        SelectCountMethod="SelecionarProdutoGrupoCont" SelectMethod="SelecionarProdutoGrupo"
                        TypeName="Loja.Modelo.Produtox.ProdutoConsulta"
                        EnablePaging="True" EnableCaching="false" OnSelecting="ObjectDataSourceProduto_Selecting">
                        <SelectParameters>
                            <asp:RouteParameter Type="String" RouteKey="gru_nomeAmigavel" Name="gru_nomeAmigavel" />
                            <asp:RouteParameter Type="String" RouteKey="proSku_cores" Name="proSku_cores" />
                            <asp:RouteParameter Type="String" RouteKey="proSku_tamanhos" Name="proSku_tamanhos" />
                            <asp:Parameter Name="startRowIndex" Type="Int32" />
                            <asp:Parameter Name="maximumRows" Type="Int32" />
                            <asp:Parameter Type="String" Name="orderBy" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
        <uc2:Rodape ID="Rodape1" runat="server" />
    </form>
</body>
</html>
