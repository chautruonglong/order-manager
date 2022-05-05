using System.IO;

using AspNet.Tutorial.Core.Entities;
using AspNet.Tutorial.Infrastructure.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace AspNet.Tutorial.Infrastructure.Configurations
{
    public class ProductConfiguration : BaseEntityConfiguration<Product>
    {
        private readonly IConfiguration _configuration;

        public ProductConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasJsonData($"{_configuration["StaticPath"]}{Path.DirectorySeparatorChar}products");

            builder
                .Property(p => p.Name)
                .IsUnicode()
                .IsRequired();

            builder
                .Property(p => p.Image)
                .IsRequired();

            builder
                .Property(p => p.Price)
                .IsRequired();

            builder
                .Property(p => p.Discount)
                .IsRequired();
        }
    }
}
