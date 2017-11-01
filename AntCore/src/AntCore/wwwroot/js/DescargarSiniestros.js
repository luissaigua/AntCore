$("#tbfecIniSin").datepicker({ dateFormat: "yy-mm-dd" }).mask("9999-99-99"); // CONTROL DE FECHA  (AÑO-MES-DIA)
$("#tbfecFinSin").datepicker({ dateFormat: "yy-mm-dd" }).mask("9999-99-99"); // CONTROL DE FECHA  (AÑO-MES-DIA)



$('#btnProcesarSin').click(function () {
   
    var fecini = $('#tbfecIniSin').val();
    var fecfin = $('#tbfecFinSin').val();
    var codprov = $('#codprov').val();
    var codautori = $('#codaut').val();
    
    $('#tbFechaini').val(fecini);
    $('#tbFechafin').val(fecfin)
    $('#tbcodprov').val(codprov)
    $('#tbcodautoridad').val(codautori)
    //alert(fecini + ' ' + fecfin + '' + codprov + '' + codautori);
    var mensaje = "";
    ($('#panelMensajeDesS').css("display", "none"));
    if (fecini === "" || fecfin === null) {
        mensaje = "Ingrese la fecha de inicio";
        alert(mensaje);
        $('#mensajeLabelDesS').html(mensaje);
        $('#panelMensajeDesS').css("display", "block");
    }
    else if (fecfin === "" || fecfin === null) {
        mensaje = "Ingrese la fecha de fin";
        $('#mensajeLabelDesS').html(mensaje);
        $('#panelMensajeDesS').css("display", "block");
        alert(mensaje);
    }
    else if (codprov != "") {
        $("#dt_SiniestrosVista").find("tr:gt(0)").remove();
        $.ajax({
            type: "get",
            url: 'DescargaSiniestros/ProcesarVistaSiniestros',
            data: { fecini: fecini, fecFin: fecfin, codprov: codprov, codauto: codautori },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (jsonResult) {
                console.log(jsonResult);
                $.each(jsonResult, function (key, value) {
                    /* Vamos agregando a nuestra tabla las filas necesarias */
                    $("#dt_SiniestrosVista").append("<tr><td style=\"table-layout:fixed\">" + value.codsin + "</td><td>" + value.fecsin + "</td><td>" + value.horsin + "</td><td>" + value.zonsin + "</td><td>" + value.nomprov + "</td><td>" + value.descant + "</td><td class=\"ajustar\">" + value.dirsin + "</td><td>" + value.agente_responsable + "</td><td>" + value.supervisor_responsable + "</td><td>" + value.autoridad + "</td></tr>");
                });
                ($('#panelMensajeDesS').css("display", "none"));

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(textStatus + ": " + XMLHttpRequest.responseText);
            }

        });
    }

});