

/* 
 * 
 * Eliminar el tipo de ausencia 
 * 
 * */
function eliminarTipoAusencia(idAusencia) {
    
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
                codigo : idAusencia
            };  
            $.ajax({
                url: "/Catalogo_Ausencias/Eliminar",    // Nombre del controlador/ accion del controlador
                type: "POST",
                data: envio,
                success: function (response) {
                    if (response.success == true) {

                        if (response.deleted == true) // Si el tipo de ausencia se elimino correctamente
                        {
                            Swal.fire(
                                'Eliminado!',
                                'El tipo de ausencia se elimino correctamente.',
                                'success'
                            )

                            
                            refrescarCatalogoAusencia();

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


    var envio = {
        nombre: document.getElementById("motivoAusencia").value
    };
    $.ajax({
        url: "/Catalogo_Ausencias/Insertar",    // Nombre del controlador/ accion del controlador
        type: "post",
        data: envio,
        success: function (response) {
            if (response.success == true) { // Si se elimino
                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Datos registrador exitosamente.',
                    showConfirmButton: false,
                    timer: 1500
                })

                refrescarCatalogoAusencia();
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







// Cargar los datos a editar en el modal
function cargarEditCAusencia(idTAusencia) {
    $('#motivoEdit').val("");
    var envio = {
        codigo : idTAusencia
    };
    $.ajax({
        url: "/Catalogo_Ausencias/Obtener",    // Nombre del controlador/ accion del controlador
        type: "post",
        data: envio,
        success: function (response) {
            var dato = JSON.parse(response.resultado);
            $('#idEdit').val(dato.TN_Id_Tipo_Ausencia);
            $('#motivoEdit').val(dato.TC_Tipo_Ausencia);
            
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
 * editar un tipo de ausencia
 *
 * */
function editarTipoAusencia() {
    var envio = {
        nombre: document.getElementById("motivoEdit").value,
        codigo: document.getElementById("idEdit").value
    };

    $.ajax({
        url: "/Catalogo_Ausencias/Editar",    // Nombre del controlador/ accion del controlador
        type: "POST",
        data: envio,
        success: function (response) {
            if (response.success == true) { // Si se elimino
                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Datos modificados exitosamente',
                    showConfirmButton: false,
                    timer: 1500
                })

     
                refrescarCatalogoAusencia();
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
function refrescarCatalogoAusencia() {
    $.ajax({
        url: "/Catalogo_Ausencias/Refrescar",    // Nombre del controlador/ accion del controlador
        type: "post",
        success: function (response) {

            var lista = JSON.parse(response.resultado);

            $('#contenidoTabla tr').remove();      // Eliminar contenido de la tabla
            // Construir la nueva fila

            for (x = 0; x < lista.length; x++) {
                var info = '<tr> <td>' + lista[x].TN_Id_Tipo_Ausencia + '</td>' +
                    '<td>' + lista[x].TC_Tipo_Ausencia + '</td>' +
                    '<td><div class="d-flex justify-content-center">' +
                    '<a data-toggle="modal" data-target="#modal-editar" href= "#" onclick="cargarEditCAusencia(' + lista[x].TN_Id_Tipo_Ausencia + ', this)"> <i class="fas fa-edit text-dark" style="font-size: 1.2em;"></i></a>' +
                    '<a onclick="eliminarTipoAusencia(' + lista[x].TN_Id_Tipo_Ausencia + ', this)" href="#"> <i class="fas fa-trash text-dark" style="font-size: 1.2em;"></i></a>' +
                    '</div > </td> </tr>';

                document.getElementById("contenidoTabla").innerHTML += info;
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



