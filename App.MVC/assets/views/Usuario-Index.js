$(function () {
    var table = $('#tableUsuarios');
    var oTable = table.dataTable({
        "language": {
            url: '//cdn.datatables.net/plug-ins/3cfcc339e89/i18n/Portuguese.json'
        },

        buttons: [
            { extend: 'pdf', className: 'btn red btn-outline' },
            { extend: 'excel', className: 'btn green btn-outline' },
        ],

        // setup colreorder extension: http://datatables.net/extensions/colreorder/
        //colReorder: {
        //    reorderCallback: function () {
        //        console.log('callback');
        //    }
        //},

        "lengthMenu": [
            [5, 10, 15, 20, -1],
            [5, 10, 15, 20, "All"] // change per page values here
        ],
        // set the initial value
        "pageLength": 10,

        "dom": "<'row' <'col-md-12'B>><'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r><'table-scrollable't><'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>", // horizobtal scrollable datatable
    });
});

$(".editar").click(function () {
    var id = $(this).attr("data-id");
    $("#modalEdit").load("Detalhe?id=" + id, function () {
        try {
            $("#modalEdit").modal('show');
        } catch (e) {

        }
        
    });
});

$(".excluir").click(function () {
    var id = $(this).data("item-id");
    swal({
        title: "Excluir Usuário?",
        text: "Você realmente deseja excluir este Usuário?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Sim, Excluir agora!",
        cancelButtonText: "Cancelar",
        closeOnConfirm: false
    },
        function () {
            $.get("/Usuario/Excluir", { id: id }, function (item) {
                swal({
                    title: "Excluído",
                    text: "O Usuário foi excluído com sucesso!",
                    type: "success",
                    loseOnConfirm: false
                }, function () {
                    window.location.href = "/Usuario/";
                });
            });

        });
});