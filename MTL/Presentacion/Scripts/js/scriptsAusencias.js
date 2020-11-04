
function eliminar(titleText, message) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
        title: titleText,
        text: message,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si',
        cancelButtonText: 'No',
        reverseButtons: true
    }).then((result) => {
        if (result.value) {
            swalWithBootstrapButtons.fire(
                'Eliminados',
                'Los datos se eliminaron correctamente',
                'success'
            )
        } else if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire(
                'Cancelado',
                'Ocurrió un error en la eliminación de los datos',
                'error'
            )
        }
    })
}

function ausenciasEmpleado(idEmpleado) {
    var data = { idEmpleado: idEmpleado} 
    $.ajax({
        url: '/Empleado_Ausencias/EmpleadoAusencia',
        type: 'POST',
        data: data,
        success: function (response) {
            window.location.href = response.url; 
        },
        error: function () {
            Swal.fire({
                position: 'top-end',
                icon: 'error',
                title: 'Error de conexión',
                showConfirmButton: false,
                timer: 1500
            })
        }
    });
    return false;
}


function InsertarAusencia(){
    var rango = document.getElementById("rangoAusenciasIns").value;
    var motivo = document.getElementById("motivoIns").value;
    var data = { fechai: separarRangoFechas(rango)[0], fechaf: separarRangoFechas(rango)[1], motivo: motivo }
    $.ajax({
        url: '/Empleado_Ausencias/InsertarAusencia',
        type: 'POST',
        data: data,
        success: function (response) {
            if (response.success == true) { // Si se elimino
                if (response.inserted == true) {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: 'Ausencia registrada',
                        showConfirmButton: false,
                        timer: 1500
                    })

                    refrescarAusencias();

                } else {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'error',
                        title: 'Ausencia existente',
                        showConfirmButton: false,
                        timer: 1500
                    })
                }
                
            } else {
                Swal.fire({
                    position: 'top-end',
                    icon: 'error',
                    title: 'Error en la conexión',
                    showConfirmButton: false,
                    timer: 1500
                })
            }
        },
        error: function (response) {
            Swal.fire({
                position: 'top-end',
                icon: 'error',
                title: 'Error en la conexión',
                showConfirmButton: false,
                timer: 1500
            })
        }
    });

    return false;
}

function eliminarAusencia(id) {

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

            var data = {
                id: id
            };
            $.ajax({
                url: "/Empleado_Ausencias/EliminarAusencia",    // Nombre del controlador/ accion del controlador
                type: "POST",
                data: data,
                success: function (response) {
                    if (response.success == true) {

                        if (response.deleted == true) // Si el tipo de ausencia se elimino correctamente
                        {
                            Swal.fire({
                                position: 'top-end',
                                icon: 'success',
                                title: 'Ausencia Eliminada',
                                showConfirmButton: false,
                                timer: 1500
                            })


                            refrescarAusencias();

                        }
                        else { // si no se elimino                          
                            Swal.fire({
                                position: 'top-end',
                                icon: 'error',
                                title: 'Ausencia Inexistente',
                                showConfirmButton: false,
                                timer: 1500
                            })
                        }

                    }
                    else { // No se encontro la ausencia
                        Swal.fire({
                            position: 'top-end',
                            icon: 'error',
                            title: 'Error en la conexión',
                            showConfirmButton: false,
                            timer: 1500
                        })
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
    return false;
}

function prepararEditAusencia(id) {
    $('#hiddenAusID').val(id);
}

function ActualizarAusencia() {
    var rango = document.getElementById("arangoAusencias").value;
    var motivo = document.getElementById("amotivo").value;
    var data = { idAusencia: $('#hiddenAusID').val(), tipo: motivo ,fechai: separarRangoFechas(rango)[0], fechaf: separarRangoFechas(rango)[1] }
    $.ajax({
        url: '/Empleado_Ausencias/EditarAusencia',
        type: 'POST',
        data: data,
        success: function (response) {
            if (response.success == true) { // Si se elimino
                if (response.updated == true) {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: 'Ausencia actualizada',
                        showConfirmButton: false,
                        timer: 1500
                    })

                    refrescarAusencias();

                } else {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'error',
                        title: 'Ausencia existente',
                        showConfirmButton: false,
                        timer: 1500
                    })
                }

            } else {
                Swal.fire({
                    position: 'top-end',
                    icon: 'error',
                    title: 'Error en la conexión',
                    showConfirmButton: false,
                    timer: 1500
                })
            }
        },
        error: function (response) {
            Swal.fire({
                position: 'top-end',
                icon: 'error',
                title: 'Error en la conexión',
                showConfirmButton: false,
                timer: 1500
            })
        }
    });
    return false;
}

function separarRangoFechas(rango) {
    var separador = rango.indexOf('-');
    var fechaI = rango.substring(0, separador - 1)
    var fechaF = rango.substring(separador + 2, rango.length);
    return [fechaI, fechaF];
}

function refrescarAusencias() {
    $.ajax({
        url: '/Empleado_Ausencias/refrescarAusencias',
        type: 'POST',
        success: function (response) {
            var array = JSON.parse(response);
            $('#contenidoTabla').html('');
            for (i = 0; i < array.length; i++) {
                $('#contenidoTabla').append('<tr>');
                $('#contenidoTabla').append('<td>' + array[i].TC_Tipo_Ausencia + '</td>');
                $('#contenidoTabla').append('<td>' + array[i].TF_Fecha_Salida + '</td>');
                $('#contenidoTabla').append('<td>' + array[i].TF_Fecha_Regreso + '</td>');
                $('#contenidoTabla').append('<td class="d-flex justify-content-center">\
                <a data-toggle="modal" data-target="#modal-editar" onclick="prepararEditAusencia('+ array[i].TN_Id_Ausencia+')" href="#"> <i class="fas fa-edit text-dark" style="font-size: 1.2em;"></i></a>\
                    <a onclick="eliminarAusencia('+array[i].TN_Id_Ausencia+')" href="#"> <i class="fas fa-trash text-dark" style="font-size: 1.2em;"></i></a>\
                </td>');
                $('#contenidoTabla').append('</tr>');
            }
            //$('#rangoAusenciasIns').val('');
            //$('#rangoAusenciasIns').attr("placeholder", "Rango de Fechas");
        },
        error: function (response) {
            alert('Error refrescando ausencias')
        }
    });

    return false;
}

