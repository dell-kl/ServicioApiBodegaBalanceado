using Domain;
using Domain.DTO;
using Microsoft.AspNetCore.Http;

namespace Business.Services.IService
{
    public interface IRawMaterialService : IService<RawMaterialDto, RawMaterial>
    {
        public Task SaveImages(IEnumerable<IFormFile> formFiles, Guid guid);

    }
}
