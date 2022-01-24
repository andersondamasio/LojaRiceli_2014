<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SocialLinksCompartilhar.ascx.cs" Inherits="_1_WebForm.UserControls.SocialLinksCompartilhar" %>
<div id="compartilhe">  
    <a href="http://www.facebook.com/share.php?u=<asp:Literal ID="LiteralLinkCompatilharFb" runat="server"></asp:Literal>" onclick="return AbrirJanela(this, 600, 600);">
        <img src="imagens/objetos/ic_fb.png" style="margin: 5px;" alt="Compartilhar com Facebook" /></a>
    <a href="https://plus.google.com/share?url=<asp:Literal ID="LiteralLinkCompatilharGp" runat="server"></asp:Literal>" onclick="return AbrirJanela(this, 600, 600);">
        <img src="imagens/objetos/ic_gpl.png" style="margin: 5px;" alt="Compartilhar Google+" /></a>
    <a href="https://twitter.com/intent/tweet?original_referer=<asp:Literal ID="LiteralLinkCompatilharTw" runat="server"></asp:Literal>&amp;text=Olha gente&amp;tw_p=tweetbutton&url=<asp:Literal ID="LiteralLinkCompatilharTw2" runat="server"></asp:Literal>&amp;via=LojaRiceli" onclick="return AbrirJanela(this, 600, 600);">
        <img src="imagens/objetos/ic_twt.png" style="margin: 5px;" /></a>
</div>
<div>Ao compatilhar você Ganha Pontos e troca por produtos na loja. <a href="<%= Page.GetRouteUrl("PaginaInicial", null) %>institucional/ajuda/como-posso-ganhar-pontos.aspx" target="_blank">Veja como aqui</a></div>