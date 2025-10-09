using Data.Repository.IRepository;
using Domain;

namespace Data.Repository
{
    public class KGMonitoringRepository : RepositoryGeneric<KgMonitoring>, IKGMonitoring
    {
        private readonly ApplicationDbContext _context;
        public KGMonitoringRepository(ApplicationDbContext context) : base(context) { 
            _context = context;
        }
    }
}
