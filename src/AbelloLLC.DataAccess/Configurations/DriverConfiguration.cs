using AbelloLLC.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.DataAccess.Configurations
{
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.Property(d=>d.Name).HasMaxLength(128).IsRequired(true);
            builder.Property(d => d.Owner).HasMaxLength(128).IsRequired(true);
            builder.Property(d => d.Phone).HasMaxLength(128).IsRequired(true);
            builder.Property(d => d.Dimensions).HasMaxLength(128).IsRequired(true);
            builder.Property(d=>d.Capacity).IsRequired(true);
            builder.Property(d=>d.isReserved).HasDefaultValue(false);
            builder.Property(d=>d.IsActive).HasDefaultValue(true);
            builder.Property(d=>d.ZipCode).HasMaxLength(128);
            builder.HasCheckConstraint("DriverName","Len(Name) >= 3");
            builder.HasCheckConstraint("OwnerDriver", "Len(Owner) >= 3");
            builder.HasCheckConstraint("PhoneDriver", "Len(Phone) >= 3");
            builder.HasCheckConstraint("DimensionDriver", "Len(Dimensions) >= 3");
          

        }
    }
}
