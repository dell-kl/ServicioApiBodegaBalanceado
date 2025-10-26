using Business.Services.IService;
using Domain;
using Domain.DTO;
using Domain.DTO.RequestDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Utility.DetectSO;
using Utility.Exceptions;

namespace ServicioApiBodegaBalanceado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RawMaterialController : ControllerBase
    {
        private readonly IServiceManagement _serviceManagement;
        public RawMaterialController(IServiceManagement serviceManagement)
        {
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
        public async Task<IActionResult> DetalleMateriaPrima(string guid)
        {

            RawMaterialDetailsRequestDto rawMaterialDetails = await _serviceManagement._RawMaterialService.GetDetailesRawMaterial(Guid.Parse(guid));

            return Ok(JsonConvert.SerializeObject(rawMaterialDetails));
        }

        [HttpPost("RegistrarMateriaPrima")]
        public IActionResult RegistrarMateriaPrima([FromBody] RawMaterialDto rawMaterial)
        {
            try
            {
                _serviceManagement._RawMaterialService.Agregate(rawMaterial);
                return Ok("Datos registrados exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "El servidor tuvo un problema en guardar los datos");
            }
        }

        [HttpPost("RegistrarImagenes")]
        public async Task<IActionResult> RegistrarImagenes([FromForm] IEnumerable<IFormFile> formFiles, [FromForm] string identificador)
        {

            try
            {
                ICollection<DataImage> dataImages = await _serviceManagement._RawMaterialService.SaveImages(formFiles, Guid.Parse(identificador));

                return Ok(new { mensaje = "Imagenes subidas exitosamente", imagenes = dataImages });

            }
            catch (OperationAbortExceptions operation)
            {
                return BadRequest("Maximo de imagenes superado. Solo se permite 10 imagenes en total");
            }
        }

        [HttpPost("AgregarEnStock")]
        public async Task<IActionResult> AgregarEnStock([FromBody] StockRawMaterial stockRawMaterial)
        {

            await _serviceManagement._RawMaterialService.AddStockRawMaterial(stockRawMaterial);

            return Ok("Se ha agregado mas material a tu bodega");
        }

        [HttpPut("EditNameRawMaterial")]
        public async Task<IActionResult> EditNameRawMaterial([FromBody] RawMaterialDto data)
        {
            try
            {
                RawMaterial rawMaterial = await _serviceManagement._RawMaterialService.Buscar(Guid.Parse(data.id_dto!));
                rawMaterial.RawMaterial_name = data.nombre_dto;
                rawMaterial.RawMaterial_updated = DateTime.Now;
                await _serviceManagement._RawMaterialService.EditDataRawMaterial(rawMaterial);

                return Ok("Materia Prima Editado Exitosamente");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "El servidor no pudo actualizar los datos, intentalo en otro momento");
            }
        }

        [HttpGet("ViewImage/{guid}")]
        public IActionResult ViewImage(string guid)
        {
            Response.Headers?.Add("Content-Type", "image/jpeg");

            string pathPartial = "\\FilesPublic\\ImageRawMaterial";

            if (DetectSystemOperation.IsLinux())
                pathPartial = pathPartial.Replace("\\", "//");


            string ruta = Path.Combine($"{Directory.GetCurrentDirectory()}{pathPartial}", guid);

            Console.WriteLine(ruta);

            if (Path.Exists(ruta))
            {
                byte[] byteLists = System.IO.File.ReadAllBytes(ruta);

                return File(byteLists, "image/jpeg");
            }

            return null;
        }


        [HttpPost("DeleteRawMaterial")]
        public async Task<IActionResult> DeleteRawMaterial([FromBody] ICollection<DataImage> listadoImagenes)
        {
            try
            {
                await _serviceManagement._RawMaterialService.DeleteImages(listadoImagenes);

                return Ok("Imagenes eliminadas exitosamente");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "El servidor tuvo un problema al eliminar las imagenes");
            }
        }
    }
}
