using Domain;
using Domain.DTO;
using Domain.DTO.RequestDto;
using Microsoft.AspNetCore.Http;
using ServicioApiBodegaBalanceado.Domain.DTO;

namespace Business.Services.IService
{
    public interface ICatalogProductService : IService<CatalogProductDto, CatalogProduction>
    {
        public Task<ICollection<DataImageDto>> SaveImages(IEnumerable<IFormFile> formFiles, Guid guid);

        public Task DeleteImages(ICollection<DataImageDto> images);

        public Task AgregateDataProduct(CatalogProductDto catalogProductDto);

        public Task<IEnumerable<DataCatalogProduct>> ObtenerDataCatalogProduct(int skip, string data, int idCatalogProduct);

        public Task<CatalogProductDetailsRequestDto> DetalleDataCatalogProduct(Guid guid);
    }
}