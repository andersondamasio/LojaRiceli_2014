function CalculaPrazo() {
    try {
        document.getElementById("imgFreteCarregando").style.display = "";
        var cepDestino = document.getElementById("car_cepTextBox").value.replace("-", "");
        Loja.Servicos.WebServiceFrete.CalculaPrazo(cepDestino, OnSuccessCalculaPrazo, OnFailedCalculaPrazo);
    } catch (ex) {
        document.getElementById("imgFreteCarregando").style.display = "none";
    }
}

function OnSuccessCalculaPrazo(result) {
    document.getElementById("car_freteSpan").innerHTML = "Entrega em até " + result.car_prazoEntrega + " dias úteis";
    document.getElementById("imgFreteCarregando").style.display = "none";
}

function OnFailedCalculaPrazo(result) {
    alert(result);
}

function CalculaPrecoPrazo(input) {
    try {
        var cepDestino = input.value.replace("-", "");
        Loja.Servicos.WebServiceFrete.CalculaPrecoPrazo(cepDestino, OnSuccessCalculaPrecoPrazo, OnFailedCalculaPrecoPrazo);
    } catch (ex) {
    }
}

function OnSuccessCalculaPrecoPrazo(result) {
    document.getElementById("ent_tipo").innerHTML = result.car_meioEntrega;
    document.getElementById("ent_valor").innerHTML = "R$ " + result.car_valorEntrega;
    document.getElementById("ent_prazo").innerHTML = "Até " + result.car_prazoEntrega + " dias úteis.";
    document.getElementById("car_totalEntregaLabel").innerHTML = "R$ " + result.car_valorEntrega;

    var car_total = document.getElementById("car_totalLabel").innerHTML.replace("R$","").trim();

    alert(parseFloat(car_total + result.car_valorEntrega));

    document.getElementById("car_totalLabel").innerHTML = "R$ " + float2moeda(parseFloat(car_total + result.car_valorEntrega));
    document.getElementById("ent_aviso").style.display = "none";
    document.getElementById("ent_resultado").style.display = "";
}

function OnFailedCalculaPrecoPrazo(result) {
    alert(result);
}

function SelecionarEndereco(input) {
    try {
        var cepDestino = input.value.replace("-", "");
        if (input.id == "FormViewCadastroCliente_cli_cepTextBox")
        Loja.Servicos.WebServiceFrete.SelecionarEndereco(cepDestino, OnSuccessSelecionarEndereco, OnFailedSelecionarEndereco);
        else Loja.Servicos.WebServiceFrete.SelecionarEndereco(cepDestino, OnSuccessSelecionarEnderecoAdicional, OnFailedSelecionarEndereco);
        
    } catch (ex) {
    }
}

function OnSuccessSelecionarEndereco(result) {
    document.getElementById("FormViewCadastroCliente_cli_enderecoTextBox").value = result.corr_endereco;
    document.getElementById("FormViewCadastroCliente_cli_complementoTextBox").value = result.corr_complemento;
    document.getElementById("FormViewCadastroCliente_cli_bairroTextBox").value = result.corr_bairro;
    document.getElementById("FormViewCadastroCliente_cli_cidadeTextBox").value = result.corr_cidade;
    var ddl = document.getElementById('FormViewCadastroCliente_cli_estadoDropDownList');
        var opts = ddl.options.length;
        for (var i = 0; i < opts; i++) {
            if (ddl.options[i].value == result.corr_estado) {
                ddl.options[i].selected = true;
                break;
            }
        } 
}

function OnSuccessSelecionarEnderecoAdicional(result) {

    document.getElementById("ListViewCadastroClienteAdicional_cliEnd_enderecoTextBox").value = result.corr_endereco;
    document.getElementById("ListViewCadastroClienteAdicional_cliEnd_complementoTextBox").value = result.corr_complemento;
    document.getElementById("ListViewCadastroClienteAdicional_cliEnd_bairroTextBox").value = result.corr_bairro;
    document.getElementById("ListViewCadastroClienteAdicional_cliEnd_cidadeTextBox").value = result.corr_cidade;
    var ddl = document.getElementById('ListViewCadastroClienteAdicional_cliEnd_estadoDropDownList');

    var opts = ddl.options.length;
    for (var i = 0; i < opts; i++) {
        if (ddl.options[i].value == result.corr_estado) {
            ddl.options[i].selected = true;
            break;
        }
    }
}

function OnFailedSelecionarEndereco(result) {
    alert(result);
}

function CalculaPrecoPrazoLocalidade(input) {
    var valor = input.value.replace("-", "");
    if (valor.length == 8) {
        CalculaEntrega(input);
        //CalculaPrecoPrazo(input);
        SelecionarEndereco(input);   
    }
}

function SelecionarLocalidade(input) {
    var valor = input.value.replace("-", "");
    if (valor.length == 8) {
        SelecionarEndereco(input);
    }
}


function CalculaEntrega(input) {
    document.getElementById("cli_cepHiddenField").value = input.value;
    __doPostBack("ButtonCalculaEntrega", "");
}