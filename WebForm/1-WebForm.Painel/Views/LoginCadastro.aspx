<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginCadastro.aspx.cs" Inherits="Loja.Views.LoginCadastro" %>

<%@ Register Src="Rodape.ascx" TagName="Rodape" TagPrefix="uc2" %>
<%@ Register Src="CabecalhoFinalizarPedido.ascx" TagName="CabecalhoFinalizarPedido" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:CabecalhoFinalizarPedido ID="CabecalhoFinalizarPedido1" runat="server" />
        <table border="1">
            <tr>
                <td>JÁ SOU CLIENTE</td>
                <td>SOU NOVO CLIENTE</td>
            </tr>
            <tr>
                <td>Seu email
                    <asp:TextBox ID="cli_emailTextBox" runat="server" MaxLength="128" ValidationGroup="validGroupLogin"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="validGroupLogin" ControlToValidate="cli_emailTextBox" Display="Dynamic" ErrorMessage="<br/>Coloque seu email"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorCli_emailTextBox" runat="server"
                    ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="cli_emailTextBox"
                    ErrorMessage="Email inválido" ValidationGroup="validGroupLogin" Display="Dynamic" SetFocusOnError="true"></asp:RegularExpressionValidator>
                     </td>
                <td>Em apenas um passo, crie uma conta na Riceli e finalize sua compra. É Rápido e Seguro.<br />
                </td>
            </tr>
            <tr>
                <td>Senha
                    <asp:TextBox ID="cli_senhaTextBox" runat="server" MaxLength="32" ValidationGroup="validGroupLogin" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="validGroupLogin" ControlToValidate="cli_senhaTextBox" Display="Dynamic" ErrorMessage="<br/>Coloque sua senha"></asp:RequiredFieldValidator>
                    <br />
                    <a href="#" onclick="document.getElementById('divRecuperarSenha').style.display = (document.getElementById('divRecuperarSenha').style.display == 'none') ? '' : 'none'">Esqueceu a senha?</a>
                    <div id="divRecuperarSenha" style="display: none">
                        Coloque seu email para recuperar sua senha<br/>
                        <asp:TextBox ID="TextBoxEmailRecuperarSenha" runat="server" MaxLength="128" ValidationGroup="groupRecuperarSenha"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBoxEmailRecuperarSenha" ValidationGroup="groupRecuperarSenha" Display="Dynamic" SetFocusOnError="true"  ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                    ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="TextBoxEmailRecuperarSenha"
                    ErrorMessage="Email inválido" ValidationGroup="TextBoxEmailRecuperarSenha" Display="Dynamic" SetFocusOnError="true"></asp:RegularExpressionValidator>
              
                        <asp:Button ID="ButtonRecuperarSenha" runat="server" OnClick="ButtonRecuperarSenha_Click" Text="Recuperar Senha" ValidationGroup="groupRecuperarSenha" />
                    </div>
                </td>
                <td>Seu email
                    <asp:TextBox ID="cli_emailNovoTextBox" runat="server" MaxLength="128" ValidationGroup="validGroupCadastroNovo"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" SetFocusOnError="true" ValidationGroup="validGroupCadastroNovo" ControlToValidate="cli_emailNovoTextBox" Display="Dynamic" ErrorMessage="<br/>Coloque seu email"></asp:RequiredFieldValidator>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                    ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="cli_emailNovoTextBox"
                    ErrorMessage="Email inválido" ValidationGroup="validGroupCadastroNovo" Display="Dynamic" SetFocusOnError="true"></asp:RegularExpressionValidator>
   
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="ButtonEntrar" runat="server" Text="Entrar" ValidationGroup="validGroupLogin" OnClick="ButtonEntrar_Click" OnClientClick="return DesabilitarDuploClick(this,'Validando seu usuário...','validGroupLogin', true);" /></td>
                <td>
                    <asp:Button ID="ButtonCadastrar" runat="server" Text="Cadastrar" ValidationGroup="validGroupCadastroNovo" OnClientClick="return DesabilitarDuploClick(this,'Carregando...','validGroupCadastroNovo', true);" OnClick="ButtonCadastrar_Click" /></td>
            </tr>
        </table>
        <uc2:Rodape ID="Rodape1" runat="server" />
    </form>
</body>
</html>
