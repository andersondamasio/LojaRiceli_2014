<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecuperarSenha.aspx.cs" Inherits="_1_WebForm.RecuperarSenha" %>

<%@ Register Src="~/Cabecalho.ascx" TagPrefix="uc1" TagName="Cabecalho" %>
<%@ Register Src="~/Rodape.ascx" TagPrefix="uc1" TagName="Rodape" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
</head>
<body>

    <form id="form2" runat="server">
            <div class="persist-area">
                <uc1:Cabecalho runat="server" ID="Cabecalho" />

                <div id="corpo">
  <div id="conteudo" style="width:990px; margin-top:5px;">
<h1>Recuperar Senha</h1>
<p><img src="imagens/objetos/bulett.png" width="10" height="10">Preencha os campos com sua nova senha:</p>
<form name="form1" method="post" action="">
  <p>
    <label for="textfield">Nova Senha:</label>
     <asp:TextBox ID="cli_senhaTextBox" runat="server" MaxLength="32" TextMode="Password" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="groupRecuperarSenha" ControlToValidate="cli_senhaTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
  </p>
  <p>
    <label for="textfield2">Confirmar senha:</label>
    <asp:TextBox ID="cli_confirmacaoSenhaTextBox" runat="server" MaxLength="32" TextMode="Password" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="groupRecuperarSenha" ControlToValidate="cli_confirmacaoSenhaTextBox" ErrorMessage="Campo obrigátorio" SetFocusOnError="True" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="compareSenha" runat="server" ValidationGroup="groupRecuperarSenha" ControlToCompare="cli_senhaTextBox" ControlToValidate="cli_confirmacaoSenhaTextBox" ErrorMessage="Senha e confirmação estão diferentes" Display="Dynamic" />
  </p>
  <p>&nbsp;</p>
</form>
<p>&nbsp;</p>
<p><asp:Button ID="ButtonAlterar" runat="server" Text="Alterar" ValidationGroup="groupRecuperarSenha" OnClick="ButtonAlterar_Click" /></p>

    <div style="clear:both; height:10px;"></div>
  </div><!-- Fecha div Conteudo -->
</div>


 <uc1:Rodape runat="server" ID="Rodape" />
            </div>
    </form>
</body>
</html>
