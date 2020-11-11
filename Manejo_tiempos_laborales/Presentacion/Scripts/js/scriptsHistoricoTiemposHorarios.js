

/* 
 * 
 * Eliminar el Tiempo de horario
 * 
 * */
function eliminarHTiempo(idTiempo) {

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

            var dataHorario = {
                "idTiempo": idTiempo
            };
            $.ajax({
                url: "/Historico_Tiempos_Laborales/Eliminar",    // Nombre del controlador/ accion del controlador
                type: "POST",
                data: dataHorario,
                success: function (response) {
                    if (response == 1) // Si el tiempo de horario se elimino correctamente
                    {
                        Swal.fire(
                            'Eliminado!',
                            'El tiempo de horario se elimino correctamente.',
                            'success'
                        )

                        actualizarTablaTiemposHistorico()
                    }
                    else { // si no se elimino
                        Swal.fire(
                            'No eliminado',
                            'El tiempo de horario seleccionado no se puede eliminar.',
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






var btnEditHorario = null;
var fechaAnterior = null;
var horaAnterior = null;
var horarioAnterior = null;

/*
 *
 * editar un tipo de ausencia
 *
 * */
function editarHTiempo() {



    var envio = {
        anterior: motivoAnterior,
        nuevo: $('#motivoEdit').val()
    }

    $.ajax({
        url: "/Historico_Tiempos_Laborales/Horarios",    // Nombre del controlador/ accion del controlador
        type: "POST",
        data: envio,
        dataType: "json",
        success: function (response) {
            if (response.success == true) { // Si se elimino
                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Datos modificados exitosamente',
                    showConfirmButton: false,
                    timer: 1500
                })


                var row = $(btnEdit).closest('tr'); // Obtener la fila
                // Editar las columnas de la fila seleccionada
                row.find("td").eq(0).html(tAusencia);
                row.find("td").eq(1).html('<div class="d-flex justify-content-center">' +
                    '<a data-toggle="modal" data-target="#modal-editar" href= "#" > <i class="fas fa-edit text-dark" style="font-size: 1.2em;"></i></a>' +
                    '<a onclick="eliminarTipoAusencia(' + tAusencia + ', this)" href="#"> <i class="fas fa-trash text-dark" style="font-size: 1.2em;"></i></a>' +
                    '</div >');
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

function cargarModalHTiempo(idT, idU, idH, fecha, hora, tiempo) {
    window.localStorage.setItem('idT', idT);
    window.localStorage.setItem('idU', idU);
    window.localStorage.setItem('idH', idH);
    document.getElementById('fechaEdit').value = fecha;
    document.getElementById('horaEdit').value = hora;
    document.getElementById('tiempoEdit').value = tiempo;
}

function editarHHorario() {

    var option = document.getElementById('tiempoEdit');
    var select = option.options[option.selectedIndex].id;

    parametros = {
        "idT": window.localStorage.getItem('idT'),
        "idU": window.localStorage.getItem('idU'),
        "idH": select,
        "fecha": document.getElementById('fechaEdit').value,
        "hora": document.getElementById('horaEdit').value,
        "tiempo": document.getElementById('tiempoEdit').value
    };

    $.ajax(
        {
            data: parametros,
            url: '/Historico_Tiempos_Laborales/Editar',
            type: "POST",
            success: function (response) {

                if (response == 1) {
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: 'Datos modificados exitosamente',
                        showConfirmButton: false,
                        timer: 1500
                    })

                    actualizarTablaTiemposHistorico()
                } else {
                    Swal.fire({
                        position: 'center',
                        icon: 'warning',
                        title: 'Error al modificar',
                        showConfirmButton: false,
                        timer: 1500
                    })
                }
            }
        }
    );

}

function actualizarTablaTiemposHistorico() {
    document.getElementById('contenidoTabla').innerHTML = "";

    $.ajax(
        {
            url: '/Historico_Tiempos_Laborales/RefrescarTablaHistorico',
            type: 'POST',
            success: function (response) {
                var array = JSON.parse(response);
                document.getElementById('contenidoTabla').innerHTML = '';
                for (i = 0; i < array.length; i++) {
                    var tr = document.createElement('tr')

                    var td1 = document.createElement('td');
                    td1.innerHTML = array[i].empleado.TC_Identificacion

                    var td2 = document.createElement('td');
                    td2.innerHTML = array[i].empleado.TC_Nombre_Usuario

                    var td3 = document.createElement('td');
                    td3.innerHTML = array[i].empleado.TC_Primer_Apellido

                    var td4 = document.createElement('td');
                    td4.innerHTML = array[i].empleado.TC_Segundo_Apellido

                    var td5 = document.createElement('td');
                    td5.innerHTML = array[i].TF_Fecha

                    var td6 = document.createElement('td');
                    td6.innerHTML = array[i].TH_Hora

                    var td7 = document.createElement('td');
                    td7.innerHTML = array[i].horario.TC_Horario

                    var td8 = document.createElement('td');

                    var div = document.createElement('div');

                    var a1 = document.createElement('a');
                    var i1 = document.createElement('i');
                    var a2 = document.createElement('a');
                    var i2 = document.createElement('i');

                    var oncc = 'cargarModalHTiempo(' + array[i].TN_Id_Tiempo + ', ' + array[i].TN_Id_Usuario + ', ' + array[i].TN_Id_Horario + ', ' + array[i].TF_Fecha + ', ' + array[i].TH_Hora + ', ' + array[i].TC_Horario + ')'

                    a1.setAttribute('data-toggle', 'modal');
                    a1.setAttribute('data-target', '#modal-editar');
                    a1.setAttribute('onclick', oncc);
                    i1.setAttribute('class', 'fas fa-edit text-dark pointer');
                    i1.setAttribute('style', 'font-size: 1.2em;');
                    a1.appendChild(i1);

                    a2.setAttribute('onclick', 'eliminarHTiempo(' + array[i].TN_Id_Tiempo + ')');
                    i2.setAttribute('class', 'fas fa-trash text-dark pointer');
                    i2.setAttribute('style', 'font-size: 1.2em;');
                    a2.appendChild(i2);
                    
                    div.appendChild(a1);
                    div.appendChild(a2);

                    td8.appendChild(div);

                    tr.appendChild(td1);
                    tr.appendChild(td2);
                    tr.appendChild(td3);
                    tr.appendChild(td4);
                    tr.appendChild(td5);
                    tr.appendChild(td6);
                    tr.appendChild(td7);
                    tr.appendChild(td8);

                    document.getElementById('contenidoTabla').appendChild(tr);
                }
            }
        }
    );
}