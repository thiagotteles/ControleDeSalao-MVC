$.get("/Dashboard/LucroAnual", {}, function (item) {
    var chartData = [];

    for (i = 0; i < item.length; i++) {
        var milli = item[i].date.replace(/\/Date\((-?\d+)\)\//, '$1');
        var d = new Date(parseInt(milli));

        chartData.push({
            date: new Date(d.getFullYear(), d.getMonth(), d.getDate()),
            value: item[i].value,
            volume: item[i].volume
        });
    }

    var chart = AmCharts.makeChart("lucroAnual", {
        type: "stock",
        "theme": "light",
        pathToImages: App.getGlobalPluginsPath() + "amcharts/amcharts/images/",
        "fontFamily": 'Open Sans',
        "color": '#888',
        dataSets: [{
            color: "#67b7dc",
            fieldMappings: [{
                fromField: "value",
                toField: "value"
            }, {
                fromField: "volume",
                toField: "volume"
            }],
            dataProvider: chartData,
            categoryField: "date",
            // EVENTS
        }],
        panels: [{
            title: "Lucro",
            percentHeight: 70,
            stockGraphs: [{
                id: "g1",
                valueField: "value"
            }],

            stockLegend: {
                valueTextRegular: " ",
                markerType: "none"
            }
        }],
        chartScrollbarSettings: {
            graph: "g1"
        },
        chartCursorSettings: {
            valueBalloonsEnabled: true,
            graphBulletSize: 5,
            valueLineBalloonEnabled: true,
            valueLineEnabled: true,
            valueLineAlpha: 0.5
        },

        periodSelector: {
            periods: [{
                period: "DD",
                count: 10,
                label: "10 days"
            }, {
                period: "MM",
                count: 1,
                label: "1 month"
            }, {
                period: "YYYY",
                count: 1,
                label: "1 year"
            }, {
                period: "MAX",
                label: "Este ano"
            }]
        },

        panelsSettings: {
            usePrefixes: true
        }
    });



});

$.get("/Dashboard/DespesasPorPlanoDeConta", {}, function (item) {
    var chart = AmCharts.makeChart("despesasPorPlanoDeConta", {
        "type": "pie",
        "theme": "light",

        "fontFamily": 'Open Sans',

        "color": '#888',

        "dataProvider": item,
        "valueField": "value",
        "titleField": "label",
        "outlineAlpha": 0.4,
        "depth3D": 15,
        "balloonText": "[[title]]<br><span style='font-size:14px'><b>[[value]]</b> ([[percents]]%)</span>",
        "angle": 30,
        "exportConfig": {
            menuItems: [{
                icon: '/lib/3/images/export.png',
                format: 'png'
            }]
        }
    });
});

$.get("/Dashboard/ReceitaDespesaNoMes", {}, function (item) {

    var chart = AmCharts.makeChart("receitaDespesa", {
        "type": "serial",
        "theme": "light",
        "pathToImages": App.getGlobalPluginsPath() + "amcharts/amcharts/images/",
        "autoMargins": true,
        "fontFamily": 'Open Sans',
        "color": '#888',
        "dataProvider": item,
        "valueAxes": [{
            "axisAlpha": 0.2
        }],
        "startDuration": 1,
        "graphs": [{
            "alphaField": "alpha",
            "balloonText": "<span style='font-size:13px;'>[[title]] em [[category]]:<b> R$[[value]]</b> [[additional]]</span>",
            "dashLengthField": "dashLengthColumn",
            "fillAlphas": 1,
            "title": "Receitas",
            "type": "column",
            "valueField": "receita"
        }, {
            "balloonText": "<span style='font-size:13px;'>[[title]] em [[category]]:<b> R$[[value]]</b> [[additional]]</span>",
            "bullet": "round",
            "dashLengthField": "dashLengthLine",
            "lineThickness": 3,
            "bulletSize": 7,
            "bulletBorderAlpha": 1,
            "bulletColor": "#FFFFFF",
            "useLineColorForBulletBorder": true,
            "bulletBorderThickness": 3,
            "fillAlphas": 0,
            "lineAlpha": 1,
            "title": "Despesas",
            "valueField": "despesa"
        }],
        "categoryField": "mes",
        "categoryAxis": {
            "gridPosition": "start",
            "axisAlpha": 0,
            "tickLength": 0
        }
    });

});
$('#receitaDespesa').closest('.portlet').find('.fullscreen').click(function () {
    chart.invalidateSize();
});
$('#despesasPorPlanoDeConta').closest('.portlet').find('.fullscreen').click(function () {
    chart.invalidateSize();
});
$('#lucroAnual').closest('.portlet').find('.fullscreen').click(function () {
    chart.invalidateSize();
});