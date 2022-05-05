using System;

using AspNet.Tutorial.Core.Entities;
using AspNet.Tutorial.Core.Interfaces.IRepositories;
using AspNet.Tutorial.Infrastructure.Contexts;

using AutoMapper;

namespace AspNet.Tutorial.Infrastructure.Repositories
{
    public class OrderRepository : CrudRepository<Order, Guid>, IOrderRepository
    {
        private readonly AspNetTutorialContext _context;

        public OrderRepository(AspNetTutorialContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
        }
    }
}
