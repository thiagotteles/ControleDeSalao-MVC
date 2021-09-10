try {
    $(function () {
        $("#btnReceber").click(function () {
            if ($("#NomeProfissional").val() == "") {
                sweetAlert('Informe o profissional...', ' ', 'info')
                return false;
            }
            if ($("#NomeCliente").val() == "") {
                sweetAlert('Informe o cliente...', ' ', 'info')
                return false;
            }
            return true;
        });

        $("#ValorDesconto").blur(function () {
            $.get("/Comanda/SalvarDesconto", { id: $("#Id").val(), desconto: $(this).val() }, function (item) {

                if ($("#ValorDesconto").val() != "") {
                    var subtotal = parseFloat($("#subtotal").val().replace(',', '.')).toFixed(2);
                    var desconto = parseFloat($("#ValorDesconto").val().replace(',', '.')).toFixed(2);
                    $("#ValorTotal").val((subtotal - desconto).toString().replace('.', ','));
                    $("#totalComanda").val((subtotal - desconto).toString().replace('.', ','));
                    $("#totalRestante").val((subtotal - desconto).toString().replace('.', ','));
                } else {
                    $("#ValorTotal").val($("#subtotal").val());
                    $("#totalComanda").val($("#subtotal").val());
                    $("#totalRestante").val($("#subtotal").val());
                }
            });
        });

        $('#SData').bootstrapMaterialDatePicker({ time: false, format: "DD/MM/YYYY", lang: 'pt-Br' });

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

        $("#NomeProfissional").change(function () {
            $.get("/Comanda/SalvarProfissional", { profissional: $(this).val(), id: $("#Id").val() }, function (item) {

                if (item.Id == 0) {
                    $("#NomeProfissional").val("");
                } else {
                    $("#NomeProfissional").val(item.Nome);
                }
            });
        });

        $('#NomeCliente').blur(function () {
            $.get("/Comanda/SalvarCliente", { cliente: $(this).val(), id: $("#Id").val() }, function (item) {
                if (item.Id == 0) {
                    $("#NomeCliente").val("");
                } else {
                    $("#NomeCliente").val(item.Nome);
                }
            });
        });

        $('#SData').change(function () {
            $.get("/Comanda/SalvarData", { data: $(this).val(), id: $("#Id").val() }, function (item) {
                if (item.Id == 0) {
                    $("#SData").val("");
                } else {
                    $("#SData").val(item);
                }
            });
        });

        $('.btnModal').click(function () {
            $.get("/Comanda/SalvarCliente", { cliente: $('#NomeCliente').val(), id: $("#Id").val() }, function (item) {
                if (item.Id == 0) {

                    $("#NomeCliente").val("");
                } else {
                    $("#NomeCliente").val(item.Nome);
                }
            });
        });

        $('#NomeCliente').on('typeahead:select', function (event) {
            $.get("/Comanda/SalvarCliente", { cliente: $(this).val(), id: $("#Id").val() }, function (item) {
                if (item.Id == 0) {
                    $("#NomeCliente").val("");
                } else {
                    $("#NomeCliente").val(item.Nome);
                }
            });
        });

        $(".excluir").click(function () {
            swal({
                title: "Excluir comanda?",
                text: "Você realmente deseja excluir esta comanda?",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Sim, Excluir agora!",
                cancelButtonText: "Cancelar",
                closeOnConfirm: false
            },
                function () {
                    $.get("/Comanda/Excluir", { id: $("#Id").val() }, function (item) {
                        swal({
                            title: "Excluída",
                            text: "A comanda foi excluída.",
                            type: "success",
                            loseOnConfirm: false
                        }, function () {
                            window.location.href = "/Comanda/";
                        });
                    });

                });
        });


        if ($("#ValorDesconto").val() != "") {
            var subtotal = parseFloat($("#subtotal").val().replace(',', '.')).toFixed(2);
            var desconto = parseFloat($("#ValorDesconto").val().replace(',', '.')).toFixed(2);
            $("#ValorTotal").val((subtotal - desconto).toString().replace('.', ','));
            $("#totalComanda").val((subtotal - desconto).toString().replace('.', ','));
            $("#totalRestante").val((subtotal - desconto).toString().replace('.', ','));
        } else {
            $("#ValorTotal").val($("#subtotal").val());
            $("#totalComanda").val($("#subtotal").val());
            $("#totalRestante").val($("#subtotal").val());
        }
    });
} catch (e) {
    alert(e);
}
