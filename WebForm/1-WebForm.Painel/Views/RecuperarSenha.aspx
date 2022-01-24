<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecuperarSenha.aspx.cs" Inherits="Loja.Views.RecuperarSenha" %>

<%@ Register Src="Cabecalho.ascx" TagName="Cabecalho" TagPrefix="uc1" %>
<%@ Register Src="Rodape.ascx" TagName="Rodape" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:Cabecalho ID="Cabecalho1" runat="server" />

        <asp:TextBox ID="TextBoxSenha" runat="server" MaxLength="32"></asp:TextBox>

        <asp:TextBox ID="TextBoxConfirmarSenha" runat="server" MaxLength="32"></asp:TextBox>
      
        <br/>  <asp:Button ID="ButtonAlterar" runat="server" Text="Alterar" OnClick="ButtonAlterar_Click" />

    <uc2:Rodape ID="Rodape1" runat="server" />
    </form>
</body>
</html>
