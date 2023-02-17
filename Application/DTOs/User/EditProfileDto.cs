using Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class EditProfileDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }  
        public DateTime BirthDate { get; set; }
        public string? ImageUrl { get; set; }
        public string? Genre { get; set; }
        public string? Email { get; set; }
    }
}
