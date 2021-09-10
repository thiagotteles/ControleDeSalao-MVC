
$('.make-switch').on('switchChange.bootstrapSwitch', function (event, state) {
    $("#IncluirComoPago").val(state);
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

    $('#NomeQuem').typeahead(null, {
        name: 'cliente',
        hint: (App.isRTL() ? false : true),
        displayKey: 'Nome',
        source: clientes.ttAdapter(),
    });
} catch (e) {
    alert(e)
}



$('#NomeQuem').on('typeahead:select', function (event) {
    $.get("/Cliente/ResgataCliente", { nome: $(this).val() }, function (item) {
        if (item.Id > 0) {
            $("#NomeQuem").val(item.Nome);
        }
    });
});


$(document).ready(function () {
    $("#NomeQuem").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Cliente/ClienteAutoComplete", type: "POST", dataType: "json",
                data: { id: request.term },

                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.label, value: item.label, id: item.id };
                    }));
                },
                select: function (event, ui) {
                    $("#NomeQuem").val(ui.item.id);
                }
            });
        },
    });

    $('.datepicker').datepicker({
        format: "dd/mm/yyyy",
        language: "pt-BR",
        autoclose: true
    });
});
