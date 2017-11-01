//$('#btnBuscarVictima').click(function () {
//    var _numidenvicinv = $('#numidenvicinv').val();
//    alert(_numidenvicinv);
//    var mensaje = "";
//    ($('#panelMensajeCal').css("display", "none"));
//    ($('#panelMensajeCalE').css("display", "none"));

//    if (_numidenvicinv === "") {
//        mensaje = "Ingrese el número de identificación de la víctima.";
//        $('#mensajeLabelCalE').html(mensaje);
//        ($('#panelMensajeCalE').css("display", "block"));
//    }
//    else if (_numidenvicinv.length < 10) {
//        mensaje = "Ingrese el número de identificación de la víctima válido.";
//        $('#mensajeLabelCalE').html(mensaje);
//        ($('#panelMensajeCalE').css("display", "block"));
//    }
//    else if (_numidenvicinv != "") {
//        $("#dt_busqueda").find("tr:gt(0)").remove();
//        $.ajax({
//            type: "get",
//            url: 'Consultas/FindVistaVictimas',
//            data: { numidenvicinv: _numidenvicinv },
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            success: function (jsonResult) {
//                console.log(jsonResult);
//                $.each(jsonResult, function (key, value) {
//                    /* Vamos agregando a nuestra tabla las filas necesarias */
//                    $("#dt_busqueda").append("<tr><td>" + value.codsin + "</td><td>" + value.fecsin + "</td><td>" + value.numidenvicinv + "</td><td>" + value.nomprov + "</td><td>" + value.descant + "</td><td>" + value.convicinv24 + "</td><td><a class=\"btnvistaVicc btn\" data-id=" + value.codsin + " ><span class=\"glyphicon glyphicon-edit\" onclick=\" PruebaVistaVic(" + value.codsin + ")\"></a></td></tr>");
//                });
//                ($('#panelMensajeCalE').css("display", "none"));

//            },
//            error: function (XMLHttpRequest, textStatus, errorThrown) {
//                alert("Error en la conexión con la base de datos");
//            }

//        });
//    }

//});





//function PruebaVistaVic(Id) {
//    console.log(pathname);
    
//    console.log('codigo sin');
//    console.log(Id);
//    if (Id) {
//        var codAccion = Id;
//        $.ajax({
//            type: "post",
//            url: 'Siniestro/FindSiniestro',
//            data: { id: Id },
           
          
//            error: function (XMLHttpRequest, textStatus, errorThrown) {
//                alert(textStatus + ": " + XMLHttpRequest.responseText);
//            }

//        });
       

//    }
//};
