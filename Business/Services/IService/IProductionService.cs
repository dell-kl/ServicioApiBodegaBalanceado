using Domain;
using Domain.DTO;
using Domain.DTO.RequestDto;

namespace Business.Services.IService
{
    public interface IProductionService : IService<ProductionDto, Production>
    {
        public Task generarProduccion(ProductionDto productionDto);

        public Task editarEstadoProduccion(IEnumerable<ProductionRequestDto> productionRequestDto);
    }
}