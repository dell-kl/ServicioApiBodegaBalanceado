using Data.Repository.IRepository;
using Domain;

namespace Data.Repository
{
    public class MaterialProductionRepository : RepositoryGeneric<MaterialProduction>, IMaterialProduction
    {
        private readonly ApplicationDbContext _context;

        public MaterialProductionRepository(ApplicationDbContext context)
        : base(context)
        {
            _context = context;
        }

    }
}