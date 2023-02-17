﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.auth
{
    public class RegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string? Genre { get; set; }
        public string? Password { get; set; }
        public string? ConfirmePassword { get; set; }
        public string? role { get; set; }
    }
}
