using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence.Configurations.Entities
{
    public class OffreConfiguration : IEntityTypeConfiguration<Offre>
    {
        public void Configure(EntityTypeBuilder<Offre> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Car).WithMany(b => b.Offres).HasForeignKey(x => x.CarId);
        }
    }
}
