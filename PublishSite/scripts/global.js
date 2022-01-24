function UpdateTableHeaders() {
    $(".persist-area").each(function () {

        var el = $(this),
            offset = el.offset(),
            scrollTop = $(window).scrollTop(),
            floatingHeader = $(".floatingHeader", this)

        if ((scrollTop > 50) && (scrollTop < offset.top + el.height())) {
            floatingHeader.css({
                "visibility": "visible"
            });
        } else {
            floatingHeader.css({
                "visibility": "hidden"
            });
        };
    });
}

// DOM Ready      
$(function () {

    var clonedHeaderRow;

    $(".persist-area").each(function () {
        clonedHeaderRow = $(".persist-header", this);
        clonedHeaderRow
          .before(clonedHeaderRow.clone())
          .css("width", clonedHeaderRow.width())
          .addClass("floatingHeader");

    });

    $(window)
     .scroll(UpdateTableHeaders)
     .trigger("scroll");

});

function DesabilitarDuploClick(botao,mensagem, validatorGroup) {

    if (typeof (Page_ClientValidate) == 'function') {
        if (Page_ClientValidate(validatorGroup) == false) {
            event.returnValue = false;
            return false;
        }
    }

    botao.value = mensagem;
    botao.disabled = true;
    botao.disabled = true;

    __doPostBack(botao.name, '');
    
    event.returnValue = false;
    return false;
}

function ObjetoVisible(objeto) {
    if ($("#" + objeto).css("display") == "none")
        $("#" + objeto).css("display", "");
    else $("#" + objeto).css("display", "none");
}

function float2moeda(num) {
    x = 0;
    if (num < 0) {
        num = Math.abs(num);
        x = 1;
    }
    if (isNaN(num)) num = "0";
    cents = Math.floor((num * 100 + 0.5) % 100);

    num = Math.floor((num * 100 + 0.5) / 100).toString();

    if (cents < 10) cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3) ; i++)
        num = num.substring(0, num.length - (4 * i + 3)) + '.'
               + num.substring(num.length - (4 * i + 3));
    ret = num + ',' + cents;
    if (x == 1) ret = ' - ' + ret; return ret;

}


function AbrirJanela(href, height, width) {
    var left = (screen.width - width) / 2;
    var top = (screen.height - height) / 4;
    window.open(href, "", "menubar=no,toolbar=no,resizable=yes,scrollbars=yes,height=" + height + ",width=" + width + ", top=" + top + ", left=" + left); return false;
        return false;
}


