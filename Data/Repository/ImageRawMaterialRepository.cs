using Data.Repository.IRepository;
using Domain;

namespace Data.Repository
{
    public class ImageRawMaterialRepository : RepositoryGeneric<ImageRawMaterial>, IImageRawMaterial
    {
        public ImageRawMaterialRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
