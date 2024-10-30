using DSWI_EF_API_OcampoWilmer;

namespace DSW1_WebAPI.Services
{
    public interface IPaisService
    {
        IEnumerable<Pais> Paises();
        Pais Buscar(string? id = null);
    }
}
