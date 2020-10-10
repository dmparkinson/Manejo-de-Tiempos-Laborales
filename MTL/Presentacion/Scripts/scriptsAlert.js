


function alertErrorLogin() {
    Swal.fire({
        type: 'error',
        title: 'Usuario inválido. ',
        text: 'El usuario o la contraseña son incorrectas'
    })
}



function alertDatosInvalidos() {
    Swal.fire({
        type: 'error',
        title: 'Datos inválido. ',
        text: 'Los datos que desea ingresar no son válidos.'
    })
}


function alertDatosValidos() {
    Swal.fire({
        position: 'top-end',
        type: 'success',
        title: 'Datos ingresados correctamente.',
        showConfirmButton: false,
        timer: 3500
    })
}



function alertAdvertenciaEliminar() {
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


