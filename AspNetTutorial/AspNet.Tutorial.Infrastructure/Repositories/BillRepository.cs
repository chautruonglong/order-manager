using System;

using AspNet.Tutorial.Core.Entities;
using AspNet.Tutorial.Core.Interfaces.IRepositories;
using AspNet.Tutorial.Infrastructure.Contexts;

using AutoMapper;

namespace AspNet.Tutorial.Infrastructure.Repositories
{
    public class BillRepository : CrudRepository<Bill, Guid>, IBillRepository
    {
        private readonly AspNetTutorialContext _context;

        public BillRepository(AspNetTutorialContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
        }
    }
}
