using Domain.Entities;
using Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence.Configurations.Entities
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x=> x.Seller).WithMany(b => b.Cars).HasForeignKey(x => x.SellerId);

            builder.Property(x => x.Carburant).HasConversion(
                v => v.ToString(),
                v => (CarburantType) Enum.Parse(typeof(CarburantType), v)
                );

            builder.Property(x => x.Gear).HasConversion(
               v => v.ToString(),
               v => (GearType)Enum.Parse(typeof(GearType), v)
               );

            builder.Property(x => x.Mark).HasConversion(
               v => v.ToString(),
               v => (CarMark)Enum.Parse(typeof(CarMark), v)
               );
        }
    }
}
