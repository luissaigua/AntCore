var pathname = window.location.pathname; // Returns path only
$('#btbNuevoFun').click(function (eve) {

    $('#modal-content').load(pathname + '/Create')
});

$('.btnEditarFun').click(function (eve) {

    $('#modal-content').load(pathname + '/Edit/' + $(this).data('id'))
});

