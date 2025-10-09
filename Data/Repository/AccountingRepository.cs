using Data.Repository.IRepository;
using Domain;

namespace Data.Repository
{
    public class AccountingRepository : RepositoryGeneric<Accounting>, IAccounting
    {
        private readonly ApplicationDbContext _context;
        public AccountingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        // M�todos espec�ficos de Accounting pueden agregarse aqu�
    }
}
