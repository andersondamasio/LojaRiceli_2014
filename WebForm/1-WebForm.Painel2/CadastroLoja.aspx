<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadastroLoja.aspx.cs" Inherits="_1_WebForm.Painel2.CadastroLoja" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>Efetue o Cadastro de Sua Loja</h1>

        <div>
            <table border="2">
                <tr>
                    <td>Nome de Sua Loja:</td>
                    <td><asp:TextBox ID="loj_nomeTextBox" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Seu Domínio:</td>
                    <td><asp:TextBox ID="loj_dominioTextBox" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>E-mail:</td>
                    <td><asp:TextBox ID="loj_emailTextBox" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Cep:</td>
                    <td><asp:TextBox ID="loj_cepTextBox" runat="server"></asp:TextBox></td>
                </tr>
                 <tr>
                     <td>Seu Nome de Usuário(Utilize apenas letras):</td>
                    <td><asp:TextBox ID="usu_nomeTextBox" runat="server"></asp:TextBox></td>
                </tr>
                 <tr>
                     <td>Sua Senha:</td>
                    <td><asp:TextBox ID="usu_senhaTextBox" runat="server" TextMode="Password"></asp:TextBox></td>
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
