using Domain.Entities.Order_Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItems>
    {
        public void Configure(EntityTypeBuilder<OrderItems> builder)
        {
            builder.OwnsOne(order => order.Product, product => product.WithOwner());
            builder.Property(oi => oi.Price)
            .HasColumnType("decimal(18,2)");
        }
    }
}
