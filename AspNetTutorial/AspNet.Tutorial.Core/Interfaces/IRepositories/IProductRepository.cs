using System;
using System.Linq;
using System.Linq.Expressions;

using AspNet.Tutorial.Core.Entities;
using AspNet.Tutorial.Core.Enums;

namespace AspNet.Tutorial.Core.Interfaces.IRepositories
{
    public interface IProductRepository : ICrudRepository<Product, Guid>
    {
        IQueryable<Product> Search<TOrderKey>(int page, int size, string question, Expression<Func<Product, TOrderKey>> order, SortDirections direction);
    }
}
