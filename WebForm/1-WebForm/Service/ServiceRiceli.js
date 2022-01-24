function InsertProdutoSkuAviso() {

    if (Page_ClientValidate("groupCadastraAviso")) {
        var proxy = new ServiceRiceli();

        var produtoSkuAvisoDto = new _2_Library.Dao.ProdutoSkuAvisoX.ProdutoSkuAvisoDto();
        produtoSkuAvisoDto.proSkuAvi_email = $("#TextBoxProSkuAvi_email").val();
        produtoSkuAvisoDto.proSkuAvi_nome = $("#TextBoxProSkuAvi_nome").val();
        produtoSkuAvisoDto.proSku_id = $("#HiddenFieldProSku_id").val();
        //produtoSkuAvisoDto.cli_id = null;

        proxy.InsertProdutoSkuAviso(produtoSkuAvisoDto, InsertProdutoSkuAvisoOnSuccess, null, null);
    }
}

function InsertProdutoSkuAvisoOnSuccess(result) {
    $('#DivProdutoSkuAviso').css('display', 'none');
    alert("Aviso cadastrado com sucesso. Você será avisado quando o produto estiver disponível.");
}

function InsertBoletimInfo() {

    var bo_sexo = $("input[name='radioRecebaPromocao']:checked").val()
    var bo_email = $("#TextFieldbo_email").val();
    if (bo_sexo != null && (bo_email != null && bo_email != "")) {

        var proxy = new ServiceRiceli();
        var boletimInfoDto = new _2_Library.Dao.BoletimInfoX.BoletimInfoDto();
        boletimInfoDto.bo_email = bo_email;
        boletimInfoDto.bo_sexo = bo_sexo;

        proxy.InsertBoletimInfo(boletimInfoDto, InsertBoletimInfoOnSuccess, null, null);

    } else {
        alert("Insira o sexo e o e-mail.");
    }
}

function InsertBoletimInfoOnSuccess(result) {
    alert("Boletim cadastrado com sucesso. Você será avisado sobre novas promoções.");
    $("#TextFieldbo_email").val("");
}

function SelectCorreioLocalidade(input) {
    var valor = input.value.replace("-", "");
    if (valor.length == 8) {
        var proxy = new ServiceRiceli();
        proxy.SelectCorreioLocalidade(valor, SelectCorreioLocalidadeOnSuccess, null, null);
    }
}

function SelectCorreioLocalidadeOnSuccess(result) {
    PreencherCamposAddForm(result)
}


function SelectCorreioCalcPrazo() {
    if (Page_ClientValidate("groupCalcPrazo") == true) {
        var sCepDestino = $("#TextBoxCep").val();
        var proSku_id = $("#HiddenFieldProSku_id").val();
        if (proSku_id == "") proSku_id = null;
        var proxy = new ServiceRiceli();
        proxy.SelectCorreioCalcPrazo(sCepDestino,proSku_id, SelectCorreioCalcPrazoOnSuccess, null, null);
        $("#spanResultadoCalcPrazo").html("Carregando...");
        $("#ButtonCalcPrazo").val("Calculando...");
        $("#ButtonCalcPrazo").attr("disabled", "disabled");
    } else {
        $("#spanResultadoCalcPrazo").html("");
        $("#ButtonCalcPrazo").val("Calcular");
    }
    return false;
}

function SelectCorreioCalcPrazoOnSuccess(result) {
   
    if (result.co_msgErro != "" && result.co_msgErro != null)
        $("#spanResultadoCalcPrazo").html(result.co_msgErro);
    else {
        $("#spanResultadoCalcPrazo").html("Em até " + result.co_prazoEntrega + " dias úteis.");
    }
   
    $("#ButtonCalcPrazo").val("Calcular");
    $("#ButtonCalcPrazo").removeAttr("disabled");
}


function SelectCorreioCalcPrecoPrazoLocalidade(input) {
    var valor = input.value.replace("-", "");
    if (valor.length == 8) {
        $("#HiddenFieldCepEntrega").val(valor);
        SelectCarrinhoTotais();
    }
}

function PreencherCamposForm(correioDto) {
    if (correioDto != null)
        if ($("#cli_enderecoTextBox") != null) {
            $("#cli_enderecoTextBox").val(correioDto.co_endereco);
            $("#cli_bairroTextBox").val(correioDto.co_bairro);
            $("#cli_cidadeTextBox").val(correioDto.co_cidade);
            $("#cli_estadoDropDownList").val(correioDto.co_estado);
        }
}

function PreencherCamposAddForm(correioDto) {
    if ($("#ListViewEnderecoAdicionalEntrega_cliEnd_enderecoTextBox") != null) {
        $("#ListViewEnderecoAdicionalEntrega_cliEnd_enderecoTextBox").val(correioDto.co_endereco);
        $("#ListViewEnderecoAdicionalEntrega_cliEnd_bairroTextBox").val(correioDto.co_bairro);
        $("#ListViewEnderecoAdicionalEntrega_cliEnd_cidadeTextBox").val(correioDto.co_cidade);
        $("#ListViewEnderecoAdicionalEntrega_cliEnd_estadoDropDownList").val(correioDto.co_estado);
    }
}

function SelectCarrinhoTotais() {
    var sCepDestino = $("#HiddenFieldCepEntrega").val();
    var cu_chave = $("#TextBoxChaveCupom").val();
    var proxy = new ServiceRiceli();

        proxy.SelectCarrinhoTotais(sCepDestino, cu_chave, SelectCarrinhoTotaisOnSuccess, null, null);
        $('#ListaCarrinho').text("Carregando...");
        $('#cart_subTotal').text("Carregando...");
        $('#cart_entregaTotal').text("Carregando...");
        $('#cart_total').text("Carregando...");
        $('#cart_condicao').text("Carregando..."); 
        $('#cup_msgErro').text(cu_chave != "" ? "Carregando..." : "");
        $('#TextBoxChaveCupom').text(cu_chave != "" ? "Carregando..." : "");
        $('#ListaTiposEntrega').css('display', 'none');
        $('#ListaTiposEntregaCarrega').css('display', '');
        $('#ListaTiposEntregaCarrega').text("Carregando...");
    
    return false;
}

function SelectCarrinhoTotaisOnSuccess(result) {
    try {
       
        if (result.carrinhoDto == null || result.carrinhoDto.length == 0) {
            alert("Seu carrinho está vazio.");
            document.location = paginaInicial;
        }

        if (result.cart_entregaTotal != null && result.cart_entregaPrazoTotal != null) {
            $('#ListaTiposEntrega').css('display', '');
            $('#ListaTiposEntregaCarrega').css('display', 'none');
            $('#ListaTiposEntrega').children('li').each(function (n, v) {
                if (n == 1)
                    $(this).text(float2moeda(result.cart_entregaTotal))
                else
                    if (n == 2)
                        $(this).text(result.cart_entregaPrazoTotal + " dias úteis.")
            });
        } else {
            $('#ListaTiposEntregaCarrega').text('Digite seu cep para calcular seu frete');
        }
        
        //alert(float2moeda(result.cart_entregaTotal));
        $("#ListaCarrinho").html("");
        var imagem;
        $.each(result.carrinhoDto, function (index, value) {
            if (value.produtoSkuFotoDto != null)
                imagem = "<img title=\"" + value.proSku_nome + " " + value.proSkuCor_nome + " " + value.proSkuTam_nome + "\" src=\"imagens/produtos/fotos/" + value.loj_id + "/" + value.pro_id + "/" + value.produtoSkuFotoDto.proSkuFot_nome + "-v" + value.produtoSkuFotoDto.proSkuFot_extensao + "\" style=\"float: left; width: 30px;\" />";
            else imagem = "<img title=\"" + value.proSku_nome + " " + value.proSkuCor_nome + " " + value.proSkuTam_nome + "\" src=\"imagens/produtos/fotos/" + value.loj_id + "/sem-foto/sem-foto.jpg\" style=\"float: left; width: 30px;\" />";
           
            $("#ListaCarrinho").append("<li style=\"width: 155px !important\">" +
                                         imagem + value.proSku_nome + " " + value.proSkuCor_nome + " " + value.proSkuTam_nome +
                                       "</li>" +
                                       "<li>" + value.car_quantidade + "</li>" +
                                       "<li>" + float2moeda(value.car_itemSubTotal) + "</li>");
        });
      
        $('#cart_subTotal').text(float2moeda(result.cart_subTotal));
        $('#cart_total').text(float2moeda(result.cart_total));
        $('#cart_condicao').html("em até " + result.cart_condicao);

        if (result.cart_entregaTotal != null) {
            $('#cart_entregaTotal').text(float2moeda(result.cart_entregaTotal));
        } else {
            $('#cart_entregaTotal').text("Digite seu cep");
        }

       
        if (result.cupomDto != null)
            if (result.cupomDto.cup_msgErro != "0") {
                $('#cup_msgErro').text(result.cupomDto.cup_msgErro);
            } else {
                $('#cup_valor').text(float2moeda(result.cupomDto.cup_valor));
                $('#cup_msgErro').text("Cupom aplicado.");
                $('#ListaDesconto').css('display', '');
            }

       
        if(result.correioDto != null)
            if (result.correioDto.co_msgErro != "" && result.correioDto.co_msgErro != null) {
                $('#ListaTiposEntrega').text(result.correioDto.co_msgErro);
                $('#cart_entregaTotal').text(result.correioDto.co_msgErro);
            }


        if ($("#cli_cpfTextBox").attr("disabled") != "disabled")
            PreencherCamposForm(result.correioDto);

    } catch (ex) {
        alert(ex);
    }
}

function RecalculaEntrega() {
    __doPostBack("ButtonCalculaEntrega", "");
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
