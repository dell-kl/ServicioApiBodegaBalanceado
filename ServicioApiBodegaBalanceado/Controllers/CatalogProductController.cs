using Business.Services.IService;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
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
            return Ok(new { mensaje = "Imagenes subidas exitosamente", imagenes = "" });
        }
    }
}