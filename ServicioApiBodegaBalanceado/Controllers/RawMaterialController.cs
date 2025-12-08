using Business.Services.IService;
using Domain;
using Domain.DTO;
using Domain.DTO.RequestDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServicioApiBodegaBalanceado.Domain.DTO;
using System.Threading.Tasks;
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
            var listado = await _serviceManagement._RawMaterialService.Obtener(skip, "ImageRawMaterials, KgMonitorings");

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

            return Ok(datos);
        }

        [HttpGet("SolicitarKgMonitoring/{skip}")]
        public async Task<IActionResult> SolicitarKgMonitoring(int skip, [FromQuery] string guid)
        {
            if (string.IsNullOrWhiteSpace(guid))
                return StatusCode(StatusCodes.Status400BadRequest, "El identificador del producto no puede estar vacío");

            Guid identificador;
            try
            {
                identificador = Guid.Parse(guid);
            }
            catch (FormatException)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "El identificador del producto no tiene el formato correcto");
            }

            RawMaterial rawMaterial;
            try
            {
                rawMaterial = await _serviceManagement._RawMaterialService.Buscar(identificador, "KgMonitorings");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, "No se pudo encontrar el registro");
            }

            // Paginamos los KgMonitorings asociados similar al otro endpoint.
            IEnumerable<KgMonitoring> listado = await _serviceManagement._RawMaterialService.ObtenerKgMonitorings(identificador, skip, "RawMaterial");

            // Mapear a DTO sencillo (reutilizamos KGMonitoringDto para minimizar creación de nuevas clases)
            ICollection<KgMonitoringRequestDto> datos = new List<KgMonitoringRequestDto>();
            foreach (var item in listado)
            {
                KgMonitoringRequestDto kgDto = new()
                {
                    Guid = item.KgMonitoring_guid.ToString(),
                    Cantidad = (int)item.KgMonitoring_Total,
                    Kg = item.KgMonitoring_KGStandard,
                    Precio = item.KgMonitoring_priceUnit,
                    KgTotal = item.KgMonitoring_Total,
                    PrecioTotal = item.KgMonitoring_priceTotal,
                    FechaCreacion = item.KgMonitoring_created,
                    FechaActualizacion = item.KgMonitoring_updated
                };
                datos.Add(kgDto);
            }

            return Ok(datos);
        }

        [HttpGet("DetalleMateriaPrima/{guid}")]
        public async Task<IActionResult> DetalleMateriaPrima(string guid)
        {

            RawMaterialDetailsRequestDto rawMaterialDetails = await _serviceManagement._RawMaterialService.GetDetailesRawMaterial(Guid.Parse(guid));

            return Ok(rawMaterialDetails);
        }

        [HttpPost("RegistrarMateriaPrima")]
        public IActionResult RegistrarMateriaPrima([FromBody] RawMaterialDto rawMaterial)
        {
            try
            {
                _serviceManagement._RawMaterialService.Agregate(rawMaterial);
                return Ok("Datos registrados exitosamente");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "El servidor tuvo un problema en guardar los datos");
            }
        }

        // Eliminado endpoint duplicado SolicitarKgMonitorings para unificar lógica en SolicitarKgMonitoring

        [HttpPost("RegistrarImagenes")]
        public async Task<IActionResult> RegistrarImagenes([FromForm] IEnumerable<IFormFile> formFiles, [FromForm] string identificador)
        {

            try
            {
                ICollection<DataImageDto> dataImages = await _serviceManagement._RawMaterialService.SaveImages(formFiles, Guid.Parse(identificador));

                return Ok(new { mensaje = "Imagenes subidas exitosamente", imagenes = dataImages });

            }
            catch (OperationAbortExceptions)
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "El servidor no pudo actualizar los datos, intentalo en otro momento");
            }
        }


        [HttpPost("DeleteRawMaterial")]
        public async Task<IActionResult> DeleteRawMaterial([FromBody] ICollection<DataImageDto> listadoImagenes)
        {
            try
            {
                await _serviceManagement._RawMaterialService.DeleteImages(listadoImagenes);

                return Ok("Imagenes eliminadas exitosamente");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "El servidor tuvo un problema al eliminar las imagenes");
            }
        }
    }
}
