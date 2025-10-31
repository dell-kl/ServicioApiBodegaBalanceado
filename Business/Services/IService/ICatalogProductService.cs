using Domain;
using Domain.DTO;

namespace Business.Services.IService
{
    public interface ICatalogProductService : IService<CatalogProductDto, CatalogProduction>
    {
        public Task AgregateDataProduct(CatalogProductDto catalogProductDto);
    }
}