using Data.Repository.IRepository;
using ServicioApiBodegaBalanceado.Data.Repository;
using ServicioApiBodegaBalanceado.Data.Repository.IRepository;

namespace Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IRawMaterialRepository RawMaterialRepository { set; get; }
        public IKGMonitoring KgMonitoring { set; get; }
        public IAccounting Accounting { set; get; }
        public IImageRawMaterial ImageRawMaterialRepository { set; get; } // Agregado
        public ICatalogProduct CatalogProductRepository { set; get; }
        public IImageCatalogProduction ImageCatalogProductionRepository { set; get; }

        public IMaterialProduction MaterialProductionRepository { set; get; }

        public IDataCatalogProduct DataCatalogProductRepository { set; get; }

        public IProfile ProfileRepository { set; get; }

        public IProduction ProductionRepository { set; get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            this._context = context;
            this.RawMaterialRepository = new RawMaterialRepository(context);
            this.KgMonitoring = new KGMonitoringRepository(context);
            this.Accounting = new AccountingRepository(context);
            this.ImageRawMaterialRepository = new ImageRawMaterialRepository(context); // Instanciado
            this.CatalogProductRepository = new CatalogProductRepository(context);
            this.ImageCatalogProductionRepository = new ImageCatalogProductionRepository(context);
            this.DataCatalogProductRepository = new DataCatalogProductRepository(context);
            this.MaterialProductionRepository = new MaterialProductionRepository(context);
            this.ProfileRepository = new ProfileRepository(context);
            this.ProductionRepository = new ProductionRepository(context);
        }

        public void Dispose() => this._context.Dispose();
        public void Save() => this._context.SaveChanges();
    }
}
