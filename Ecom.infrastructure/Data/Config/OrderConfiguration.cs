using Ecom.Core.Entites.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Data.Config;

public class OrderConfiguration : IEntityTypeConfiguration<Orders>
{
    public void Configure(EntityTypeBuilder<Orders> builder)
    {
        builder.OwnsOne(x => x.ShippingAddress, n => { n.WithOwner(); });
        builder.HasMany(x => x.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        builder.Property(x=>x.Status).HasConversion(o=>o.ToString(),
            o=>(Status)Enum.Parse(typeof(Status),o));
        builder.Property(x => x.SubTotal).HasColumnType("decimal(18,2)");
    }
}
