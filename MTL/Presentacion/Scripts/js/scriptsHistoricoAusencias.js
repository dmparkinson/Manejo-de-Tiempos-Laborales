
/* 
 * 
 * Eliminar el tipo de ausencia 
 * 
 * */
function eliminarHAusencia(codigo) {
    
    Swal.fire({
        title: '¿Seguro?',
        text: 'No se podrán revertir los cambios',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, eliminar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.value) {
            // Proceso de eliminacion de datos

            var envio = {
                codigo: codigo
            };
            $.ajax({
                url: "/Historico_Ausencias/Eliminar",    // Nombre del controlador/ accion del controlador
                type: "post",
                data: envio,
                success: function (response) {
                    
                    if (response.success == true) {

                        if (response.deleted == true) // Si el tiempo de ausencia se elimino correctamente
                        {
                            Swal.fire(
                                'Eliminado!',
                                'El tiempo de ausencia se elimino correctamente.',
                                'success'
                            )
                            refrescarHistoricoAusencias();

                        }
                        else { // si no se elimino
                            Swal.fire(
                                'No eliminado',
                                'El tiempo de ausencia seleccionado no se puede eliminar.',
                                'warning'
                            )
                        }
                    }
                    else { // No se encontro el tiempo de horario
                        alert(response.dato);
                        Swal.fire(
                            'No encontrado',
                            'El el tiempo de ausencia no se encuentra registrado.',
                            'warning'
                        )
                    }
                },
                error: function () {
                    Swal.fire(
                        'Error inesperado',
                        'Ocurrió un error en la operación.',
                        'error'
                    )
                }
            });

        }
    })
}







// Cargar los datos a editar en el modal,
function cargarEditHisAusencia(codigoA, codigoTipo, codigoEmpleado) {
    $('#idAusencia').val(codigoA);
    $('#idEmpleado').val(codigoEmpleado);
    $("#motivoE").val(codigoTipo);
    $('#fechaFomat').val('');
    $('#fechaFomat').attr("placeholder", "Rango de Fechas");

}




/*
 *
 * editar un tipo de ausencia
 *
 */

function editarHisAusencia() {


    var rango = document.getElementById("fechaFomat").value;
    var codigo = document.getElementById("idAusencia").value;
    var motivoAusencia = document.getElementById("motivoE").value;
    var empleado = document.getElementById("idEmpleado").value; 

    var envio = {
        idAusencia: codigo,
        codEmpelado : empleado,
        tipo: motivoAusencia,
        fechaSalida: separarRangoFechas(rango)[0],
        fechaRegreso: separarRangoFechas(rango)[1]
    };

   
    $.ajax({
        url: "/Historico_Ausencias/Editar",    // Nombre del controlador/ accion del controlador
        type: "post",
        data: envio,
        success: function (response) {
            console.log(response);
            if (response.success == true) { // Si se elimino
                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Datos modificados exitosamente',
                    showConfirmButton: false,
                    timer: 1500
                })


                refrescarHistoricoAusencias();
            }
            else { // No se se elimino
                Swal.fire({
                    
                    icon: 'error',
                    title: 'Problemas en el registro',
                    text: 'Los datos no se modificaron.',
                })
            }
        },
        error: function () {
            
             Swal.fire({
                icon: 'error',
                title: 'Error inesperado',
                text: 'Ocurrió un error en la operación.',
            }) 
        }
    });
    return false; // Permitir el uso de HTML5
}








/*
 *
 * Refrescar tabla
 *
 * */
function refrescarHistoricoAusencias() {
    $.ajax({
        url: "/Historico_Ausencias/Refrescar",    // Nombre del controlador/ accion del controlador
        type: "post",
        success: function (response) {

            var lista = JSON.parse(response.resultado);

            $('#contenidoTabla tr').remove();      // Eliminar contenido de la tabla
            // Construir la nueva fila

            for (x = 0; x < lista.length; x++) {
                var info = '<tr> <td>' + lista[x].empleado.TC_Identificacion + '</td>' +
                    '<td>' + lista[x].empleado.TC_Nombre_Usuario + '</td>' +
                    '<td>' + lista[x].empleado.TC_Primer_Apellido + '</td>' +
                    '<td>' + lista[x].empleado.TC_Segundo_Apellido + '</td>' +
                    '<td>' + lista[x].tipoAusencia.TC_Tipo_Ausencia + '</td>' +
                    '<td>' + lista[x].TF_Fecha_Salida + '</td>' +
                    '<td>' + lista[x].TF_Fecha_Regreso + '</td>' +
                    ' <td><div class="d-flex justify-content-center">' +
                    '<a data-toggle="modal" data-target="#modal-editar" href= "#" onclick="cargarEditHisAusencia(' + lista[x].TN_Id_Ausencia + ', ' + lista[x].tipoAusencia.TN_Id_Tipo_Ausencia + ')"> <i class="fas fa-edit text-dark" style="font-size: 1.2em;"></i></a>' +
                    '<a onclick="eliminarHAusencia(' + lista[x].TN_Id_Ausencia + ', this)" href="#"> <i class="fas fa-trash text-dark" style="font-size: 1.2em;"></i></a>' +
                    '</div > </td> </tr>';

                $("#contenidoTabla").append(info);
            }

        },
        error: function () {
            Swal.fire({
                icon: 'error',
                title: 'Error inesperado',
                text: 'Ocurrió un error en la operación. Recargue la página',
            })
        }
    });
    return false; // Permitir el uso de HTML5
}






// Formato del calendario para fecha de salida y fecha de regreso de la ausencia
$('#fechaFomat').daterangepicker();

$('#fechaFomat').daterangepicker({
    locale: {
        format: 'YYYY/MM/DD'
    }
});







function separarRangoFechas(rango) {
    var separador = rango.indexOf('-');
    var fechaSalida = rango.substring(0, separador - 1)
    var fechaRegreso = rango.substring(separador + 2, rango.length);
    return [fechaSalida, fechaRegreso];
}


/* -----------------------------------------------------
 *               Seccion de filtros
 *------------------------------------------------------                     
*/




function formatoFiltro() {
    $('#fechaFomatFiltro').daterangepicker();
    $('#fechaFomatFiltro').daterangepicker({
        locale: {
            format: 'YYYY/MM/DD'
        }
    });

}






function filtrarAusencias() {
    filter();
    var motivoValue = document.getElementById("motivoFiltro");
    var fechaValue = document.getElementById("fechaFomatFiltro").value;

    if (motivoValue.length > 0) { // Si se filtra por motivo
        
        var valueMotivo = motivoValue.options[motivoValue.selectedIndex].text.toLowerCase();
        $("#contenidoTabla tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(valueMotivo) > -1)
        })
    }
    if (fechaValue.length > 0) {//A,B,D

    }
}




function vaciarFiltroAusencias() {
    $("#contenidoTabla tr").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf("") > -1)
    })

    $('#fechaFomatFiltro').val("");
    $("#motivoFiltro").val(0);
}

