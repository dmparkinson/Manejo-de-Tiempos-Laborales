
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

            var dataHorario = {
                ausencia: idAusencia
            };
            $.ajax({
                url: "/Historico_Ausencias/Eliminar",    // Nombre del controlador/ accion del controlador
                type: "post",
                data: dataHorario,
                success: function (response) {
                    
                    if (response.success == true) {

                        if (response.deleted == true) // Si el tiempo de ausencia se elimino correctamente
                        {
                            Swal.fire(
                                'Eliminado!',
                                'El tiempo de ausencia se elimino correctamente.',
                                'success'
                            )

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
function cargarEditHisAusencia(idAusencia) {
   /* var envio = {
        codigo: idAusencia
    };
    $.ajax({
        url: "/Historico_Ausencias/Obtener",    // Nombre del controlador/ accion del controlador
        type: "post",
        data: envio,
        success: function (response) {
            var dato = JSON.parse(response.resultado);
            $('#idEdit').val(dato.TN_Id_Circuito);
            $('#nombreEdit').val(dato.TC_Desc_Circuito);

        },
        error: function () {
            Swal.fire({
                icon: 'error',
                title: 'Error inesperado',
                text: 'Ocurrió un error en la operación.',
            })
        }
    });
    return false; // Permitir el uso de HTML5*/

}









// Establece que la fecha limite de regreso es la de salida
function fechaRegresoValidacion() {
    $('#regresoE').attr('min', document.getElementById("salidaE").value.split("/").reverse().join("-"));
    
}



/*
 *
 * editar un tipo de ausencia
 *
 */

function editarHisAusencia() {

    var envio = {
        // id Datos a cambiar
        idAusencia : document.getElementById("idAusencia").value,

        // Nuevos datos
        idUsuario: document.getElementById("idUsuario").value,
        tipoNuevo: document.getElementById("motivoE").value,
        fechaSalidaNuevo: document.getElementById("salidaE").value ,
        fechaRegresoNuevo: document.getElementById("regresoE").value
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



                dateSalida = new Date(document.getElementById("salidaE").value);
                dateSalidaFinal = (dateSalida.getDate()+1) + "/" + (dateSalida.getMonth()+1) + "/" + dateSalida.getFullYear();

                dateRegreso = new Date(document.getElementById("regresoE").value);
                dateRegresoFinal = (dateRegreso.getDate()+1) + "/" + (dateRegreso.getMonth()+1) + "/" + dateRegreso.getFullYear();
                console.log(dateSalidaFinal + "," + dateRegresoFinal);

                var row = $(btnFilaAusencia).closest('tr'); // Obtener la fila
                // Editar las columnas de la fila seleccionada
                row.find("td").eq(4).html(document.getElementById("motivoE").value);
                row.find("td").eq(5).html(dateSalidaFinal);
                row.find("td").eq(6).html(dateRegresoFinal);

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
                var info = '<tr> <td>' + lista[x].TC_Desc_Circuito + '</td>' +
                    ' <td><div class="d-flex justify-content-center">' +
                    '<a data-toggle="modal" data-target="#modal-editar" href= "#" onclick="cargarEditCircuito(' + lista[x].TN_Id_Circuito + ', this)"> <i class="fas fa-edit text-dark" style="font-size: 1.2em;"></i></a>' +
                    '<a onclick="eliminarCircuito(' + lista[x].TN_Id_Circuito + ', this)" href="#"> <i class="fas fa-trash text-dark" style="font-size: 1.2em;"></i></a>' +
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