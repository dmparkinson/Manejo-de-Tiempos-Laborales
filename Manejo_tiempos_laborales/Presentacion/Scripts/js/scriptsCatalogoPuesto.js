
/* 
 * 
 * Eliminar un puesto
 * 
 * */
function eliminarPuesto(codigo) {

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
                url: "/Catalogo_Puestos/Eliminar",    // Nombre del controlador/ accion del controlador
                type: "POST",
                data: envio,
                success: function (response) {
                    if (response.success == true) {

                        if (response.deleted == true) // Si el puesto se elimino correctamente
                        {
                            Swal.fire(
                                'Eliminado!',
                                'El puesto se elimino correctamente.',
                                'success'
                            )


                            refrescarCatalogoPuesto();

                        }
                        else { // si no se elimino
                            Swal.fire(
                                'No eliminado',
                                'El puesto seleccionado no se puede eliminar.',
                                'warning'
                            )
                        }

                    }
                    else { // No se encontro el puesto
                        Swal.fire(
                            'No encontrado',
                            'El puesto no se encuentra registrado.',
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
 * Agregar un nuevo puesto
 *
 * */
function agregarPuesto() {


    var envio = {
        nombre: document.getElementById("nombre").value,
        salario: document.getElementById("salario").value
    };
    $.ajax({
        url: "/Catalogo_Puestos/Insertar",    // Nombre del controlador/ accion del controlador
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

                refrescarCatalogoPuesto();
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
function cargarEditPuesto(codigo) {

    $('#nombreEdit').val("");
    $('#salarioEdit').val("");

    var envio = {
        codigo: codigo
    };
    $.ajax({
        url: "/Catalogo_Puestos/Obtener",    // Nombre del controlador/ accion del controlador
        type: "post",
        data: envio,
        success: function (response) {
            var dato = JSON.parse(response.resultado);
            $('#idEdit').val(dato.TN_Id_Puesto);
            $('#nombreEdit').val(dato.TC_Nombre_Puesto);
            $('#salarioEdit').val(dato.TN_Salario_Horario);

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
 * editar un puesto
 *
 * */
function editarPuesto() {
    var envio = {
        nombre: document.getElementById("nombreEdit").value,
        salario: document.getElementById("salarioEdit").value,
        codigo: document.getElementById("idEdit").value
    };

    $.ajax({
        url: "/Catalogo_Puestos/Editar",    // Nombre del controlador/ accion del controlador
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


                refrescarCatalogoPuesto();
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
function refrescarCatalogoPuesto() {
    $.ajax({
        url: "/Catalogo_Puestos/Refrescar",    // Nombre del controlador/ accion del controlador
        type: "post",
        success: function (response) {

            var lista = JSON.parse(response.resultado);

            $('#contenidoTabla tr').remove();      // Eliminar contenido de la tabla
            // Construir la nueva fila

            for (x = 0; x < lista.length; x++) {
                var info = '<tr> <td>' + lista[x].TC_Nombre_Puesto + '</td>' +
                    '<td>' + lista[x].TN_Salario_Horario + '</td>' +
                    '<td><div class="d-flex justify-content-center">' +
                    '<a data-toggle="modal" data-target="#modal-editar" href= "#" onclick="cargarEditPuesto(' + lista[x].TN_Id_Puesto + ')"> <i class="fas fa-edit text-dark" style="font-size: 1.2em;"></i></a>' +
                    '<a onclick="eliminarPuesto(' + lista[x].TN_Id_Puesto + ')" href="#"> <i class="fas fa-trash text-dark" style="font-size: 1.2em;"></i></a>' +
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




