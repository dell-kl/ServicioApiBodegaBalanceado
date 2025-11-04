using System.Threading.Tasks;
using Business.Services.IService;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Domain.DTO.RequestDto;
using Newtonsoft.Json;
using ServicioApiBodegaBalanceado.Domain.DTO;
using Utility.Exceptions;

namespace ServicioApiBodegaBalanceado.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CatalogProductController : ControllerBase
    {
        private readonly IServiceManagement _serviceManagement;

        public CatalogProductController(IServiceManagement serviceManagement)
        {
            _serviceManagement = serviceManagement;
        }

        [HttpGet("SolicitarCatalogProduct/{skip}")]
        public async Task<IActionResult> SolicitarCatalogProduct(int skip)
        {
            var listado = await _serviceManagement._CatalogProductService.Obtener(skip, "ImageCatalogProductions, DataCatalogProduct");

            ICollection<CatalogProductRequestDto> datos = new List<CatalogProductRequestDto>();

            foreach (var item in listado)
            {
                var register = new CatalogProductRequestDto()
                {
                    guid = item.CatalogProduction_guid.ToString(),
                    nombreProducto = item.CatalogProduction_name,
                    rutaImagen = item.ImageCatalogProductions.Any() ? item.ImageCatalogProductions.First().ImageCatalogProduction_name : "default_icon.png"
                };

                datos.Add(register);
            }

            return Ok(JsonConvert.SerializeObject(datos));
        }

        [HttpPost("RegistrarDataCatalogProduct")]
        public IActionResult RegistrarDataCatalogProduct([FromBody] CatalogProductDto catalogProduct)
        {
            try
            {
                _serviceManagement._CatalogProductService.Agregate(catalogProduct);

                return Ok("Producto registrado exitosamente");
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error del servidor, no se puedo registrar tu producto intentalo en otro momento");
            }
        }

        [HttpPost("AgregarDataProductDataCatalogProduct")]
        public async Task<IActionResult> AgregarDataProductDataCatalogProduct([FromBody] CatalogProductDto catalogProduct)
        {
            try
            {
                await _serviceManagement._CatalogProductService.AgregateDataProduct(catalogProduct);

                return Ok("Los nuevos datos de venta del producto fueron agregados exitosamente");
            }
            catch (OperationAbortExceptions)
            {
                return StatusCode(StatusCodes.Status404NotFound, "No se ha podido identificar el producto, intentalo nuevamente.");
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error del servidor, no se lograron agregar los respectivos datos de venta del producto");
            }
        }

        [HttpPost("RegistrarImagenes")]
        public async Task<IActionResult> RegistrarImagenes([FromForm] IEnumerable<IFormFile> formFiles, [FromForm] string identificador)
        {
            try
            {
                ICollection<DataImageDto> listadoImagenes = await _serviceManagement._CatalogProductService.SaveImages(formFiles, Guid.Parse(identificador));
                return Ok(new { mensaje = "Imagenes subidas exitosamente", imagenes = listadoImagenes });
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error del servidor. No se pudieron guardar tus imagenes");
            }
        }

        [HttpPost("EliminarImagenes")]
        public async Task<IActionResult> EliminarImagenes([FromBody] ICollection<DataImageDto> listadoImagenes)
        {
            try
            {
                await _serviceManagement._CatalogProductService.DeleteImages(listadoImagenes);

                return Ok("Imagenes eliminadas exitosamente");
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error del servidor. No se puedo eliminar tus imagenes, intentalo en otro momento.");
            }
        }
    }
}