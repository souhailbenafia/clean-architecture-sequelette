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
    public class SuggestionConfiguration : IEntityTypeConfiguration<Suggestion>
    {
        public void Configure(EntityTypeBuilder<Suggestion> builder)
        {
            builder.HasKey(x => x.Id);

           builder.HasOne(x => x.Customer).WithMany(b => b.Suggestions).HasForeignKey(x => x.CustomerId);
            builder.HasOne(x => x.Offre).WithMany(b => b.Suggestions).HasForeignKey(x => x.OffreId).OnDelete(DeleteBehavior.Restrict); 

            builder.Property(x => x.staus).HasConversion(
                v => v.ToString(),
                v => (SuggestionStatus)Enum.Parse(typeof(SuggestionStatus), v)
                );

        }
    }
}
