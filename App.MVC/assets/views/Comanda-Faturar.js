$(function () {
    $(".txtvlr").blur(function () {
        calcularValores();
    });

    $(".btnSubmit").click(function () {
        calcularValores();

        if (parseFloat($("#totalRestante").val().replace(',', '.')) > 0) {
            swal("O Valor está menor :(", "O Valor total dos pagamentos tem que ser maior ou igual o da comanda", "info");
        } else {
            $.post("/Comanda/Faturar", { id: $("#Id").val(), dinheiro: $("#pagoDinheiro").val(), credito: $("#pagoCredito").val(), debito: $("#pagoDebito").val(), cheque: $("#pagoCheque").val(), promissoria: $("#pagoPromissoria").val(), pacote: $("#pagoPacote").val() }, function (item) {
                swal({
                    title: "Parabéns",
                    text: "O Sistema recebeu o pagamento e já está no seu financeiro !",
                    type: "success",
                    showCancelButton: false,
                    confirmButtonText: "Ok",
                },
                    function () {
                        window.location.href = "/Comanda/";
                    });
            });
        }

    });

    function calcularValores() {
        var total = parseFloat($("#totalComanda").val().replace(',', '.')).toFixed(2);
        var restante;
        var troco;
        var dinheiro;
        var credito;
        var debito;
        var cheque;

        dinheiro = $("#pagoDinheiro").val() != "" ? parseFloat($("#pagoDinheiro").val().replace(',', '.')).toFixed(2) : 0;
        credito = $("#pagoCredito").val() != "" ? parseFloat($("#pagoCredito").val().replace(',', '.')).toFixed(2) : 0;
        debito = $("#pagoDebito").val() != "" ? parseFloat($("#pagoDebito").val().replace(',', '.')).toFixed(2) : 0;
        cheque = $("#pagoCheque").val() != "" ? parseFloat($("#pagoCheque").val().replace(',', '.')).toFixed(2) : 0;
        promissoria = $("#pagoPromissoria").val() != "" ? parseFloat($("#pagoPromissoria").val().replace(',', '.')).toFixed(2) : 0;
        pacote = $("#pagoPacote").val() != "" ? parseFloat($("#pagoPacote").val().replace(',', '.')).toFixed(2) : 0;
        if ((parseFloat(dinheiro) + parseFloat(credito) + parseFloat(debito) + parseFloat(cheque) + parseFloat(promissoria) + parseFloat(pacote)) <= parseFloat(total)) {
            restante = (parseFloat(total) - (parseFloat(dinheiro) + parseFloat(credito) + parseFloat(debito) + parseFloat(cheque) + parseFloat(promissoria) + parseFloat(pacote))).toFixed(2);
            troco = 0;
        } else {
            restante = 0;
            troco = ((parseFloat(dinheiro) + parseFloat(credito) + parseFloat(debito) + parseFloat(cheque) + parseFloat(promissoria) + parseFloat(pacote)) - parseFloat(total)).toFixed(2);
        }

        $("#totalRestante").val(restante.toString().replace('.', ','));
        $("#totalTroco").val(troco.toString().replace('.', ','));

    }
});