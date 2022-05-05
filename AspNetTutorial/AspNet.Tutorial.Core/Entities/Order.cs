using System;

namespace AspNet.Tutorial.Core.Entities
{
    public class Order : BaseEntity
    {
        public long Quantity { get; set; }

        public Guid ProductId { get; set; }

        public Guid UserId { get; set; }

        public Guid BillId { get; set; }

        public Product Product { get; set; }

        public User User { get; set; }

        public Bill Bill { get; set; }
    }
}
