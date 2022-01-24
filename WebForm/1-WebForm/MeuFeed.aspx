<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeuFeed.aspx.cs" Inherits="_1_WebForm.MeuFeed" %>


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
                                <li class="aba2"><a href="MeusEnderecos">Meus Endereços</a></li>
                                <li class="aba2"><a href="MeusPedidos">Meus Pedidos</a></li>
                                <li class="aba2"><a href="MeusAmigos">Meus Amigos</a></li>
                                <li class="aba2"><a href="MeusPontos">Meus Pontos</a></li>
                                <li>Feed de Compras</li>

                            </ul>
                        </div>

                        Aqui você pode ver as compras efetuadas por seus <a href="MeusAmigos">amigos conectados</a>, assim você pode trocar ideais e sugestões de pessoas que conhece.<br />
                        <br />

                        Apenas <a href="MeusAmigos">seus amigos conectados</a> poderam ver suas compras, você pode alterar isso no menu Privacidade.<br />
                        <br/>
                        <center>
                        <table style="text-align: center;border: 1px solid #123456;">
                            <tr><td>
                        <asp:Repeater ID="RepeaterPedidoFeed" runat="server">
                            <ItemTemplate>
                               <div style="text-align:left;">Olha só o que seu amigo <b><%# Eval("pedidoDto.ped_cliEnd_nome")  %></b> acabou de escolher:</div><br />

                                <asp:Repeater ID="RepeaterPedidoProduto" runat="server" DataSource='<%# Eval("pedidoDto.pedidoProdutoDto") %>'>
                                    <ItemTemplate>
                                        <a href="<%= Page.GetRouteUrl("PaginaInicial", null) %><%# Eval("pedPro_pro_nomeAmigavel") %>">
                                             <div style="float: left; overflow: hidden;">
                                            <asp:Repeater ID="RepeaterProdutoSkuFoto" runat="server" DataSource='<%# Eval("produtoSkuFotoDto") %>'>
                                                <ItemTemplate>
                                                   
                                                        <img  src="<%= Page.GetRouteUrl("PaginaInicial", null) %>imagens/produtos/fotos/<%# Eval("loj_id") %>/<%# Eval("pro_id") %>/<%# Eval("proSkuFot_nome") %>-v<%# Eval("proSkuFot_extensao") %>" /><br />
                                                   
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <%# Eval("pedPro_pro_nome") %> </div>
                                        </a>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </ItemTemplate>
                        </asp:Repeater>
                         </td></tr></table></center>
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
