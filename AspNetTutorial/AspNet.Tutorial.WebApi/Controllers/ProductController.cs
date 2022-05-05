using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

using AspNet.Tutorial.Core.DataTransfers.Products;
using AspNet.Tutorial.Core.Entities;
using AspNet.Tutorial.Core.Enums;
using AspNet.Tutorial.Core.Exceptions;
using AspNet.Tutorial.Core.Interfaces.IServices;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AspNet.Tutorial.WebApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        private readonly IProductService _productService;

        public ProductController(IProductService productService, IMapper mapper, IConfiguration configuration)
        {
            _productService = productService;
            _mapper = mapper;
            _configuration = configuration;
        }

        // [ResponseCache(Duration = 60)]
        [HttpGet]
        public async Task<IActionResult> GetMany([FromQuery] int page, [FromQuery] int size, [FromQuery] string question, [FromQuery] string sort)
        {
            if (page < 0)
            {
                throw new PaginationException("Page must be greater than or equal to 0");
            }

            if (size < 1)
            {
                throw new PaginationException("Size must be greater than or equal to 1");
            }

            sort ??= ProductSorts.ModifiedAtDesc.ToString();
            if (!Enum.TryParse(sort.Replace("_", ""), true, out ProductSorts sortType))
            {
                throw new SortTypeException("Product sort type is not accepted");
            }

            IEnumerable<Product> products;
            if (String.IsNullOrEmpty(question))
            {
                products = await _productService.GetMany(page, size, sortType);
            }
            else
            {
                products = await _productService.Search(page, size, question, sortType);
            }

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out Guid guid))
            {
                throw new RecordNotFoundException("Product not found in database");
            }

            Product product = await _productService.Get(guid);
            return Ok(_mapper.Map<ProductDto>(product));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            IFormFile file = Request.Form.Files["image"];

            if (file == null)
            {
                throw new ValidationException("Image is required");
            }

            string ext = Path.GetExtension(file.FileName);
            string filename = $"{Guid.NewGuid().ToString()}{ext}";

            ProductCreationDto productCreationDto = new ProductCreationDto
            {
                Name = Request.Form["name"],
                Description = Request.Form["description"],
                Price = double.Parse(Request.Form["price"]),
                Discount = double.Parse(Request.Form["discount"]),
                Image = filename
            };

            Validator.ValidateObject(productCreationDto, new ValidationContext(productCreationDto));

            string savedPath = $"{_configuration["StaticPath"]}{Path.DirectorySeparatorChar}products{Path.DirectorySeparatorChar}{filename}";
            using (FileStream stream = new FileStream(savedPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            Product product = await _productService.Create(productCreationDto);
            return Created(new Uri($"{Request.Host}/api/products/{product.Id}"), _mapper.Map<ProductDto>(product));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] ProductModificationDto productModificationDto)
        {
            if (!Guid.TryParse(id, out Guid guid))
            {
                throw new RecordNotFoundException("Product not found in database");
            }

            Product product = await _productService.Update(guid, productModificationDto);
            return Ok(_mapper.Map<ProductDto>(product));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out Guid guid))
            {
                throw new RecordNotFoundException("Product not found in database");
            }

            Product product = await _productService.Delete(guid);
            return Ok(_mapper.Map<ProductDto>(product));
        }
    }
}
