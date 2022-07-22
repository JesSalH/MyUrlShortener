using System.Linq.Expressions;

namespace MyUrlShortener.DataAccess.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        void Remove(T entity);

        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null);

        T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null);
    }
}
