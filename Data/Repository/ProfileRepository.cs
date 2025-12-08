using Data;
using Data.Repository;
using Domain;
using ServicioApiBodegaBalanceado.Data.Repository.IRepository;

namespace ServicioApiBodegaBalanceado.Data.Repository
{
    public class ProfileRepository : RepositoryGeneric<Profile>, IProfile
    {
        private readonly ApplicationDbContext _context;

        public ProfileRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}