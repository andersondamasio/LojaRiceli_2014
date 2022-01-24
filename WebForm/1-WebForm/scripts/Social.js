var idPerfil;
function EnviarConvite(amigos) {
    try {
        idPerfil = amigos;
        FB.ui(
          {
              display: '',
              method: 'apprequests',
              new_style_message: true,
              title: 'Destacar e enviar convite aos meus amigos',
              message: 'Oi, estou na Riceli.com, aceita?',
              to: amigos
          }
          , EnviarConviteResult);
    }catch(ex){
        alert(ex);
    }
}

function EnviarConviteResult(response) {

    var ids = response["to"];

    if (ids != undefined && ids.length > 0)
        alert("Parabéns, convite enviado. Agora é só aguardar que seu amigo aceite seu convite.");
    else (confirm("O convite não foi enviado, que tal tentarmos novamente?") ? EnviarConvite(idPerfil) : "");
}

function EnviarConviteTodos() {

    var arrayIds = new Array();
    var ids = document.getElementsByName("idPerfil");

    for (var i = 0; i < ids.length; i++) {
        var obj = document.getElementsByName("idPerfil").item(i);
        arrayIds.push(obj.id);
        if (i == 49)
            break;
    }
    EnviarConvite(arrayIds);
}