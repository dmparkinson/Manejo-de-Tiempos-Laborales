
var listado = []; // Aqui los datos recuperados de DB
var listadoFiltro = []; // Datos filtardos de la pagina( inician con el listado completo)
var listadoBuscar = []; // Datos del buscador, buscan en la listaFiltro

$(document).ready(function () {

     $('#table').DataTable({
        "searching": false,
        "paging": true,
        "info": false,
        "lengthChange": false,
        "responsive": true,
        "autoWidth": false,


    });


});


$("#searchInput").on("keyup", function () {
 
    filter();
});


function reloadDatatable() {
     $('#table').DataTable({
        "searching": false,
        "paging": true,
        "info": false,
        "lengthChange": false,
        "responsive": true,
        "autoWidth": false,
    });
}








/* -----------------------------------------------------
 *               Seccion de filtros
 *------------------------------------------------------                     
*/



function filter() {
    var value = $("#searchInput").val().toLowerCase();
    $("#contenidoTabla tr").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
}





function filtrarAusencias() {

    $("#contenidoTabla tr").filter(function () {
        var motivoValue = document.getElementById("motivoFiltro");
        valueMotivo = motivoValue.options[motivoValue.selectedIndex].text.toLowerCase();
        $(this).toggle($(this).text().toLowerCase().indexOf(valueMotivo) > -1)
    })


    /*
     var fechaFiltro = document.getElementById("fechaFomatFiltro").value;
     let rows = document.querySelectorAll("#contenidoTabla tr");  // Obtener las filas de la tabla

    
    for (let this_row = 0; this_row < rows.length; this_row++) {
        if (motivoFiltro.length > 0) { // Si se filtra por motivo

            var motivo = motivoValue.options[motivoValue.selectedIndex].text;
            var row = $(rows[this_row]).closest('tr');
            var tipo = row.find("td").eq(4).html();            

            if (tipo != motivo ) {  // Si el tipo de la tabla es igual al del filtro entonces no ocul
                //rows[this_row].style.visibility = "hidden"; // Ocultar elemento
                rows[this_row].hide();
                
            }
        }

        if (fechaFiltro.length > 0) {//A,B,D
            var fechaSalida = separarFechasHistoricoAusencia(fechaValue)[0];
            var fechaRegreso = separarFechasHistoricoAusencia(fechaValue)[1];

            document.getElementById("fechaSalida").style.visibility = "hidden";
            document.getElementById("fechaRegreso").style.visibility = "hidden";
        }
    }

    $('#table').DataTable().destroy();

    //$("#contenidoTabla tr").filter(function () {
    //    $(this).toggle($(this).text().toLowerCase().indexOf(getComputedStyle().visibility === "hidden") > -1)
   // })
 

    /*

    $("#contenidoTabla tr").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(valueMotivo) > -1)
    })
    */
}


$('#fechaFomatFiltro').daterangepicker();

$('#fechaFomatFiltro').daterangepicker({
    locale: {
        format: 'YYYY/MM/DD'
    }
});
$('#fechaFomatFiltro').val('');
$('#fechaFomatFiltro').attr("placeholder", "Rango de Fechas");




function vaciarFiltroAusencias() {
    $("#contenidoTabla tr").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf("") > -1)
    })
    $("#motivoFiltro").val(0);

    vaciarFecha();
}


function vaciarFecha() {
    $('#fechaFomatFiltro').val('');
    $('#fechaFomatFiltro').attr("placeholder", "Rango de Fechas");
}






