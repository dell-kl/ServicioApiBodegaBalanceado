using Business.Services.IService;
using Domain;
using Domain.DTO;
using Domain.DTO.RequestDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ServicioApiBodegaBalanceado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RawMaterialController : ControllerBase
    {
        private readonly IServiceManagement _serviceManagement;
        public RawMaterialController(IServiceManagement serviceManagement) {
            _serviceManagement = serviceManagement;
        }

        [HttpGet("SolicitarMateriaPrima/{skip}")]
        public async Task<IActionResult> SolicitarMateriaPrima(int skip)
        {
            var listado = 
                (await _serviceManagement._RawMaterialService.Obtener(skip, "ImageRawMaterials, KgMonitorings"));

            ICollection<RawMaterialRequestDto> datos = new List<RawMaterialRequestDto>();
            foreach (var item in listado)
            {
                var register = new RawMaterialRequestDto(1)
                {
                    guid = item.RawMaterial_guid.ToString(),
                    nombreProducto = item.RawMaterial_name,
                    rutaImagen = item.ImageRawMaterials.Any() ? item.ImageRawMaterials.First().ImageRawMaterial_url : "default_icon.png",
                    fechaUltimaCompra = item.KgMonitorings.OrderByDescending(item => item.KgMonitoring_id).First().KgMonitoring_created,
                    kgTotal = item.RawMaterial_KgTotal
                };

                datos.Add(register);
            }

            return Ok(JsonConvert.SerializeObject(datos));
        }

        [HttpGet("DetalleMateriaPrima/{guid}")]
        public async Task<IActionResult> DetalleMateriaPrima(string guid) {

           RawMaterialDetailsRequestDto rawMaterialDetails = await _serviceManagement._RawMaterialService.GetDetailesRawMaterial(Guid.Parse(guid));
            
            return Ok(JsonConvert.SerializeObject(rawMaterialDetails));
        }

        [HttpPost("RegistrarMateriaPrima")]
        public IActionResult RegistrarMateriaPrima([FromBody] RawMaterialDto rawMaterial) {
            _serviceManagement._RawMaterialService.Agregate(rawMaterial);
            return Ok("OK");
        }

        [HttpPost("RegistrarImagenes")]
        public async Task<IActionResult> RegistrarImagenes([FromForm] IEnumerable<IFormFile> formFiles, [FromForm] string identificador) {

            await _serviceManagement._RawMaterialService.SaveImages(formFiles, Guid.Parse(identificador));

            return Ok("OK");
        }

        [HttpPost("AgregarEnStock")]
        public async Task<IActionResult> AgregarEnStock([FromBody] StockRawMaterial stockRawMaterial) {

            await _serviceManagement._RawMaterialService.AddStockRawMaterial(stockRawMaterial);
            
            return Ok("Agregado Exitosamente");
        }

        [HttpPut("EditNameRawMaterial")]
        public async Task<IActionResult> EditNameRawMaterial([FromBody] RawMaterialDto data)
        {
            RawMaterial rawMaterial = await _serviceManagement._RawMaterialService.Buscar(Guid.Parse(data.id_dto!));
            rawMaterial.RawMaterial_name = data.nombre_dto;
            rawMaterial.RawMaterial_updated = DateTime.Now;
            await _serviceManagement._RawMaterialService.EditDataRawMaterial(rawMaterial);

            return Ok("Materia Prima Editado Exitosamente");
        }

        [HttpGet("ViewImage/{guid}")]
        public IActionResult ViewImage(string guid)
        {
            Response.Headers?.Add("Content-Type", "image/jpeg");

            string ruta = Path.Combine($"{Directory.GetCurrentDirectory()}\\FilesPublic\\ImageRawMaterial", guid);

            if ( Path.Exists(ruta))
            {
                byte[] byteLists = System.IO.File.ReadAllBytes(ruta);

                return File(byteLists, "image/jpeg");
            }

            return null;
        }
    }
}
