




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
                "tipoAusencia": idAusencia
            };  
            $.ajax({
                url: "Eliminar",    // Nombre del controlador/ accion del controlador
                type: "POST",
                data: JSON.stringify(dataTAusencia),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    if (response.success == true) {

                        if (response.deleted == true) // Si el tipo de ausencia se elimino correctamente
                        {
                            Swal.fire(
                                'Eliminado!',
                                'El tipo de ausencia se elimino correctamente.',
                                'success'
                            )

                            
                            deleteRow(btn); // Eliminacion de la fila en la tabla

                            reloadDatatable();  // recargar datatable

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
                        'Error',
                        'Ocurrió un error en la operación.',
                        'error'
                    )
                }
            });

        } 
    })
}




function agregarTipoAusencia() {

    Swal.fire({
        position: 'top-end',
        icon: 'success',
        title: 'Your work has been saved',
        showConfirmButton: false,
        timer: 1500
    })



    var table = $('#table').DataTable();

    var info = ["Prueba", '<div class="d-flex justify-content-center">' +
                                            '<a data-toggle="modal" data-target="#modal-editar" href= "#" > <i class="fas fa-edit text-dark" style="font-size: 1.2em;"></i></a>'+
                                            '<a onclick="eliminarTipoAusencia("HOLA", this)" href="#"> <i class="fas fa-trash text-dark" style="font-size: 1.2em;"></i></a>'+
                                            '</div >'];
    
        table.row.add(info).draw(false);


    return false;
}











//Actualizacion del datatable

function reloadDatatable() {
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



// Eliminacion de la fila en la tabla
function deleteRow(btn) {

    var row = $(btn).closest('tr'); // Eliminar una fila de la tabla
    row.fadeOut(500, function () { // Tiempo de desvanecimiento, sin esto no elimina en ajax
        row.remove();
    });
}