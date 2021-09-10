function mascara(o, f) {
    v_obj = o
    v_fun = f
    setTimeout("execmascara()", 1)
}

function execmascara() {
    v_obj.value = v_fun(v_obj.value)
}

function mtel(v) {
    v = v.replace(/\D/g, ""); //Remove tudo o que não é dígito
    v = v.replace(/^(\d{2})(\d)/g, "($1) $2"); //Coloca parênteses em volta dos dois primeiros dígitos
    v = v.replace(/(\d)(\d{4})$/, "$1-$2");    //Coloca hífen entre o quarto e o quinto dígitos
    return v;
}

/*Função Pai de Mascaras*/
function Mascara(o, f) {
    v_obj = o
    v_fun = f
    setTimeout("execmascara()", 1)
}

function Integer(v) {
    return v.replace(/\D/g, "")
}

function leech(v) {
    v = v.replace(/o/gi, "0")
    v = v.replace(/i/gi, "1")
    v = v.replace(/z/gi, "2")
    v = v.replace(/e/gi, "3")
    v = v.replace(/a/gi, "4")
    v = v.replace(/s/gi, "5")
    v = v.replace(/t/gi, "7")
    return v
}

function soNumeros(v) {
    return v.replace(/\D/g, "")
}

function telefone(v) {
    v = v.replace(/\D/g, "") //Remove tudo o que não é dígito 
    v = v.replace(/^(\d\d)(\d)/g, "($1) $2") //Coloca parênteses em volta dos dois primeiros dígitos 
    v = v.replace(/(\d{4})(\d)/, "$1 - $2") //Coloca hífen entre o quarto e o quinto dígitos 
    return v
}

function cpf(v) {
    v = v.replace(/\D/g, "") //Remove tudo o que não é dígito 
    v = v.replace(/(\d{3})(\d)/, "$1.$2") //Coloca um ponto entre o terceiro e o quarto dígitos 
    v = v.replace(/(\d{3})(\d)/, "$1.$2") //Coloca um ponto entre o terceiro e o quarto dígitos 
    //de novo (para o segundo bloco de números) 
    v = v.replace(/(\d{3})(\d{1,2})$/, "$1-$2") //Coloca um hífen entre o terceiro e o quarto dígitos 
    return v
}

function cep(v) {
    v = v.replace(/\D/g, "") //Remove tudo o que não é dígito 
    v = v.replace(/(\d{2})(\d)/, "$1.$2") //Coloca um ponto entre o terceiro e o quarto dígitos 
    v = v.replace(/(\d{3})(\d{1,3})$/, "$1-$2") //Coloca um hífen entre o terceiro e o quarto dígitos 
    return v
}

function cnpj(v) {
    v = v.replace(/\D/g, "") //Remove tudo o que não é dígito 
    v = v.replace(/^(\d{2})(\d)/, "$1.$2") //Coloca ponto entre o segundo e o terceiro dígitos 
    v = v.replace(/^(\d{2})\.(\d{3})(\d)/, "$1.$2.$3") //Coloca ponto entre o quinto e o sexto dígitos 
    v = v.replace(/\.(\d{3})(\d)/, ".$1/$2") //Coloca uma barra entre o oitavo e o nono dígitos 
    v = v.replace(/(\d{4})(\d)/, "$1-$2") //Coloca um hífen depois do bloco de quatro dígitos 
    return v
}

function romanos(v) {
    v = v.toUpperCase() //Maiúsculas 
    v = v.replace(/[^IVXLCDM]/g, "") //Remove tudo o que não for I, V, X, L, C, D ou M 
    while (v.replace(/^M{0,4}(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})$/, "") != "")
        v = v.replace(/.$/, "")
    return v
}

function site(v) {
    //Esse sem comentarios para que você entenda sozinho  
    v = v.replace(/^http:\/\/?/, "")
    dominio = v
    caminho = ""
    if (v.indexOf("/") > -1)
        dominio = v.split("/")[0]
    caminho = v.replace(/[^\/]*/, "")
    dominio = dominio.replace(/[^\w\.\+-:@]/g, "")
    caminho = caminho.replace(/[^\w\d\+-@:\?&=%\(\)\.]/g, "")
    caminho = caminho.replace(/([\?&])=/, "$1")
    if (caminho != "") dominio = dominio.replace(/\.+$/, "")
    v = "http://" + dominio + caminho
    return v
}

function data(v) {
    v = v.replace(/\D/g, "") //Remove tudo o que não é dígito 
    v = v.replace(/^(\d{2})(\d)/, "$1/$2") //Coloca ponto entre o segundo e o terceiro dígitos 
    v = v.replace(/\.(\d{2})(\d)/, ".$1/$2") //Coloca uma barra entre o oitavo e o nono dígitos 
    v = v.replace(/(\d{2})(\d)/, "$1/$2") //Coloca um hífen depois do bloco de quatro dígitos 
    return v
}

function moeda(v) {
    v = v.replace(/\D/g, "") // permite digitar apenas numero 
    //v = v.replace(/(\d{1})(\d{15})$/, "$1.$2") // coloca ponto antes dos ultimos digitos 
    //v = v.replace(/(\d{1})(\d{11})$/, "$1.$2") // coloca ponto antes dos ultimos 13 digitos 
    //v = v.replace(/(\d{1})(\d{8})$/, "$1.$2") // coloca ponto antes dos ultimos 10 digitos 
    //v = v.replace(/(\d{1})(\d{5})$/, "$1.$2") // coloca ponto antes dos ultimos 7 digitos 
    v = v.replace(/(\d{1})(\d{1,2})$/, "$1,$2") // coloca virgula antes dos ultimos 4 digitos 
    return v;
}

function porcentagem(v) {
    v = v.replace(/\D/g, "") // permite digitar apenas numero 
    //v = v.replace(/(\d{1})(\d{15})$/, "$1.$2") // coloca ponto antes dos ultimos digitos 
    //v = v.replace(/(\d{1})(\d{11})$/, "$1.$2") // coloca ponto antes dos ultimos 13 digitos 
    //v = v.replace(/(\d{1})(\d{8})$/, "$1.$2") // coloca ponto antes dos ultimos 10 digitos 
    //v = v.replace(/(\d{1})(\d{5})$/, "$1.$2") // coloca ponto antes dos ultimos 7 digitos 
    v = v.replace(/(\d{1})(\d{1,2})$/, "$1,$2") // coloca virgula antes dos ultimos 4 digitos 
    return v + "%";
}

//valida o CNPJ digitado
function ValidarCNPJ(ObjCnpj) {
    var cnpj = ObjCnpj.value;

    if (cnpj == "")
        return;

    var valida = new Array(6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2);
    var dig1 = new Number;
    var dig2 = new Number;

    exp = /\.|\-|\//g
    cnpj = cnpj.toString().replace(exp, "");
    var digito = new Number(eval(cnpj.charAt(12) + cnpj.charAt(13)));

    for (i = 0; i < valida.length; i++) {
        dig1 += (i > 0 ? (cnpj.charAt(i - 1) * valida[i]) : 0);
        dig2 += cnpj.charAt(i) * valida[i];
    }
    dig1 = (((dig1 % 11) < 2) ? 0 : (11 - (dig1 % 11)));
    dig2 = (((dig2 % 11) < 2) ? 0 : (11 - (dig2 % 11)));

    if (((dig1 * 10) + dig2) != digito)
        alert('CNPJ Invalido!');

}

function ValidarCPF(Objcpf) {
    var cpf = Objcpf.value;
    if (cpf == "")
        return;
    exp = /\.|\-/g
    cpf = cpf.toString().replace(exp, "");
    var digitoDigitado = eval(cpf.charAt(9) + cpf.charAt(10));
    var soma1 = 0, soma2 = 0;
    var vlr = 11;

    for (i = 0; i < 9; i++) {
        soma1 += eval(cpf.charAt(i) * (vlr - 1));
        soma2 += eval(cpf.charAt(i) * vlr);
        vlr--;
    }
    soma1 = (((soma1 * 10) % 11) == 10 ? 0 : ((soma1 * 10) % 11));
    soma2 = (((soma2 + (2 * soma1)) * 10) % 11);

    var digitoGerado = (soma1 * 10) + soma2;
    if (digitoGerado != digitoDigitado) {
        Objcpf.value = "";
        swal('CPF Invalido!');
        
    }
}

function validaData(campo, valor) {
    if (valor == "")
        return true;
    
    var date=valor;
    var ardt=new Array;
    var ExpReg=new RegExp("(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/[12][0-9]{3}");
    ardt=date.split("/");
    erro=false;
    if ( date.search(ExpReg)==-1){
        erro = true;
    }
    else if (((ardt[1]==4)||(ardt[1]==6)||(ardt[1]==9)||(ardt[1]==11))&&(ardt[0]>30))
        erro = true;
    else if ( ardt[1]==2) {
        if ((ardt[0]>28)&&((ardt[2]%4)!=0))
            erro = true;
        if ((ardt[0]>29)&&((ardt[2]%4)==0))
            erro = true;
    }
    if (erro) {
        campo.focus();
        campo.value = "";
        return true;
    }
    return true;
}