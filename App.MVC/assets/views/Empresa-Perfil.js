
$('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
    //save the latest tab; use cookies if you like 'em better:
    localStorage.setItem('lastTab', $(e.target).attr('id'));
});

//go to the latest tab, if it exists:
var lastTab = localStorage.getItem('lastTab');
if (lastTab) {
    $('#' + lastTab).tab('show');
}

$(".imprimir").click(function () {
    var id = $(this).attr("data-id");
    var url = "/Empresa/VisualizarBoleto/" + id;
    window.open(url, "WindowPopup", 'width=688,height=780');
});

$(function () {
    var table = $('#tableMensalidades');
    var oTable = table.dataTable({
        "language": {
            url: '//cdn.datatables.net/plug-ins/3cfcc339e89/i18n/Portuguese.json'
        },

        buttons: [
        ],

        // setup colreorder extension: http://datatables.net/extensions/colreorder/
        colReorder: {
            reorderCallback: function () {
                console.log('callback');
            }
        },

        "order": [
            [0, 'dsc']
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
