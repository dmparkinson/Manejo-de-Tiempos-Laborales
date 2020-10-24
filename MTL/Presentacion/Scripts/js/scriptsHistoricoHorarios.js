

/* 
 * 
 * Eliminar el tipo de ausencia 
 * 
 * */
function eliminarHHorario(idHorario, btn) {

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
                "horario": idHorario
            };
            $.ajax({
                url: "/Historico_Horarios/Eliminar",    // Nombre del controlador/ accion del controlador
                type: "POST",
                data: JSON.stringify(dataHorario),
                dataType: "json",
                success: function (response) {
                    if (response.success == true) {

                        if (response.deleted == true) // Si el tiempo de horario se elimino correctamente
                        {
                            Swal.fire(
                                'Eliminado!',
                                'El tiempo de horario se elimino correctamente.',
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
                                'El tiempo de horario seleccionado no se puede eliminar.',
                                'warning'
                            )
                        }
                    }
                    else { // No se encontro el tiempo de horario
                        Swal.fire(
                            'No encontrado',
                            'El el tiempo de horario no se encuentra registrado.',
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
var fechaAnterior = null;
var horaAnterior = null;
var horarioAnterior = null;


// Cargar los datos a editar en el modal, y guardar la fila seleccionada
function cargarEditHHorario(fecha,hora,horario, btn) {
    $('#fechaEdit').val(fecha);
    $('#horaEdit').val(hora);
    $('#tiempoEdit').val(tiempo);
    btnEditHorario = btn;
    fechaAnterior = fecha;
    horaAnterior = hora;
    horarioAnterior = horario;
}


/*
 *
 * editar un tipo de ausencia
 *
 * */
function editarHHorario() {



    var envio = {
        anterior: motivoAnterior,
        nuevo: $('#motivoEdit').val()
    }

    $.ajax({
        url: "/Historico/Horarios",    // Nombre del controlador/ accion del controlador
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


