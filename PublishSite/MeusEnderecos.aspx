<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeusEnderecos.aspx.cs" Inherits="_1_WebForm.MeusEnderecos" %>

<%@ Register Src="~/Cabecalho.ascx" TagPrefix="uc1" TagName="Cabecalho" %>
<%@ Register Src="~/Rodape.ascx" TagPrefix="uc1" TagName="Rodape" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <script src="<%= Page.GetRouteUrl("PaginaInicial", null) %>scripts/Validacao.js"></script>
    </asp:PlaceHolder>
</head>
<body>
    <form id="form1" runat="server">
            <div class="persist-area">
                <uc1:Cabecalho runat="server" ID="Cabecalho" />
        <div id="corpo">
                    <div id="conteudo" style="width: 990px; margin-top: 5px;">
                        <h1>Minha Conta</h1>
                        <div id="conta">
                            <div style="border-bottom: 1px solid #8A8A7B; height: 30px;">
                                <ul class="abas">
                                    <li class="aba2"><a href="MeuCadastro">Meu Cadastro</a></li>
                                    <li>Meus Endereços</li>
                                    <li class="aba2"><a href="MeusPedidos">Meus Pedidos</a></li>
                                    <li class="aba2"><a href="MeusAmigos">Meus Amigos</a></li>
                                    <li class="aba2"><a href="MeusPontos">Meus Pontos</a></li>
                                    <li class="aba2" runat="server" visible="false"><a href="MeuFeed">Feed de Compras</a></li>
                                </ul>
                            </div>
                            Os campos do form do cadastro são os mesmos da finalizaçao do pedido 


                        </div>
                        <div style="clear: both; height: 10px;"></div>
                    </div>
                    <!-- Fecha div Conteudo -->
                    <uc1:Rodape runat="server" ID="Rodape" />
                </div>
                </div>
    </form>
</body>
</html>

