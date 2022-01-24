<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginSocialRetorno.aspx.cs" Inherits="_1_WebForm.Social.LoginSocialRetorno" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../scripts/jquery-1.10.2.min.js"></script>
     <script type="text/javascript">
        function recebeDados(dados) {
            try {
                if (navigator.userAgent.indexOf('MSIE') != -1 || navigator.appVersion.indexOf('Trident/') > 0) {
                    window.opener = window.open('', 'mainwin');
                }
                jQuery(document).ready(function () {
                    window.opener.dadosPerfil(dados);
                });
               
                jQuery(document).ready(function () {
                    fecharJanela();
                });
                  
                
            } catch (e) {
                alert(e);
                //debugger;
            }
        }

        function fecharJanela() {
                setTimeout('self.close()', 500);
        }

    </script>
   

</head>
<body>
    <form id="form1" runat="server">
    <div>
    <script> recebeDados(dadosPerfil);</script>
    </div>
    </form>
</body>
</html>
