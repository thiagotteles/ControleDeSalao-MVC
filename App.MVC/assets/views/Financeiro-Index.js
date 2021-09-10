$(".pagarReceber").click(function () {
    var id = $(this).attr("data-id");
    $("#modal").load("PagarReceber?id=" + id, function () {
        $("#modal").modal('show');
    });
});

$('#dataInicial').bootstrapMaterialDatePicker({ time: false, format: "DD/MM/YYYY", lang: 'pt-Br' });
$('#dataFinal').bootstrapMaterialDatePicker({ time: false, format: "DD/MM/YYYY", lang: 'pt-Br' });


$('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
    //save the latest tab; use cookies if you like 'em better:
    localStorage.setItem('lastTab', $(e.target).attr('id'));
});

//go to the latest tab, if it exists:
var lastTab = localStorage.getItem('lastTab');
if (lastTab) {
    $('#' + lastTab).tab('show');
}

$(function () {
    var table = $('#tableDespesa');
    var oTable = table.dataTable({
        "language": {
            url: '//cdn.datatables.net/plug-ins/3cfcc339e89/i18n/Portuguese.json'
        },

        buttons: [
            { extend: 'pdf', className: 'btn red btn-outline' },
            { extend: 'excel', className: 'btn green btn-outline' },
        ],

        rowReorder: {
        },

        "order": [
            [2, 'asc']
        ],

        "lengthMenu": [
            [5, 10, 15, 20, -1],
            [5, 10, 15, 20, "All"] // change per page values here
        ],
        // set the initial value
        "pageLength": 10,

        "dom": "<'row' <'col-md-12'B>><'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r><'table-scrollable't><'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>", // horizobtal scrollable datatable
    });
});

$(function () {
    var table = $('#tableReceita');
    var oTable = table.dataTable({
        "language": {
            url: '//cdn.datatables.net/plug-ins/3cfcc339e89/i18n/Portuguese.json'
        },

        buttons: [
            { extend: 'pdf', className: 'btn red btn-outline' },
            { extend: 'excel', className: 'btn green btn-outline' },
        ],

        rowReorder: {
        },

        "order": [
            [2, 'asc']
        ],

        "lengthMenu": [
            [5, 10, 15, 20, -1],
            [5, 10, 15, 20, "All"] // change per page values here
        ],
        // set the initial value
        "pageLength": 10,

        "dom": "<'row' <'col-md-12'B>><'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r><'table-scrollable't><'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>", // horizobtal scrollable datatable
    });
});

$(function () {
    var table = $('#tableFluxo');
    var oTable = table.dataTable({
        "bSort": false,
        "language": {
            url: '//cdn.datatables.net/plug-ins/3cfcc339e89/i18n/Portuguese.json'
        },

        buttons: [
            { extend: 'pdf', className: 'btn red btn-outline' },
            { extend: 'excel', className: 'btn green btn-outline' },
        ],

        // setup colreorder extension: http://datatables.net/extensions/colreorder/
        colReorder: {
            reorderCallback: function () {
                console.log('callback');
            }
        },

        "lengthMenu": [
            [5, 10, 15, 20, -1],
            [5, 10, 15, 20, "All"] // change per page values here
        ],
        // set the initial value
        "pageLength": 100,

        "dom": "<'row' <'col-md-12'B>><'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r><'table-scrollable't><'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>", // horizobtal scrollable datatable
    });
});

$(".excluir").click(function () {
    var id = $(this).data("item-id");
    swal({
        title: "Excluir lançamento?",
        text: "Você realmente deseja excluir este lançamento?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Sim, Excluir agora!",
        cancelButtonText: "Cancelar",
        closeOnConfirm: false
    },
        function () {
            $.get("/Financeiro/Excluir", { id: id }, function (item) {
                swal({
                    title: "Excluído",
                    text: "O lançamento foi excluído com sucesso!",
                    type: "success",
                    loseOnConfirm: false
                }, function () {
                    window.location.href = "/Financeiro/";
                });
            });

        });
});

