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
    public IDataCatalogProduct DataCatalogProductRepository { get; }
        public void Save();
    }
}
