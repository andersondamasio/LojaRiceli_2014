<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CriarLoja.aspx.cs" Inherits="_1_WebForm.Admin.CriarLoja" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>Efetue o Cadastro de Sua Loja<asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        </h1>

        <div>
            <table border="2">
                <tr>
                    <td>Nome de Sua Loja:</td>
                    <td><asp:TextBox ID="loj_nomeTextBox" runat="server" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="loj_nomeTextBox" ErrorMessage="Campo obrigatorio" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Nome do subdomínio de sua loja:(utilize apenas letras)</td>
                    <td><asp:TextBox ID="loj_dominioTextBox" runat="server" MaxLength="35"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="loj_dominioTextBox_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="LowercaseLetters" TargetControlID="loj_dominioTextBox">
                        </asp:FilteredTextBoxExtender>
                        .riceli.com.br   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Campo obrigatorio" Display="Dynamic" SetFocusOnError="True" ControlToValidate="loj_dominioTextBox"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>E-mail:</td>
                    <td><asp:TextBox ID="loj_emailTextBox" runat="server" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Campo obrigatorio" Display="Dynamic" SetFocusOnError="True" ControlToValidate="loj_emailTextBox"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Cep:</td>
                    <td><asp:TextBox ID="loj_cepTextBox" runat="server" MaxLength="8"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="loj_cepTextBox_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="loj_cepTextBox">
                        </asp:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Campo obrigatorio" Display="Dynamic" SetFocusOnError="True" ControlToValidate="loj_cepTextBox"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                 <tr>
                     <td>Seu Nome de Usuário(utilize apenas letras):</td>
                    <td><asp:TextBox ID="usu_nomeTextBox" runat="server" MaxLength="30"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="usu_nomeTextBox_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="LowercaseLetters" TargetControlID="usu_nomeTextBox">
                        </asp:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Campo obrigatorio" Display="Dynamic" SetFocusOnError="True" ControlToValidate="usu_nomeTextBox"></asp:RequiredFieldValidator>
                     </td>
                </tr>
                 <tr>
                     <td>Sua Senha:</td>
                    <td><asp:TextBox ID="usu_senhaTextBox" runat="server" TextMode="Password" MaxLength="15"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Campo obrigatorio" Display="Dynamic" SetFocusOnError="True" ControlToValidate="usu_senhaTextBox"></asp:RequiredFieldValidator>
                     </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="ButtonSalvar" runat="server" Text="Salvar" OnClick="ButtonSalvar_Click" /></td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
