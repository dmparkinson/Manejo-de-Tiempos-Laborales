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
