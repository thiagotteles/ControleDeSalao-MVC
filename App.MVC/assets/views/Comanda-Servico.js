$('#NovoServico').on('change', function (evt, params) {
    //make ajax call to action method to get the description for this value
    $.get("/Servico/ResgataServico", { servico: $(this).val() }, function (item) {

        if (item.Id == 0) {
            $("#NomeServico").val("");
        } else {
            $("#NovoValor").val(item.Valor.toFixed(2).replace('.', ','));
            $("#NovaComissao").val("");
            $("#NovaHoraDuracao").val("");
        }

        if (item.Valor == 0.00) {
            $("#divConfig").show(100);
            $("#NovoValor").val("");
            $("#NovaComissao").val("");
            $("#NovaHoraDuracao").val("");

        } else {
            $("#divConfig").hide(100);
        }

    });
});