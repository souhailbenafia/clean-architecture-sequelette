using Microsoft.AspNetCore.Identity;
using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities.identity
{
    public class User : IdentityUser<string>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Genre { get; set; } = null;
        public string? ImageUrl  { get; set; }
        public DateTime BirthDate { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; } 

        public virtual IList<Car>? Cars { get; private set; }

        public virtual IList<Offre>? Offres { get; private set; }

        public virtual IList<Rent>? Rents { get; private set; }
        public virtual IList<Suggestion>? Suggestions { get; private set; }

    }
}
