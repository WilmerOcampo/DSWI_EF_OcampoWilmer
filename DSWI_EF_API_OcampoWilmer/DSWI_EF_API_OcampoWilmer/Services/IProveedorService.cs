using DSWI_EF_API_OcampoWilmer.Models;

namespace DSW1_WebAPI.Services
{
    public interface IProveedorService
    {
        IEnumerable<Proveedor> Proveedores(string? b = null);
        Proveedor Buscar(int? id = null);
        string Insertar(Proveedor p);
        string Actualizar(Proveedor p);
        string Eliminar(int? id = null);
        int GenerarId();
    }
}
