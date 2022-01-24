<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MensagemBoasVindas.ascx.cs" Inherits="_1_WebForm.UserControls.MensagemBoasVindas" %>
<asp:LinkButton ID="LinkButtonLoginFacebook" runat="server" OnClientClick="return LoginFB();">clique aqui e logue com o Facebook</asp:LinkButton>
Olá <asp:Label ID="LabelMensagemBoasVindas" runat="server"></asp:Label>
<asp:LinkButton ID="LinkButtonSair" runat="server" OnClick="LinkButtonSair_Click" Visible="False">Sair</asp:LinkButton>

<script type="text/javascript">
    window.name = 'mainwin'
    function dadosPerfil(dados) {
        var socialPerfilDto = dados;

        if (socialPerfilDto.cli_id == null)
            document.location.href = paginaInicial + "LoginMinhaConta?conectarRede=1&ReturnUrl=" + paginaAtual;
        else
            if (socialPerfilDto.cli_id == 0)
                document.location.href = paginaInicial + "LoginMinhaConta?conectarRede=0&ReturnUrl=" + paginaAtual;
            else {
                document.location.href = "<%= Request.QueryString["ReturnUrl"] != null ? Request.QueryString["ReturnUrl"] : Request.RawUrl %>";
            }
    }

    function LoginFB() {
        <%= string.Format("AbrirJanela(\"{0}Social/LoginSocial.aspx\", 400, 400);", Page.GetRouteUrl("PaginaInicial", null))%>
        return false;
    }
    </script>