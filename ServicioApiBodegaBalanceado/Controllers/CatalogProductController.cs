using Business.Services.IService;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Domain.DTO.RequestDto;
using Newtonsoft.Json;
using ServicioApiBodegaBalanceado.Domain.DTO;
using Utility.Exceptions;
using Domain;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

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
            IEnumerable<CatalogProduction> listado = await _serviceManagement._CatalogProductService.Obtener(skip, "ImageCatalogProductions, DataCatalogProduct");

            ICollection<CatalogProductRequestDto> datos = new List<CatalogProductRequestDto>();

            foreach (var item in listado)
            {
                var register = new CatalogProductRequestDto()
                {
                    identificador = item.CatalogProduction_guid.ToString(),
                    nombreProducto = item.CatalogProduction_name,
                    rutaImagen = item.ImageCatalogProductions.Any() ? item.ImageCatalogProductions.First().ImageCatalogProduction_name : "default_icon.png",
                    fechaCreacion = item.CatalogProduction_created,
                    numeroCategorias = item.DataCatalogProduct.Count(),
                    totalSacosCatalogo = item.DataCatalogProduct.Sum(item => item.DataCatalogProduct_countTotal)
                };

                datos.Add(register);
            }

            return Ok(JsonConvert.SerializeObject(datos));
        }

        [HttpGet("SolicitarDataCatalogProduct/{skip}")]
        public async Task<IActionResult> SolicitarDataCatalogProduct(int skip, [FromQuery] string guid)
        {
            if (guid.IsNullOrEmpty())
                return StatusCode(StatusCodes.Status500InternalServerError, "El identificador del producto no puede estar vacio");

            CatalogProduction? catalogProduction = null;
            try
            {
                Guid identificador = Guid.Parse(guid);
                catalogProduction = await _serviceManagement._CatalogProductService.Buscar(identificador);
            }
            catch (FormatException) { return StatusCode(StatusCodes.Status500InternalServerError, "El identificador del producto no tiene el formato correcto"); }

            if (catalogProduction is null)
                return StatusCode(StatusCodes.Status500InternalServerError, "No se puedo encontrar el registro");

            IEnumerable<DataCatalogProduct> dataCatalogProducts = await _serviceManagement._CatalogProductService.ObtenerDataCatalogProduct(skip, "KG_Catalog, Price_KG", catalogProduction.CatalogProduction_id);

            ICollection<DataCatalogProductoRequestDto> datos = new List<DataCatalogProductoRequestDto>();

            foreach (DataCatalogProduct dataCatalog in dataCatalogProducts)
            {
                DataCatalogProductoRequestDto dataCatalogProductRequestDto = new()
                {
                    guid = dataCatalog.DataCatalogProduct_guid.ToString(),
                    cantidadTotal = dataCatalog.DataCatalogProduct_countTotal,
                    fechaCreacion = dataCatalog.DataCatalogProduct_created,
                    fechaActualizacion = dataCatalog.DataCatalogProduct_updated,
                    pesoKg = dataCatalog.KG_Catalog.KGCatalog_cantidad,
                    precio = dataCatalog.Price_KG.PriceKG_price
                };
                datos.Add(dataCatalogProductRequestDto);
            }

            return Ok(datos);
        }

        [HttpGet("DetalleDataCatalogProduct/{guid}")]
        public async Task<IActionResult> DetalleDataCatalogProduct(string guid)
        {
            if (Guid.TryParse(guid, out Guid identificador))
            {
                try
                {
                    return Ok(await _serviceManagement._CatalogProductService.DetalleDataCatalogProduct(identificador));
                }
                catch (OperationAbortExceptions ex)
                {
                    return StatusCode(StatusCodes.Status404NotFound, ex.message);
                }
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "El identificador del producto no tiene el formato correcto");
        }

        [HttpPut("EditarDataCatalogProduct")]
        public async Task<IActionResult> EditarDataCatalogProduct([FromBody] CatalogProductDto catalogProduct)
        {
            CatalogProduction? catalogProduction = await _serviceManagement._CatalogProductService.Buscar(Guid.Parse(catalogProduct.identificador));

            if (catalogProduction is null)
                return StatusCode(StatusCodes.Status404NotFound, "No se ha podido identificar el producto, intentalo nuevamente.");

            catalogProduction.CatalogProduction_name = catalogProduct.nombreProducto;
            catalogProduction.CatalogProduction_updated = DateTime.Now;

            _serviceManagement._CatalogProductService.Actualizar(catalogProduction);

            return Ok("Catalogo Producto Editado Exitosamente");
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