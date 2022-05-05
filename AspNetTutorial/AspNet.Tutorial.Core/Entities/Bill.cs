using System.Collections.Generic;

namespace AspNet.Tutorial.Core.Entities
{
    public class Bill : BaseEntity
    {
        public double Total { get; set; }

        public ICollection<Order> Orders { get; set; }

        public Bill()
        {
            Orders = new List<Order>();
        }
    }
}
