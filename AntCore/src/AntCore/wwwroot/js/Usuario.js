
//var pathname = window.location.pathname; // Returns path only
//$('#btbNuevoUser').click(function (eve) {

//    $('#modal-content').load(pathname + '/Create')
//});

//$('.btnEditarUser').click(function (eve) {

//    $('#modal-content').load(pathname + '/Edit/' + $(this).data('id'))
//});

//$(document).ready(function () {
//    $('#PU000').val('');
//    $('#PU001').val('');
//});

$('#PU000').focus(function () {
    var _Usuario = $('#PU000').val();
    $('#PU001').val(_Usuario)
});
$('#BtnGuardarUsuario').click(function () {
    var Usuario = $('#PU000').val();
    var Password = $('#PU001').val();
    var Nombres = $('#PU002').val();
    var Apellidos = $('#PU003').val();
    var Email = $('#PU006').val();
    var Departamento = $('#PDCOD').val();
    var CodigoAutoridad = $('#PFCOD').val();
    
    if (Usuario === '')
    {
        alert("Ingrese el usuario");
    }
    else if (Password === "")
    {
        alert("Ingrese el password del usuario");
    }
    else if (Nombres === "")
    {
        alert("Ingrese los nombres  del usuario");
    }
    else if (Apellidos === "") {
        alert("Ingrese los Apellidos  del usuario");
    }
    else if (Email === "") {
        alert("Ingrese el email  del usuario");
    }
    else if (Departamento === "") {
        alert("Seleccione el departamento");
    }
    else if (CodigoAutoridad === "") {
        alert("Seleccione el ente de control o autoridad");
    }
    else {
    }
});

