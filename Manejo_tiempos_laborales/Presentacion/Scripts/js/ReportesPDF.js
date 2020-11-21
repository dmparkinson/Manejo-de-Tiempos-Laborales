function exportPDF(tipo) {

    $.ajax({
        url: '/Empleados/getOficina',
        type: 'POST',
        success: function (response) {
            var doc = new jsPDF('l', 'mm', 'a4');
            doc.fromHTML('<h1 style="font-family:sans-serif"><b>' + response + '</b></h1>', 15, 5);
            doc.text(15, 28, 'Reporte de ' + $('#tituloReportes').html());

            var date = new Date();
            var datetime = date.getFullYear() + "/" + (date.getMonth() + 1) + "/" + date.getDate() + " " + date.getHours() + ":" + date.getMinutes();

            doc.fromHTML('<h5 style="font-family:sans-serif">Fecha del reporte: ' + datetime + '</h5>', 15, 26);

            var acc = document.getElementById('acc');
            if (acc) {
                acc.style.display = 'none';
            }

            doc.autoTable({
                html: '#table',
                startY: 43
            });

            if (acc) {
                acc.style.display = '';
            }

            doc.save(tipo + '.pdf');
        },
        error: function (response) {
            alerta('Error inesperado haciendo el reporte');
        }
    });
    return false;
}

