using Microsoft.EntityFrameworkCore;
using TrucksApi.DataAccess;

namespace TrucksApi.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly TrucksContext _ctx;
    private readonly DbSet<T> _dbSet;

    public BaseRepository(TrucksContext ctx)
    {
        _ctx = ctx;
        _dbSet = ctx.Set<T>();
    }

    public async Task CommitChanges()
    {
        await _ctx.SaveChangesAsync();
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<List<T>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetById(string id)
    {
        return await _dbSet.FindAsync(id);
    }

    public IQueryable<T> Querry()
    {
        return _dbSet.AsQueryable();
    }

    public T Update(T entity)
    {
        return _dbSet.Update(entity).Entity;
    }
}
