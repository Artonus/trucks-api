using Azure.Identity;

namespace TrucksApi.Repositories;

public interface IBaseRepository<T> where T: class
{
    Task<List<T>> GetAll();
    IQueryable<T> Querry();
    Task<T?> GetById(string id);
    T Update(T entity);
    void Delete(T entity);

    Task CommitChanges();
}
