

var pathname = window.location.pathname; // Returns path only
$('#btbNuevoAut').click(function (eve) {

    $('#modal-content').load(pathname + '/Create')
});

$('.btnEditarAut').click(function (eve) {

    $('#modal-content').load(pathname + '/Edit/' + $(this).data('id'))
});

