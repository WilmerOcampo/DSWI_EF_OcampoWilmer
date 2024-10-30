using DSWI_EF_API_OcampoWilmer.Models;
using Microsoft.Data.SqlClient;

namespace DSW1_WebAPI.Services
{
    public class ProveedorService : IProveedorService
    {
        public Proveedor Buscar(int? id = null)
        {
            var proveedor = Proveedores().FirstOrDefault(p => p.IdProveedor == id);
            if (proveedor == null) return new Proveedor { NombreProveedor = "Registro no encontrado" };
            return proveedor;
        }

        public IEnumerable<Proveedor> Proveedores(string? b = null)
        {
            List<Proveedor> proveedores = new List<Proveedor>();

            SqlDataReader dr = DatabaseHelper.ReturnDataReader("SP_ProveedoresAPI", b ?? string.Empty);
            while (dr.Read())
            {
                proveedores.Add(new Proveedor()
                {
                    IdProveedor = dr.GetInt32(0),
                    NombreProveedor = dr.GetString(1),
                    NombreContacto = dr.GetString(2),
                    CargoContacto = dr.GetString(3),
                    Direccion = dr.GetString(4),
                    IdPais = dr.GetString(5),
                    Telefono = dr.GetString(6),
                    Fax = dr.GetString(7)
                });
            }
            dr.Close();

            return proveedores;
        }

        public string Insertar(Proveedor p)
        {
            return ExecuteStoredProcedure("SP_InsertarProveedorAPI", p, "registrado");
        }

        public string Actualizar(Proveedor p)
        {
            return ExecuteStoredProcedure("SP_ActualizarProveedorAPI", p, "actualizado");
        }

        private string ExecuteStoredProcedure(string storedProcedure, Proveedor p, string action)
        {
            string msg;
            try
            {
                DatabaseHelper.ExecuteStoredProcedure(storedProcedure, p.IdProveedor + "", p.NombreProveedor + "", p.NombreContacto + "", p.CargoContacto + "", p.Direccion + "", p.IdPais + "", p.Telefono + "", p.Fax + "");
                msg = $"El proveedor {p.IdProveedor + " | " + p.NombreProveedor} fue {action}";
            }
            catch (Exception e)
            {
                msg = "Error: " + e.Message;
            }
            return msg;
        }

        public string Eliminar(int? id = null)
        {
            string msg;
            Proveedor proveedor = Buscar(id);
            try
            {
                DatabaseHelper.ExecuteStoredProcedure("SP_EliminarProveedorAPI", id);

                msg = $"El cliente {proveedor.IdProveedor + " | " + proveedor.NombreProveedor} fue eliminado";
            }
            catch (Exception e)
            {
                msg = "Error: " + e.Message;
            }
            return msg;
        }

        public int GenerarId()
        {
            int id;
            try
            {
                id = (int)DatabaseHelper.ReturnScalarValue("SP_GenerarIdProveedorAPI");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar el ID del producto: " + ex.Message);
            }
            return id;
        }
    }
}
