using AspNet.Tutorial.Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNet.Tutorial.Infrastructure.Configurations
{
    public class BillConfiguration : BaseEntityConfiguration<Bill>
    {
        public override void Configure(EntityTypeBuilder<Bill> builder)
        {
            base.Configure(builder);

            builder.ToTable("Bills");

            builder
                .Property(b => b.Total)
                .HasDefaultValue(0)
                .IsRequired();
        }
    }
}
