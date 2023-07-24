$(document).ready(function (){
    $('#tblData').DataTable({
        "language": {
            "lengthMenu": "Exibir _MENU_ registros por página",
            "search": "Pesquisar",
            "zeroRecords": "Nenhum resultado encontrado",
            "info": "Exibindo página _PAGE_ de _PAGES_",
            "infoEmpty": "Sem dados para exibir",
            "infoFiltered": "(filtrado de _MAX_ registros)",
            "paginate": {
                "first": "Primeiro",
                "last": "Último",
                "next": "Próximo",
                "previous": "Anterior"
            }
        }
    });
    document.getElementById("tblData_length").classList.add("text-xxs");
    document.getElementById("tblData_info").classList.add("text-xs");
    document.getElementById("tblData_paginate").classList.add("text-xs");
});