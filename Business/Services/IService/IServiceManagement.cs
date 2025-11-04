using Business.Services.ProductService;

namespace Business.Services.IService
{
    public interface IServiceManagement
    {
        public IRawMaterialService _RawMaterialService { get; }

        public IKGMonitoringService _KgMonitoringService { get; }

        public ICatalogProductService _CatalogProductService { get; }
    }
}
