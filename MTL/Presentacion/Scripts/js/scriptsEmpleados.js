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
            selectId = document.tiemposF.optradio[i].id;
            break;
        }
    }

    alert("select= " + select);
    alert("select id= " + selectId);

    if (select == "") {
        Swal.fire({
            type: 'warning',
            title: 'Seleccione un tiempo',
            text: message
        })
    } else {
        parametros = { "idTiempo": selectId, "tiempo": select };
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
                if (response.success == true) { // Si se elimino
                    if (response.inserted == true) {
                        Swal.fire({
                            position: 'top-end',
                            icon: 'success',
                            title: 'Datos registrador exitosamente.',
                            showConfirmButton: false,
                            timer: 1500
                        })

                        refrescarEmpleados();

                    } else {
                        Swal.fire({
                            position: 'top-end',
                            icon: 'error',
                            title: 'Identificación o Usuario existente',
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

function eliminarEmpleado(id) {

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
                url: "/Empleados/Eliminar",    // Nombre del controlador/ accion del controlador
                type: "POST",
                data: data,
                success: function (response) {
                    if (response.success == true) {

                        if (response.deleted == true) // Si el tipo de ausencia se elimino correctamente
                        {
                            Swal.fire({
                                position: 'top-end',
                                icon: 'success',
                                title: 'Empleado Eliminado',
                                showConfirmButton: false,
                                timer: 1500
                            })


                            refrescarEmpleados();

                        }
                        else { // si no se elimino
                            Swal.fire({
                                position: 'top-end',
                                icon: 'error',
                                title: 'Imposible Eliminar, registros relacionados',
                                showConfirmButton: false,
                                timer: 1500
                            })
                        }

                    }
                    else { // No se encontro la ausencia
                        Swal.fire({
                            position: 'top-end',
                            icon: 'error',
                            title: 'Empleado Inexistente',
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

function prepararEdit(id) {

    $('#acedula').val("");
    $('#anombre').val("");
    $('#aapUno').val("");
    $('#aapDos').val("");
    $('#acorreo').val("");


    var envio = {
        codigo: id
    };
    $.ajax({
        url: "/Empleados/Obtener",    // Nombre del controlador/ accion del controlador
        type: "post",
        data: envio,
        success: function (response) {
            var dato = JSON.parse(response.resultado);
            $('#hiddenID').val(dato.TN_Id_Usuario);
            $('#acedula').val(dato.TC_Identificacion);
            $('#anombre').val(dato.TC_Nombre_Usuario);
            $('#aapUno').val(dato.TC_Primer_Apellido);
            $('#aapDos').val(dato.TC_Segundo_Apellido);
            $('#acorreo').val(dato.TC_Correo);
            $("#atipo").val(dato.TC_Tipo_Usuario);
            $("#aestado").val(dato.TB_Activo );
            $("#apuesto").val(dato.puesto.TN_Id_Puesto);
            $("#aoficina").val(dato.oficina.TN_Id_Oficina);

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

function actualizarEmpleado() {

    var id = document.getElementById("hiddenID").value;
    var cedula = document.getElementById("acedula").value;
    var nombre = document.getElementById("anombre").value;
    var apUno = document.getElementById("aapUno").value;
    var apDos = document.getElementById("aapDos").value;
    var correo = document.getElementById("acorreo").value;
    var tipo = document.getElementById("atipo").value;
    var estado = document.getElementById("aestado").value;
    var puesto = document.getElementById("apuesto").value;
    var oficina = document.getElementById("aoficina").value;
    var data = { id:id,cedula: cedula, nombre: nombre, apUno: apUno, apDos: apDos, correo: correo, tipo: tipo, estado: estado, puesto: puesto, oficina: oficina };
    $.ajax({
        url: '/Empleados/Actualizar',
        type: 'POST',
        data: data,
        success: function (response) {
            if (response.success == true) { // Si se elimino
                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Empleado Actualizado',
                    showConfirmButton: false,
                    timer: 1500
                })


                refrescarEmpleados();

            } else {
                Swal.fire({
                    position: 'top-end',
                    icon: 'error',
                    title: 'Error Actualizando',
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

function refrescarEmpleados() {
    $.ajax({
        url: '/Empleados/Refrescar',
        type: 'POST',
        success: function (response) {
            var array = JSON.parse(response);
            $('#contenidoTabla').html('');
            for (i = 0; i < array.length; i++) {
                $('#contenidoTabla').append('<tr>');
                $('#contenidoTabla').append('<td>' + array[i].TC_Identificacion + '</td>');
                $('#contenidoTabla').append('<td>' + array[i].TC_Nombre_Usuario + '</td>');
                $('#contenidoTabla').append('<td>' + array[i].TC_Primer_Apellido + '</td>');
                $('#contenidoTabla').append('<td>' + array[i].TC_Segundo_Apellido + '</td>');
                $('#contenidoTabla').append('<td>' + array[i].TC_Correo + '</td>');
                if (array[i].TB_Activo == "1") {
                    $('#contenidoTabla').append('<td>Activo</td>');
                } else {
                    $('#contenidoTabla').append('<td>Inactivo</td>');
                }
                
                $('#contenidoTabla').append('<td>' + array[i].TC_Tipo_Usuario + '</td>');
                $('#contenidoTabla').append('<td>' + array[i].TC_Nombre_Puesto + '</td>');
                $('#contenidoTabla').append('<td>' + array[i].TC_Nombre_Oficina + '</td>');
                $('#contenidoTabla').append('<td class="d-flex justify-content-center">\
                    <a class="ml-1" onclick="location.href=&quot@Url.Action("Listar_de_Admin", "Empleado_Horarios") &quot" title="Tiempo de horarios" href="#">\
                        <i class="fas fa-business-time text-dark" style="font-size: 1.2em;"></i></a>\
                    <a class="ml-1" onclick="ausenciasEmpleado('+ array[i].TN_Id_Usuario +')" title="Tiempo de ausencias" href="#"><i class="fas fa-calendar-times text-dark" style="font-size: 1.2em;"></i></a>\
                        <a class="ml-1" data-toggle="modal" data-target="#modal-editar" onclick="prepararEdit('+ array[i].TN_Id_Usuario +')" href="#" title="Editar empleado"> <i class="fas fa-edit text-info" style="font-size: 1.2em;"></i></a>\
                        <a class="ml-1" onclick="eliminarEmpleado('+ array[i].TN_Id_Usuario +')" title="Eliminar empleado" href="#"> <i class="fas fa-trash text-red" style="font-size: 1.2em;"></i></a>\
                    </td>');
                $('#contenidoTabla').append('</tr>');
            }
        },
        error: function (response) {
            alert('Error refrescando empleados')
        }
    });

    return false;
}


