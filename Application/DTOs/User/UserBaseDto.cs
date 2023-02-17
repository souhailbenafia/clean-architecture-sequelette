using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class UserBaseDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Genre { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        
    }
}
