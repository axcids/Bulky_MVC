﻿$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/prodcut/getall' },
        "columns": [
            { data: 'name', "width": "15%" },
            { data: 'name', "width": "15%" },
            { data: 'name', "width": "15%" },
            { data: 'name', "width": "15%" }
        ]
    });
}



