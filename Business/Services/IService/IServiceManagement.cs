using Business.Services.ProductService;
using ServicioApiBodegaBalanceado.Business.Services.IService;

namespace Business.Services.IService
{
    public interface IServiceManagement
    {
        public IRawMaterialService _RawMaterialService { get; }

        public IKGMonitoringService _KgMonitoringService { get; }

        public ICatalogProductService _CatalogProductService { get; }

        public IProductionService _ProductionService { get; }
        
        public IManufacturedServices _ManufacturedServices { get; }
    }
}
