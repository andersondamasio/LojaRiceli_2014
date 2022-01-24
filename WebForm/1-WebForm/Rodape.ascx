<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Rodape.ascx.cs" Inherits="_1_WebForm.Rodape" %>

<div id="barraRodape">
  <ul>
    <li>Dúvidas Frequentes</li>
    <li>Política de Trocas e Devoluções</li>
    <li>Prazo de Entrega</li>
    <li>Como Comprar</li>
    <li>Mapa do Site</li>
    <li><a href="institucional/potitica-de-privacidade.aspx">Política de Privacidade</a></li>
    <li>Política de Frete</li>
    <li onclick="alert('texto titulo e descricao e email para contato. Modelo http://www.magazineluiza.com.br/parcerias');">Parceria</li>
    <li onclick="alert('Formulário para contato com nome, email, telefone, envio de anexo e mensagem');">Trabalhe Conosco</li>
    <li><a href="http://blog.riceli.com.br" target="_blank" title="Blog Riceli">Blog Riceli</a></li>
    <li>Sobre</li>
  </ul>

</div>
<div style="width:1003px; margin:auto;">
    <div id="col1">
    <h1>Formas de Pagamento</h1>
    <p><img src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/objetos/cart_visa.png" width="50" height="30"><img src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/objetos/cart_master.png" width="50" height="30"><img src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/objetos/cart_amex.png" width="50" height="30"><img src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/objetos/boleto.png" width="50" height="30"></p>
    </div>
    <div id="col2">
    <a href="https://developers.facebook.com/docs/plugins/follow-button">Followers</a></div>
    <div id="col3">
        <h1>+ Riceli</h1>
        <p><img src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/objetos/sprint_social_network.png" width="271" height="29"></p>
    </div>
</div>
<div id="copyright">xxx - xxxx xxxxx / CNPJ: xx.xxx.xxx/xxxx-xxx / Endereço: Rua xxxxxxxx xxxxxx, 000 - xxxxxxxxx, xx - xxxxx-xxx </div>