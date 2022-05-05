using System.Collections.Generic;

using AspNet.Tutorial.Core.DataTransfers.Orders;

namespace AspNet.Tutorial.Core.DataTransfers.Bills
{
    public class BillCreationDto
    {
        public double Total { get; set; }
        
        public IEnumerable<OrderCreationDto> Orders { get; set; }
    }
}
