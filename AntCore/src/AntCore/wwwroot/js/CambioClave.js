

$('#btnModificarCcontrasenia').click(function () {
    ($('#panelMensajeCC').css("display", "none"));
    $('#mensajeLabelCC').html("");
    ($('#panelMensajeCC1').css("display", "none"));
    $('#mensajeLabelCC1').html("");
    var mensajeError = "";
    var mensajeOk = "";
    var claveactual = $('#tb_contrasenia_actual').val();
    var nuevaclave = $('#tb_nueva_contrasenia').val();
    var confirmarnuevaclave = $('#tb_confirmar_nueva_contrasenia').val();
    console.log(claveactual);
    console.log(nuevaclave);
    console.log(confirmarnuevaclave);
    if (claveactual === '')
    {
        mensajeError = "Ingrese la clave actual";
    }
    else if (nuevaclave === '') {
        mensajeError = "Ingrese la nueva clave";
    }
    else if (confirmarnuevaclave === '') {
        mensajeError = "Ingrese la confirmación de la   nueva clave";
        ($('#panelMensajeCC').css("display", "block"));
        $('#mensajeLabelCC').html(mensajeError);
    }
    else if (confirmarnuevaclave !== nuevaclave) {
        mensajeError = "No coincide la nueva clave con la clave de confirmacion";
        ($('#panelMensajeCC').css("display", "block"));
        $('#mensajeLabelCC').html(mensajeError);
    }
    else if (claveactual === nuevaclave) {
        mensajeError = "La nueva clave debe ser distinta a la actual";
        ($('#panelMensajeCC').css("display", "block"));
        $('#mensajeLabelCC').html(mensajeError);
    }
    else {

        $.ajax({

            type: "GET",
            url: "ModificarClave",
            data:{clave_nueva:confirmarnuevaclave},
            contentType: "aplication/json; charset=utf-8",
            dataType: "json",
            success: function (jsonResult) {
                console.log(jsonResult);
                if (jsonResult.toString() ==='0')
                {
                    mensajeError = "No se puede modificar la contraseña";
                    ($('#panelMensajeCC').css("display", "block"));
                    $('#mensajeLabelCC').html(mensajeError);
                }
                else if(jsonResult.toString() !== '0')
                {
                    mensajeOk = "Contraseña modificada correctamente";
                    ($('#panelMensajeCC1').css("display", "block"));
                    $('#mensajeLabelCC1').html(mensajeOk);
                }
            }

        });
    }

   

});
