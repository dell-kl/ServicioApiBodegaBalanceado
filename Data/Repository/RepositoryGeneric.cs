using Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace Data.Repository
{
    public class RepositoryGeneric<T> : IRepositoryGeneric<T> where T : class
    {

        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet { set; get; }

        public RepositoryGeneric(ApplicationDbContext context)
        {
            this._context = context;
            dbSet = this._context.Set<T>();
        }

        public async Task Create(T entity)
        {

            await this.dbSet.AddAsync(entity);
        }

        public async Task CreateAll(IEnumerable<T> entities)
        {
            await this.dbSet.AddRangeAsync(entities);
        }

        public async Task<T> Buscar(Expression<Func<T, bool>> predicate, string includeProperties = null)
        {
            IQueryable<T> resultado = this.dbSet.Where(predicate);

            if (!includeProperties.IsNullOrEmpty())
            {
                string[] properties = includeProperties.Split(",");

                foreach (var propertie in properties)
                {
                    resultado = resultado.Include(propertie.Trim());
                }
            }

            return await resultado.FirstAsync();
        }

        public async Task<IEnumerable<T>> Buscar(int skip, string includeProperties = null)
        {
            IQueryable<T> resultado = this.dbSet.Skip(skip)
                .Take(10);

            if (!includeProperties.IsNullOrEmpty())
            {
                string[] properties = includeProperties.Split(",");

                foreach (var propertie in properties)
                {
                    resultado = resultado.Include(propertie.Trim());
                }
            }

            return resultado.ToList();
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
