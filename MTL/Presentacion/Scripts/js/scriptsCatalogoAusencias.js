

/* 
 * 
 * Eliminar el tipo de ausencia 
 * 
 * */
function eliminarTipoAusencia(idAusencia, btn) {
    
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

            var dataTAusencia = {
                "_tipoAusencia": idAusencia
            };  
            $.ajax({
                url: "/Catalogo_Ausencias/Eliminar",    // Nombre del controlador/ accion del controlador
                type: "POST",
                data: dataTAusencia,
                success: function (response) {
                    if (response.success == true) {

                        if (response.deleted == true) // Si el tipo de ausencia se elimino correctamente
                        {
                            Swal.fire(
                                'Eliminado!',
                                'El tipo de ausencia se elimino correctamente.',
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
                                'El tipo de ausencia seleccionado no se puede eliminar.',
                                'warning'
                            )
                        }

                    }
                    else { // No se encontro la ausencia
                        Swal.fire(
                            'No encontrado',
                            'El tipo de ausencia no se encuentra registrado.',
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






/*
 *
 * Agregar un nuevo tipo de ausencia
 *
 * */
function agregarTipoAusencia() {

    var tAusencia = document.getElementById("motivoAusencia").value;

    var tipoAusencia = {
        "nombre": tAusencia
    };
    $.ajax({
        url: "/Catalogo_Ausencias/Insertar",    // Nombre del controlador/ accion del controlador
        type: "post",
        data: tipoAusencia,
        success: function (response) {
            if (response.success == true) { // Si se elimino
                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Datos registrador exitosamente.',
                    showConfirmButton: false,
                    timer: 1500
                })
                alert(response.dato);

                var table = $('#table').DataTable(); // Obtener la tabla

                // Construir la nueva fila
                var info = [tAusencia, '<div class="d-flex justify-content-center">' +
                    '<a data-toggle="modal" data-target="#modal-editar" href= "#" > <i class="fas fa-edit text-dark" style="font-size: 1.2em;"></i></a>' +
                    '<a onclick="eliminarTipoAusencia(' + tAusencia + ', this)" href="#"> <i class="fas fa-trash text-dark" style="font-size: 1.2em;"></i></a>' +
                    '</div >'];

                table.row.add(info).draw(false); // Agregar la nueva fila a la taba
            }
            else { // No se se elimino
                Swal.fire({
                    icon: 'error',
                    title: 'Problemas en el registro',
                    text: 'Los datos no se registraron en el sistema.',
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




var btnEdit = null;
var motivoAnterior = null;


// Cargar los datos a editar en el modal, y guardar la fila seleccionada
function cargarEditCAusencia(idTAusencia, btn) {
    $('#motivoEdit').val(idTAusencia);
    btnEdit = btn;
    motivoAnterior = idTAusencia;
}


/*
 *
 * editar un tipo de ausencia
 *
 * */
function editarTipoAusencia() {

    var tAusencia = document.getElementById("motivoEdit").value; // La nueva ausencia


    var envio = {
        anterior: motivoAnterior,
        nuevo: $('#motivoEdit').val()
    }

    $.ajax({
        url: "/Catalogo_Ausencias/Editar",    // Nombre del controlador/ accion del controlador
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








