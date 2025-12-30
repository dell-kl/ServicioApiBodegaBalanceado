using Data.Repository.IRepository;
using Domain;

namespace Data.Repository;

public class ProductManufacturedRepository : RepositoryGeneric<ProductManufactured>, IProductManufactured
{
    private readonly ApplicationDbContext _context;
    
    public ProductManufacturedRepository(ApplicationDbContext context) : base(context)
    {
        this._context = context;
    }
}