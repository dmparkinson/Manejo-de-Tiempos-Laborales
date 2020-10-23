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

    var data = "";

    

    if (document.getElementById("Entrada").checked) {
        
    } else {
        Swal.fire({
            position: 'center',
            type: 'warning',
            title: 'Seleccione uno',
            showConfirmButton: false,
            timer: 4000
        })
    }
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
            position: 'center',
            type: 'warning',
            title: 'Debe seleccionar un elemento',
            showConfirmButton: false,
            timer: 4000
        })
    } else {
        var fecha = document.getElementById('fechaA').value;
        var hora = document.getElementById('horaA').value;
        parametros = { "horario": button.id };
        $.ajax(
            {
                data: parametros,
                url: '/Empleado_Horarios/lista_tiempos_for_js',
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
                    newSpan.innerHTML = 'Confirmando...';

                    div.appendChild(divSpin);
                    div.appendChild(newSpan);

                    document.getElementById('respuesta').appendChild(div);
                }, //antes de enviar
                success: function (response) {



                } //se ha enviado
            }
        );
    }
}
