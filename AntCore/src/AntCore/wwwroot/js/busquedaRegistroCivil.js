
$('#btnBuscarVict1').click(function () {
    ObtenerDatosConsultaPorCedula1();
});





function ObtenerDatosConsultaPorCedula1() {
    var tipoI = '';
    var cedula = $("#tb_IdentificacionVictima1").val();
    console.log("ingresa WS1");
    if ($("#tb_IdentificacionVictima1").val().length === 10) {
        $("#divLoaderSinBus").css("display", "block");
        $.ajax({
            type: "get",
            url: 'ObtenerDatosWSRC',
            data: { numeroIdentificacion: cedula },
            contentType: "application/json;",
            dataType: "json",
            success: function (jsonResult) {
                console.log("ingresa WS2");
                console.log( jsonResult);
              
                if (jsonResult.length > 0) {
                    $.each(jsonResult, function (key, value) {


                        $('#tb_NombresVictima1').val(value.nombre);
                        $("#tb_edadVictima1").val(value.edad);
                        if (value.sexo === 'HOMBRE') {
                            $('#tb_sexov1').val('H');
                        }
                        else if (value.sexo === 'MUJER') {
                            $('#tb_sexov1').val('M');
                        }
                        else {
                            $('#tb_sexov1').val('-1');
                        }

                        mensaje = "Búsqueda exitosa.";
                        alert(mensaje.toString() + ' ' + value.nombre);
                        $("#divLoaderSinBus").css("display", "none");

                    });
                }
                else {
                    alert("No hay resultado en  la busqueda. Favor verifique el número de indetificación ingresado.");
                    $("#divLoaderSinBus").css("display", "none");
                }
                   
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("No hay resultado en  la busqueda. Error con el servidor del registro civil.");
                $("#divLoaderSinBus").css("display", "none");
            }

        });

    }
    else {
        alert("Ingrese un número de identificación correcto. Recuerde que debe contener 10 dígitos");
    }
};
