using Ads.Core.Entities;
using Ads.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ads.Infrastructure.Data;

public class EfRepository<T> : IRepository<T> where T : BaseEntity, IAggregateRoot
{
    private readonly AdsDbContext _db;
    private readonly DbSet<T> _entities;

    public EfRepository(AdsDbContext db)
    {
        _db = db;
        _entities = db.Set<T>();
    }

    public async Task Add(T entity)
    {
        await _db.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public Task<T?> Get(int id)
    {
        return _entities.SingleOrDefaultAsync(e => e.Id == id);
    }

    public IQueryable<T> GetAll()
    {
        return _entities.AsQueryable();
    }

    public async Task Remove(T entity)
    {
        _entities.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        _entities.Update(entity);
        await _db.SaveChangesAsync();
    }
}