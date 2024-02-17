
namespace DataAccess.Repositories.Abstract;

public interface IBaseRepository<T> where T : class
{
    Task<List<T>> GetAll();
    IQueryable<T> Query();
    Task<T?> GetById(string id);
    Task<T?> Update(string id, T entity);
    void Delete(T entity);
    Task<T> Add(T entity);
    Task CommitChanges();
}
