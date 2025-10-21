using Domain;
using Domain.DTO;
using Domain.DTO.RequestDto;
using Microsoft.AspNetCore.Http;

namespace Business.Services.IService
{
    public interface IRawMaterialService : IService<RawMaterialDto, RawMaterial>
    {
        public Task<ICollection<DataImage>> SaveImages(IEnumerable<IFormFile> formFiles, Guid guid);

        public Task AddStockRawMaterial(StockRawMaterial stockRawMaterial);

        public Task<RawMaterialDetailsRequestDto> GetDetailesRawMaterial(Guid guid);

        public Task EditDataRawMaterial(RawMaterial rawMaterial);

        public Task DeleteImages(ICollection<DataImage> images);
    }
}
