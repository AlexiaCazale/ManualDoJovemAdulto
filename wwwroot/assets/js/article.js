let dataTable;

$(document).ready(function (){
    loadDataTable();
});

function loadDataTable(){
    dataTable = $('#tblData').DataTable({
        "ajax" :{
            "url": "/Article/GetAll"
        },
        "columns": [
            { "data" : "name", "width": "40%" },
            { "data" : "category.name", "width": "30%" },
            { "data" : "id", "width": "15%", "bSortable": false,
                "render": function(data) {
                    return `
                        <div class="text-xs text-secondary mb-0">
                            <a href="/Article/Edit?id=${data}" title="Editar">
                                <span class="material-icons">edit</span>
                            </a> &nbsp &nbsp
                            <a href="/Article/Details?id=${data}" title="Detalhes">
                                <span class="material-icons">info</span>
                            </a> &nbsp &nbsp
                            <a href="javascript:void(0)" onclick="Delete('/Article/Delete?id=${data}')" title="Excluir">
                                <span class="material-icons">delete</span>
                            </a> &nbsp &nbsp
                        </div>
                    `
                }
            }
        ],
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
};