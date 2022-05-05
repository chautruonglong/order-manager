using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AspNet.Tutorial.Core.DataTransfers.Products;
using AspNet.Tutorial.Core.Entities;
using AspNet.Tutorial.Core.Enums;

namespace AspNet.Tutorial.Core.Interfaces.IServices
{
    public interface IProductService : IService
    {
        Task<Product> Get(Guid id);

        Task<IEnumerable<Product>> GetMany(int page, int size, ProductSorts sortType);

        Task<IEnumerable<Product>> Search(int page, int size, string question, ProductSorts sortType);

        Task<Product> Create(ProductCreationDto productCreationDto);

        Task<Product> Update(Guid id, ProductModificationDto productModificationDto);

        Task<Product> Delete(Guid id);
    }
}
