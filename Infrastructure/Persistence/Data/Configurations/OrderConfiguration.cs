﻿using Domain.Entities.Order_Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(order => order.Address, address => address.WithOwner());
            builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Property(o => o.PaymentStatus).HasConversion(s=>s.ToString(), s=>Enum.Parse<PaymentStatus>(s));
            builder.HasOne(o => o.DeliveryMethod)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);
            builder.Property(o => o.SubTotal)
                .HasColumnType("decimal(18,2)");
            builder.Property(o => o.Total)
                .HasColumnType("decimal(18,2)");
        }
    }
}
