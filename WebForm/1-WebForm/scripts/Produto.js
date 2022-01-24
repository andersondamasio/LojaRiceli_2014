
function SelecionaProSkuCor(pro_id, proSku_id) {
    $("div[id^='divProSkuConteudo" + pro_id + "']").each(function () {
        $("div[id^='divProSkuConteudo" + pro_id + "']").css("display", "none");
    });
    $("#divProSkuConteudo" + pro_id + proSku_id).css("display", "");
}

function SelecionaProSkuCorOnError(imagem) { 
    imagem.src += "indefinido.gif";
}

