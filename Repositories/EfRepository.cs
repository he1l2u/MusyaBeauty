using BeautySalonApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BeautySalonApp.Repositories;

public class EfRepository<T> : IRepository<T> where T : class
{
    private readonly SalonDbContext _db;

    public EfRepository(SalonDbContext db)
    {
        _db = db;
    }

    public Task<List<T>> GetAllAsync()
    {
        return _db.Set<T>().AsNoTracking().ToListAsync();
    }

    public Task<T?> GetByIdAsync(int id)
    {
        return _db.Set<T>().FindAsync(id).AsTask();
    }

    public Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return _db.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _db.Set<T>().AddAsync(entity);
    }

    public Task SaveChangesAsync()
    {
        return _db.SaveChangesAsync();
    }
}
