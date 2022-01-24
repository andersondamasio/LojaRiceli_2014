function FacebookLogin(accessToken) {
    if (accessToken != "") {
        var proxy = new ServiceSocial();
        proxy.FacebookLogin(accessToken, FacebookLoginOnSuccess, null, null);
    } else {
        alert("Aplicativo não configurado - 'accessToken'")
    }
}

function FacebookLoginOnSuccess(result) {

    var socialPerfilDto = result;

    if (socialPerfilDto.cli_id == null)
        document.location.href = paginaInicial + "LoginMinhaConta?conectarRede=1&ReturnUrl=" + paginaAtual;

    if (socialPerfilDto.cli_id == 0)
        document.location.href = paginaInicial + "LoginMinhaConta?conectarRede=0&ReturnUrl=" + paginaAtual;

}
