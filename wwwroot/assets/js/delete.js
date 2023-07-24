function Delete(url){
    Swal.fire({
        title: 'Confirma a Exclusão?',
        text: 'Esta ação não pode ser revertida!',
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim, Excluir!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        Swal.fire(data.message, '', 'success').then(() => {
                            // Recarregando os dados da tabela
                            if (typeof dataTable == "undefined")
                                window.location.reload();
                            else
                                $("#tblData").DataTable().ajax.reload();
                        });
                    }
                    else{
                        Swal.fire('Problemas ao excluir', data.message, 'error');
                    }
                }
            });
        }
    });
}