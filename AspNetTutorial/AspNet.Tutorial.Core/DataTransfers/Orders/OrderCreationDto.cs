using System;

namespace AspNet.Tutorial.Core.DataTransfers.Orders
{
    public class OrderCreationDto
    {
        public long Quantity { get; set; }
        
        public Guid ProductId { get; set; }
    }
}
