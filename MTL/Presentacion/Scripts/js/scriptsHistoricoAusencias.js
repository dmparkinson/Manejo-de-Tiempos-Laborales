
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
                "_ausencia": idAusencia
            };
            $.ajax({
                url: "/Historico_Ausencias/Eliminar",    // Nombre del controlador/ accion del controlador
                type: "POST",
                data: JSON.stringify(dataHorario),
                dataType: "json",
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






var btnEditHorario = null;
var tipoA = null;
var fechaSalidaA= null;
var fechaRegresoA = null;

// Cargar los datos a editar en el modal, y guardar la fila seleccionada
function cargarEditHAusencia(tipo, fecha_salida, fecha_regreso, btn) {
    $('#motivoE').val(fecha);
    $('#salidaE').val(hora);
    $('#regresoE').val(tiempo);
    btnEditHorario = btn;
    tipoA = tipo;
    fechaSalidaA = fecha_salida;
    fechaRegresoA = fecha_regreso;
}


/*
 *
 * editar un tipo de ausencia
 *
 * */
function editarHHorario() {




    $.ajax({
        url: "/Historico_Ausencias/Editar",    // Nombre del controlador/ accion del controlador
        type: "POST",
        data: envio,
        dataType: "json",
        success: function (response) {
            if (response.success == true) { // Si se elimino
                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Datos modificados exitosamente',
                    showConfirmButton: false,
                    timer: 1500
                })


                var row = $(btnEdit).closest('tr'); // Obtener la fila
                // Editar las columnas de la fila seleccionada
                row.find("td").eq(0).html(tAusencia);
                row.find("td").eq(1).html('<div class="d-flex justify-content-center">' +
                    '<a data-toggle="modal" data-target="#modal-editar" href= "#" > <i class="fas fa-edit text-dark" style="font-size: 1.2em;"></i></a>' +
                    '<a onclick="eliminarTipoAusencia(' + tAusencia + ', this)" href="#"> <i class="fas fa-trash text-dark" style="font-size: 1.2em;"></i></a>' +
                    '</div >');
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
