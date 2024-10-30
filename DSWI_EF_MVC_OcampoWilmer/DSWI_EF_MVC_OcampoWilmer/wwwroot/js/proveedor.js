$('.btn-eliminar').click(function () {
    var id = $(this).data('id');
    Swal.fire({
        title: '¿Estás seguro?',
        text: "No podrás revertir esto!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, bórralo!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/Proveedores/Delete/' + id,
                type: 'DELETE',
                success: function () {
                    Swal.fire(
                        'Eliminado!',
                        'El proveedor ha sido eliminado.',
                        'success'
                    ).then((result) => {
                        if (result.isConfirmed) {
                            location.reload();
                        }
                    });
                }
            });
        }
    })
});
