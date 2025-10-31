using Domain;
using Domain.DTO;
using Microsoft.AspNetCore.Http;
using ServicioApiBodegaBalanceado.Domain.DTO;

namespace Business.Services.IService
{
    public interface ICatalogProductService : IService<CatalogProductDto, CatalogProduction>
    {
        public Task<ICollection<DataImageDto>> SaveImages(IEnumerable<IFormFile> formFiles, Guid guid);

        public Task DeleteImages(ICollection<DataImageDto> images);

        public Task AgregateDataProduct(CatalogProductDto catalogProductDto);
    }
}