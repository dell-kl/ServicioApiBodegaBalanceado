using Business.Services.IService;
using Domain;
using Domain.DTO;
using Domain.DTO.RequestDto;
using Microsoft.AspNetCore.Mvc;

namespace ServicioApiBodegaBalanceado.Controllers;

[Route("api/{controller}")]
[ApiController]
public class ReadyProductsManufacturedController : ControllerBase
{
    private readonly IServiceManagement _serviceManagement;
    
    public ReadyProductsManufacturedController(IServiceManagement serviceManagement)
    {
        this._serviceManagement = serviceManagement;
    }

    [HttpGet("GetProductionReady/{skip}")]
    public async Task<IActionResult> GetProductionReady(int skip)
    {
        IEnumerable<Production> listado = await _serviceManagement._ManufacturedServices.Obtener(skip, "CatalogProduction");
        
        IEnumerable<ProductionRequestDto> listadoDto = listado.Select(item =>
        {
            return new ProductionRequestDto()
            {
                Identificador = item.Production_guid.ToString(),
                IdentificadorCatalogoProduccion = item.CatalogProduction.CatalogProduction_guid.ToString(),
                KgTotal = item.Production_KGTotal,
                Estado = (int)item.Production_status,
                NombreProducto = item.Production_name,
                TipoProduccion = item.CatalogProduction.CatalogProduction_name,
                Creado = item.Production_created,
                Actualizado = item.Production_updated
            };
        });

        return Ok(listadoDto);
    }

    [HttpGet("GetManufactured/{identificador}")]
    public async Task<IActionResult> ObtenerDataCatalogProducto(string identificador)
    {
       IEnumerable<DataCatalogProduct> listadoDataCatalogProducts = await _serviceManagement._ManufacturedServices.GetDataCatalogProduct(identificador);
        
        return Ok(listadoDataCatalogProducts);
    }

    [HttpPost("IngresarProductosABodega")]
    public async Task<IActionResult> IngresarProductosABodega([FromBody] ManufacturedDto manufactured)
    {
        try
        {
            _serviceManagement._ManufacturedServices.Agregate(manufactured);
            return Ok("Productos guardados correctamente");
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error del servidor, no se puedo guardar tus productos");
        }

    }
}