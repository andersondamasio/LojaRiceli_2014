<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CabecalhoFinalizarPedido.ascx.cs" Inherits="Loja.Views.CabecalhoFinalizarPedido" %>

<link href="<%= Page.GetRouteUrl("PaginaInicial", null) %>Views/css/global.css" rel="stylesheet" type="text/css" />
<script src="<%= Page.GetRouteUrl("PaginaInicial", null) %>Views/js/Global.js"></script>

<div id="divCarregando" style="display:none">
    <img  src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/outros/zoomloader.gif"/>
</div>


<div>
Cabeçalho
    <br/>
    <a href="<%= Page.GetRouteUrl("PaginaInicial", null) %>">Inicial</a>
</div>

