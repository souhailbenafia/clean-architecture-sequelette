using Domain.Entities.identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Identity
{
    public interface IAuthService
    {
        AuthData GetAuthData(User user, List<string> role);
    }
}
