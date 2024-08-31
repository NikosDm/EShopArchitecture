using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id).HasConversion(
                            orderId => orderId.Value,
                            dbId => OrderId.Of(dbId));

            // Each order belongs to one Customer. 
            // However a Customer can have more than one orders. 
            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .IsRequired();

            builder.HasIndex(i => i.CustomerId);

            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId);

            // For OrderName, Billing and Shipping Address we use the complex property.
            builder.ComplexProperty(
                o => o.OrderName, nameBuilder =>
                {
                    nameBuilder.IsRequired();
                    nameBuilder.Property(n => n.Value)
                        .HasColumnName(nameof(Order.OrderName))
                        .HasMaxLength(100)
                        .IsRequired();
                });

            builder.ComplexProperty(
                o => o.ShippingAddress, addressBuilder =>
                {
                    addressBuilder.IsRequired();

                    addressBuilder.Property(a => a.FirstName)
                        .HasMaxLength(50)
                        .IsRequired();

                    addressBuilder.Property(a => a.LastName)
                        .HasMaxLength(50)
                        .IsRequired();

                    addressBuilder.Property(a => a.EmailAddress)
                        .HasMaxLength(50);

                    addressBuilder.Property(a => a.AddressLine)
                        .HasMaxLength(180)
                        .IsRequired();

                    addressBuilder.Property(a => a.Country)
                        .HasMaxLength(50);

                    addressBuilder.Property(a => a.State)
                        .HasMaxLength(50);

                    addressBuilder.Property(a => a.ZipCode)
                        .HasMaxLength(5)
                        .IsRequired();
                });

            builder.ComplexProperty(
                o => o.BillingAddress, addressBuilder =>
                {
                    addressBuilder.IsRequired();

                    addressBuilder.Property(a => a.FirstName)
                        .HasMaxLength(50)
                        .IsRequired();

                    addressBuilder.Property(a => a.LastName)
                        .HasMaxLength(50)
                        .IsRequired();

                    addressBuilder.Property(a => a.EmailAddress)
                        .HasMaxLength(50);

                    addressBuilder.Property(a => a.AddressLine)
                        .HasMaxLength(180)
                        .IsRequired();

                    addressBuilder.Property(a => a.Country)
                        .HasMaxLength(50);

                    addressBuilder.Property(a => a.State)
                        .HasMaxLength(50);

                    addressBuilder.Property(a => a.ZipCode)
                        .HasMaxLength(5)
                        .IsRequired();
                });

            builder.ComplexProperty(
                o => o.Payment, paymentBuilder =>
                {
                    paymentBuilder.IsRequired(); // EF Core is complaining that Payment is optional but it should be required. That is why this was added. 

                    paymentBuilder.Property(p => p.CardName)
                        .HasMaxLength(50);

                    paymentBuilder.Property(p => p.CardNumber)
                        .HasMaxLength(24)
                        .IsRequired();

                    paymentBuilder.Property(p => p.Expiration)
                        .HasMaxLength(10);

                    paymentBuilder.Property(p => p.CVV)
                        .HasMaxLength(3);

                    paymentBuilder.Property(p => p.PaymentMethod);
                });

            builder.Property(o => o.Status)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(
                    s => s.ToString(),
                    dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

            builder.Property(o => o.TotalPrice).HasPrecision(18, 2);
        }
    }
}