function valida_CPFCNPJ(oSrc, args) {

    if (args.Value.length == 14) {

        valida_CPF(oSrc, args);

    } else if (args.Value.length == 18) {

        valida_CNPJ(oSrc, args);

    } else {

        return args.IsValid = false;

    }

}

//Valida‡Æo de CPF

function valida_CPF(oSrc, args) {
    s = args.Value;

    //substituir os caracteres que nÆo sÆo n£meros

    if (document.layers && parseInt(navigator.appVersion) == 4) {

        x = s.substring(0, 3);

        x += s.substring(4, 7);

        x += s.substring(8, 11);

        x += s.substring(12, 14);

        s = x;

    }

    else {

        s = s.replace(".", "");

        s = s.replace(".", "");

        s = s.replace("-", "");

    }

    if (s.length != 11 || s == "00000000000" || s == "11111111111" ||

    s == "22222222222" || s == "33333333333" || s == "44444444444" ||

    s == "55555555555" || s == "66666666666" || s == "77777777777" ||

    s == "88888888888" || s == "99999999999") {

        return args.IsValid = false;

    }

    //args.isValid = (s >= 3);

    //document.write(oSrc.Value + ',' + args.Value);

    if (isNaN(s)) {

        return args.IsValid = false;

    }

    var i;

    var c = s.substr(0, 9);

    var dv = s.substr(9, 2);

    var d1 = 0;

    for (i = 0; i < 9; i++) {

        d1 += c.charAt(i) * (10 - i);

    }

    if (d1 == 0) {

        return args.IsValid = false;

    }

    d1 = 11 - (d1 % 11);

    if (d1 > 9) d1 = 0;

    if (dv.charAt(0) != d1) {

        return args.IsValid = false;

    }

    d1 *= 2;

    for (i = 0; i < 9; i++) {

        d1 += c.charAt(i) * (11 - i);

    }

    d1 = 11 - (d1 % 11);

    if (d1 > 9) d1 = 0;

    if (dv.charAt(1) != d1) {

        return args.IsValid = false;

    }

    return args.IsValid = true;

}



//Valida‡Æo de CNPJ

function valida_CNPJ(oSrc, args) {

    s = args.Value;

    //substituir os caracteres que nÆo sÆo n£meros

    if (document.layers && parseInt(navigator.appVersion) == 4) {

        x = s.substring(0, 2);

        x += s.substring(3, 6);

        x += s.substring(7, 10);

        x += s.substring(11, 15);

        x += s.substring(16, 18);

        s = x;

    }

    else {

        s = s.replace(".", "");

        s = s.replace(".", "");

        s = s.replace("-", "");

        s = s.replace("/", "");

    }





    if (isNaN(s)) {

        return args.IsValid = false;

    }

    var i;

    var c = s.substr(0, 12);

    var dv = s.substr(12, 2);

    var d1 = 0;

    for (i = 0; i < 12; i++) {

        d1 += c.charAt(11 - i) * (2 + (i % 8));

    }

    if (d1 == 0)

        return args.IsValid = false;

    d1 = 11 - (d1 % 11);

    if (d1 > 9) d1 = 0;

    if (dv.charAt(0) != d1) {

        return args.IsValid = false;

    }

    d1 *= 2;

    for (i = 0; i < 12; i++) {

        d1 += c.charAt(11 - i) * (2 + ((i + 1) % 8));

    }

    d1 = 11 - (d1 % 11);

    if (d1 > 9)

        d1 = 0;

    if (dv.charAt(1) != d1) {

        return args.IsValid = false;

    }

    return args.IsValid = true;

}