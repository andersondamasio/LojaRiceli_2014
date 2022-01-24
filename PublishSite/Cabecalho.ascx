<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Cabecalho.ascx.cs" Inherits="_1_WebForm.Cabecalho" EnableViewState="true" %>
<%@ Register src="UserControls/MensagemBoasVindas.ascx" tagname="MensagemBoasVindas" tagprefix="uc1" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<link href="<%= Page.GetRouteUrl("PaginaInicial", null) %>css/estilos.css" rel="stylesheet" type="text/css" />
<asp:Literal ID="LiteralCssLoja" runat="server"></asp:Literal>
<script src="<%= Page.GetRouteUrl("PaginaInicial", null) %>scripts/global.js"></script>
<script src="<%= Page.GetRouteUrl("PaginaInicial", null) %>scripts/tabber-minimized.js"></script>
<link href="<%= Page.GetRouteUrl("PaginaInicial", null) %>css/example.css" rel="stylesheet" />
<!--[if gte IE 9]>
  <style type="text/css">
    .gradient {
       filter: none;
    }
  </style>
<![endif]-->
<div id="topo">
     <uc1:MensagemBoasVindas ID="MensagemBoasVindas" runat="server" />
    <img src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/objetos/ico_conta.png" style="vertical-align: -20%; margin-right: 5px; margin-left: 5px;">
    <a href="<%= Page.GetRouteUrl("MeuCadastro", null) %>">Sua Conta</a> |
    <img src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/objetos/ico_pedidos.png" style="vertical-align: -20%; margin-right: 5px; margin-left: 5px;">
    <a href="<%= Page.GetRouteUrl("MeusPedidos", null) %>">Seus Pedidos</a>
</div>
<div id="header">
    <div>
        <div style="width: 1003px; margin: auto; height: 100px;">
            <a href="<%= Page.GetRouteUrl("PaginaInicial", null) %>">
                <img src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/objetos/logo_header.png" title="Página Inicial" id="Logotipo"></a>
            <asp:Panel id="PanelBusca" runat="server" DefaultButton="ImageButtonBusca" CssClass="buscaBox">
                  <asp:TextBox ID="TextBoxBusca" runat="server" Text="O que você deseja?" CssClass="busca" onfocus="if(this.value=='O que você deseja?')this.value='';" onblur="if(this.value=='')this.value='O que você deseja?';"></asp:TextBox>
                <!-- Se alterar o id do campo busca, alterar no css tb -->
                  <asp:ImageButton ID="ImageButtonBusca" runat="server" ImageUrl="~/imagens/objetos/botao-busca.png" style="float: right; margin: 2px;" OnClick="ImageButtonBusca_Click" />
            </asp:Panel>
            
            <div id="BotaoCarrinho" title="Meu Carrinho de compras" style="cursor:pointer" onclick="location.href = '<%= Page.GetRouteUrl("Carrinho", null) %>'">
                <strong style="padding-left: 15px;"><a id="linkCarrinho" href="<%= Page.GetRouteUrl("Carrinho", null) %>" style="color:white;">Seu Carrinho (<%= RepeaterCarrinho.Items.Count %>)</a></strong>
                <div id="VisualizarCarrinho">
                    <h1><%= RepeaterCarrinho.Items.Count %> produto no meu carrinho</h1>
                    <asp:Repeater ID="RepeaterCarrinho" runat="server" DataSourceID="ObjectDataSourceCarrinho">
                        <ItemTemplate>
                         <span class="itensCarrinho">
                            <hr noshade>
                            <asp:Panel ID="PanelProdutoSkuComFoto" runat="server" Visible='<%# (Eval("produtoSkuFotoDto") != null) %>'>
                                <img src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/produtos/fotos/<%# Eval("loj_id") %>/<%# Eval("pro_id") %>/<%# Eval("produtoSkuFotoDto.proSkuFot_nome") %>-m<%# Eval("produtoSkuFotoDto.proSkuFot_extensao") %>" height="50" style="float: left;" />
                            </asp:Panel>
                            <asp:Panel ID="PanelProdutoSkuSemFoto" runat="server" Visible='<%# !(Eval("produtoSkuFotoDto") != null) %>'>
                                <img src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/produtos/fotos/<%# Eval("loj_id") %>/sem-foto/sem-foto.jpg" height="50" style="float: left;" />
                            </asp:Panel>

                            <div class="Car_nome_prod"><%#Eval("proSku_nome") %> <%#Eval("proSkuCor_nome") %> <%#Eval("proSkuTam_nome") %></div>
                            <div class="Car_nome_preco">
                                <%#Eval("proSku_precoVenda") %>
                            </div>
                           </span>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:ObjectDataSource ID="ObjectDataSourceCarrinho" runat="server"
                        SelectMethod="SelectCarrinho" TypeName="_2_Library.Dao.Site.CarrinhoX.CarrinhoTd" OldValuesParameterFormatString="original_{0}">
                        <SelectParameters>
                            <asp:Parameter Name="loj_dominio" Type="String" />
                            <asp:SessionParameter Name="car_sessionId" SessionField="car_sessionId" Type="String" />
                            <asp:Parameter Name="ent_cep" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>

                </div>
            </div>
        </div>
    </div>
    <div id="MenuPrincipal">
        <a href="#">
            <img src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/objetos/btn_home.png" style="padding: 5px; float: left;" title="Página Inicial"></a>
       <%--<div id="BotaoCategorias">
            <a href="#">Todas as categorias</a>
            <div id="submenu">
                <asp:Literal ID="LiteralMenuTodos" runat="server" Visible="false"></asp:Literal>
                <ul class="ul_coluna">
                    <li>Categoria </li>
                    <li>Categoria </li>
                    <li>Categoria </li>
                    <li>Categoria </li>
                    <li>Categoria </li>
                    <li>Categoria </li>
                    <li>Categoria </li>
                </ul>
                <ul>
                    <li>Categoria </li>
                    <li>Categoria </li>
                </ul>
            </div>
        </div> --%> 

        <asp:Menu ID="MenuGrupo" runat="server"  DataSourceID="XmlDataSourceGrupos"
            ExpandDepth="2" StaticDisplayLevels="3" StaticMenuStyle-Height="15px" Orientation="Horizontal" DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="0.8em" StaticSubMenuIndent="10px" BackColor="#E3EAEB" ForeColor="#666666">
            <DataBindings>
                <asp:MenuItemBinding
                    DataMember="MenuItem"
                    TextField="gru_nome"
                    ValueField="gru_id"
                    Text=""
                    Value="gru_nome"
                    NavigateUrlField="gru_nomeAmigavel"
                    NavigateUrl="gru_nome"
                    SelectableField="gru_nome" />
            </DataBindings>
            <DynamicHoverStyle BackColor="#666666" ForeColor="White" />
            <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
            <DynamicMenuStyle BackColor="#E3EAEB" VerticalPadding="1px" />
            <DynamicSelectedStyle BackColor="#1C5E55" VerticalPadding="1px" />
            <StaticHoverStyle BackColor="#666666" ForeColor="White" />
            <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />

<StaticMenuStyle Height="15px"></StaticMenuStyle>

            <StaticSelectedStyle BackColor="#1C5E55" VerticalPadding="1px" />
        </asp:Menu>
    </div>
</div>



<asp:XmlDataSource ID="XmlDataSourceGrupos" runat="server" CacheDuration="1"
    XPath="MenuItems/MenuItem" TransformFile="~/App_Code/Cache/TransformXSLTGrupo.xsl" />
