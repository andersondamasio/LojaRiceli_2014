<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Teste.aspx.cs" Inherits="_1_WebForm.Teste" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script src="scripts/global.js"></script>
</head>
<body>

    <div id="fb-root"></div>
<script>
    window.fbAsyncInit = function () {
        FB.init({
            appId: '275042665941123', // App ID
            status: true, // check login status
            cookie: true, // enable cookies to allow the server to access the session
            xfbml: true  // parse XFBML
        });
        // Additional initialization code here

        FB.Event.subscribe('auth.authResponseChange', function (response) {
            if (response.status === 'connected') {
                // the user is logged in and has authenticated your
                // app, and response.authResponse supplies
                // the user's ID, a valid access token, a signed
                // request, and the time the access token 
                // and signed request each expire
                var uid = response.authResponse.userID;
                var accessToken = response.authResponse.accessToken;

                // TODO: Handle the access token

                // Do a post to the server to finish the logon
                // This is a form post since we don't want to use AJAX

                //alert("Autenticado!");

                 var form = document.createElement("form");
                form.setAttribute("method", 'post');
                form.setAttribute("action", 'Social/FacebookLogin.ashx');

                var field = document.createElement("input");
                field.setAttribute("type", "hidden");
                field.setAttribute("name", 'accessToken');
                field.setAttribute("value", accessToken);
                form.appendChild(field);

                document.body.appendChild(form);
                form.submit();


            } else if (response.status === 'not_authorized') {
                // the user is logged in to Facebook, 
                // but has not authenticated your app
            } else {
                // the user isn't logged in to Facebook.
            }
        });

    };

    // Load the SDK Asynchronously
    (function (d) {
        var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
        if (d.getElementById(id)) { return; }
        js = d.createElement('script'); js.id = id; js.async = true;
        js.src = "//connect.facebook.net/pt_BR/all.js";
        ref.parentNode.insertBefore(js, ref);
    }(document));
</script>

    <form id="form1" runat="server">
    <div>
 <div class="fb-login-button" data-show-faces="true" data-scope="email,user_friends,user_birthday,user_location,user_friends" data-width="400" data-max-rows="1"></div>
    </div>
       

        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" Visible="False" />
       

        <br />
        <br />
        <br />
        <asp:Button ID="ButtonAtualizaFrete" runat="server" OnClick="ButtonAtualizaFrete_Click" Text="AtualizaFrete" Visible="False" />
       

        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Button" />
       

    </form>
</body>
</html>
