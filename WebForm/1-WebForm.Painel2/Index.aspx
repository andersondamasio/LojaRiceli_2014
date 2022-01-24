<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Loja.Painel.Index" %>

<%@ Register Src="Cabecalho.ascx" TagName="Cabecalho" TagPrefix="uc1" %>
<%@ Register Src="Rodape.ascx" TagName="Rodape" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
          <table width="1024px" align="center">
            <tr>
                <td>
                    <uc1:Cabecalho ID="Cabecalho1" runat="server" />

                     <uc1:Rodape ID="Rodape1" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
