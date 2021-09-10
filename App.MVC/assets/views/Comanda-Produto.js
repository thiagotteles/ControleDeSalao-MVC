$('#NovoProduto').on('change', function (evt, params) {
    //make ajax call to action method to get the description for this value
    $.get("/Produto/ResgataProduto", { produto: $(this).val() }, function (item) {

        if (item.Id == 0) {
            $("#NovoProduto").val("");
            $("#NovoValorProduto").val("");
            $("#NovaUnidade").val("");
            $("#NovaQtdProduto").val("");
            $("#qtdEstoque").val("0");
        } else {
            $("#NovaQtdProduto").val("1");
            $("#NovaUnidade").val(item.Unidade.toString());
            $("#NovoValorProduto").val(item.PrecoVenda.toFixed(2).replace('.', ','));
            $("#qtdEstoque").val(item.QtdEstoque);
        }

    });
});

$(function () {
    $('#NovoProduto').blur(function () {
        $.get("/Produto/ResgataProduto", { produto: $(this).val() }, function (item) {
            if (item.Id == 0) {
                $("#NovoProduto").val("");
                $("#NovoValorProduto").val("");
                $("#NovaUnidade").val("");
                $("#NovaQtdProduto").val("");
            } else {
                $("#NovaQtdProduto").val("1");
                $("#NovaUnidade").val(item.Unidade.toString());
                $("#NovoValorProduto").val(item.PrecoVenda.toFixed(2).replace('.', ','));
            }
        });
    });
});

$(function () {
    $("#AddProduto").submit(function () {
        //alert(parseFloat($('#qtdEstoque').val()) < parseFloat($("#NovaQtdProduto").val()))
        if ($('#qtdEstoque').val() != '' && $('#qtdEstoque').val() > 0 && parseFloat($('#qtdEstoque').val()) < parseFloat($("#NovaQtdProduto").val())) {
            
            sweetAlert('Oops...','Quantidade indisponível no seu estoque!','info')
            return false;
        }
    });
    return true;
});