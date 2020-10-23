function eliminar(idAusencia) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
        title: '¿Seguro?',
        text: 'Está acción es irreversible',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si',
        cancelButtonText: 'No',
        reverseButtons: true
    }).then((result) => {
        if (result.value) {

            // Aqui el llamado del controlador, la accion para eliminar el tipo de ausencia

            var dataUsser = {
                "tipoAusencia": idAusencia
            };

            $.ajax({
                url: "Catalogo_Ausencias/Eliminar",    // Nombre del controlador/ accion del controlador
                type: "POST",
                data: JSON.stringify(dataUsser),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    if (response.success == true) {
                        //Si los datos se eliminaron correntamente
                        swalWithBootstrapButtons.fire(
                            'Eliminados',
                            'Los datos se eliminaron correctamente',
                            'success'
                        )
                        $('#example1').DataTable().ajax.reload();
                    }
                    else {
                        // Si hubo un error en la eliminacion de los datos
                        walWithBootstrapButtons.fire(
                            'Error inesperado',
                            'Ocurrió un error en la eliminación de los datos.',
                            'error'
                        )
                    }
                },
                error: function () {
                    // Si hubo un error en la eliminacion de los datos
                    walWithBootstrapButtons.fire(
                        'Error inesperado',
                        'Ocurrió un error en la eliminación de los datos.',
                        'error'
                    )
                }
            });
        } else if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire(
                'Cancelado',
                'Se cancelo la eliminacion de datos',
                'error'
            )
        }
    })
}

