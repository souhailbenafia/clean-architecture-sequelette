using Domain.Entities.identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence.Configurations.Entities.identity
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasOne(c => c.User).WithOne().HasForeignKey<UserRole>(c => c.UserId);
            builder.HasOne(c => c.Role).WithOne().HasForeignKey<UserRole>(c => c.RoleId);
        }
    }
}
