using System;

using AspNet.Tutorial.Core.Entities;

namespace AspNet.Tutorial.Core.Interfaces.IRepositories
{
    public interface IOrderRepository : ICrudRepository<Order, Guid>
    {
    }
}
