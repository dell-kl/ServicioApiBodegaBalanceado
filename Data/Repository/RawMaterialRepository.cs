using Data.Repository.IRepository;
using Domain;

namespace Data.Repository
{
    public class RawMaterialRepository :  RepositoryGeneric<RawMaterial>, IRawMaterialRepository
    {
        private readonly ApplicationDbContext _context;

        public RawMaterialRepository(ApplicationDbContext context)
        : base(context)
        {
            _context = context;
        }


    }
}
