using System.Linq.Expressions;

namespace Contracts
{
    public interface IRepositoryBase<T>//a base interface for all repositories to CRUD operations
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
