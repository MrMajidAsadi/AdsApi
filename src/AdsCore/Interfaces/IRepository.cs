using Ads.Core.Entities;

namespace Ads.Core.Interfaces;

public interface IRepository<T> where T : BaseEntity, IAggregateRoot
{
    IQueryable<T> GetAll();
    Task<T?> Get(int id);
    Task Add(T entity);
    Task Update(T entity);
    Task Remove(T entity);
}