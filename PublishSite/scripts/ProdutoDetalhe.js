var proSku_nome = "";

function SelecionarProSkuTamanho(proSku_id) {

    $("span[id^='proSkuTamanho']").each(function (index, element) {
        if ($(this).is("#proSkuTamanho" + proSku_id)) {
            $("#proSkuTamanho" + proSku_id).css({ "background-color": "rgb(150, 150, 219)", "border": "1px solid rgb(214, 80, 80)" });
        } else {
            $("#" + element.id).css({ "background-color": "", "border": "" });
        }
    });

    $("span[id^='SpanPanelProdutoSkuPreco']").each(function (index, element) {

        if ($(this).is("#SpanPanelProdutoSkuPreco" + proSku_id)) {
            $("#SpanPanelProdutoSkuPreco" + proSku_id).css("display", "");
        } else {
            $("#" + element.id).css("display", "none");
        }
    });

    if (proSku_nome == "")
        proSku_nome = $("#h1NomeProduto").html();
   
    $("#HiddenFieldProSku_id").attr("value", proSku_id); 
    $("#h1NomeProduto").html(proSku_nome + " " + $("#proSkuTamanho" + proSku_id).html());
    $("#spanResultadoCalcPrazo").html("");
}

///////////////////////////////////////////////////////////////
function IniCarregar() {
    $(document).ready(function () {
        $('.jqzoom').jqzoom({
            zoomType: 'standard',
            zoomWidth: '480',
            zoomHeight: '409',
            position: "right",
            offset: 21,
            preloadText: 'Carregando...',
            title: false,
            lens: true,
            preloadImages: false,
            alwaysOn: false,
            showEffect: "fadein"
        });
    });
}

function IniDescarregar() {

    if ($("span[id^='proSkuTamanho']").size() == 1) {

        $("span[id^='proSkuTamanho']").each(function (index, element) {
            SelecionarProSkuTamanho(element.id.replace("proSkuTamanho", ""));
            return false;
        });
    }
}

function addLoadEvent(func) {
    var oldonload = window.onload;
    if (typeof window.onload != 'function') {
        window.onload = func;
    } else {
        window.onload = function () {
            if (oldonload) {
                oldonload();
            }
            func();
        }
    }
}

function SelecionaProSkuOnError(imagem) {
    
   
}

IniCarregar();
addLoadEvent(IniDescarregar);


/*function AlteraImagemProSkuDetalhe(imagem, proSkuFot_nome, proSkuFot_extensao) {
    var nomeImagemM = proSkuFot_nome + "-m" + proSkuFot_extensao;
    var nomeImagemD = proSkuFot_nome + "-d" + proSkuFot_extensao;
    var nomeImagemA = proSkuFot_nome + "-a" + proSkuFot_extensao;
    var proSkuFotLinkD = imagem.src.replace(nomeImagemM, nomeImagemA);
    var proSkuFotImageD = imagem.src.replace(nomeImagemM, nomeImagemD);

    //alert($('#mini0').attr('rel')); alert(proSkuFotImageD + " - " + document.getElementById("proSkuFotImageD").src);

    if (proSkuFotImageD != document.getElementById("proSkuFotImageD").src) {
        //document.getElementById("proSkuFotLinkD").href = proSkuFotLinkD;
        document.getElementById("proSkuFotImageD").src = proSkuFotImageD;
    }
}*/
