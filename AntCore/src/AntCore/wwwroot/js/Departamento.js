// Write your Javascript code.
var pathname = window.location.pathname; // Returns path only
$('#btbNuevoDep').click(function (eve) {
    console.log(pathname);
    $('#modal-content').load(pathname + '/Create')
});

$('.btnEditarDep').click(function (eve) {

    $('#modal-content').load(pathname + '/Edit/' + $(this).data('id'))
});

