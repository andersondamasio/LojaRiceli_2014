function SelecionarRegistro(URL, controleDestinoText, controleDestinoHidden) {
    URL = URL + '?controleDestinoText=' + controleDestinoText + '&controleDestinoHidden=' + controleDestinoHidden;
    document.getElementById("iFrameGrupo").src = URL;
}

function SetaValorRegistro(controleDestinoText, controleDestinoHidden, valorText, valorHidden) {
    document.getElementById(controleDestinoText).value = valorText;
    document.getElementById(controleDestinoHidden).value = valorHidden;
    document.getElementById("ButtonFechar").click();
}


 function calcHeight() {
     if (document.getElementById('iFrameGrupo').height > 50) {
         document.getElementById('iFrameGrupo').height = 50;
         document.getElementById('iFrameGrupo').width = 50;
     }
    var the_height = document.getElementById('iFrameGrupo').contentWindow.document.body.scrollHeight;
    var the_width = document.getElementById('iFrameGrupo').contentWindow.document.body.scrollWidth;
    document.getElementById('iFrameGrupo').height = the_height + 5;
    document.getElementById('iFrameGrupo').width = the_width;
}


 function DesabilitarDuploClick(botao, mensagem, validatorGroup, validar) {
     if (validar) {
         if (typeof (Page_ClientValidate) == 'function') {
             if (validatorGroup != "") {
                 if (Page_ClientValidate(validatorGroup) == false)
                     return false;
             } else {
                 if (Page_ClientValidate() == false)
                     return false;
             }
         }
     }

     try {
         if (Page_IsValid) {
             botao.value = mensagem;
             botao.disabled = true;
             document.getElementById(botao.id).disabled = true;
         } else {
             botao.click();
         }
     
     } catch (err) {
         botao.value = mensagem;
         botao.disabled = true;
         document.getElementById(botao.id).disabled = true;
     }
     
     try {
         __doPostBack(botao.name, '');

     } catch (err) {
         alert(err);
     }

     return true;
 }

 function AbrirJanela(href, height, width) {
     var x = screen.width / 2 - 700 / 2;
     var y = screen.height / 2 - 450 / 2;
     window.open(href, "", "menubar=no,toolbar=no,resizable=yes,scrollbars=yes,height=" + height + ",width=" + width + ",left=" + x + ",top=" + y);
     return false;
 }
