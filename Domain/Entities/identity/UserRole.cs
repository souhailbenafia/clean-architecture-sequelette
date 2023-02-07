using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.identity
{
    public class UserRole : IdentityUserRole<string>
    {
        public virtual User? User { get; private set; }

        public virtual Role? Role { get; private set; }
    }
}
