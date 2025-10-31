using Data.Repository.IRepository;
using Domain;

namespace Data.Repository
{
    public class DataCatalogProductRepository : RepositoryGeneric<DataCatalogProduct>, IDataCatalogProduct
    {
        public DataCatalogProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
