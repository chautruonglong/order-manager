using AspNet.Tutorial.Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNet.Tutorial.Infrastructure.Configurations
{
    public class OrderConfiguration : BaseEntityConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder
                .Property(o => o.BillId)
                .IsRequired();

            builder
                .Property(o => o.ProductId)
                .IsRequired();

            builder
                .Property(o => o.UserId)
                .IsRequired();
        }
    }
}
