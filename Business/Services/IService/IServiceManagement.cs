using Business.Services.ProductService;

namespace Business.Services.IService
{
    public interface IServiceManagement
    {
        public IRawMaterialService _RawMaterialService { get; }
    }
}
