using DSWI_EF_MVC_OcampoWilmer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace DSWI_EF_MVC_OcampoWilmer.Controllers
{
    public class ProveedoresController : Controller
    {
        private async Task<List<Proveedor>> ProveedoresAPI(string? b = null)
        {
            using (var proveedor = new HttpClient())
            {
                proveedor.BaseAddress = new Uri("https://localhost:7041/api/Proveedores/");
                HttpResponseMessage responseMessage = await proveedor.GetAsync($"proveedores?b={b}");
                string apiResponse = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Proveedor>>(apiResponse);
            }
        }

        public async Task<IActionResult> Proveedores(string? b = null)
        {
            List<Proveedor> proveedores = await ProveedoresAPI(b);

            var paises = await PaisesAPI();
            if (proveedores != null)
            {
                foreach (var proveedor in proveedores)
                {
                    proveedor.NombrePais = paises.FirstOrDefault(p => p.IdPais == proveedor.IdPais)?.NombrePais;
                }
            }

            return View(await Task.Run(() => proveedores));
        }

        private async Task<IEnumerable<Pais>> PaisesAPI()
        {
            List<Pais> paises = new List<Pais>();
            using (var pais = new HttpClient())
            {
                pais.BaseAddress = new Uri("https://localhost:7041/api/Proveedores/");
                HttpResponseMessage responseMessage = await pais.GetAsync("paises");
                string apiResponse = await responseMessage.Content.ReadAsStringAsync();
                paises = JsonConvert.DeserializeObject<List<Pais>>(apiResponse).ToList();
            }
            return await Task.Run(() => paises);
        }

        private async Task<int> GenerarIdAPI()
        {
            int id = 0;
            using (var proveedor = new HttpClient())
            {
                proveedor.BaseAddress = new Uri("https://localhost:7041/api/Proveedores/");
                HttpResponseMessage responseMessage = await proveedor.GetAsync("generarid");
                string apiResponse = await responseMessage.Content.ReadAsStringAsync();
                id = JsonConvert.DeserializeObject<int>(apiResponse);
            }
            return await Task.Run(() => id);
        }

        public async Task<IActionResult> Create()
        {
            var paises = await PaisesAPI();
            ViewBag.paises = new SelectList(paises, "IdPais", "NombrePais");

            int id = await GenerarIdAPI();
            return View(await Task.Run(() => new Proveedor() { IdProveedor = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Proveedor p)
        {
            string msg = "";
            using (var proveedor = new HttpClient())
            {
                proveedor.BaseAddress = new Uri("https://localhost:7041/api/Proveedores/");
                StringContent content = new StringContent(JsonConvert.SerializeObject(p), Encoding.UTF8, "application/json");

                HttpResponseMessage message = await proveedor.PostAsync("insertar", content);
                string apiResponse = await message.Content.ReadAsStringAsync();
                msg = apiResponse;
            }

            var paises = await PaisesAPI();
            ViewBag.paises = new SelectList(paises, "IdPais", "NombrePais");

            ViewBag.msg = msg;
            return View(await Task.Run(() => p));
        }

        public async Task<IActionResult> Edit(int? id = null)
        {
            if (!id.HasValue) return RedirectToAction("Proveedores");

            Proveedor c = new Proveedor();
            using (var proveedor = new HttpClient())
            {
                proveedor.BaseAddress = new Uri("https://localhost:7041/api/Proveedores/");
                HttpResponseMessage message = await proveedor.GetAsync($"proveedor/{id}");
                string apiResponse = await message.Content.ReadAsStringAsync();
                c = JsonConvert.DeserializeObject<Proveedor>(apiResponse);
            }

            var paises = await PaisesAPI();
            ViewBag.paises = new SelectList(paises, "IdPais", "NombrePais");

            return View(await Task.Run(() => c));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Proveedor p)
        {
            string msg = "";
            using (var proveedor = new HttpClient())
            {
                proveedor.BaseAddress = new Uri("https://localhost:7041/api/Proveedores/");
                StringContent content = new StringContent(JsonConvert.SerializeObject(p), Encoding.UTF8, "application/json");

                HttpResponseMessage message = await proveedor.PutAsync("actualizar", content);
                string apiResponse = await message.Content.ReadAsStringAsync();
                msg = apiResponse;
            }

            var paises = await PaisesAPI();
            ViewBag.paises = new SelectList(paises, "IdPais", "NombrePais");

            ViewBag.msg = msg;
            return View(await Task.Run(() => p));
        }

        public async Task<ActionResult> Details(int? id = null)
        {
            Proveedor proveedor = new Proveedor();
            List<Proveedor> proveedores = await ProveedoresAPI();

            proveedor = proveedores.FirstOrDefault(p => p.IdProveedor == id);

            var paises = await PaisesAPI();
            foreach (var client in proveedores)
            {
                client.NombrePais = paises.FirstOrDefault(p => p.IdPais == client.IdPais)?.NombrePais;
            }
            return View(await Task.Run(() => proveedor));
        }

        public async Task<IActionResult> Delete(int? id = null)
        {
            if (!id.HasValue) return RedirectToAction("Proveedores");

            string msg = "";
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("https://localhost:7041/api/Proveedores/");
                HttpResponseMessage message = await cliente.DeleteAsync($"eliminar/{id}");
                string apiResponse = await message.Content.ReadAsStringAsync();
                msg = apiResponse;
            }

            ViewBag.msg = msg;
            return RedirectToAction("Proveedores");
        }
    }
}
