<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeusAmigos.aspx.cs" Inherits="_1_WebForm.MeusAmigos" %>

<%@ Register Src="~/Cabecalho.ascx" TagPrefix="uc1" TagName="Cabecalho" %>
<%@ Register Src="~/Rodape.ascx" TagPrefix="uc1" TagName="Rodape" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="scripts/Social.js"></script>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <script src="<%= Page.GetRouteUrl("PaginaInicial", null) %>scripts/Validacao.js"></script>
    </asp:PlaceHolder>


</head>
<body>
    <div id="fb-root"></div>
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
                                <li>Meus Amigos</></li>
                                <li class="aba2"><a href="MeusPontos">Meus Pontos</a></li>
                                <li class="aba2" runat="server" visible="false"><a href="MeuFeed">Feed de Compras</a></li>
                            </ul>
                        </div>
                        <asp:Panel ID="PanelAmigos" runat="server" Visible="false">
                            Seu perfil está desconectado. Comece agora<br />

                            <asp:LinkButton ID="LinkButtonLoginFacebookAmigos" runat="server" OnClientClick="return LoginFB();">Clique aqui para conectar aos seus amigos.</asp:LinkButton>

                        </asp:Panel>

                        <table border="1">
                            <tr>
                                <td style="text-align: center; vertical-align: top;"><b>Amigos Conectados</b><br />
                                    <div style="position: relative; width: 250px; height: 495px; z-index: 1; overflow: scroll;">
                                        <asp:Repeater ID="RepeaterAmigosConectados" runat="server">
                                            <ItemTemplate>
                                                <img border="0" height="50" src="http://graph.facebook.com/<%# Eval("uid") %>/picture?type=normal" class="avatar"><br />
                                                <%# Eval("name") %>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </td>
                                <td style="text-align: center; vertical-align: top;"><b>Amigos Desconectados - <a style="cursor: pointer" onclick="EnviarConviteTodos();">conectar todos</a></b><br />
                                    <div style="position: relative; width: 250px; height: 495px; z-index: 1; overflow: scroll;">
                                        <asp:Repeater ID="RepeaterAmigosDesconectados" runat="server">
                                            <ItemTemplate>
                                                <img style="opacity: 0.4; filter: alpha(opacity=40);" border="0" height="50" src="http://graph.facebook.com/<%# Eval("uid") %>/picture?type=normal" class="avatar"><br />
                                                <%# Eval("name") %>- <a name="idPerfil" id="<%# Eval("uid") %>" href="#" onclick="EnviarConvite('<%# Eval("uid") %>');">Conectar</a>
                                                <br />
                                            </ItemTemplate>
                                        </asp:Repeater>
                                          <asp:Panel ID="PanelAmigosNaoEncontrado" runat="server" Visible="false">
                                            Olá, infelizmente não possível recuperar sua lista de amigos, porém ela poderá ser contabilizada a medida que eles forem se cadastrando no site.
                                        </asp:Panel>
                                    </div>
                                </td>
                                <td style="vertical-align: top;">
                                    A cada amigo conectado você acumula pontos e pode trocar por produtos da loja, comece agora!
                                </td>
                            </tr>
                        </table>
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

