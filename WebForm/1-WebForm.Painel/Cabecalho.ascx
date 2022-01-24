<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Cabecalho.ascx.cs" Inherits="Loja.Painel.Cabecalho" %>
<link href="css/global.css" rel="stylesheet" type="text/css" />
<script src="js/utils.js" type="text/javascript"></script>
<link href="css/ui-lightness/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />

<script src="js/jquery-1.9.1.min.js" type="text/javascript"></script>
<script src="js/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
<script src="js/jquery-ui-1.10.3.custom.js" type="text/javascript"></script>
<script src="js/jquery.ui.datepicker-pt-BR.js" type="text/javascript"></script>
<div>

    <asp:Menu ID="MenuCabecalho" runat="server" Orientation="Horizontal" 
    BackColor="#E3EAEB" DynamicHorizontalOffset="2" Font-Names="Verdana" 
    Font-Size="11px" ForeColor="#666666" StaticSubMenuIndent="10px" OnMenuItemDataBound="MenuCabecalho_MenuItemDataBound" DataSourceID="XmlDataSourceMenuCabecalho">
    <StaticHoverStyle ForeColor="#FFFFFF" CssClass="Menu-Skin-DynamicHover" 
        BackColor="#666666" />
    <DynamicMenuItemStyle HorizontalPadding="8px" VerticalPadding="2px" />
    <DynamicHoverStyle ForeColor="#FFFFFF" CssClass="Menu-Skin-DynamicHover" 
        BackColor="#666666" />
    <DynamicMenuStyle BackColor="#E3EAEB" />
    <DynamicSelectedStyle BackColor="#1C5E55" />
    
        <DataBindings>
        <asp:MenuItemBinding DataMember="Item" NavigateUrlField="NavigateUrl" ValueField="Value" TextField="Text" />
    </DataBindings>
      

    <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
    <StaticSelectedStyle BackColor="#1C5E55" />
</asp:Menu>

        <asp:XmlDataSource ID="XmlDataSourceMenuCabecalho" runat="server" DataFile="~/App_Data/MenuPainel.xml"
            XPath="/Items/Item"></asp:XmlDataSource>

        </div>
        <hr/>