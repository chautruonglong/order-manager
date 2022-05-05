using System;
using System.Linq;
using System.Linq.Expressions;

using AspNet.Tutorial.Core.Entities;
using AspNet.Tutorial.Core.Enums;
using AspNet.Tutorial.Core.Interfaces.IRepositories;
using AspNet.Tutorial.Infrastructure.Contexts;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

namespace AspNet.Tutorial.Infrastructure.Repositories
{
    public class ProductRepository : CrudRepository<Product, Guid>, IProductRepository
    {
        private readonly AspNetTutorialContext _context;

        public ProductRepository(AspNetTutorialContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
        }

        public IQueryable<Product> Search<TOrderKey>(int page, int size, string question, Expression<Func<Product, TOrderKey>> order, SortDirections direction)
        {
            return GetMany(order, direction)
                .Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{question.ToLower()}%") ||
                            EF.Functions.Like(p.Description.ToLower(), $"%{question.ToLower()}%"))
                .Skip(page * size)
                .Take(size);
        }
    }
}
