
function login(btn) {




    std.usserName = $("#usser").val();
    std.usserPass = $("#passw").val();
    $.ajax({
        type: "POST",
        url: '@Url.Action("("Sesion", "Login")',
        data: '{std: ' + JSON.stringify(std) + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function () {
            alert("Data has been added successfully.");  
           // LoadData();
        },
        error: function () {
            alert("Error while inserting data");
        }  
    }); 


    return false; //IMPORTANTE!!!!! no borrar, esto funciona para utilizar validaciones html5 junto a sweetalert
}
