using System;

using AspNet.Tutorial.Core.DataTransfers.Products;

namespace AspNet.Tutorial.Core.DataTransfers.Orders
{
    public class OrderProductDto
    {
        public Guid Id { get; set; }

        public long Quantity { get; set; }

        public ProductDto Product { get; set; }
    }
}
