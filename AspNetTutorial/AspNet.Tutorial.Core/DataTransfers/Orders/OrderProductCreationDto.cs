using AspNet.Tutorial.Core.DataTransfers.Products;

namespace AspNet.Tutorial.Core.DataTransfers.Orders
{
    public class OrderProductCreationDto
    {
        public long Quantity { get; set; }

        public ProductDto Product { get; set; }
    }
}
