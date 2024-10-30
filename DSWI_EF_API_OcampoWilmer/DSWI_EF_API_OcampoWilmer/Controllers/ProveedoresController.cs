using DSW1_WebAPI.Services;
using DSWI_EF_API_OcampoWilmer.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DSWI_EF_API_OcampoWilmer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly IProveedorService _proveedorService;
        private readonly IPaisService _paisService;

        public ProveedoresController(IProveedorService proveedorService, IPaisService paisService)
        {
            _proveedorService = proveedorService;
            _paisService = paisService;
        }

        [HttpGet("proveedores")]
        public async Task<ActionResult<List<Proveedor>>> Proveedores(string? b = null)
        {
            var list = await Task.Run(() => _proveedorService.Proveedores(b));
            return Ok(list);
        }

        [HttpGet("paises")]
        public async Task<ActionResult<List<Pais>>> Paises()
        {
            var list = await Task.Run(() => _paisService.Paises());
            return Ok(list);
        }

        [HttpGet("proveedor/{id}")]
        public async Task<ActionResult<List<Proveedor>>> Buscar(int? id = null)
        {
            var list = await Task.Run(() => _proveedorService.Buscar(id));
            return Ok(list);
        }

        [HttpPost("insertar")]
        public async Task<ActionResult<string>> InsertCliente(Proveedor p)
        {
            var msg = await Task.Run(() => _proveedorService.Insertar(p));
            return Ok(msg);
        }

        [HttpPut("actualizar")]
        public async Task<ActionResult<string>> Actualizar(Proveedor p)
        {
            var msg = await Task.Run(() => _proveedorService.Actualizar(p));
            return Ok(msg);
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<ActionResult<List<Proveedor>>> DeleteCliente(int? id = null)
        {
            var list = await Task.Run(() => _proveedorService.Eliminar(id));
            return Ok(list);
        }

        [HttpGet("generarid")]
        public async Task<ActionResult<int>> GenerarId()
        {
            var id = await Task.Run(() => _proveedorService.GenerarId());
            return Ok(id);
        }
    }
}
