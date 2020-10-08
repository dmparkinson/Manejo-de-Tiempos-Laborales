
var listado = []; // Aqui los datos recuperados de DB
var listadoFiltro = []; // Datos filtardos de la pagina( inician con el listado completo)
var listadoBuscar = []; // Datos del buscador, buscan en la listaFiltro

$(document).ready(function () {
    
    //listado = localStorage.getItme("Listado");


    $("#searchInput").on("keyup", function () {
        console.log("Entro");
        var value = $(this).val().toLowerCase();
        $("#contenidoTabla tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });


    /*
    var table = $('#table').DataTable({
        "searching": false,
        "paging": true,
        "info": false,
        "lengthChange": false,
        "responsive": true,
        "autoWidth": false,
    });
    */

});




/*$(function () {
    $("#example1").DataTable({
        "responsive": true,
        "autoWidth": false,
    });
    $('#example2').DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": false,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
    });
});
*/



