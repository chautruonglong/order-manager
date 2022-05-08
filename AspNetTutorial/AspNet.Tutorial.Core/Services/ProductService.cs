using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AspNet.Tutorial.Core.DataTransfers.Products;
using AspNet.Tutorial.Core.Entities;
using AspNet.Tutorial.Core.Enums;
using AspNet.Tutorial.Core.Interfaces.IRepositories;
using AspNet.Tutorial.Core.Interfaces.IServices;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

namespace AspNet.Tutorial.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Product> Get(Guid id)
        {
            return await _productRepository.Get(id);
        }

        public async Task<IEnumerable<Product>> GetMany(int page, int size, ProductSorts sortType)
        {
            IQueryable<Product> products;

            switch (sortType)
            {
                case ProductSorts.NameAsc:
                    products = _productRepository.GetMany(page, size, e => e.Name, SortDirections.Asc);
                    break;

                case ProductSorts.NameDesc:
                    products = _productRepository.GetMany(page, size, e => e.Name, SortDirections.Desc);
                    break;

                case ProductSorts.CreatedAtAsc:
                    products = _productRepository.GetMany(page, size, e => e.CreatedAt, SortDirections.Asc);
                    break;

                case ProductSorts.CreatedAtDesc:
                    products = _productRepository.GetMany(page, size, e => e.CreatedAt, SortDirections.Desc);
                    break;

                case ProductSorts.ModifiedAtAsc:
                    products = _productRepository.GetMany(page, size, e => e.ModifiedAt, SortDirections.Asc);
                    break;

                case ProductSorts.ModifiedAtDesc:
                default:
                    products = _productRepository.GetMany(page, size, e => e.ModifiedAt, SortDirections.Desc);
                    break;
            }

            return await products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> Search(int page, int size, string question, ProductSorts sortType)
        {
            IQueryable<Product> products;

            switch (sortType)
            {
                case ProductSorts.NameAsc:
                    products = _productRepository.Search(page, size, question, e => e.Name, SortDirections.Asc);
                    break;

                case ProductSorts.NameDesc:
                    products = _productRepository.Search(page, size, question, e => e.Name, SortDirections.Desc);
                    break;

                case ProductSorts.CreatedAtAsc:
                    products = _productRepository.Search(page, size, question, e => e.CreatedAt, SortDirections.Asc);
                    break;

                case ProductSorts.CreatedAtDesc:
                    products = _productRepository.Search(page, size, question, e => e.CreatedAt, SortDirections.Desc);
                    break;

                case ProductSorts.ModifiedAtAsc:
                    products = _productRepository.Search(page, size, question, e => e.ModifiedAt, SortDirections.Asc);
                    break;

                case ProductSorts.ModifiedAtDesc:
                default:
                    products = _productRepository.Search(page, size, question, e => e.ModifiedAt, SortDirections.Desc);
                    break;
            }

            return await products.ToListAsync();
        }

        public async Task<Product> Create(ProductCreationDto productCreationDto)
        {
            Product product = _mapper.Map<Product>(productCreationDto);
            return await _productRepository.Insert(product);
        }

        public async Task<Product> Update(Guid id, ProductModificationDto productModificationDto)
        {
            Product product = _mapper.Map<Product>(productModificationDto);
            product.Id = id;

            Product dbProduct = await _productRepository.Get(id);

            _mapper.Map(product, dbProduct);

            return await _productRepository.Update(dbProduct);
        }

        public async Task<Product> Delete(Guid id)
        {
            return await _productRepository.SoftDelete(id);
        }
    }
}
