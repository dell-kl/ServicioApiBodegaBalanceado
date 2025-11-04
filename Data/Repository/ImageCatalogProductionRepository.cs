using Data.Repository.IRepository;
using Domain;

namespace Data.Repository
{
    public class ImageCatalogProductionRepository : RepositoryGeneric<ImageCatalogProduction>, IImageCatalogProduction
    {
        public ImageCatalogProductionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
