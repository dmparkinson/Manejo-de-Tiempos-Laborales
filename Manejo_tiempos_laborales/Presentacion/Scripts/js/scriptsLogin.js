
function login(btn) {


    var dataUsser = {
                    "name": $("#usser").val(),
                    "password": $("#passw").val()
                    };

    $.ajax({
        url: "Account/CheckUser",    // Nombre del controlador/ accion del controlador
        type: "POST",
        data: JSON.stringify(dataUsser),
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            if (response.success == true) { 
                window.location.href = response.url;
            }
            else{
                Swal.fire({
                    title: 'Usuario no encontrado',
                    text: 'El usuario o contraseña son incorrectos.',
                    icon: 'warning'
                })
            }
        },
        error: function () {
            Swal.fire({
                icon: 'error',
                title: "Error inesperado",
                text: "Ocurrio un error en el inicio de sesion, intente denuevo."
            })
        }


        
    });
    return false; //IMPORTANTE!!!!! no borrar, esto funciona para utilizar validaciones html5 junto a sweetalert
}
