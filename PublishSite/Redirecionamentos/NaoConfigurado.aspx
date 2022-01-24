<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NaoConfigurado.aspx.cs" Inherits="_1_WebForm.Redirecionamentos.NaoConfigurado" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <%= Request.Params["mensagem"] %>
    </div>
    </form>
</body>
</html>
