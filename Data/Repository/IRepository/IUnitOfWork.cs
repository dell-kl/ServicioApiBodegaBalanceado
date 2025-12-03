using ServicioApiBodegaBalanceado.Data.Repository.IRepository;

namespace Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        public IRawMaterialRepository RawMaterialRepository { get; }
        public IKGMonitoring KgMonitoring { get; }
        public IAccounting Accounting { get; }
        public IImageRawMaterial ImageRawMaterialRepository { get; } // Agregado
        public ICatalogProduct CatalogProductRepository { get; }
        public IImageCatalogProduction ImageCatalogProductionRepository { get; }
        public IMaterialProduction MaterialProductionRepository { get; }

        public IDataCatalogProduct DataCatalogProductRepository { get; }

        public IProfile ProfileRepository { get; }

        public IProduction ProductionRepository { get; }

        public void Save();
    }
}
