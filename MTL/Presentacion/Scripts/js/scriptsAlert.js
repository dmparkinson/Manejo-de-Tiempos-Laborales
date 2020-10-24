/* 
 * 
 * 
 * 
 * Script para plantilla de alertas
 * 
 * 
 * 
 */


// Alerta para advertencias en operaciones
function alertWarning(titleText, message) {
    Swal.fire({
        type: 'warning',
        title: titleText,
        text: message
    })
}



// Alerta para errores en operaciones o del sistema
function alertError(titleText, message) {
    Swal.fire({
        type: 'error',
        title: titleText,
        text: message
    })
}




// Alerta para datos ingresados correctamente, no posee botones
function alertValid(titleText) {
    Swal.fire({
        position: 'top-end',
        type: 'success',
        title: titleText,
        showConfirmButton: false,
        timer: 1500
    })
}





// Plantilla para eliminacion de datos
function alertDeleted(titleText, message) {
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
