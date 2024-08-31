using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasConversion(
                    orderItemId => orderItemId.Value, 
                    dbId => OrderItemId.Of(dbId));

            // Each order item relates to One product
            // But many products can relate to all of different order items
            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(o => o.ProductId);

            builder.Property(o => o.Quantity).IsRequired();
            builder.Property(o => o.Price).IsRequired();
        }
    }
}