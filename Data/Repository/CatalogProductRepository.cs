using Data.Repository.IRepository;
using Domain;

namespace Data.Repository
{
    public class CatalogProductRepository : RepositoryGeneric<CatalogProduction>, ICatalogProduct
    {
        private readonly ApplicationDbContext _context;

        public CatalogProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
