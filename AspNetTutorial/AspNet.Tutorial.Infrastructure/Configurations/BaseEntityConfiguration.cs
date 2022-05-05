using System;

using AspNet.Tutorial.Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNet.Tutorial.Infrastructure.Configurations
{
    public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(b => b.Id);

            builder
                .Property(b => b.CreatedAt)
                .HasDefaultValue(DateTime.UtcNow)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder
                .Property(b => b.ModifiedAt)
                .HasDefaultValue(DateTime.UtcNow)
                .ValueGeneratedOnAddOrUpdate()
                .IsRequired();

            builder
                .Property(b => b.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();
        }
    }
}
