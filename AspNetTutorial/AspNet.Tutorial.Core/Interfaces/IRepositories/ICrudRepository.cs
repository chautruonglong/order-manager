using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using AspNet.Tutorial.Core.Entities;
using AspNet.Tutorial.Core.Enums;

namespace AspNet.Tutorial.Core.Interfaces.IRepositories
{
    public interface ICrudRepository<TEntity, TKey> : IRepository where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetMany<TKeyOrder>(Expression<Func<TEntity, TKeyOrder>> order, SortDirections direction);

        IQueryable<TEntity> GetMany<TOrderKey>(int page, int size, Expression<Func<TEntity, TOrderKey>> order, SortDirections direction);

        Task<TEntity> Get(TKey key);

        Task<TEntity> Insert(TEntity entity);

        Task InsertMany(IEnumerable<TEntity> entities);

        Task<TEntity> Update(TEntity entity);

        Task UpdateMany(IEnumerable<TEntity> entities);

        Task<TEntity> SoftDelete(TKey key);

        Task<TEntity> HardDelete(TKey key);

        Task<long> Count();
    }
}
