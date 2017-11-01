

$(document).ready(function () {
    $("#feccal").datepicker({ dateFormat: "yy-mm-dd" }).mask("9999-99-99"); // CONTROL DE FECHA  (AÑO-MES-DIA)
    $("#fecinical").datepicker({ dateFormat: "yy-mm-dd" }).mask("9999-99-99"); // CONTROL DE FECHA  (AÑO-MES-DIA)
    $("#fecFin").datepicker({ dateFormat: "yy-mm-dd" }).mask("9999-99-99"); // CONTROL DE FECHA  (AÑO-MES-DIA)


});

$('#btnProcesar').click(function () {
    var fecini = $('#fecinical').val();
    var fecfin = $('#fecFin').val();
    var codprov = $('#codprov').val();
    $('#tbFechaini').val(fecini);
    $('#tbFechafin').val(fecfin)
    $('#tbcodprov').val(codprov)
    var mensaje = "";
    ($('#panelMensajeCalE').css("display", "none"));
    if (fecini === "" || fecfin === null)
    {
        mensaje = "Ingrese la fecha de inicio";
        $('#mensajeLabelCalE').html(mensaje);
        ($('#panelMensajeCalE').css("display", "block"));
    }
    else if (fecfin === "" || fecfin === null) {
        mensaje = "Ingrese la fecha de fin";
        $('#mensajeLabelCalE').html(mensaje);
        ($('#panelMensajeCalE').css("display", "block"));
    }
    else if (codprov != "")
    {
        $("#dt_calificacionesVista").find("tr:gt(0)").remove();
        $.ajax({
            type: "get",
            url: 'ProcesarVista',
            data: { fecinical: fecini, fecFin: fecfin, codprov: codprov },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (jsonResult) {
                console.log(jsonResult);
                $.each(jsonResult, function (key, value) {
                    /* Vamos agregando a nuestra tabla las filas necesarias */
                    $("#dt_calificacionesVista").append("<tr><td>" + value.feccal + "</td><td>" + value.nombreProvincia + "</td><td>" + value.cumcal + "</td><td>" + value.puncal + "</td><td>" + value.calcal + "</td><td>" + value.comcal + "</td><td>" + value.totcal + "</td></tr>");
                });
                ($('#panelMensajeCalE').css("display", "none"));

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(textStatus + ": " + XMLHttpRequest.responseText);
            }

        });
    }

});


$('#ch_reemplazar').change (function () {
    if ($('#ch_reemplazar').prop("checked") === true)
    {
        if (confirm('Va a habilitar la opción para reemplazar las calificaciones en la fecha seleccionada. Desea continuar !') == true) {
            $('#reemplazar').val("SI");
        }
        else {
            $('#reemplazar').val("NO");
            $("#ch_reemplazar").prop("checked", false);
        }
        
    }
    if ($('#ch_reemplazar').prop("checked") === false) {
        $('#reemplazar').val("NO");
        
    }
});