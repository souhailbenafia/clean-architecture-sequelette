using Domain.Entities.identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence.Configurations.Entities.identity
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
              new IdentityRole
              {
                  Id = "Recruiter",
                  Name = "Recruiter",
                  NormalizedName = "RECRUITER"
              },
               new IdentityRole
               {
                   Id = "Recruiter",
                   Name = "Recruiter",
                   NormalizedName = "RECRUITER"
               },
                new IdentityRole
                {
                    Id = "Recruiter",
                    Name = "Recruiter",
                    NormalizedName = "RECRUITER"
                }



                );
        }
    }
}
