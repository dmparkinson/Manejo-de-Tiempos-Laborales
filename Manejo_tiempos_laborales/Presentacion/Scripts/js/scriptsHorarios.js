
function agregarTiempoLaboral() {

    parametros = { "horario": document.getElementById('horario').value };

    $.ajax(
        {
            data: parametros,
            url: '/Catalogo_Horarios/Agregar',
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
                newSpan.innerHTML = 'Registrando....';

                div.appendChild(divSpin);
                div.appendChild(newSpan);
                document.getElementById('respuesta').innerHTML = '';
                document.getElementById('respuesta').appendChild(div);
            }, //antes de enviar

            success: function (response) {
                document.getElementById('respuesta').innerHTML = '';

                if (response == 1) {
                    Swal.fire({
                        type: 'success',
                        title: 'Registrado',
                        text: 'Registro realizado correctamente.'
                    })

                    //hay que actualizar la tabla
                    refreshTableCatalogoHorarios()
                } else {
                    Swal.fire({
                        type: 'warning',
                        title: 'Error',
                        text: 'Ha ocurrido un error al registrar. Inténtelo de nuevo más tarde.'
                    })
                }
            }
        }
    );

}

function editMyModal(button) {
    document.getElementById('viejo').value = "";
    document.getElementById('nuevo').value = "";
    document.getElementById('viejo').value = button.id;
}

function actualizarHorario() {
    var viejo = document.getElementById('viejo').value;
    var nuevo = document.getElementById('nuevo').value;

    parametros = { "viejo": viejo, "nuevo": nuevo };
    $.ajax(
        {
            data: parametros,
            url: '/Catalogo_Horarios/Actualizar',
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
                newSpan.innerHTML = 'Actualizando....';

                div.appendChild(divSpin);
                div.appendChild(newSpan);
                document.getElementById('respuestaModalEdit').innerHTML = '';
                document.getElementById('respuestaModalEdit').appendChild(div);
            }, //antes de enviar

            success: function (response) {
                document.getElementById('respuestaModalEdit').innerHTML = '';
                if (response == 1) {
                    Swal.fire({
                        type: 'success',
                        title: 'Registrado',
                        text: 'Registro actualizado correctamente.'
                    })

                    //hay que actualizar la tabla
                    refreshTableCatalogoHorarios()
                } else {
                    Swal.fire({
                        type: 'warning',
                        title: 'Error',
                        text: 'Ha ocurrido un error al actualizar. Inténtelo de nuevo más tarde.'
                    })
                }
            }
        }
    );
}

function alertAdvertenciaEliminarTiempos(button) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
        title: '¿Seguro?',
        text: "¿Desea eliminar los datos permanentemente?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si',
        cancelButtonText: 'No',
        reverseButtons: true
    }).then((result) => {
        if (result.value) {
            //acá debo llamar al método para borrar los datos

            parametros = { "horario": button.id };
            $.ajax(
                {
                    data: parametros,
                    url: '/Catalogo_Horarios/Eliminar',
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
                        newSpan.innerHTML = 'Eliminando los datos. Espere por favor';

                        div.appendChild(divSpin);
                        div.appendChild(newSpan);

                        document.getElementById('respuesta').appendChild(div);
                    }, //antes de enviar

                    success: function (response) {
                        document.getElementById('respuesta').innerHTML = "";
                        if (response == 1) {
                            swalWithBootstrapButtons.fire(
                                'Eliminados',
                                'Los datos se eliminaron correctamente.',
                                'success'
                            )


                            //actualizar la tabla
                            refreshTableCatalogoHorarios()
                        } else {
                            if (response == 0) {
                                swalWithBootstrapButtons.fire(
                                    'Cancelado',
                                    'El registro que desea eliminar no existe',
                                    'error'
                                )
                            } else {
                                swalWithBootstrapButtons.fire(
                                    'Cancelado',
                                    'Ocurrió un error en la eliminación de los datos. Inténtelo dentro de unos minutos',
                                    'error'
                                )
                            }
                        }
                    } //se ha enviado
                }
            );

            //acá debo llamar al método para borrar los datos
        } else if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire(
                'Cancelado',
                'Ocurrió un error en la eliminación de los datos. Inténtelo dentro de unos minutos',
                'error'
            )
        }
    })
}



function refreshTableCatalogoHorarios(){
    $.ajax({

        url: '/Catalogo_Horarios/RefrescarTablaCatalogo',
        type: 'POST',
        success: function (response) {
            var array = JSON.parse(response);
            document.getElementById('contenidoTabla').innerHTML = '';
            for (i = 0; i < array.length; i++) {

                var tr = document.createElement('tr')

                var td0 = document.createElement('td');
                td0.innerHTML = array[i].TN_Id_Horario;

                var td1 = document.createElement('td');
                td1.innerHTML = array[i].TC_Horario

                var td2 = document.createElement('td');
                td2.setAttribute('class', 'd-flex justify-content-center');

                var div1 = document.createElement('div');
                div1.setAttribute('class', 'row');

                var div2 = document.createElement('div');
                div2.setAttribute('class', 'col-md-6');

                var div3 = document.createElement('div');
                div3.setAttribute('class', 'col-md-6');

                var btn1 = document.createElement('button');
                btn1.setAttribute('id', array[i].TC_Horario);
                btn1.setAttribute('type', 'button');
                btn1.setAttribute('class', 'btn btn-success btn-sm');
                btn1.setAttribute('data-toggle', 'modal');
                btn1.setAttribute('data_target', '#modal-editar');
                var i1 = document.createElement('i');
                i1.setAttribute('class', 'fa fa-edit text-dark pointer');
                i1.setAttribute('style', 'font-size: 1.2em;');
                btn1.appendChild(i1);

                //<button id="@temp.TC_Horario" class="btn btn-danger btn-sm" onclick="alertAdvertenciaEliminarTiempos(this)"><i class="fa fa-trash pointer" style="font-size:1.5em;"></i> </button>
                var btn2 = document.createElement('button');
                btn2.setAttribute('id', array[i].TC_Horario);
                btn2.setAttribute('type', 'button');
                btn2.setAttribute('class', 'btn btn-danger btn-sm');
                btn2.setAttribute('onclick', 'alertAdvertenciaEliminarTiempos(this)');
                var i2 = document.createElement('i');
                i2.setAttribute('class', 'fas fa-trash text-dark pointer');
                i2.setAttribute('style', 'font-size: 1.2em;');
                btn2.appendChild(i2);

                div2.appendChild(btn1);
                div3.appendChild(btn2);

                div1.appendChild(div2);
                div1.appendChild(div3);

                td2.appendChild(div1);

                tr.appendChild(td0);
                tr.appendChild(td1);
                tr.appendChild(td2);

                document.getElementById('contenidoTabla').appendChild(tr);
            }
            //$('#rangoAusenciasIns').val('');
            //$('#rangoAusenciasIns').attr("placeholder", "Rango de Fechas");
        },
        error: function (response) {
            Swal.fire({
                type: 'warning',
                title: 'Error',
                text: 'Error al refrescar las marcas de tiempo. Inténtelo más tarde'
            })
        }

    })
}