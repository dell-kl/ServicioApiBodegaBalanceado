using Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace Data.Repository
{
    public class RepositoryGeneric<T>  : IRepositoryGeneric<T> where T: class
    {

        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet { set; get; }

        public RepositoryGeneric(ApplicationDbContext context) {
            this._context = context;
            dbSet = this._context.Set<T>();
        }

        public async Task Create(T entity) {
            
            await this.dbSet.AddAsync(entity); 
        }

        public async Task CreateAll(IEnumerable<T> entities)
        {
            await this.dbSet.AddRangeAsync(entities);
        }

        public async Task<T> Buscar(Expression<Func<T, bool>> predicate, string includeProperties = null)
        {
            T resultado;

            if (includeProperties.IsNullOrEmpty())
                resultado = await this.dbSet.Where(predicate).FirstAsync();
            else
                resultado = await this.dbSet.Where(predicate)
                        .Include(includeProperties)
                        .FirstAsync();

            return resultado;   
        }

        public async Task<IEnumerable<T>> Buscar(int skip, string includeProperties = null)
        {
            var resultados =  this.dbSet
                                .Skip(skip)
                                .Take(10)
                                .Include(includeProperties)
                                .ToList();

            return resultados;
        }

        public void Update(T entity) => this.dbSet.Update(entity);

        public IEnumerable<T> Get(string includeProperties)
        {
            return this.dbSet
                .Include(includeProperties)
                .ToList();
        }

        public void Delete(T entity)
        {
            this.dbSet.Remove(entity);
        }
    }
}
