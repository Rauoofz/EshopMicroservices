using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasConversion(
            orderId => orderId.Value,
            dbId => OrderId.Of(dbId));

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(o => o.CustomerId);

        builder.HasMany<OrderItem>()
            .WithOne()
            .HasForeignKey(oi => oi.OrderId);

        builder.ComplexProperty(
            o => o.OrderName, nameBuilder =>
            {
                nameBuilder.Property(o => o.Value)
                .HasColumnName(nameof(Order.OrderName))
                .HasMaxLength(100)
                .IsRequired();
            });

        builder.ComplexProperty(
            o => o.ShippingAddress, AddressBuilder =>
            {
                AddressBuilder.Property(a => a.FirstName)
                .HasMaxLength(50)
                .IsRequired();

                AddressBuilder.Property(a => a.LastName)
                .HasMaxLength(50)
                .IsRequired();

                AddressBuilder.Property(a => a.AddressLine)
                .HasMaxLength(180)
                .IsRequired();

                AddressBuilder.Property(a => a.EmailAddress)
                .HasMaxLength(50);

                AddressBuilder.Property(a => a.Counrty)
                .HasMaxLength(50);

                AddressBuilder.Property(a => a.State)
                .HasMaxLength(50);

                AddressBuilder.Property(a => a.ZipCode)
                .HasMaxLength(5)
                .IsRequired();
            });

        builder.ComplexProperty(
            o => o.BillingAddress, AddressBuilder =>
            {
                AddressBuilder.Property(a => a.FirstName)
                .HasMaxLength(50)
                .IsRequired();

                AddressBuilder.Property(a => a.LastName)
                .HasMaxLength(50)
                .IsRequired();

                AddressBuilder.Property(a => a.AddressLine)
                .HasMaxLength(180)
                .IsRequired();

                AddressBuilder.Property(a => a.EmailAddress)
                .HasMaxLength(50);

                AddressBuilder.Property(a => a.Counrty)
                .HasMaxLength(50);

                AddressBuilder.Property(a => a.State)
                .HasMaxLength(50);

                AddressBuilder.Property(a => a.ZipCode)
                .HasMaxLength(5)
                .IsRequired();
            });

        builder.ComplexProperty(
            o => o.Payment, PaymentBuilder =>
            {
                PaymentBuilder.Property(p => p.CardName)
                .HasMaxLength(50);

                PaymentBuilder.Property(p => p.CardNumber)
                .HasMaxLength(24)
                .IsRequired();

                PaymentBuilder.Property(p => p.CVV)
                .HasMaxLength(3);

                PaymentBuilder.Property(p => p.Expiration)
                .HasMaxLength(10);

                PaymentBuilder.Property(p => p.PaymentMethod);

            });

        builder.Property(o => o.OrderStatus)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(
            orderStatus => orderStatus.ToString(),
            dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

        builder.Property(o => o.TotalPrice);
    }
}
