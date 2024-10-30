using DSWI_EF_API_OcampoWilmer;
using Microsoft.Data.SqlClient;

namespace DSW1_WebAPI.Services
{
    public class PaisService : IPaisService
    {
        public Pais Buscar(string? id = null)
        {
            return Paises().FirstOrDefault(p => p.IdPais == id);
        }

        public IEnumerable<Pais> Paises()
        {
            List<Pais> paises = new List<Pais>();


            SqlDataReader dr = DatabaseHelper.ReturnDataReader("SP_PaisesAPI");
            while (dr.Read())
            {
                paises.Add(new Pais()
                {
                    IdPais = dr.GetString(0),
                    NombrePais = dr.GetString(1)
                });
            }
            dr.Close();

            return paises;
        }
    }
}
