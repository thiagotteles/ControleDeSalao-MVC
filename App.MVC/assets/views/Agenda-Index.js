var year = new Date().getFullYear();
var month = new Date().getMonth();
var day = new Date().getDate();

var dt = new Date();

var softSlider = document.getElementById('zoom');

$(document).ready(function () {
    updateCalendar();
    var lastSel = localStorage.getItem('lastSel');
    if (lastSel) {
        $("#profissional").val(lastSel);
    }

    $("#profissional").change(function () {
        localStorage.setItem('lastSel', $("#profissional").val());
        updateCalendar();
    });

    var softSlider = document.getElementById('zoom');
    noUiSlider.create(softSlider, {
        start: 4,
        range: {
            min: 1,
            max: 10
        },
        step: 1,
        pips: {
            mode: 'steps',
            density: 5
        }
    });

    softSlider.noUiSlider.on('change', function (values, handle) {
        updateCalendar();
    });
});


function updateCalendar() {
    try {
        var lastSel = localStorage.getItem('lastSel');
        if (lastSel == null) {
            lastSel = $("#profissional").val();
        }

        var softSlider = document.getElementById('zoom');
        var value = 4;
        if (softSlider.noUiSlider != null)
            value = softSlider.noUiSlider.get();

        var $calendar = $('#calendar').weekCalendar({
            timeslotsPerHour: value,//$("#tempo").val(),
            scrollToHourMillis: 0,
            dateFormat: "d M",
            businessHours: { start: 7, end: 22, limitDisplay: true },
            hourLine: true,
            allowCalEventOverlap: true,
            overlapEventsSeparate: true,
            firstDayOfWeek: dt.getDay(),
            height: function ($calendar) {
                return $(window).height();
            },
            eventRender: function (calEvent, $event) {
                if (calEvent.end.getTime() < new Date().getTime()) {
                    $event.css('backgroundColor', '#aaa');
                    $event.find('.time').css({ 'backgroundColor': '#999', 'border': '1px solid #888' });
                }
            },
            eventNew: function (calEvent, $event) {
                $("#Parametros").val(calEvent.start + "|" + lastSel);
                $('#myModal').modal('show');
                $('#calendar').weekCalendar("removeUnsavedEvents");
            },
            eventDrop: function (calEvent, $event) {
                $.get("/Agenda/AtualizarAgenda", { id: calEvent.id, start: calEvent.start, end: calEvent.end }, function (item) {
                });
            },
            eventClick: function (calEvent, $event) {
                swal({
                    title: 'Ir pra comanda',
                    text: "Deseja ver ou criar a comanda?",
                    type: 'info',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Sim, ir agora!',
                    cancelButtonText: 'Não, Cancelar!',
                    confirmButtonClass: 'btn btn-success',
                    cancelButtonClass: 'btn btn-danger',
                    buttonsStyling: false,
                    closeOnConfirm: false,
                    closeOnCancel: true
                },
                    function (isConfirm) {
                        if (isConfirm) {
                            window.location.href = "/Comanda/Incluir/" + calEvent.id;
                        }
                    });
            },
            eventResize: function (calEvent, $event) {
                $.get("/Agenda/AtualizarAgenda", { id: calEvent.id, start: calEvent.start, end: calEvent.end }, function (item) {
                });
            },
            data: function (start, end, callback) {
                $.get("/Agenda/ResgataAgenda", { id: lastSel, start: start, end: end }, function (item) {
                    var result = {
                        options: {
                            allowEventDelete: true,
                            eventDelete: function (calEvent, element, dayFreeBusyManager, calendar, clickEvent) {
                                swal({
                                    title: "Excluir agenda?",
                                    text: "Você realmente deseja excluir este agendamento?",
                                    type: "warning",
                                    showCancelButton: true,
                                    confirmButtonColor: "#DD6B55",
                                    confirmButtonText: "Sim, Excluir agora!",
                                    cancelButtonText: "Cancelar",
                                    closeOnConfirm: false
                                },
                                    function () {
                                        $.get("/Agenda/CancelarAgenda", { id: calEvent.id }, function (item) {

                                            calendar.weekCalendar('removeEvent', calEvent.id);
                                            swal("Excluída!", "O agendamento foi excluído.", "success");
                                        });

                                    });
                            },
                            deletable: function (calEvent, element) {
                                return calEvent.start > Date.today();
                            }
                        },
                        events: []
                    };

                    for (i = 0; i < item.length; i++) {
                        var year = parseInt(item[i].SData.substring(6, 10));
                        var month = parseInt(item[i].SData.substring(3, 5));
                        var day = parseInt(item[i].SData.substring(0, 2));
                        var d = new Date(year + '-' + month + '-' + day);

                        var hrIni = 0;
                        var minIni = 0;
                        if (item[i].HoraInicial.toString().length == 3) {
                            hrIni = item[i].HoraInicial.toString().substring(0, 1);
                            minIni = item[i].HoraInicial.toString().substring(1, 3);
                        } else {
                            hrIni = item[i].HoraInicial.toString().substring(0, 2);
                            minIni = item[i].HoraInicial.toString().substring(2, 4);
                        }

                        var hrFim = 0;
                        var minFim = 0;
                        if (item[i].HoraFinal.toString().length == 3) {
                            hrFim = item[i].HoraFinal.toString().substring(0, 1);
                            minFim = item[i].HoraFinal.toString().substring(1, 3);
                        } else {
                            hrFim = item[i].HoraFinal.toString().substring(0, 2);
                            minFim = item[i].HoraFinal.toString().substring(2, 4);
                        }
                        var title = "" + item[i].Cliente.Nome + " <br /> " + (item[i].Cliente.Celular != null ? item[i].Cliente.Celular : "") + "<br />" + item[i].Servico.Descricao;

                        result.events.push({ 'id': item[i].Id, 'start': new Date(d.getFullYear(), d.getMonth(), d.getDate(), hrIni, minIni), 'end': new Date(d.getFullYear(), d.getMonth(), d.getDate(), hrFim, minFim), 'title': title });
                    }
                    callback(result);
                });


                //callback(resgataAgenda());
            }
        });

        $calendar.weekCalendar('refresh');
    } catch (e) {
        alert(e)
    }
}
