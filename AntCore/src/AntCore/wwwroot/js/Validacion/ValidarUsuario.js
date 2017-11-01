$("#PU000").change(function () {

    var user = $("#PU000").val();

    $.get("/Usuarios/ValidacionUsuario", { usuario: user }, function (data) {
        var d = data;
        if (d) {

            $("#mensajeLabelSiniestro").text("El usuario ya se encuentra registrado");
            $('#divMensajeSiniestro').show()

            $('#BtnGuardar').hide();
        } else {

            $("#mensajeLabelSiniestro").text("NewText");
            $('#divMensajeSiniestro').hide();

            $('#BtnGuardar').show();
        }

    })

});

