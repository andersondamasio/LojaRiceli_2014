<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginSocial.aspx.cs" Inherits="_1_WebForm.Social.LoginSocial" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
	Conectando...
</title>

    <script type="text/javascript">
        function redirect(idApp, redirect_uri) {
            var url = "https://www.facebook.com/dialog/oauth?client_id=" + idApp + "&redirect_uri=" + redirect_uri + "&response_type=code&scope=email%2cpublish_actions%2cuser_friends%2cuser_interests%2cuser_activities%2cuser_likes%2cuser_work_history%2cuser_about_me%2cuser_website%2cuser_location%2cuser_birthday&display=popup";
            window.location = url;
        }

        function closeWindow() {
            self.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Carregando...
    </div>
    </form>
</body>
</html>
