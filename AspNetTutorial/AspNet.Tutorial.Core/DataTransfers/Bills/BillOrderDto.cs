using System;
using System.Collections.Generic;

using AspNet.Tutorial.Core.DataTransfers.Orders;

namespace AspNet.Tutorial.Core.DataTransfers.Bills
{
    public class BillOrderDto
    {
        public Guid Id { get; set; }

        public IEnumerable<OrderProductDto> Orders { get; set; }

        public double Total { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }
    }
}
