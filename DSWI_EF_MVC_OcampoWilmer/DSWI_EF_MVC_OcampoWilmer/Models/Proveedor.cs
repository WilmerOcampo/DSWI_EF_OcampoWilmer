using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;

namespace DSWI_EF_MVC_OcampoWilmer.Models
{
    public class Proveedor
    {
        [Display(Name = "ID"), Required(ErrorMessage = "Campo obligatorio")]
        public int IdProveedor { get; set; }
        [Display(Name = "Proveedor"), Required(ErrorMessage = "Este campo es obligatorio")]
        public string? NombreProveedor { get; set; }
        [Display(Name = "Contacto"), Required(ErrorMessage = "Este campo es obligatorio")]
        public string? NombreContacto { get; set; }
        [Display(Name = "Cargo"), Required(ErrorMessage = "Este campo es obligatorio")]
        public string? CargoContacto { get; set; }
        [Display(Name = "Dirección"), Required(ErrorMessage = "Este campo es obligatorio")]
        public string? Direccion { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? IdPais { get; set; }
        [Display(Name = "Telefono"), Required(ErrorMessage = "Este campo es obligatorio")]
        public string? Telefono { get; set; }
        [Display(Name = "Fax")]
        public string? Fax { get; set; }

        [Display(Name = "Pais")]
        public string? NombrePais { get; set; }
    }
}
