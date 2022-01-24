<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmaEmail.aspx.cs" Inherits="_1_WebForm.ConfirmaEmail" %>

<%@ Register Src="CabecalhoPagamento.ascx" TagName="CabecalhoPagamento" TagPrefix="uc1" %>

<%@ Register Src="Rodape.ascx" TagName="Rodape" TagPrefix="uc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <link href="css/estilos.css" rel="stylesheet" />
        <div id="header">
            <div id="Topo">
                Site 100% Seguro <a href="<%= Page.GetRouteUrl("PaginaInicial", null) %>">Continuar comprando</a>
                <div style="margin: auto; height: 40px; width: 620px; margin-top: 50px;"></div>
                <div id="Pagamento"><div class="clear"></div><div class="clear"></div>
                    <div style="text-align:center">
                        <h1>Instruções de confirmação foi enviado com sucesso para xxx@xxxx.com.br. No entanto, devido ao tráfego de rede, pode demorar até uma hora para o e-mail de verificação para chegar ao seu endereço de e-mail.</h1>
                        <br />
                        Por razões de segurança, por favor verificar o seu endereço de e-mail o mais rápido possível.<br />
                        Entre na sua conta de e-mail e siga as instruções dadas pela verificação.
                        <br />
                        Observação: As instruções de verificação são válidos por pouco tempo.<br />
                        Se você não receber nossas instruções de verificação, verifique a sua <b>Pasta Spam</b> de seu webmail para ver se ele está lá ou clique aqui para obter novas instruções enviadas para você.
                    </div>
                    <div class="clear"></div>
                </div> <div class="clear"></div>
            </div> <div class="clear"></div>
        </div><div class="clear"></div><div class="clear"></div><div class="clear"></div><div class="clear"></div><div class="clear"></div>
        <uc2:Rodape ID="Rodape1" runat="server" />
    </form>

</body>
</html>
