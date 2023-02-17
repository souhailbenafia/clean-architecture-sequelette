using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.identity
{
    public class Role : IdentityRole<string>
    {
        public virtual IList<UserRole> UserRoles { get; set; }
    }
}
