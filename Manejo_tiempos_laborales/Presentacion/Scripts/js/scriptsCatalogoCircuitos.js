
/* 
 * 
 * Eliminar el circuito
 * 
 * */
function eliminarCircuito(codigo) {

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
                url: "/Catalogo_Circuitos/Eliminar",    // Nombre del controlador/ accion del controlador
                type: "POST",
                data: envio,
                success: function (response) {
                    if (response.success == true) {

                        if (response.deleted == true) // Si el circuito se elimino correctamente
                        {
                            Swal.fire(
                                'Eliminado!',
                                'El circuito se elimino correctamente.',
                                'success'
                            )


                            refrescarCatalogoCircuito();

                        }
                        else { // si no se elimino
                            Swal.fire(
                                'No eliminado',
                                'El circuito seleccionado no se puede eliminar.',
                                'warning'
                            )
                        }

                    }
                    else { // No se encontro el circuito
                        Swal.fire(
                            'No encontrado',
                            'El circuito no se encuentra registrado.',
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
 * Agregar un nuevo circuito
 *
 * */
function agregarCircuito() {


    var envio = {
        nombre: document.getElementById("nombre").value
    };
    $.ajax({
        url: "/Catalogo_Circuitos/Insertar",    // Nombre del controlador/ accion del controlador
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

                refrescarCatalogoCircuito();
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
function cargarEditCircuito(codigo) {
    $('#nombreEdit').val("");
    var envio = {
        codigo: codigo
    };
    $.ajax({
        url: "/Catalogo_Circuitos/Obtener",    // Nombre del controlador/ accion del controlador
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
    return false; // Permitir el uso de HTML5
}





/*
 *
 * editar un circuito
 *
 * */
function editarCircuito() {
    var envio = {
        nombre: document.getElementById("nombreEdit").value,
        codigo: document.getElementById("idEdit").value
    };

    $.ajax({
        url: "/Catalogo_Circuitos/Editar",    // Nombre del controlador/ accion del controlador
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


                refrescarCatalogoCircuito();
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
function refrescarCatalogoCircuito() {
    $.ajax({
        url: "/Catalogo_Circuitos/Refrescar",    // Nombre del controlador/ accion del controlador
        type: "post",
        success: function (response) {

            var lista = JSON.parse(response.resultado);

            $('#contenidoTabla tr').remove();      // Eliminar contenido de la tabla
            // Construir la nueva fila

            for (x = 0; x < lista.length; x++) {
                var info = '<tr> <td>' + lista[x].TN_Id_Circuito+ '</td>' +
                    '<td>' + lista[x].TC_Desc_Circuito + '</td>' +
                    ' <td><div class="d-flex justify-content-center">' +
                    '<a data-toggle="modal" data-target="#modal-editar" href= "#" onclick="cargarEditCircuito(' + lista[x].TN_Id_Circuito + ', this)"> <i class="fas fa-edit text-dark" style="font-size: 1.2em;"></i></a>' +
                    '<a onclick="eliminarCircuito(' + lista[x].TN_Id_Circuito + ', this)" href="#"> <i class="fas fa-trash text-dark" style="font-size: 1.2em;"></i></a>' +
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


