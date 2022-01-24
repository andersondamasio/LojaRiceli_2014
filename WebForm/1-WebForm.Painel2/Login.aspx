<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Loja.Painel.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   <asp:Panel ID="Panellogin" runat="server" GroupingText="Coloque seu Usuário se Senha" DefaultButton="ButtonEntrar" Style="width:400px">
	<div style="padding:20px;">
    </div>
       <label style="width:80px; text-align:right; display:inline-block">Usuário:</label> <asp:TextBox ID="usu_nomeTextBox" runat="server" Columns="32" 
            MaxLength="32" style="width:180px;"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                runat="server" ErrorMessage="Insira seu usuário" ControlToValidate="usu_nomeTextBox" SetFocusOnError="True"></asp:RequiredFieldValidator>
         
            <br/>
       <label style="width:80px; text-align:right; display:inline-block">Senha:</label> <asp:TextBox style="width:180px;" ID="usu_senhaTextBox" runat="server" Columns="32" 
            MaxLength="32" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                runat="server" ErrorMessage="Insira sua senha" 
            ControlToValidate="usu_senhaTextBox" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <br>
              
        <div style="text-align:right; padding-right:10px;">
            <asp:Button ID="ButtonEntrar" runat="server" Text="Entrar" 
                onclick="ButtonEntrar_Click" style="background-color:" />
        </asp:Panel>
        </div>
		
        <div style="font-size:11px; font-family:tahoma; text-align:center; margin-top:10px;">
        Desenvolvido por:
        </div>
        <script>
            document.getElementById("<%= usu_nomeTextBox.ClientID %>").focus();
        </script>
    </form>
</body>
</html>
