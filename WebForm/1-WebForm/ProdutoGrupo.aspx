<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProdutoGrupo.aspx.cs" Inherits="_1_WebForm.ProdutoGrupo" %>

<%@ Register Src="~/Cabecalho.ascx" TagPrefix="uc1" TagName="Cabecalho" %>
<%@ Register Src="~/Rodape.ascx" TagPrefix="uc1" TagName="Rodape" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Template Riceli</title>
    <asp:PlaceHolder runat="server">
        <script src="<%= Page.GetRouteUrl("PaginaInicial", null) %>scripts/Produto.js"></script>
        <script type="text/javascript" src="<%= Page.GetRouteUrl("PaginaInicial", null) %>scripts/jquery-1.10.2.min.js"></script>
    </asp:PlaceHolder>
</head>
<body>

    <form id="form1" runat="server">
        <div>
            <div class="persist-area">
                <uc1:Cabecalho runat="server" ID="Cabecalho" />
                <div id="banner">
                    <img src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/objetos/banner-modelo.jpg" width="800" height="237">
                </div>
                <div id="corpo">
                    <nav id="menuLateral">
                        <asp:Literal ID="LiteralMenu" runat="server"></asp:Literal>

                        <asp:Panel runat="server" ID="PanelFiltroLimpar">
                            <h1>Filtrando por:</h1>
                            <asp:Repeater ID="RepeaterProdutoSkuTamanhoLimpar" runat="server" DataSource='<%# _2_Library.Utils.Tratamento.FiltroTamanhoLimparList((string)RouteData.Values["proSku_tamanhos"]) %>' EnableViewState="false">
                                <HeaderTemplate>Tamanho:</HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <%# Container.DataItem.ToString()  %> - <a href="<%# Page.GetRouteUrl("PaginaInicial", null)+_2_Library.Utils.Tratamento.FiltroTamanhoLimparRemover((string)RouteData.Values["proSku_tamanhos"],(string)RouteData.Values["proSku_cores"],Container.DataItem.ToString()) %>">...........Limpar</a>
                                    </div>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <br />
                                </FooterTemplate>
                            </asp:Repeater>

                            <asp:Repeater ID="RepeaterProdutoSkuCorLimpar" runat="server" DataSource='<%# _2_Library.Utils.Tratamento.FiltroCorLimparList((string)RouteData.Values["proSku_cores"]) %>' EnableViewState="false">
                                <HeaderTemplate>
                                    Cor:
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <%# Container.DataItem.ToString()  %> - <a href="<%# Page.GetRouteUrl("PaginaInicial", null)+_2_Library.Utils.Tratamento.FiltroCorLimparRemover((string)RouteData.Values["proSku_cores"],(string)RouteData.Values["proSku_tamanhos"],Container.DataItem.ToString()) %>">...........Limpar</a>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </asp:Panel>


                        <asp:Panel runat="server" ID="PanelLabelTamanho">
                            <h1>Tamanho</h1>
                        </asp:Panel>
                        <div id="AlteraTamanho">
                            <asp:Panel ID="PanelProdutoSkuTamanhoLimparFiltros" runat="server" CssClass="LimparFiltros">
                                <a href="<%= Page.GetRouteUrl("PaginaInicial", null) %><%= _2_Library.Utils.Tratamento.FiltroLimparTudo((string)RouteData.Values["proSku_cores"],string.Empty) %>">Limpar selecionados</a>
                            </asp:Panel>
                            <div style="padding: 5px; height: 30px;">

                                <asp:Repeater ID="RepeaterProdutoSkuTamanho" runat="server">
                                    <ItemTemplate>
                                        <asp:Panel runat="server" ID="PanelTamanho" Visible='<%# _2_Library.Utils.Tratamento.FiltroTamanhoConsultada((string)RouteData.Values["proSku_tamanhos"],(string)Eval("proSkuTam_nome")) %>'>
                                            <span class="checado"><%# Eval("proSkuTam_nome") %></span>
                                        </asp:Panel>
                                        <asp:Panel runat="server" ID="PanelTamanho2" Visible='<%# !_2_Library.Utils.Tratamento.FiltroTamanhoConsultada((string)RouteData.Values["proSku_tamanhos"],(string)Eval("proSkuTam_nome")) %>'>
                                            <a href="<%# Page.GetRouteUrl("PaginaInicial", null)+_2_Library.Utils.Tratamento.FiltroTamanho((string)RouteData.Values["proSku_tamanhos"],(string)RouteData.Values["proSku_cores"],(string)Eval("proSkuTam_nome")) %>">
                                                <span><%# Eval("proSkuTam_nome") %></span>
                                            </a>
                                        </asp:Panel>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <asp:Panel runat="server" ID="PanelLabelCor">
                            <h1>Cor</h1>
                        </asp:Panel>
                        <div id="AlteraCor">
                            <asp:Panel ID="PanelProdutoSkuCorLimparFiltros" runat="server" CssClass="LimparFiltros">
                                <a href="<%= Page.GetRouteUrl("PaginaInicial", null) %><%= _2_Library.Utils.Tratamento.FiltroLimparTudo(string.Empty,(string)RouteData.Values["proSku_tamanhos"]) %>">Limpar selecionados</a>
                            </asp:Panel>
                            <div class="rolagem">
                                <asp:Repeater ID="RepeaterProdutoSkuCor" runat="server">
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <spam runat="server" id="PanelCor" cssclass="clear" visible='<%# _2_Library.Utils.Tratamento.FiltroCorConsultada((string)RouteData.Values["proSku_cores"],(string)Eval("proSkuCor_nome")) %>'>
                                            <div class="clear checado">
                                                <img class="Cores" src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/produtos/cores/<%# Eval("proSkuCor_imagem") %>" onerror="this.style.display = 'none'" />
                                                <%# Eval("proSkuCor_nome") %>
                                            </div>
                                        </spam>

                                        <asp:Panel runat="server" ID="PanelCor2" CssClass="clear" Style="cursor: pointer" Visible='<%# !_2_Library.Utils.Tratamento.FiltroCorConsultada((string)RouteData.Values["proSku_cores"],(string)Eval("proSkuCor_nome")) %>'>
                                            <a href="<%# Page.GetRouteUrl("PaginaInicial", null)+_2_Library.Utils.Tratamento.FiltroCor((string)RouteData.Values["proSku_cores"],(string)RouteData.Values["proSku_tamanhos"],(string)Eval("proSkuCor_nome")) %>">
                                                <img class="Cores" src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/produtos/cores/<%# Eval("proSkuCor_imagem") %>" onerror="this.style.display = 'nones'" />
                                                <%# Eval("proSkuCor_nome") %>
                                            </a>
                                        </asp:Panel>

                                    </ItemTemplate>
                                </asp:Repeater>

                            </div>
                        </div>
                        <%-- <h1>Marca</h1>
                        <div id="AlteraMarca">
                            <div class="LimparFiltros">
                                Limpar selecionados
                            </div>
                        </div> --%>
                    </nav>
                    <div id="conteudo">
                        <ul id="breadcrumb">
                            <li><a href="<%= Page.GetRouteUrl("PaginaInicial", null) %>" title="Página inicial">Início</a></li>
                            <asp:Repeater ID="RepeaterGrupoCaminho" runat="server">
                                <ItemTemplate>
                                    <li><a href="<%= Page.GetRouteUrl("PaginaInicial", null) %><%# Eval("gru_nomeAmigavel") %>"><%# Eval("gru_nome") %></a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
   
                        <div class="Ordenar">
                            <strong>Ordenar por: </strong>
                            <asp:LinkButton ID="LinkButtonOrderMenorPreco" runat="server" OnClick="LinkButtonOrderMenorPreco_Click">Menor Preço</asp:LinkButton>
                            &nbsp;|
                            <asp:LinkButton ID="LinkButtonOrderMaiorPreco" runat="server" OnClick="LinkButtonOrderMaiorPreco_Click">Maior Preço</asp:LinkButton>
                            &nbsp;|
                            <asp:LinkButton ID="LinkButtonOrderMaiorDesconto" runat="server" OnClick="LinkButtonOrderMaiorDesconto_Click">Maior Desconto</asp:LinkButton>

                        </div>
                        <div class="paginacao">
                            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="ListViewProduto_Grupo" PageSize="6" QueryStringField="pagina">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Image" ShowFirstPageButton="true" ShowLastPageButton="false" ShowNextPageButton="false" />
                                    <asp:NumericPagerField ButtonCount="5" />
                                    <asp:NextPreviousPagerField ButtonType="Image" ShowLastPageButton="true" ShowNextPageButton="true"
                                        ShowPreviousPageButton="false" />
                                    <asp:TemplatePagerField>
                                        <PagerTemplate>
                                            <%# (this.DataPager1.Visible = Container.TotalRowCount > this.DataPager1.PageSize).ToString().Replace("True",string.Empty) %>
                                        </PagerTemplate>
                                    </asp:TemplatePagerField>
                                </Fields>
                            </asp:DataPager>
                        </div>

                        <asp:ListView ID="ListViewProduto_Grupo" runat="server" DataSourceID="ObjectDataSourceProduto_Grupo">
                            <EmptyDataTemplate>
                                <span>Nenhum dado foi retornado.</span>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <div class="produto">
                                    <asp:Repeater ID="RepeaterProdutoGrupoConteudo" runat="server" DataSource='<%# Eval("produtoSkuDto")%>'>
                                        <ItemTemplate>
                                            <div id='divProSkuConteudo<%# Eval("pro_id") %><%# Eval("proSku_id") %>' class="produtoInterno" style="display: none">
                                                <asp:Panel ID="PanelProdutoSkuDesconto" runat="server" Visible='<%# ((decimal)Eval("proSku_percDesconto") > 0) %>' CssClass="produto_desconto">
                                                    -<%# Eval("proSku_percDesconto","{0:P0}") %>
                                                </asp:Panel>
                                                <a href="<%= Page.GetRouteUrl("PaginaInicial", null) %><%# Eval("gru_nomeAmigavel") %><%# Eval("pro_nomeAmigavel") %>" title="<%# Eval("proSku_nome") %> <%# Eval("proSkuCor_nome") %> <%# Eval("proSkuTam_nome") %>">
                                                    <asp:Panel ID="PanelProdutoSkuComFoto" runat="server" Visible='<%# (Eval("produtoSkuFotoDto") != null) %>'>
                                                        <img src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/produtos/fotos/<%# Eval("loj_id") %>/<%# Eval("pro_id") %>/<%# Eval("produtoSkuFotoDto.proSkuFot_nome") %>-v<%# Eval("produtoSkuFotoDto.proSkuFot_extensao") %>" alt="<%# Eval("proSku_nome") %> <%# Eval("proSkuCor_nome") %> <%# Eval("proSkuTam_nome") %>"/>
                                                    </asp:Panel>
                                                    <asp:Panel ID="PanelProdutoSkuSemFoto" runat="server" Visible='<%# !(Eval("produtoSkuFotoDto") != null) %>'>
                                                        <img src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/produtos/fotos/<%# Eval("loj_id") %>/sem-foto/sem-foto.jpg" height="232" width="160" alt="<%# Eval("proSku_nome") %> <%# Eval("proSkuCor_nome") %> <%# Eval("proSkuTam_nome") %>"/>
                                                    </asp:Panel>
                                                    <div class="produto_nome">
                                                        <%# Eval("proSku_nome") %>
                                                    </div>
                                                </a>
                                                <asp:Panel ID="PanelProdutoSkuPreco" runat="server" Visible='<%# (Boolean)Eval("proSku_disponivel") %>'>
                                                    <span runat="server" id="PanelProSkuPrecoAnterior" visible='<%# ((decimal)Eval("proSku_percDesconto") > 0) %>'>De <%# Eval("proSku_precoAnterior") %>
                                                    </span>
                                                    <span class="produto_preco">Por <%# Eval("proSku_precoVenda") %></span>
                                                   <asp:Repeater ID="RepeaterProdutoSkuParcelamentoParcela" runat="server" DataSource='<%# ((List<_2_Library.Dao.Site.Produto_GrupoX.ParcelamentoParcelaDto>)Eval("parcelamentoDto.parcelamentoParcelaDto")).OrderByDescending(s=>s.parcPar_quantidade).GroupBy(s => s.parcPar_percentualJuro).Select(s => s.FirstOrDefault()).OrderBy(s=>s.parcPar_percentualJuro)  %>'>
                                                                <ItemTemplate>
                                                                      <asp:Panel runat="server" ID="PanelSemJuros" Visible='<%# (decimal)Eval("parcPar_percentualJuro") == 0 %>' Wrap="false">
                                                                         em até <%# Eval("parcPar_quantidade") %>x 
                                                                         R$ <%# Eval("parcPar_valor","{0:N}") %> 
                                                                    </asp:Panel>
                                                                     <asp:Panel runat="server" ID="PanelComJuros" Visible='<%# (decimal)Eval("parcPar_percentualJuro") != 0 %>' Wrap="false">
                                                                         ou até <%# Eval("parcPar_quantidade") %>x 
                                                                          R$ <%# Eval("parcPar_valor","{0:N}") %>
                                                                    </asp:Panel>
                                                                </ItemTemplate>
                                                            </asp:Repeater>

                                                    <asp:Panel runat="server" ID="PanelEntregaGratis" CssClass="produto_frete" Visible='<%#!(bool)Eval("entregaDto.ent_bloquear") &&  (bool)Eval("entregaDto.ent_gratis") %>'>
                                                        Frete Grátis
                                                    </asp:Panel>
                                                </asp:Panel>

                                                <asp:Panel ID="PanelProdutoSkuIndisponivel" runat="server" Visible='<%# !(Boolean)Eval("proSku_disponivel") %>' Style="height: 51px">
                                                    Indisponível
                                                </asp:Panel>

                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Repeater ID="RepeaterProdutoGrupoCor" runat="server" DataSource='<%# Eval("produtoSkuDto")%>'>
                                        <ItemTemplate>
                                            <span id='divCor<%# Eval("proSku_id") %>'>
                                                <img id='proSkuCor' title="escolher cor <%# Eval("proSkuCor_nome") %>" onerror="SelecionaProSkuCorOnError(this);" onload="if(<%#Container.ItemIndex%> == 0)SelecionaProSkuCor('<%# Eval("pro_id") %>','<%# Eval("proSku_id") %>')" onmouseover="SelecionaProSkuCor('<%# Eval("pro_id") %>','<%# Eval("proSku_id") %>')" src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/produtos/cores/<%# Eval("proSkuCor_imagem") %>" style="margin: 3px; cursor: pointer" /></span>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                 <%# (Container.DisplayIndex+1) % 3 == 0 ? "<div style=\"clear: both; height: 10px;\"></div>" : string.Empty  %>
                            </ItemTemplate>
                            <LayoutTemplate>
                                <div style="" id="itemPlaceholderContainer" runat="server">
                                    <span runat="server" id="itemPlaceholder" />
                                </div>
                            </LayoutTemplate>

                        </asp:ListView>
                        <div class="paginacao">
                            <asp:DataPager ID="DataPager2" runat="server" PagedControlID="ListViewProduto_Grupo" PageSize="6" QueryStringField="pagina">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Image" ShowFirstPageButton="true" ShowLastPageButton="false" ShowNextPageButton="false" />
                                    <asp:NumericPagerField ButtonCount="5" />
                                    <asp:NextPreviousPagerField ButtonType="Image" ShowLastPageButton="true" ShowNextPageButton="true"
                                        ShowPreviousPageButton="false" />
                                    <asp:TemplatePagerField>
                                        <PagerTemplate>
                                            <%# (this.DataPager2.Visible = Container.TotalRowCount > this.DataPager2.PageSize).ToString().Replace("True",string.Empty) %>
                                        </PagerTemplate>
                                    </asp:TemplatePagerField>
                                </Fields>
                            </asp:DataPager>
                        </div>
                    </div>
                    <!-- Fecha div Conteudo -->
                </div>
            </div>
            <uc1:Rodape runat="server" ID="Rodape" />
            <asp:ObjectDataSource ID="ObjectDataSourceProduto_Grupo" runat="server"
                SelectMethod="SelectProduto_GrupoProdutoGrupo" TypeName="_2_Library.Dao.Site.Produto_GrupoX.Produto_GrupoTd"
                EnablePaging="True" SelectCountMethod="SelectProduto_GrupoProdutoGrupoCount" OnSelecting="ObjectDataSourceProduto_Selecting">
                <SelectParameters>
                    <asp:Parameter Name="loj_dominio" Type="String" />
                    <asp:RouteParameter Type="String" Name="gru_nomeAmigavel" />
                    <asp:RouteParameter Type="String" RouteKey="proSku_cores" Name="proSku_cores" />
                    <asp:RouteParameter Type="String" RouteKey="proSku_tamanhos" Name="proSku_tamanhos" />
                    <asp:Parameter Name="startRowIndex" Type="Int32" />
                    <asp:Parameter Name="maximumRows" Type="Int32" />
                    <asp:Parameter Name="orderBy" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
    </form>
</body>
</html>
