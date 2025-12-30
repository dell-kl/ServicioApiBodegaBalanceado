using System.Threading.Tasks;
using Business.Services.IService;
using Domain;
using Domain.DTO;
using Domain.DTO.RequestDto;
using Microsoft.AspNetCore.Mvc;
using Utility.Exceptions;

namespace ServicioApiBodegaBalanceado.Controllers;

[Route("api/{controller}")]
[ApiController]
public class ManufactureController : ControllerBase
{
    private readonly IServiceManagement _serviceManagement;

    public ManufactureController(IServiceManagement serviceManagement)
    {
        this._serviceManagement = serviceManagement;
    }
    
    // GET
    [HttpGet("ObtenerDatosProduccion/{skip}")]
    public async Task<IActionResult> ObtenerDatosProduccion(int skip)
    {
        IEnumerable<Production> listado = await this._serviceManagement._ProductionService.Obtener(skip, "CatalogProduction");

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

    // POST
    [HttpPost("GenerarProduccion")]
    public async Task<IActionResult> GenerarProduccion([FromBody] ProductionDto productionDto)
    {
        try
        {
            await this._serviceManagement._ProductionService.generarProduccion(productionDto);
            return Ok("Produccion generada correctamente");
        }
        catch (OperationAbortExceptions exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, exception.message);
        }
    }


    // PUT  
    // Editar el estado de la produccion.
    [HttpPut("EditarEstadoProduccion")]
    public async Task<IActionResult> EditarEstadoProduccion([FromBody] IEnumerable<ProductionRequestDto> productionsDto)
    {
        await this._serviceManagement._ProductionService.editarEstadoProduccion(productionsDto);

        return Ok("Estados de produccion actualizados correctamente");
    }
}