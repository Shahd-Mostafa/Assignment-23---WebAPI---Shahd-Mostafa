using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    internal class ProductConfiguartions : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.HasOne(b=>b.Brand)
                .WithMany()
                .HasForeignKey(b => b.BrandId);
            builder.HasOne(t => t.Type)
                .WithMany()
                .HasForeignKey(t => t.TypeId);
            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
