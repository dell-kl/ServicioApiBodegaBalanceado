using System.Linq.Expressions;

namespace Data.Repository.IRepository
{
    public interface IRepositoryGeneric<T> where T : class
    {
        public IEnumerable<T> Get(string includeProperties);

        public Task Create(T entity);

        public Task CreateAll(IEnumerable<T> entities);

        public Task<T> Buscar(Expression<Func<T, bool>> predicate, string includeProperties = null);

        public Task<IEnumerable<T>> Buscar(int skip, string includeProperties = null);

        public Task<IEnumerable<T>> Buscar(Expression<Func<T, bool>> predicate, int skip, string includeProperties = null);

        public void Update(T entity);

        public Task UpdateAll(IEnumerable<T> entities);

        public void Delete(T entity);
    }
}
