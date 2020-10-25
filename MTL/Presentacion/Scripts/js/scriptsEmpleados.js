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

function registrarTiemposEmpleado() {
    var i
    var select = "";
    for (i = 0; i < document.tiemposF.optradio.length; i++) {
        if (document.tiemposF.optradio[i].checked) {
            select = document.tiemposF.optradio[i].value;
            break;
        }
    }

    if (select == "") {
        Swal.fire({
            type: 'warning',
            title: 'Seleccione un tiempo',
            text: message
        })
    } else {
        parametros = { "tiempo": select };
        $.ajax(
            {
                data: parametros,
                url: '/Empleado_Horarios/registrarTiemposEmpleado',
                type: 'post',
                beforeSend: function () {
                    var div = document.createElement('div');
                    div.setAttribute('class', 'd-flex align-items-center justify-content-center');

                    var divSpin = document.createElement('div');
                    divSpin.setAttribute('class', 'spinner-grow text-primary');
                    divSpin.setAttribute('style', 'width: 2em; height: 2em;');
                    divSpin.setAttribute('role', 'status');

                    var newSpan = document.createElement('strong');
                    //newSpan.setAttribute('class', 'sr-only');
                    newSpan.innerHTML = 'Registrando...';

                    div.appendChild(divSpin);
                    div.appendChild(newSpan);

                    document.getElementById('respuesta').appendChild(div);
                }, //antes de enviar
                success: function (response) {
                    document.getElementById('respuesta').innerHTML = "";
                    if (response == 1) {
                        Swal.fire({
                            type: 'success',
                            title: 'Registrado',
                            text: 'Registro realizado correctamente, recargue la página para ver los cambios'
                        })
                    } else {
                        if (response == 0) {
                            Swal.fire({
                                type: 'warning',
                                title: 'Error',
                                text: 'Ha ocurrido un error interno. Inténtelo dentro de unos minutos'
                            })
                        } else {
                            if (response == -1) {
                                //aun no se ha marcado la entrada
                                Swal.fire({
                                    type: 'warning',
                                    title: 'Error',
                                    text: 'Para realizar cualquier marca de tiempo, primero debe registrar su entrada al trabajo'
                                })
                            } else {
                                if (response == -2) {
                                    //se desea realizar cualquier registro, pero ya se marcó la salida del trabajo
                                    Swal.fire({
                                        type: 'warning',
                                        title: 'Error',
                                        text: 'Ya ha marcado la salida de su trabajo. No se pueden realizar más marcas.'
                                    })
                                } else {
                                    if (response == -3) {
                                        //se desea realizar cualquier registro, pero ya se marcó la salida del trabajo
                                        Swal.fire({
                                            type: 'warning',
                                            title: 'Error',
                                            text: 'La marca de tiempo ya existe. Seleccione otra'
                                        })
                                    } else {
                                        if (response == -4) {
                                            //se desea realizar cualquier registro, pero ya se marcó la salida del trabajo
                                            Swal.fire({
                                                type: 'warning',
                                                title: 'Error',
                                                text: 'La última marca de tiempo no se ha cerrado. Realice la marca de salida correspondiente'
                                            })
                                        } else {
                                            if (response == -5) {
                                                //se desea realizar cualquier registro, pero ya se marcó la salida del trabajo
                                                Swal.fire({
                                                    type: 'warning',
                                                    title: 'Error',
                                                    text: 'Para registrar una marca de salida, primero debe existir la marca de entrada'
                                                })
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                } //se ha enviado
            }
        );
    }
}

function insertarEmpleado () {

    var usuario = document.getElementById("usuario").value;
    var password = document.getElementById("password").value;
    var cedula = document.getElementById("cedula").value;
    var nombre = document.getElementById("nombre").value;
    var apUno = document.getElementById("apUno").value;
    var apDos = document.getElementById("apDos").value;
    var correo = document.getElementById("correo").value;
    var tipo = document.getElementById("tipo").value;
    var estado = document.getElementById("estado").value;
    var puesto = document.getElementById("puesto").value;
    var oficina = document.getElementById("oficina").value;
    var data = { usuario: usuario, password: password, cedula: cedula, nombre: nombre, apUno: apUno, apDos: apDos, correo: correo, tipo: tipo, estado: estado, puesto: puesto, oficina: oficina };
    $.ajax({
        url:'/Empleados/Insertar',
        type: 'POST',
        data: data,
        success: function (response) {
            if (response.success == true) { // Si se elimino
                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Datos registrador exitosamente.',
                    showConfirmButton: false,
                    timer: 1500
                })

                var table = $('#table').DataTable(); // Obtener la tabla

                // Construir la nueva fila
                var info = [cedula, nombre, apUno, apDos, correo, $('#estado').children('option:selected').html(), tipo, $('#puesto').children('option:selected').html(), $('#oficina').children('option:selected').html(), '<div class="d-flex justify-content-center">' +
                    '<a data-toggle="modal" data-target="#modal-editar" href= "#" > <i class="fas fa-edit text-dark" style="font-size: 1.2em;"></i></a>' +
                    '<a onclick="eliminarEmpleado(' + cedula + ')" href="#"> <i class="fas fa-trash text-dark" style="font-size: 1.2em;"></i></a>' +
                    '</div >'];

                table.row.add(info).draw(false); // Agregar la nueva fila a la taba
            } else { // No se se elimino
                Swal.fire({
                    icon: 'error',
                    title: 'Problemas en el registro',
                    text: 'Los datos no se registraron en el sistema.',
                })
            }
        },
        error: function (response) {
            Swal.fire({
                icon: 'error',
                title: 'Error inesperado',
                text: 'Ocurrió un error en la operación.',
            })
        }
    });

    return false;
}

function eliminarEmpleado(cedula) {

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
               cedula: cedula
            };
            $.ajax({
                url: "/Empleados/Eliminar",    // Nombre del controlador/ accion del controlador
                type: "POST",
                data: data,
                success: function (response) {
                    if (response.success == true) {

                        if (response.deleted == true) // Si el tipo de ausencia se elimino correctamente
                        {
                            Swal.fire(
                                'Eliminado!',
                                'El empleado se elimino correctamente.',
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
                                'El empleado seleccionado no se puede eliminar.',
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
