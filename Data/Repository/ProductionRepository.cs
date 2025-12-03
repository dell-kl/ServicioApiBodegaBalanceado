
using Data;
using Data.Repository;
using Domain;
using ServicioApiBodegaBalanceado.Data.Repository.IRepository;

namespace ServicioApiBodegaBalanceado.Data.Repository
{
    public class ProductionRepository : RepositoryGeneric<Production>, IProduction
    {
        public ProductionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}