
let TablaOriginal = ""; // Aqui los datos recuperados de DB
let contenidoOriginal = ""


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



// Recargar tabla
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


/* -----------------------------------------------------
 *               Seccion de filtros
 *------------------------------------------------------
*/


//Filtros de fechas

$('#fechaSearch').daterangepicker();

$('#fechaSearch').daterangepicker({
    format: 'dd/mm/yyyy',
});

$('#fechaSearch').val('');
$('#fechaSearch').attr("placeholder", "Rango de Fechas");

// Generar datos del datapicker al dar click
$("#fechaSearch").click(function () {

    if ($('#fechaSearch').val == '') {
        $('#fechaSearch').daterangepicker();

        $('#fechaSearch').daterangepicker("setDate", 'now');
    }
});


// Vaciar datapicker
function vaciarFecha() {
    $('#fechaSearch').val('');
    $('#fechaSearch').attr("placeholder", "Rango de Fechas");
}


// Input search
$("#searchInput").on("keyup", function () {
    filter();
});

function filter() {
    var value = $("#searchInput").val().toLowerCase();
    $("#contenidoTabla tr").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
}



/* -----------------------------------------------------
 *               Filtros de ausencia
 *------------------------------------------------------
*/



// Obtetener la fecha de salida y de regreso del datapickerrange
function separarFechas(rango) {
    var separador = rango.indexOf('-');
    var fechaI = rango.substring(0, separador - 1)
    var fechaF = rango.substring(separador + 2, rango.length);
    return [fechaI, fechaF];
}


// Filtrar la tabla de ausencias o historico de ausencias
function filtrarAusencias(colSalida, colRegreso, colMotivo) {
    var motivoValue = document.getElementById("motivoFiltro");
    var fechaFiltro = document.getElementById("fechaSearch").value;
    let rows = document.querySelectorAll("#contenidoTabla tr");  // Obtener las filas de la tabla
    let this_row = 0;

    if (fechaFiltro.length > 0) {// Filtrar por fecha si el usuario lo solicito
        for (this_row = 0; this_row < rows.length; this_row++) {
            var row = $(rows[this_row]).closest('tr');                  // Fila actual
            var fechaSalida = separarFechas(fechaFiltro)[0];
            var fechaRegreso = separarFechas(fechaFiltro)[1];
            var dateSalidaFiltro = new Date(fechaSalida);
            var dateRegresoFiltro = new Date(fechaRegreso);
            var tempDate = row.find("td").eq(colSalida).html().split("/");  // Columna del contenido a buscar

            var dateSalidaTabla = new Date(tempDate[2], tempDate[1] - 1, tempDate[0]);
            console.log(dateSalidaTabla)

            tempDate = row.find("td").eq(colRegreso).html().split("/");     // Columna del contenido a buscar
            var dateRegresoTabla = new Date(tempDate[2], tempDate[1] - 1, tempDate[0]);

            if ((dateSalidaFiltro > dateSalidaTabla) || (dateRegresoFiltro < dateRegresoTabla)) { // Si las fechas no estan dentro del rango entonces remover fila
                rows[this_row].remove();
            }
        }
    }

    var motivo = motivoValue.options[motivoValue.selectedIndex].text;  // Obtener el contenido del select de motivo
    if (motivo.length > 0) {                                       // Si se filtra por motivo
        console.log("filtro de motivo");
        for (this_row = 0; this_row < rows.length; this_row++) {
            var row = $(rows[this_row]).closest('tr');                  // Fila actual
            var tipo = row.find("td").eq(colMotivo).html();                     // Columna del contenido a buscar
            if (tipo != motivo) {  // Si el tipo de la tabla es igual al del filtro entonces remover
                rows[this_row].remove();

            }
        }
    }
}


// Vaciar los filtros de ausencia o historico ausencias
function vaciarFiltroAusencias(tipoVaciar) {
    if (tipoVaciar == 1) {
        $("#searchInput").val("");
        filter();
    }
    reloadDatatable();
    $("#motivoFiltro").val(0);
    vaciarFecha();

}



/* -----------------------------------------------------
 *               Filtros de horarios
 *------------------------------------------------------
*/





