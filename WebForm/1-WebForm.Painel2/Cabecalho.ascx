<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Cabecalho.ascx.cs" Inherits="Loja.Painel.Cabecalho" %>
<link href="css/global.css" rel="stylesheet" type="text/css" />
<script src="js/utils.js" type="text/javascript"></script>
<link href="css/ui-lightness/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />

<script src="js/jquery-1.9.1.min.js" type="text/javascript"></script>
<script src="js/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
<script src="js/jquery-ui-1.10.3.custom.js" type="text/javascript"></script>
<script src="js/jquery.ui.datepicker-pt-BR.js" type="text/javascript"></script>
<div style="z-index: 1000">
    <table style="width: 100%">
        <tr>
            <td>
                <asp:Menu ID="MenuCabecalho" runat="server" Orientation="Horizontal" 
                    BackColor="#E3EAEB" DynamicHorizontalOffset="2" Font-Names="Verdana"
                    Font-Size="11px" ForeColor="#666666" StaticSubMenuIndent="10px">
                    <Items>
                        <asp:MenuItem Text="Configurações" Value="usuPer_configuracao">
                            <asp:MenuItem Text="Boletos" Value="Boletos" NavigateUrl="~/ConfiguracaoBoleto.aspx" />
                            <asp:MenuItem Text="Cartões" Value="Cartões" NavigateUrl="~/ConfiguracaoCartao.aspx"></asp:MenuItem>
                            <asp:MenuItem Text="Parcelamentos" Value="Parcelamentos" NavigateUrl="~/CadastroParcelamento.aspx"></asp:MenuItem>
                            <asp:MenuItem Text="Pedido" Value="Pedido">
                                <asp:MenuItem Text="Status" Value="Status" NavigateUrl="~/ConfiguracaoPedidoStatus.aspx"></asp:MenuItem>
                            </asp:MenuItem>
                            <asp:MenuItem Text="Lojas" Value="usuPer_lojaSelecionar" NavigateUrl="~/ConfiguracaoLoja.aspx"></asp:MenuItem>
                        </asp:MenuItem>
                        <asp:MenuItem Text="Recursos" Value="Recursos">
                            <asp:MenuItem NavigateUrl="~/RecursosCarrinho.aspx" Text="Carrinho" Value="Carrinho"></asp:MenuItem>
                        </asp:MenuItem>
                        <asp:MenuItem Text="Usuários" Value="" NavigateUrl="~/Usuario.aspx"></asp:MenuItem>
                        <asp:MenuItem Text="Avise-me" Value="" NavigateUrl="~/ProdutoAviso.aspx"></asp:MenuItem>
                        <asp:MenuItem Text="Avaliações" Value="" Enabled="False"></asp:MenuItem>
                        <asp:MenuItem Text="Banners" Value="" Enabled="False"></asp:MenuItem>
                        <asp:MenuItem NavigateUrl="~/CadastroGrupo.aspx" Text="Produtos" Value="Produtos"></asp:MenuItem>
                        <asp:MenuItem Text="Clientes" Value="" NavigateUrl="~/Cliente.aspx"></asp:MenuItem>
                        <asp:MenuItem Text="Cupons" Value="" NavigateUrl="~/Cupom.aspx"></asp:MenuItem>
                        <asp:MenuItem Text="Pedidos" Value="" NavigateUrl="~/Pedido.aspx"></asp:MenuItem>
                        <asp:MenuItem Text="Newsletter" Value="" Enabled="False"></asp:MenuItem>
                        <asp:MenuItem Text="Entrega" Value="" NavigateUrl="~/Entrega.aspx"></asp:MenuItem>
                        <asp:MenuItem Text="Relatórios" Value="Relatórios" Enabled="False"></asp:MenuItem>
                    </Items>
                    <StaticHoverStyle ForeColor="#FFFFFF" CssClass="Menu-Skin-DynamicHover" BackColor="#666666" />
                    <DynamicMenuItemStyle HorizontalPadding="8px" VerticalPadding="2px" CssClass="Menu-Skin-DynamicHover" />
                    <DynamicHoverStyle ForeColor="#FFFFFF" CssClass="Menu-Skin-DynamicHover" BackColor="#666666" />
                    <DynamicMenuStyle BackColor="#E3EAEB" />
                    <DynamicSelectedStyle BackColor="#1C5E55" />
                    <DataBindings>
                        <asp:MenuItemBinding DataMember="Item" NavigateUrlField="NavigateUrl" ValueField="Value" TextField="Text" />
                    </DataBindings>
                    <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                    <StaticSelectedStyle BackColor="#1C5E55" />
                </asp:Menu>
            </td>
            <td>
                <asp:LinkButton ID="LinkButtonsair" runat="server" OnClick="LinkButtonsair_Click">Sair</asp:LinkButton></td>
        </tr>
    </table>
    <asp:XmlDataSource ID="XmlDataSourceMenuCabecalho" runat="server" DataFile="~/App_Data/MenuPainel.xml"
        XPath="/Items/Item"></asp:XmlDataSource>


</div>

<hr />
