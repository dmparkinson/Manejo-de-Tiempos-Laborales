
let TablaOriginal = ""; // Aqui los datos recuperados de DB
let contenidoOriginal =""
let TablaFiltro = ""; // Datos filtardos de la pagina( inician con el listado completo)
var listadoBuscar = []; // Datos del buscador, buscan en la listaFiltro


$(document).ready(function () {

    var tabla = $('#table').DataTable({
        "searching": false,
        "paging": true,
        "info": false,
        "lengthChange": false,
        "responsive": true,
        "autoWidth": false,
    });

    TablaOriginal = tabla;
    contenidoOriginal = document.getElementById("contenidoTabla");

});







/* -----------------------------------------------------
 *               Seccion de filtros
 *------------------------------------------------------                     
*/



$("#searchInput").on("keyup", function () {

    filter();
});




function reloadDatatable() {
    $('#table').DataTable().destroy();
    $('#table').DataTable({
        "searching": false,
        "paging": true,
        "info": false,
        "lengthChange": false,
        "responsive": true,
        "autoWidth": false,
    });

    document.getElementById("contenidoTabla").value = contenidoOriginal;
}



function filter() {
    var value = $("#searchInput").val().toLowerCase();
    $("#contenidoTabla tr").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
}


function separarFechas(rango) {
    var separador = rango.indexOf('-');
    var fechaI = rango.substring(0, separador - 1)
    var fechaF = rango.substring(separador + 2, rango.length);
    return [fechaI, fechaF];
}



function filtrarAusencias() {


     var motivoValue = document.getElementById("motivoFiltro");
     var fechaFiltro = document.getElementById("fechaFomatFiltro").value;
     let rows = document.querySelectorAll("#contenidoTabla tr");  // Obtener las filas de la tabla


    for (let this_row = 0; this_row < rows.length; this_row++) {
        var row = $(rows[this_row]).closest('tr');                  // Fila actual
        var filtrar = false;
        /*if ((motivoFiltro.length > 0) || (fechaFiltro.length > 0) ) { // Si se filtra por motivo

            var motivo = motivoValue.options[motivoValue.selectedIndex].text;
            
            var tipo = row.find("td").eq(4).html();            

            if (tipo != motivo ) {  // Si el tipo de la tabla es igual al del filtro entonces remover
                rows[this_row].remove();
                
            }
        } */

        if (fechaFiltro.length > 0) {//A,B,D
            var fechaSalida = separarFechas(fechaFiltro)[0];
            var fechaRegreso = separarFechas(fechaFiltro)[1];
            var dateSalidaFiltro = new Date(fechaSalida);
            var dateRegresoFiltro = new Date(fechaRegreso);

            var dateSalidaTabla = new Date(row.find("td").eq(5).html());
            var dateRegresoTabla = new Date(row.find("td").eq(6).html());

            if ((dateSalidaFiltro > dateRegresoFiltro) || (dateSalidaFiltro < dateSalidaTabla) || (dateRegresoFiltro > dateRegresoTabla)) {
                rows[this_row].remove();
            }
        }
    }
}


$('#fechaFomatFiltro').daterangepicker();

$('#fechaFomatFiltro').daterangepicker({
    format: 'dd/mm/yyyy',
});


$('#fechaFomatFiltro').val('');
$('#fechaFomatFiltro').attr("placeholder", "Rango de Fechas");


$("#fechaFomatFiltro").click(function () {

    if ($('#fechaFomatFiltro').val == '') {
        $('#fechaFomatFiltro').daterangepicker();

        $('#fechaFomatFiltro').daterangepicker("setDate", 'now');
    }
});




function vaciarFiltroAusencias() {

    reloadDatatable(); 
    $("#motivoFiltro").val(0);
    vaciarFecha();
}



function vaciarFecha() {
    $('#fechaFomatFiltro').val('');
    $('#fechaFomatFiltro').attr("placeholder", "Rango de Fechas");
}






