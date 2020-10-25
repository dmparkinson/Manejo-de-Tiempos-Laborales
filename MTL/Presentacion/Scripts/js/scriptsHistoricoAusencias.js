
/* 
 * 
 * Eliminar el tipo de ausencia 
 * 
 * */
function eliminarHAusencia(idAusencia, btn) {
    
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

                            
                            var row = $(btn).closest('tr'); // Obtener la fila selecionada
                            row.fadeOut(500, function () {  // Tiempo de desvanecimiento, sin esto no elimina en ajax
                                row.remove();              // Eliminacion de la fila en la tabla
                            });


                            // Recargar la datatable
                            $('#table').dataTable().fnDestroy();  // Eliminacion del datatable
                            $('#table').DataTable({                 // Creacion del datatable
                                "searching": false,
                                "paging": true,
                                "info": false,
                                "lengthChange": false,
                                "responsive": true,
                                "autoWidth": false,
                            });

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






var btnFilaAusencia = null;

// Cargar los datos a editar en el modal, y guardar la fila seleccionada
function cargarEditHisAusencia(tipo, fecha_salida, fecha_regreso, idAusencia, idUsuario, btn) {

    document.getElementById("salidaE").value = fecha_salida.split("/").reverse().join("-");    // Formato de fechea salida;
    document.getElementById("regresoE").value = fecha_regreso.split("/").reverse().join("-");    // Formato de fecha regreso;
    $('#motivoE option[value=' + tipo + ']').attr('selected', 'selected');  // Mostrar esa opcion como seleccionada
    $('#regresoE').attr('min', document.getElementById("salidaE").value.split("/").reverse().join("-"));            // Evitar ingresar una fecha de regreso que este antes de la de salida

    document.getElementById("idUsuario").value = idUsuario;
    document.getElementById("idAusencia").value = idAusencia;

    btnFilaAusencia = btn;
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


