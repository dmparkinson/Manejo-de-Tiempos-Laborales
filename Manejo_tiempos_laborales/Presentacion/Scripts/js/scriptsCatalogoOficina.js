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


function insertarOficina() {
    var codigo = document.getElementById("Ocodigo").value;
    var nombre = document.getElementById("Onombre").value;
    var estado = document.getElementById("Oestado").value;
    var circuito = document.getElementById("Ocircuito").value;
    var fechaVigencia = document.getElementById("OfechaVigencia").value;
    var data = { codigo: codigo, nombre: nombre, circuito: circuito, activo: estado, fechai: separarRangoFechas(fechaVigencia)[0], fechaf: separarRangoFechas(fechaVigencia)[1] };
    $.ajax({
        url: '/Catalogo_Oficinas/Insertar',
        type: 'POST',
        data: data,
        success: function (response) {
            if (response.success == true) { // Si se elimino
                if (response.success == true) { // Si se elimino
                    if (response.inserted == true) {
                        Swal.fire({
                            position: 'top-end',
                            icon: 'success',
                            title: 'Oficina registrada exitosamente.',
                            showConfirmButton: false,
                            timer: 1500
                        })

                        refrescarOficinas();

                    } else {
                        Swal.fire({
                            position: 'top-end',
                            icon: 'error',
                            title: 'Codigo o Nombre existente',
                            showConfirmButton: false,
                            timer: 1500
                        })
                    }

                } else { // No se se elimino
                    Swal.fire({
                        position: 'top-end',
                        icon: 'error',
                        title: 'Error en la Base de Datos',
                        showConfirmButton: false,
                        timer: 1500
                    })
                }
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

function prepararEditOficina(id) {
    $('#hiddenIDOf').val(id);
    var data = { id: id }
    $.ajax({
        url: '/Catalogo_Oficinas/GetOficina',
        type: 'POST',
        data: data,
        success: function (response) {
            var o = JSON.parse(response);
            $('#acodigo').val(o.TC_Codigo);
            $('#anombre').val(o.TC_Nombre_Oficina);
            $('#aestado').val(o.TB_Activa);
            $('#acircuito').val(o.TN_Id_Circuito);
            $('#afechaVigencia').val(o.TF_Inicio_Vigencia + ' - ' + o.TF_Fin_Vigencia);
        },
        error: function (response) {
            alert('Error recuperando datos de Oficina')
        }
    });
    return false;
}

function actualizarOficina() {
    var id = document.getElementById("hiddenIDOf").value;
    var codigo = document.getElementById("acodigo").value;
    var nombre = document.getElementById("anombre").value;
    var estado = document.getElementById("aestado").value;
    var circuito = document.getElementById("acircuito").value;
    var fechaVigencia = document.getElementById("afechaVigencia").value;
    var data = { id:id,codigo: codigo, nombre: nombre, circuito: circuito, activo: estado, fechai: separarRangoFechas(fechaVigencia)[0], fechaf: separarRangoFechas(fechaVigencia)[1] };
    $.ajax({
        url: '/Catalogo_Oficinas/Actualizar',
        type: 'POST',
        data: data,
        success: function (response) {
            if (response.success == true) { // Si se elimino
                if (response.success == true) { // Si se elimino
                    if (response.updated == true) {
                        Swal.fire({
                            position: 'top-end',
                            icon: 'success',
                            title: 'Oficina actualizada exitosamente.',
                            showConfirmButton: false,
                            timer: 1500
                        })

                        refrescarOficinas();

                    } else {
                        Swal.fire({
                            position: 'top-end',
                            icon: 'error',
                            title: 'Codigo o Nombre existente',
                            showConfirmButton: false,
                            timer: 1500
                        })
                    }

                } else { // No se se elimino
                    Swal.fire({
                        position: 'top-end',
                        icon: 'error',
                        title: 'Error en la Base de Datos',
                        showConfirmButton: false,
                        timer: 1500
                    })
                }
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

function eliminarOficinas(id) {

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
                url: "/Catalogo_Oficinas/Eliminar",    // Nombre del controlador/ accion del controlador
                type: "POST",
                data: data,
                success: function (response) {
                    if (response.success == true) {

                        if (response.deleted == true) // Si el tipo de ausencia se elimino correctamente
                        {
                            Swal.fire({
                                position: 'top-end',
                                icon: 'success',
                                title: 'Oficina Eliminada',
                                showConfirmButton: false,
                                timer: 1500
                            })


                            refrescarOficinas();

                        }
                        else { // si no se elimino                          
                            Swal.fire({
                                position: 'top-end',
                                icon: 'error',
                                title: 'Imposible Eliminar, registro relacionados',
                                showConfirmButton: false,
                                timer: 1500
                            })
                        }

                    }
                    else { // No se encontro la ausencia
                        Swal.fire({
                            position: 'top-end',
                            icon: 'error',
                            title: 'Oficina inexistente',
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


function refrescarOficinas() {
    $.ajax({
        url: '/Catalogo_Oficinas/Refrescar',
        type: 'POST',
        success: function (response) {
            var array = JSON.parse(response);
            var contenido = document.getElementById("contenidoTabla");
            contenido.innerHTML = "";
            var html = "";
            for (i = 0; i < array.length; i++) {
                html += '<tr>';
                html += '<td>' + array[i].TC_Codigo + '</td>';
                html += '<td>' + array[i].TC_Nombre_Oficina + '</td>';
                html +='<td>' + array[i].TC_Desc_Circuito + '</td>';
                if (array[i].TB_Activa == 1) {
                    html += '<td>Activa</td>';
                } else {
                    html += '<td>Inactiva</td>';
                }
                html += '<td>' + array[i].TF_Inicio_Vigencia + '</td>';
                html += '<td>' + array[i].TF_Fin_Vigencia + '</td>';
                html += '<td class="d-flex justify-content-center">\
                <a data-toggle="modal" data-target="#modal-editar" onclick="prepararEditOficina('+ array[i].TN_Id_Oficina +')" href="#"> <i class="fas fa-edit text-dark" style="font-size: 1.2em;"></i></a>\
                    <a onclick="eliminarOficinas('+ array[i].TN_Id_Oficina +')" href="#"> <i class="fas fa-trash text-dark" style="font-size: 1.2em;"></i></a>\
                </td>';
                html += '</tr>';
            }
            contenido.innerHTML = html;
        },
        error: function (response) {
            alert('Error refrescando ausencias')
        }
    });

    return false;
}




