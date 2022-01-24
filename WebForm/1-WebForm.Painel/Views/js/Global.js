function trocaImagemProdutoVitrine(pro_id, proSku_id, nomeAmigavel, loj_id, proSkuFot_nome, proSkuFot_extensao){
    document.getElementById('preco' + pro_id).innerHTML = document.getElementById('precoSku' + proSku_id).innerHTML;    
   
    document.getElementById('foto' + pro_id).src = "http://" + document.location.host + '/imagens/produtos/fotos/' + loj_id + '/' + pro_id + '/' + proSkuFot_nome + '-v' + proSkuFot_extensao;
    
    document.getElementById('divProdutoParcelamento' + pro_id).style.display = "none";
    
    nomeAmigavel = document.getElementById('hiddenNomeAmigavel' + pro_id).value +"-"+ nomeAmigavel;
    nomeAmigavel = (nomeAmigavel +"-"+ proSku_id).replace("--","-");
   
    document.getElementById("linkProduto1" + pro_id).setAttribute("href", nomeAmigavel);
    document.getElementById("linkProduto2" + pro_id).setAttribute("href", nomeAmigavel);
    document.getElementById('linkProdutoSku1' + proSku_id).setAttribute("href", nomeAmigavel);
}

function trocaImagemProdutoDetalhe(imagem, proSkuFot_nome, proSkuFot_extensao) {
    var nomeImagemM = proSkuFot_nome + "-m" + proSkuFot_extensao;
    var nomeImagemD = proSkuFot_nome + "-d" + proSkuFot_extensao;
    var nomeImagemA = proSkuFot_nome + "-a" + proSkuFot_extensao;
    var proSkuFotLinkD = imagem.src.replace(nomeImagemM, nomeImagemA);
    var proSkuFotImageD = imagem.src.replace(nomeImagemM, nomeImagemD);

    if (proSkuFotImageD != document.getElementById("proSkuFotImageD").src) {
        document.getElementById("proSkuFotLinkD").href = proSkuFotLinkD;
        document.getElementById("proSkuFotImageD").src = proSkuFotImageD;
        $('.fotoPrincipalDetalhe').removeData('jqzoom');
        Ini();
    }
}


function semFotoProdutoVitrine(imagem, loj_id) {

    document.getElementById(imagem.id).src = "http://" + document.location.host + '/imagens/produtos/fotos/' + loj_id + '/semFoto/v.jpg';
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

function produtoDetalheSelecionarProdutoSku(proSku_id) {
    document.getElementById("FormViewProdutoDetalhe_HiddenFieldProdutoSkuComprar").value = proSku_id;
    var elems = document.getElementById('spanTamanhos').getElementsByTagName("span");
    for (var i = 0; i < elems.length; i++) {
        elems[i].setAttribute("style", "");
    }
    document.getElementById("spanTamanho" + proSku_id).setAttribute("style", "background-color: #000080");
    document.getElementById("FormViewProdutoDetalhe_ButtonProdutoSkuComprar").style.display = "";

    if (document.getElementById("divPrecoSkuTamanho" + proSku_id) != null)
        document.getElementById("precoSku").innerHTML = document.getElementById("divPrecoSkuTamanho" + proSku_id).innerHTML;
    else {
        document.getElementById("precoSku").innerHTML = "Indisponível";
        document.getElementById("FormViewProdutoDetalhe_ButtonProdutoSkuComprar").style.display = "none";
    }
    if (document.getElementById("divParcelamentoSkuTamanho" + proSku_id) != null)
        document.getElementById("parcelamentoSku").innerHTML = document.getElementById("divParcelamentoSkuTamanho" + proSku_id).innerHTML;
    else document.getElementById("parcelamentoSku").innerHTML = "";
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

function zoomImgProduto() {
    $('.fotoPrincipalDetalhe').jqzoom({
        zoomType: 'reverse',
        alwaysOn: false,
        zoomWidth: 276,
        zoomHeight: 400,
        position: 'right',
        xOffset: 17,
        yOffset: 10,
        showEffect: 'fadein',
        hideEffect: 'hide',
        preloadImages: false,
        title: false
    });
}

function desmarcarRadio(objeto1,objetoNome) {
    var grd = document.getElementById(objetoNome);


    //Collect A
    var rdoArray = grd.getElementsByTagName("input");

    for (i = 0; i <= rdoArray.length - 1; i++) {
        if (rdoArray[i].type == 'radio') {
            if (rdoArray[i].id != objeto1.id) {
                rdoArray[i].checked = false;
            }
        }
    }
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

function Ini() {
    zoomImgProduto();
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
addLoadEvent(Ini);
