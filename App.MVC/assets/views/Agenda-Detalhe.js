$('.make-switch').on('switchChange.bootstrapSwitch', function (event, state) {
    $("#EnviarSMS").val(state);
});

$("#btnCancelar").click(function () {
    $('#calendar').weekCalendar("removeUnsavedEvents");
});

try {
    var clientes = new Bloodhound({
        datumTokenizer: function (d) { return Bloodhound.tokenizers.whitespace(d.Nome); },
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 10,
        prefetch: {
            url: 'http://' + $(location).attr('host') + '/Cliente/GetAll',

            cache: false //NEW!
        }
    });

    clientes.clearRemoteCache();
    clientes.initialize(true);

    $('#NomeCliente').typeahead(null, {
        name: 'cliente',
        hint: (App.isRTL() ? false : true),
        displayKey: 'Nome',
        source: clientes.ttAdapter(),
    });
} catch (e) {
    alert(e)
}



$('#NomeCliente').on('typeahead:select', function (event) {
    $.get("/Cliente/ResgataCliente", { nome: $(this).val() }, function (item) {

        if (item.Id == 0) {
            $("#CelularCliente").val("");
        } else {
            $("#CelularCliente").val(item.Celular);
        }
    });
});


$(function () {
    $('#NomeCliente').blur(function () {
        //make ajax call to action method to get the description for this value
        $.get("/Cliente/ResgataCliente", { nome: $(this).val() }, function (item) {

            if (item.Id == 0) {
                $("#CelularCliente").val("");
            } else {
                $("#CelularCliente").val(item.Celular);
            }
        });
    });
});

$('#NomeServico').on('change', function (evt, params) {
    //make ajax call to action method to get the description for this value
    $.get("/Servico/ResgataServico", { servico: $(this).val() }, function (item) {

        if (item.Id == 0) {
            $("#NomeServico").val("");
            $("#Valor").val("");
            $("#Duracao").val("00:30");

        } else {
            if (item.Valor == 0.00) {
                $("#Valor").val("");
            }
            else {
                $("#Valor").val(item.Valor.toFixed(2).replace('.', ','));
            }
            $("#Duracao").val(item.HoraDuracao + ":" + item.MinutoDuracao);
        }

        if (item.ValorParaProfissional == 0.00 || item.ValorParaProfissional == null) {
            $("#NovaComissao").show(100);
            $("#lblComissao").show(100);
            $("#iconComissao").show(100);
            $("#NovaComissao").val("");

        } else {
            $("#NovaComissao").val("");
            $("#NovaComissao").hide(100);
            $("#lblComissao").hide(100);
            $("#iconComissao").hide(100);
        }

    });
});

$('[data-dismiss=modal]').on('click', function (e) {
    var $t = $(this),
        target = $t[0].href || $t.data("target") || $t.parents('.modal') || [];

    $(target)
      .find("input,textarea,select")
         .val('')
         .end()
      .find("input[type=checkbox], input[type=radio]")
         .prop("checked", "")
         .end();
})