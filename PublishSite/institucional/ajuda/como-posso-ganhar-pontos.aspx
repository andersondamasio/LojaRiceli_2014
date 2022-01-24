<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="como-posso-ganhar-pontos.aspx.cs" Inherits="_1_WebForm.institucional.ajuda.como_posso_ganhar_pontos" %>

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
<h1>Como posso ganhar pontos?</h1>
                        <div id="conta">
    
Ao compartilhar a página inicial da loja, páginas de produtos ou páginas de promoção com seus amigos.<br/> 
Você receberá 50 pontos para cada amigo que se cadastra com sucesso em nossa loja e confirma seu email.<br/> 
Além do mais, dentro deste período de 30 dias, você receberá pontos de bônus no valor de 2% do valor total do pedido (sem incluir o frete) de todas as compras feitas por seus amigos. 1 ponto de bônus equivale a R$ 0,01.<br/> 
Ex: Se o seu amigo faz uma compra de R$ 100,00, você receberá 2% desse total em pontos (R$ 2 = 200 pontos de bônus). Estes pontos podem ser trocados por cupons.<br/>

<h2>Como posso compartilhar as páginas da loja?</h2><br/>
Ao clicar sobre os diversos botões de compartilhamento de redes sociais que pode ser encontrado na página inicial, páginas de produtos e páginas de promoção.<br />
                            Veja a localização no exemplo da imagem abaixo:<br />
                            <br/>
                            <div style="text-align:center">
   <img border="1" src="../../imagens/objetos/compartilhe-loja-riceli.png" /></div> 

                        </div>
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

